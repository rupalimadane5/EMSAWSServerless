using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using EventManagementAWSServerless.Models;
using EventManagementAWSServerless.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventManagementAWSServerless
{
    public class PostEventsLambda
    {
        EMSRepository _repository;
        public PostEventsLambda()
        {
            _repository = new EMSRepository();
        }
        /// <summary>
        /// A Lambda function to create new event and saved to dynamo db
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The list of blogs</returns>
        public async Task<CreateEventResponse> FunctionHandler(object request, ILambdaContext context)
        {
            try
            {
                Console.WriteLine($"{nameof(PostEventsLambda)} : Started processing creating event");
                Console.WriteLine($"{nameof(PostEventsLambda)} : Body creating event {JsonConvert.SerializeObject(request)}");

                JObject jobj = request as JObject;
                var entity = jobj.ToObject<EventEntity>();
                
                var response = await _repository.CreateEvent(entity);
                Console.WriteLine($"{nameof(PostEventsLambda)} : Created event successfully {response}");

                return new CreateEventResponse() {
                    Status = System.Net.HttpStatusCode.OK,
                   EventName=response ?.EventName
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured. {ex.Message}");
                throw ex;
            }

        }
    }
}
