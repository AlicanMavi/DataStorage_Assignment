namespace Business.Models;

public class ProjectModel
{
    public int Id { get; set; }

    public string ProjectNumber { get; set; } = null!;

    public string Name { get; set; } = null!; 

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string ProjectManager { get; set; } = null!;

    public int CustomerId { get; set; }

    public string ServiceDescription { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;
}
