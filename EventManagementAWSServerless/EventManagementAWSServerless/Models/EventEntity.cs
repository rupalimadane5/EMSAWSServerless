using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementAWSServerless.Models
{
    public class EventEntity
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }
        public string EventTime { get; set; }
    }
}
