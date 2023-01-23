namespace SampleProjectAPI.Model
{
    public class EmployeeUtilizationSqlModel
    {
        public string? employee_name { get; set; }
        public float? utilization_percent { get; set; }
    }

    public class EmployeeUtilizationModel
    {
        public string? employee_name { get; set; }
        public string? employee_code { get; set; }
        public float? percent_utilization { get; set; }
        public String? utilization_status { get; set; } 
        public EmployeeUtilizationModel(string? employee_name, string? employee_code, float? percent_utilization, String? utilization_status) { 
            this.utilization_status = utilization_status;
            this.percent_utilization = percent_utilization;
            this.employee_code= employee_code;
            this.employee_name= employee_name;
        }
    }
}
