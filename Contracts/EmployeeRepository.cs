using Dapper;
using SampleProjectAPI.context;
using SampleProjectAPI.Model.case_models;
using SampleProjectAPI.Model.employee_model;
using System.Data;

namespace SampleProjectAPI.Contracts
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly DapperContext _context;
        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeRepository.UpdateEmployeePosition(UpdateEmployeePositionRequest updateEmployeePositionRequest)
        {
            var procedureName = "sp_gaurav_updateEmployeePosition";
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeCode", updateEmployeePositionRequest.employeCode, DbType.String, ParameterDirection.Input);
            parameters.Add("StartDate", updateEmployeePositionRequest.startDate, DbType.String, ParameterDirection.Input);
            parameters.Add("PositionNumber", updateEmployeePositionRequest.positionHistory, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var updatePositions = await connection.QueryAsync<EmployeeModel>(
                   procedureName, parameters, commandType: CommandType.StoredProcedure
                    );
                return updatePositions;
            }
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeRepository.GetEmployees()
        {
            var query = "SELECT top(20) * FROM Employee";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<EmployeeModel>(query);
                return employees;
            }
        }
      
        async Task<IEnumerable<EmployeeModel>> IEmployeeRepository.GetEmployeesById(MinimalModel ids)
        {
            var list = ids.ids;
            var finalIds = "";
            try
            {
                if (list != null) {
                    Console.WriteLine("debugging the length"+list.Count);
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        finalIds += "'" + list[i] + "',";
                    }
                    finalIds += "'" + list[list.Count - 1] + "'";
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(finalIds);
            }
            var procedureName = "sp_gaurav_employeeById";
            var parameters = new DynamicParameters();
            parameters.Add("totalIds", finalIds, DbType.String, ParameterDirection.Input);
            var query = $"Select  ep.employee_code, MIN(e.first_name) AS first_name, MIN(e.last_name) AS last_name,MIN(e.internet_address) AS email_id,MIN(pm.title) " +
                $"AS position_name,MIN(ep.start_date) as start_date1 ,\r\nSUM(DATEDIFF(day, (ep.start_date), (ISNULL(ep.end_date, GETDATE()))))  " +
                $"AS days,\r\no.office_name as office_name1\r\nfrom employee_position_history AS ep\r\n  " +
                $"INNER JOIN\r\noffice as o\r\nON ep.office_code=o.office_code  \r\n" +
                $"INNER JOIN\r\nemployee as e\r\nON ep.employee_code=e.employee_code\r\n" +
                $"INNER JOIN\r\n  position_master as pm\r\n  ON ep.position_no=pm.position_no " +
                $"where ep.employee_code in ({finalIds})\r\n group by ep.position_no,ep.employee_code,o.office_name\r\nOrder by ep.employee_code,start_date1";
            Console.WriteLine(query);
                using (var connection = _context.CreateConnection())
                {
                    var employees = await connection.QueryAsync<EmployeeModel>(query);
                    return employees;
                }
        }
    }
}
