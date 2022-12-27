using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using Image = CleanArchitecture.Domain.ValueObjects.Image;

namespace CleanArchitecture.Domain.Entities;


public class Product 
{
    [Key]
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
 

    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public Money Price { get; set; }
    public int Amount { get; set; }
}

