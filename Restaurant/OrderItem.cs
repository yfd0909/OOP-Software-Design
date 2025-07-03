public class OrderItem
{
    public MenuItem Item { get; }
    public int Quantity { get; private set; }

    public OrderItem(MenuItem item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public void ChangeQuantity(int newQuantity)
    {
        if (newQuantity > 0)
        {
            Quantity = newQuantity;
        }
    }
    //예외처리 추가

    public decimal TotalPrice => Item.Price * Quantity;

    public override string ToString()
    {
        return $"{Item.Name} x {Quantity} = {TotalPrice:C}";
    }
}
