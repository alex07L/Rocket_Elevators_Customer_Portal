using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Rocket_Elevators_Customer_Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task<dynamic> queryAsync(String query, Object obj)
        {
            var client = new GraphQLHttpClient("https://relevator.herokuapp.com/graphql", new NewtonsoftJsonSerializer());

            var request = new GraphQLRequest
            {
                Query = query,
                Variables = obj

            };

            var response = await client.SendQueryAsync<dynamic>(request);
            return response.Data;
        }
    }
}
