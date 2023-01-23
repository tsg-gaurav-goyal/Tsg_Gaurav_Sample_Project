using SampleProjectAPI.Model;
using SampleProjectAPI.Model.case_models;

namespace SampleProjectAPI.Contracts
{
    public interface ICaseRepository
    {
        public Task<IEnumerable<CaseAllocationModel>> GetCaseHistory(CaseHistoryRequest caseHistoryRequest);
        public Task UpdateEmployeeCaseHistory(UpdateCaseHistoryRequest updateHistoryRequest);
        public Task<IEnumerable<CaseAllocationModel>> GetActiveEmployee(String oldCaseCode);
        public Task<EmployeeUtilizationModel> GetEmployeeUtilizationStatus(CaseHistoryRequest caseHistoryRequest);

    }
}
 