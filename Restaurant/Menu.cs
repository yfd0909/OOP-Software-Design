using System;
using System.Collections.Generic;

public class Menu
{
    private readonly List<MenuItem> _items = new();

    public void AddItem(MenuItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(string name) //클래스를 매개변수로 하려면 hashcode 이용하는 게 좋다?
    {
        _items.RemoveAll(item => item.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }

    public void DisplayMenu()
    {
        Console.WriteLine("메뉴판 :");
        for (int i = 0; i < _items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_items[i]}");
        }
    }
    public MenuItem? GetItemByIndex(int index)
    {
        if (index < 0 || index >= _items.Count) return null;
        return _items[index];
    }
}