using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using EventManagementAWSServerless.Models;
using EventManagementAWSServerless.Repository;

namespace EventManagementAWSServerless
{
    public class GetEventsLambda
    {
        EMSRepository _repository;
        public GetEventsLambda()
        {
            _repository = new EMSRepository();
        }

        /// <summary>
        /// A Lambda function to get event details
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The list of blogs</returns>
        public async Task<GetEventsResponse> FunctionHandler(object input, ILambdaContext context)
        {
            try
            {
                Console.WriteLine($"{nameof(GetEventsLambda)} : Started processing fetching event details");

                var response = await _repository.GetEvents();

                Console.WriteLine($"{nameof(GetEventsLambda)} : Fetching event details successfully ");
                return new GetEventsResponse()
                {
                    Status = System.Net.HttpStatusCode.OK,
                    Entities = response
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

