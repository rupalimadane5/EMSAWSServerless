using System.Net;

namespace EventManagementAWSServerless.Models
{
    public class GetUserResponse
    {
        public HttpStatusCode Status { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
