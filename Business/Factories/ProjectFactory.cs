using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class ProjectFactory
    {
        public static ProjectEntity Create(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ArgumentException("Project name cannot be null or empty.", nameof(model.Name));

            var entity = new ProjectEntity
            {
                Name = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ProjectManager = model.ProjectManager,
                CustomerId = model.CustomerId,
                ServiceDescription = model.ServiceDescription,
                TotalPrice = model.TotalPrice
            };

            if (!string.IsNullOrWhiteSpace(model.Status) &&
                Enum.TryParse<ProjectStatus>(model.Status.Trim(), true, out var parsedStatus))
            {
                entity.Status = parsedStatus;
            }
            else
            {
                entity.Status = ProjectStatus.EjPaborjat;
            }

            return entity;
        }


        public static ProjectModel Create(ProjectEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new ProjectModel
            {
                Id = entity.Id,
                ProjectNumber = entity.ProjectNumber,
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ProjectManager = entity.ProjectManager,
                CustomerId = entity.CustomerId,
                ServiceDescription = entity.ServiceDescription,
                TotalPrice = entity.TotalPrice,
                Status = entity.Status.ToString()
            };
        }
    }
}


