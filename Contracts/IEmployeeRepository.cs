using SampleProjectAPI.Model.employee_model;

namespace SampleProjectAPI.Contracts
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<EmployeeModel>> GetEmployees();

        public Task<IEnumerable<EmployeeModel>> GetEmployeesById(MinimalModel ids);
        public Task<IEnumerable<EmployeeModel>> UpdateEmployeePosition(UpdateEmployeePositionRequest updateEmployeePositionRequest);
    }
}
