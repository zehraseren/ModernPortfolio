using modernportfolio.Models;

namespace modernportfolio.Services;

public interface ISkillService
{
    Task<IEnumerable<Skill>> GetAllSkillsAsync();
    Task<Skill?> GetSkillByIdAsync(int id);
    Task<int> CreateSkillAsync(Skill skill);
    Task<bool> UpdateSkillAsync(Skill skill);
    Task<bool> DeleteSkillAsync(int id);
}
