using modernportfolio.Models;
using modernportfolio.Repositories;

namespace modernportfolio.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _repository;

    public SkillService(ISkillRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> CreateSkillAsync(Skill skill)
    {
        if (skill is null) throw new ArgumentException("Skill cannot be null!", nameof(skill));
        ValidateSkill(skill);
        skill.CreatedAt = DateTime.UtcNow;
        if (skill.DisplayOrder == 0)
        {
            var allSkills = await _repository.GetAllAsync();
            skill.DisplayOrder = allSkills.Any() ? allSkills.Max(s => s.DisplayOrder) + 1 : 1;
        }
        var result = await _repository.CreateAsync(skill);
        return result;
    }

    public async Task<bool> DeleteSkillAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("Project ID must be greater than zero!", nameof(id));
        var project = await _repository.GetByIdAsync(id);
        if (project is null) return false;
        var result = await _repository.DeleteAsync(id);
        return result;
    }

    public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
    {
        var skills = await _repository.GetAllAsync();
        var result = skills.OrderBy(s => s.DisplayOrder).ThenByDescending(s => s.CreatedAt);
        return result;
    }

    public async Task<Skill?> GetSkillByIdAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("Skill ID must be greater than zero.", nameof(id));
        var skill = await _repository.GetByIdAsync(id);
        return skill;
    }

    public async Task<bool> UpdateSkillAsync(Skill skill)
    {
        if (skill is null) throw new ArgumentException("Skill cannot be null!", nameof(skill));
        if (skill.Id <= 0) throw new ArgumentException("Skill ID must be greater than zero.", nameof(skill.Id));
        var existingSkill = await _repository.GetByIdAsync(skill.Id);
        if (existingSkill is null) throw new ArgumentException($"Skill with ID {skill.Id} not found!", nameof(existingSkill));
        ValidateSkill(skill);
        skill.CreatedAt = existingSkill.CreatedAt;
        var result = await _repository.UpdateAsync(skill);
        return result;
    }

    private void ValidateSkill(Skill skill)
    {
        // Name
        if (string.IsNullOrWhiteSpace(skill.Name)) throw new ArgumentException("Skill name cannot be empty or whitespace!", nameof(skill));

        // Name Length
        if (skill.Name.Length > 100) throw new ArgumentException("Skill name cannot exceed 100 characters!", nameof(skill));

        // Percentage
        if (skill.Percentage < 0 || skill.Percentage > 100) throw new ArgumentException("Skill percentage must be between 0 and 100!", nameof(skill));

        // Display Order
        if (skill.DisplayOrder < 0) throw new ArgumentException("Skill display order cannot be negative!", nameof(skill));
    }
}
