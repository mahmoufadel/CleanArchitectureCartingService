using GraphQL.Mutation;
using GraphQL.Queries;
using GraphQL.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddGraphQLServices(builder.Configuration);


builder.Services.AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.UseAuthentication();

app.MapGraphQL();

app.Run();
