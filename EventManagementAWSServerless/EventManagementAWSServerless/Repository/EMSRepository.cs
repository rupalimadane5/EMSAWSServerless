using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using EventManagementAWSServerless.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementAWSServerless.Repository
{
    public class EMSRepository
    {

        AmazonDynamoDBConfig clientConfig;
        AmazonDynamoDBClient client;

        public EMSRepository()
        {
            clientConfig = new AmazonDynamoDBConfig();
            clientConfig.RegionEndpoint = RegionEndpoint.USWest2;
            client = new AmazonDynamoDBClient(
                awsAccessKeyId: "***************",
                awsSecretAccessKey: "***********************",
                clientConfig: clientConfig);
        }

        public async Task<EventEntity> CreateEvent(EventEntity entity)
        {
            try
            {
                Console.WriteLine($"{nameof(CreateEvent)} : Started creating event " +
                    $"with entity {JsonConvert.SerializeObject(entity)}");

                var request = new PutItemRequest()
                {
                    TableName = "Events",
                    Item = new Dictionary<string, AttributeValue>()
                {
                         {
                        "EventId",
                        new AttributeValue
                         {
                            S=Guid.NewGuid().ToString()
                         }
                    },
                    {
                        "EventName",
                        new AttributeValue
                        {
                            S=entity.EventName
                        }
                    },
                    {
                        "EventStartDate" ,
                        new AttributeValue()
                        {
                            S=entity.EventStartDate
                        }
                    },
                     {
                        "EventTime" ,
                        new AttributeValue()
                        {
                            S=entity.EventTime
                        }
                    },
                     {
                        "EventEndDate" ,
                        new AttributeValue()
                        {
                            S=entity.EventEndDate
                        }
                    }
                }
                };

                var response = await client.PutItemAsync(request);
                if (response == null)
                {
                    return null;
                }
                Console.WriteLine($"{nameof(CreateEvent)} : Completed creating event");
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while saving data : ", ex.Message);
                throw ex;
            }
        }

        public async Task<List<EventEntity>> GetEvents()
        {
            try
            {
                List<EventEntity> eventEntities = new List<EventEntity>();

                Console.WriteLine($"{nameof(GetEvents)} : Started fetching event details ");

                var response = await client.ScanAsync(new ScanRequest() { TableName = "Events" });

                Console.WriteLine($"Response: count : {response.Count}");
                Console.WriteLine($"Response: items : {response.Items}");

                foreach (var item in response.Items)
                {
                    eventEntities.Add(new EventEntity()
                    {
                        EventId = item["EventId"].S,
                        EventName = item["EventName"].S,
                        EventStartDate = item["EventStartDate"].S,
                        EventTime = item["EventTime"].S,
                        EventEndDate = item["EventEndDate"].S
                    });

                    Console.WriteLine($"Events are : {item["EventName"].S}");
                }

                Console.WriteLine($"{nameof(GetEvents)} : Completed fetching event details");
                return eventEntities;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while fetching data : ", ex.Message);
                throw ex;
            }
        }

        public async Task<UserEntity> GetUser(string userName, string password)
        {
            try
            {
                List<UserEntity> userEntities = new List<UserEntity>();

                Console.WriteLine($"{nameof(GetUser)} : Started fetching user details ");

                var response = await client.ScanAsync(new ScanRequest() { TableName = "Users" });
                
                foreach (var item in response.Items)
                {
                    userEntities.Add(new UserEntity()
                    {
                        UserId = item["UserId"].S,
                        UserName = item["UserName"].S,
                        Password = item["Password"].S
                    });
                }

                Console.WriteLine($"{nameof(GetUser)} : Completed fetching user details");

                return userEntities.Any() ? userEntities.SingleOrDefault(x => x.UserName == userName && x.Password == password)
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while fetching data : ", ex.Message);
                throw ex;
            }
        }
    }
}
