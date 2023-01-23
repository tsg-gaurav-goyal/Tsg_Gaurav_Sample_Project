using Dapper;
using SampleProjectAPI.context;
using SampleProjectAPI.Model.practice_models;
using System.Data;

namespace SampleProjectAPI.Contracts
{
    public class PracticeRepository : IPracticeRepository
    {
        public readonly DapperContext _context;
        public PracticeRepository(DapperContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<PracticeLeadersModel>> IPracticeRepository.GetPracticeLeaders(int officeCode)
        {
            var procedureName = "sp_gaurav_getPracticeLeaders";
            var parameters = new DynamicParameters();
            parameters.Add("OfficeCode", officeCode, DbType.Int64, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var practiceLeaders = await connection.QueryAsync<PracticeLeadersModel>(
                    procedureName, parameters, commandType: CommandType.StoredProcedure);
                return practiceLeaders;
            }
        }
    }
}
