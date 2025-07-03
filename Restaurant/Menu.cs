using System;
using System.Collections.Generic;

public class Menu
{
    private readonly List<MenuItem> _items = new();

    //메뉴 디스플레이용 읽기 전용
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    public void AddItem(MenuItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(string name) //클래스를 매개변수로 하려면 hashcode 이용하는 게 좋다?
    {
        _items.RemoveAll(item => item.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }

    public MenuItem? GetItemByIndex(int index)
    {
        if (index < 0 || index >= _items.Count)
            return null;
        return _items[index];
    }
}