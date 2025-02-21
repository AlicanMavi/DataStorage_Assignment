using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
public enum ProjectStatus
{
    EjPaborjat,
    PaaGorande,
    Avslutat
}

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    public string ProjectNumber { get; private set; }

    [Required]
    public string Name { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    [Required]
    public string ProjectManager { get; set; } = null!;

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    [Required]
    public string ServiceDescription { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public ProjectStatus Status { get; set; }

    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
        public string Title { get; set; }

        public ProjectEntity()
    {
        ProjectNumber = GenerateProjectNumber();
    }

    private string GenerateProjectNumber()
    {
        Random rnd = new Random();
        int num = rnd.Next(100, 1000);
        return $"P-{num}";
    }
}
}