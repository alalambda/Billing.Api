using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Billing.Api.Models.Enums;

namespace Billing.Api.Models.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderNumber { get; init; }
    public decimal PayableAmount { get; init; }
    public PaymentGateway PaymentGateway { get; init; }
    public string? Description { get; init; }
    public DateTime CreateDate { get; init; }

    public DateTime UpdateDate { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public int UserId { get; init; }
    public User User { get; set; }
}
