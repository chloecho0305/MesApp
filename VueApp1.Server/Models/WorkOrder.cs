using Microsoft.JSInterop;
using System.Data;

namespace VueApp1.Server.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string ProductNumber { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; } // "NotStarted", "InProgress", "Completed"
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public List<ProcessRecord> ProcessRecords { get; set; }
    }
}
