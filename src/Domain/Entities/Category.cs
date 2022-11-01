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
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public string? CreatedBy { get; set; }
    public Category Parent { get; set; }
    public List<Product> Products { get; set; }
}
public class Product 
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public Money Price { get; set; }
    public int Amount { get; set; }
}
/*
 Functional Requirements:

Category has:
    Name – required, plain text, max length = 50.
    Image – optional, URL.
    Parent Category – optional
The following operations are allowed for Category: get/list/add/update/delete.
Item has:
    Name – required, plain text, max length = 50.
    Description – optional, can contain html.
    Image – optional, URL.
    Category – required, one item can belong to only one category.
    Price – required, money.
    Amount – required, positive int.
 */
