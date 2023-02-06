using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Api.Models.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

	public List<Order> Orders { get; set; }
}

