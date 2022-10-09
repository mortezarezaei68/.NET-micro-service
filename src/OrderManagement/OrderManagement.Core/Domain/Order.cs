using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Core.Domain;

public class Order
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}