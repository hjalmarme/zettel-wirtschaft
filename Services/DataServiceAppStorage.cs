using ZettelWirtschaft.Models;

namespace ZettelWirtschaft.Services;

public class DataServiceAppStorage : IDataService
{
    protected List<MenuItem> _items = new();

    public DataServiceAppStorage()
    {
    }

    public Task AddMenuItem(MenuItem menuItem)
    {
        _items.Add(menuItem);

        return Task.CompletedTask;
    }

    public Task<MenuItem> GetMenuItem(string id)
    {
        return Task.FromResult<MenuItem>(_items.FirstOrDefault(item => item.Id == id));
    }

    public Task<IEnumerable<MenuItem>> GetMenuItems()
    {
        return Task.FromResult<IEnumerable<MenuItem>>(_items.AsEnumerable());
    }

    public Task RemoveMenuItem(MenuItem menuItem)
    {
        _items.Remove(menuItem);

        return Task.CompletedTask;
    }

    public Task UpdateMenuItem(MenuItem menuItem)
    {
        var item = _items.FirstOrDefault(item => item.Id == menuItem.Id);

        if (item is null) return Task.CompletedTask;;

        item.Type = menuItem.Type;
        item.Name = menuItem.Name;
        item.Description = menuItem.Description;
        item.Price = menuItem.Price;

        return Task.CompletedTask;
    }
}
