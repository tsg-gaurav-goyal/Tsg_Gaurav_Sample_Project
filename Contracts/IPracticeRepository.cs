using SampleProjectAPI.Model.case_models;
using SampleProjectAPI.Model.practice_models;

namespace SampleProjectAPI.Contracts
{
    public interface IPracticeRepository
    {
        public Task<IEnumerable<PracticeLeadersModel>> GetPracticeLeaders(int officeCode);
    }
}
