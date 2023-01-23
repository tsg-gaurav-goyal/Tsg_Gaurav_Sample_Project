using Dapper;
using SampleProjectAPI.context;
using SampleProjectAPI.Model;
using SampleProjectAPI.Model.case_models;
using System.Data;

namespace SampleProjectAPI.Contracts
{
    public class CaseRepository : ICaseRepository
    {
        public readonly DapperContext _context;
        public CaseRepository(DapperContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<CaseAllocationModel>> ICaseRepository.GetActiveEmployee(string oldCaseCode)
        {
            var procedureName = "sp_gaurav_getActiveEmployees";
            var parameters = new DynamicParameters();
            parameters.Add("oldCaseCode", oldCaseCode, DbType.String, ParameterDirection.Input);
           
            using (var connection = _context.CreateConnection())
            {
                var activeEmployees = await connection.QueryAsync<CaseAllocationModel>(
                   procedureName,parameters,commandType:CommandType.StoredProcedure
                    );
                return activeEmployees;
            }
        }

        async Task<IEnumerable<CaseAllocationModel>> ICaseRepository.GetCaseHistory(CaseHistoryRequest caseHistoryRequest)
        {
            var procedureName = "sp_gaurav_getCaseHistory";
            var parameters = new DynamicParameters();
            parameters.Add("employeeCode", caseHistoryRequest.EmployeeCode, DbType.String, ParameterDirection.Input);
            parameters.Add("startDate", caseHistoryRequest.StartDate, DbType.String, ParameterDirection.Input);
            parameters.Add("endDate", caseHistoryRequest.EndDate, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var histories = await connection.QueryAsync<CaseAllocationModel>(
                    procedureName, parameters, commandType: CommandType.StoredProcedure
                    );
                return histories;
            }
        }

        async Task<EmployeeUtilizationModel> ICaseRepository.GetEmployeeUtilizationStatus(CaseHistoryRequest caseHistoryRequest)
        {

            var procedureName = "sp_gaurav_getEmployeeUtilization";
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeCode", caseHistoryRequest.EmployeeCode, DbType.String, ParameterDirection.Input);
            parameters.Add("StartDate", caseHistoryRequest.StartDate, DbType.String, ParameterDirection.Input);
            parameters.Add("EndDate", caseHistoryRequest.EndDate, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var utilise = await connection.QueryAsync<EmployeeUtilizationSqlModel>(
                    procedureName, parameters, commandType: CommandType.StoredProcedure
                    );
                if (utilise != null)
                {
                    var employeeModel = utilise.ToList<EmployeeUtilizationSqlModel>();
                    var employeeUtilized = employeeModel[0].utilization_percent;
                    var status = employeeUtilized > 1 ? "Over Utilization"
                        : employeeUtilized == 1
                        ? "Full Utilization" : "Under Utilization";
                    Console.WriteLine(employeeUtilized + "");
                    EmployeeUtilizationModel employeeUtilizationModel = new EmployeeUtilizationModel(employeeModel[0].employee_name, caseHistoryRequest.EmployeeCode, employeeUtilized * 100, status);
                    Console.WriteLine(employeeUtilizationModel.employee_name);
                    return employeeUtilizationModel;
                }
                return new EmployeeUtilizationModel(null, null, null, null);
            }
        }
   
        async Task ICaseRepository.UpdateEmployeeCaseHistory(UpdateCaseHistoryRequest updateHistoryRequest)
        {
            var procedureName = "sp_gaurav_updateCaseHistory";
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeCode", updateHistoryRequest.EmployeeCode, DbType.String, ParameterDirection.Input);
            parameters.Add("StartDate", updateHistoryRequest.StartDate, DbType.String, ParameterDirection.Input);
            parameters.Add("EndDate", updateHistoryRequest.EndDate, DbType.String, ParameterDirection.Input);
            parameters.Add("OldCaseCode", updateHistoryRequest.OldCaseCode, DbType.String, ParameterDirection.Input);
            parameters.Add("AllocationPercent", updateHistoryRequest.AllocationPercent, DbType.Double, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var updateStatus = await connection.QueryAsync<String>(
                    procedureName, parameters, commandType: CommandType.StoredProcedure
                    );
            }
        }
    }
}
