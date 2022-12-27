using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using APIGateWay;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using AutoFixture.Kernel;
using Moq;
using Newtonsoft.Json.Linq;

namespace ApiGateway.integration.Tests;
public class WebAppAutoMockDataAttribute : AutoDataAttribute
{
    private readonly IMockProvider mockProvider;

    public WebAppAutoMockDataAttribute()
    {
        mockProvider = new DefaultMockProvider(Fixture);
    }

    public WebAppAutoMockDataAttribute(Type mockProviderType)
    {
        if (typeof(IMockProvider).IsAssignableFrom(mockProviderType) is false)
        {
            throw new ArgumentOutOfRangeException(nameof(mockProviderType), $"Only {nameof(IMockProvider)} implementations supported.");
        }

        mockProvider = (Activator.CreateInstance(mockProviderType, Fixture) as IMockProvider)!;
        if (mockProvider == null)
        {
            throw new ArgumentOutOfRangeException(nameof(mockProviderType), $"Unable to create instance of {mockProviderType.FullName}.");
        }
    }
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));

        void AddMocks(IServiceCollection services) => mockProvider.AddMocks(services, testMethod.GetParameters());
        var configurationProviderMock = mockProvider.GetConfigurationProviderMock();
        var webApplicationFactory = new MockingWebApplicationFactory<Program>(AddMocks, () => configurationProviderMock);
        var webApplicationFactoryCustomization = new WebApplicationFactoryCustomization<Program>(webApplicationFactory);
        Fixture.Customize(webApplicationFactoryCustomization);

        return base.GetData(testMethod);
    }
}

public class WebApplicationFactoryCustomization<TStartup> : ICustomization where TStartup : class
{
    private readonly WebApplicationFactory<TStartup> webApplicationFactory;

    public WebApplicationFactoryCustomization(WebApplicationFactory<TStartup> webApplicationFactory)
    {
        this.webApplicationFactory = webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));
    }

    public void Customize(IFixture fixture)
    {
        if (fixture == null) throw new ArgumentNullException(nameof(fixture));

        fixture.Inject(webApplicationFactory);
        fixture.Customize<HttpClient>(composer => composer.FromFactory(() => webApplicationFactory.CreateClient()).OmitAutoProperties());
        fixture.Customize<Func<HttpClient>>(composer => composer.FromFactory(() => () => webApplicationFactory.CreateClient()));
        fixture.Customize<Func<WebApplicationFactoryClientOptions, HttpClient>>(composer => composer.FromFactory(() => options => webApplicationFactory.CreateClient(options)));
    }
}

public class MockingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly Action<IServiceCollection> addMocks;
    private readonly Func<IConfigurationProvider> getConfigurationProviderMock;

    public MockingWebApplicationFactory(Action<IServiceCollection> addMocks, Func<IConfigurationProvider> getConfigurationProviderMock)
    {
        this.addMocks = addMocks ?? throw new ArgumentNullException(nameof(addMocks));
        this.getConfigurationProviderMock = getConfigurationProviderMock ?? throw new ArgumentNullException(nameof(getConfigurationProviderMock));
    }

    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return WebHost.CreateDefaultBuilder();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder
            .UseEnvironment(Environments.Development)
            .ConfigureAppConfiguration(x => { x.Sources.Add(new DummyConfigurationSource(getConfigurationProviderMock())); })
            .UseStartup<TStartup>()
            .ConfigureTestServices(addMocks);
    }
}
public class DummyConfigurationSource : IConfigurationSource
{
    private readonly IConfigurationProvider configurationProvider;

    public DummyConfigurationSource(IConfigurationProvider configurationProvider)
    {
        this.configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return configurationProvider;
    }
}

public interface IMockProvider
{
    IConfigurationProvider GetConfigurationProviderMock();

    void AddMocks(IServiceCollection services, IEnumerable<ParameterInfo> testMethodParameters);
}
public abstract class MoqMockProvider : IMockProvider
{
    protected readonly IFixture Fixture;

    protected MoqMockProvider(IFixture fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true, GenerateDelegates = true });
    }

    public abstract IConfigurationProvider GetConfigurationProviderMock();

    /// <summary>
    /// Registers test service for each test method parameter of <see cref="Mock{T}"/> type.
    /// </summary>
    /// <returns></returns>
    public void AddMocks(IServiceCollection services, IEnumerable<ParameterInfo> testMethodParameters)
    {
        AddDefaultMocks(services);
        foreach (ParameterInfo parameterInfo in testMethodParameters)
        {
            if (TryGetMockType(parameterInfo, out Type? mockType))
            {
                services.AddSingleton(mockType!, _ => new SpecimenContext(Fixture).Resolve(mockType));
            }
        }
    }

    /// <summary>
    /// Registers mocks common for all tests.
    /// </summary>
    /// <returns>Empty list by default. Override to register collection of mocks according to your needs.</returns>
    protected virtual void AddDefaultMocks(IServiceCollection services)
    {
    }

    private static bool TryGetMockType(ParameterInfo parameterInfo, out Type? mockType)
    {
        if (parameterInfo.ParameterType.IsGenericType
            && parameterInfo.ParameterType.GetGenericTypeDefinition().IsAssignableTo(typeof(Mock<>)))
        {
            mockType = parameterInfo.ParameterType.GenericTypeArguments[0];
            return true;
        }

        mockType = default;
        return false;
    }
}


/// <summary>
/// Mocks provider for integration testing of PasswordReset app.
/// </summary>
public class DefaultMockProvider : MoqMockProvider
{
    private static readonly string TokenUrl = new Fixture().Create<Uri>().AbsoluteUri;

    public DefaultMockProvider(IFixture fixture) : base(fixture) { }

    protected override void AddDefaultMocks(IServiceCollection services)
    {
        services.AddSingleton(GetRestUtilsMock());
        services.AddSingleton(GetPingIdClientMock());
    }

    public override IConfigurationProvider GetConfigurationProviderMock()
    {
        var mock = Fixture.Freeze<Mock<IConfigurationProvider>>();
        mock.Setup(x => x.GetChildKeys(It.IsAny<IEnumerable<string>>(), It.IsAny<string>())).Returns<IEnumerable<string>, string>((earlierKeys, _) => earlierKeys);
        string dummyValue = string.Empty;
        mock.Setup(x => x.TryGet(It.IsAny<string>(), out dummyValue)).Returns(false);

        string authTokenUrl = TokenUrl;
        mock.Setup(x => x.TryGet(LambdaConfigurationNames.ScimApiGetAuthTokenUrl, out authTokenUrl)).Returns(true);

        string base64String = Convert.ToBase64String(Fixture.Create<byte[]>());
        mock.Setup(x => x.TryGet("use_base64_key", out base64String)).Returns(true);

        string pingIdUrl = Fixture.Create<Uri>().AbsoluteUri;
        mock.Setup(x => x.TryGet(LambdaConfigurationNames.PingIdUrl, out pingIdUrl)).Returns(true);

        return mock.Object;
    }

    
}