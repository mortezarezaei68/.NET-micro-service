using Framework.Domain.Core;

namespace BasketManagement.Core;

public class PreOrder:AggregateRoot<int>
{
    public PreOrder(decimal totalPrice)
    {
        TotalPrice = totalPrice;
    }

    public decimal TotalPrice { get; private set; }
    
}

public class PreOrderItem : Entity<int>
{
    public PreOrderItem(string productName, decimal itemPrice)
    {
        ProductName = productName;
        ItemPrice = itemPrice;
    }

    public string ProductName { get; private set; }
    public decimal ItemPrice { get; private set; }
}