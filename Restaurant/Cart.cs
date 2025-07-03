using System;
using System.Collections.Generic;
using System.Linq;

public class Cart
{
    private readonly List<OrderItem> _items = new(); //장바구니 리스트
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public void AddItem(MenuItem item, int quantity)
    {
        //장바구니에 같은 거 있나 확인용
        var existing = _items.FirstOrDefault(i => i.Item.Name == item.Name);
        if (existing != null)
        {
            existing.ChangeQuantity(existing.Quantity + quantity); //있으면 수량만 늘리기
        }
        else
        {
            _items.Add(new OrderItem(item, quantity)); //없으면 새로 추가
        }
    }

    //같은 이름 다른 메뉴인 경우?
    public void RemoveItem(string name)
    {
        _items.RemoveAll(i => i.Item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void ChangeQuantity(string name, int newQuantity)
    {
        var item = _items.FirstOrDefault(i => i.Item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            item.ChangeQuantity(newQuantity);
        }
    }


    public decimal GetTotalAmount()
    {
        return _items.Sum(i => i.TotalPrice);
    }

    public bool IsEmpty => !_items.Any();

    public List<OrderItem> GetItems()
    {
        return new List<OrderItem>(_items);
    }

    public void Clear()
    {
        _items.Clear();
    }
}
