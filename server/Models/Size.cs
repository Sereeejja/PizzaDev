using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace PizzaDev.Models;

[Table("Size")]
public class Size
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    public List<PizzaSize> PizzaSizes { get; set; } = new List<PizzaSize>();
}