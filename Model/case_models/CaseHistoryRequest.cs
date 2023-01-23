namespace SampleProjectAPI.Model.case_models
{
    public class CaseHistoryRequest
    {
        public string? EmployeeCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
    }
}
