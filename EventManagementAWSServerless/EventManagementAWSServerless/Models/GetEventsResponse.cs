using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EventManagementAWSServerless.Models
{
  public  class GetEventsResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<EventEntity> Entities { get; set; }
    }
}
