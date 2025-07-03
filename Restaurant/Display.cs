using System;
using System.Collections.Generic;
using System.Linq;

public class Display()
{
    public void DisplayMenu(IReadOnlyList<MenuItem> items)
    {
        Console.WriteLine("메뉴판 :");
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
    }
    public void DisplayCart(IReadOnlyList<OrderItem> items, Cart cart)
    {
        Console.WriteLine("장바구니:");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine($"총 금액: {cart.GetTotalAmount():C}");
    }
}