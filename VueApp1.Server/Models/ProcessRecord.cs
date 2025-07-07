namespace VueApp1.Server.Models
{
    public class ProcessRecord
    {
        public int Id { get; set; }

        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; } // navigation property（可選）

        public string ProcessName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Operator { get; set; }
        public string Equipment { get; set; }
        public string Status { get; set; } // "NotStarted", "InProgress", "Completed"
    }
}
