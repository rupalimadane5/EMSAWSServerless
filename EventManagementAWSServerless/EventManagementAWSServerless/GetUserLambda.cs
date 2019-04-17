using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using EventManagementAWSServerless.Models;
using EventManagementAWSServerless.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventManagementAWSServerless
{
    public class GetUserLambda
    {
        EMSRepository _repository;
        public GetUserLambda()
        {
            _repository = new EMSRepository();
        }

        /// <summary>
        /// A Lambda function to get event details
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The list of blogs</returns>
        public async Task<GetUserResponse> FunctionHandler(object request, ILambdaContext context)
        {
            try
            {
                Console.WriteLine($"{nameof(GetUserLambda)} : Started processing fetching user details");
                Console.WriteLine($"{nameof(GetUserLambda)} : Body c {JsonConvert.SerializeObject(request)}");

                JObject jobj = request as JObject;
                var entity = jobj.ToObject<UserEntity>();

                Console.WriteLine($"Request : ", entity.UserName, " - ", entity.Password);

                var response = await _repository.GetUser(entity.UserName, entity.Password);

                Console.WriteLine($"{nameof(GetUserLambda)} : Fetching user details successfully " +
                    $"with reponse {JsonConvert.SerializeObject(response)} ");

                if (response == null)
                {
                    return new GetUserResponse()
                    {
                        Status = System.Net.HttpStatusCode.NotFound
                    };
                }

                return new GetUserResponse()
                {
                    Status = System.Net.HttpStatusCode.OK,
                    UserId = response.UserId,
                    UserName = response.UserName
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

