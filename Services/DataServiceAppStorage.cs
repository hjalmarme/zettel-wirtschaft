using ZettelWirtschaft.Client.Models;

namespace ZettelWirtschaft.Client.Services;

public class DataServiceAppStorage : IDataService
{
    protected List<MenuItem> _items = new();

    public DataServiceAppStorage()
    {
    }

    public void AddMenuItem(MenuItem menuItem)
    {
        _items.Add(menuItem);
    }

    public MenuItem GetMenuItem(string id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public IEnumerable<MenuItem> GetMenuItems()
    {
        return _items.AsEnumerable();
    }

    public void RemoveMenuItem(MenuItem menuItem)
    {
        _items.Remove(menuItem);
    }

    public void UpdateMenuItem(MenuItem menuItem)
    {
        var item = _items.FirstOrDefault(item => item.Id == menuItem.Id);

        if (item is null) return;

        item.Type = menuItem.Type;
        item.Name = menuItem.Name;
        item.Description = menuItem.Description;
        item.Price = menuItem.Price;
    }
}
