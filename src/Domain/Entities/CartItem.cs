using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class CartItem
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Image Image { get; set; }
    public Money Price { get; set; }
    public int Quantity { get; set; }
}
