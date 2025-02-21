using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class ProjectService
{
    private readonly ProjectRepository _projectRepository;

    public ProjectService(ProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        return entities.Select(ProjectFactory.Create);
    }

    public async Task<ProjectModel?> GetProjectByIdAsync(int id)
    {
        var entity = await _projectRepository.GetByIdAsync(id);
        return entity != null ? ProjectFactory.Create(entity) : null;
    }

    public async Task CreateProjectAsync(ProjectModel model)
    {
        var entity = ProjectFactory.Create(model);
        await _projectRepository.AddAsync(entity);
    }

    public async Task UpdateProjectAsync(ProjectModel model)
    {
        var entity = await _projectRepository.GetByIdAsync(model.Id);
        if (entity != null)
        {
            entity.Title = model.Name;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.ProjectManager = model.ProjectManager;
            entity.ProjectManager = model.ProjectManager;
            entity.CustomerId = model.CustomerId;
            entity.ServiceDescription = model.ServiceDescription;
            entity.TotalPrice = model.TotalPrice;

            if (Enum.TryParse<ProjectStatus>(model.Status, out var status))
            {
                entity.Status = status;
            }
            await _projectRepository.UpdateAsync(entity);
        }
    }
    public async Task<bool> DeleteProjectAsync(int id)
    {
        return await _projectRepository.DeleteAsync(id);
    }
}

