namespace SampleProjectAPI.Model.case_models
{
    public class CaseAllocationModel
    {
        public string? employee_code { get; set; }
        public string? old_case_code { get; set; }
        public DateTime? allocation_start_date { get; set; }
        public DateTime? allocation_end_date { get; set; }
        public int? allocation_percent { get; set; }
        public string? employee_name { get; set; }
        public string? employee_office_name { get; set; }
        public string? title { get; set; }
        public string? case_name { get; set; }
        public string? client_name { get; set; }
        public string? office_name { get; set; }
    }
}
