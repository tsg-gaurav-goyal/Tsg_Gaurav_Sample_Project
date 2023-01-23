namespace SampleProjectAPI.Model.employee_model
{
    public class UpdateEmployeePositionRequest
    {
        public string? employeCode { get; set; } 
        public string? startDate { get; set; }
        public int? positionHistory { get; set; }
    }
}
