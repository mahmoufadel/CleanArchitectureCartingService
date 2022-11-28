using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class UserCommand
{
    public string  UserId { get; set; }
    public string Password { get; set; }
}
