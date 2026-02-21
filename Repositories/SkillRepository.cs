using modernportfolio.Models;

namespace modernportfolio.Repositories;

public class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(IConfiguration configuration) : base(configuration)
    {
    }
}
