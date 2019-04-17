using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EventManagementAWSServerless.Models
{
   public  class CreateEventResponse
    {
        public HttpStatusCode  Status { get; set; }
        public string  EventName { get; set; }
    }
}
