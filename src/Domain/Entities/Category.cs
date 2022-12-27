using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using Image = CleanArchitecture.Domain.ValueObjects.Image;

namespace CleanArchitecture.Domain.Entities;

public class Category
{
    public Category()
    {
        Products = new List<Product>();
    }
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public string? CreatedBy { get; set; }
    public Category Parent { get; set; }
    public ICollection<Product> Products { get; set; }
}