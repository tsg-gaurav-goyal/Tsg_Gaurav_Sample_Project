namespace SampleProjectAPI.Model.case_models
{
    public class UpdateCaseHistoryRequest
    {
        public string? EmployeeCode { get; set; }
        public string? OldCaseCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AllocationPercent { get; set; }
    }
}
