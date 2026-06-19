namespace ORSchedulingManagement.Models;

public class User
{
    public int Id { get; set; }
    public string SurgeonName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string OperationRoom { get; set; } = string.Empty;
    public string SurgeryType { get; set; } = string.Empty;
    public string ScheduledTime { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}