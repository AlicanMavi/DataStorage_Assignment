using Data.Entities;
using Business.Models;


namespace Business.Factories;

public static class ProjectFactory
{
public static ProjectModel Create(ProjectEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new ProjectModel
        {
            Id = entity.Id,
            ProjectNumber = entity.Title,
            Name = entity.Title,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            ProjectManager = entity.ProjectManager,
            CustomerId = entity.CustomerId,
            ServiceDescription = entity.ServiceDescription,
            TotalPrice = entity.Product != null ? ((Data.Entities.ProductEntity)entity.Product).ProductPrice : 0,
            Status = entity.Status.ToString()

        };
    }

    public static ProjectEntity Create(ProjectModel model)
    {
        if(model == null) 
            throw new ArgumentNullException(nameof (model));

        var entity = new ProjectEntity
        {
            Id = model.Id,
            Title = model.Name,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            ProjectManager = model.ProjectManager,
            CustomerId = model.CustomerId,
            ServiceDescription = model.ServiceDescription,
            TotalPrice = model.TotalPrice,
        };

        if (Enum.TryParse<ProjectStatus>(model.Status, out var status))
        {
            entity.Status = status;
        }
        else
        {
            entity.Status = ProjectStatus.EjPaborjat;
        }

        return entity;
    }
}
