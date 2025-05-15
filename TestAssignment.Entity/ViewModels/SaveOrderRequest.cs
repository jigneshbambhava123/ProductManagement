namespace TestAssignment.Entity.ViewModels;

public class SaveOrderRequest
{
    public decimal TotalAmount { get; set; }
    public List<ItemData> Items { get; set; }

    public class ItemData
    {
        public int ItemId { get; set; }
        public short Quantity { get; set; }
        public decimal ItemPrice { get; set; }
    }

}
