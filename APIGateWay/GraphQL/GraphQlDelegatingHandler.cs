using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using GraphQL.Types;
using GraphQL;
using Ocelot.Requester;
using Ocelot.Responses;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace APIGateWay;

public class GraphQlDelegatingHandler : DelegatingHandler
{
    //private readonly ISchema _schema;
    private readonly IDocumentExecuter _executer;
    //private readonly IDocumentWriter _writer;

    public GraphQlDelegatingHandler(IDocumentExecuter executer)
    {
        _executer = executer;
        //_writer = writer;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //try get query from body, could check http method :)
        var query = await request.Content.ReadAsStringAsync();

        //if not body try query string, dont hack like this in real world..
        if (query.Length == 0)
        {
            var decoded = WebUtility.UrlDecode(request.RequestUri.Query);
            query = decoded.Replace("?query=", "");
        }

        var result = await _executer.ExecuteAsync(_ =>
        {
            _.Query = query;
        });


        var responseBody = "";// await _writer.WriteToStringAsync(result);

        //maybe check for errors and headers etc in real world?
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseBody)
        };

        //ocelot will treat this like any other http request...
        return response;
    }
}
