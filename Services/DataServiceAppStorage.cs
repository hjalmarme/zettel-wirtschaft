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

    public Task AddOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<MenuItem> GetMenuItem(string id)
    {
        return Task.FromResult<MenuItem>(_items.FirstOrDefault(item => item.Id == id));
    }

    public Task<IEnumerable<MenuItem>> GetMenuItems()
    {
        return Task.FromResult<IEnumerable<MenuItem>>(_items.AsEnumerable());
    }

    public Task<Order> GetOrder(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Task RemoveMenuItem(string id)
    {
        _items.RemoveAll(menuItem => menuItem.Id == id);

        return Task.CompletedTask;
    }

    public Task RemoveOrder(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMenuItem(MenuItem menuItem)
    {
        var item = _items.FirstOrDefault(item => item.Id == menuItem.Id);

        if (item is null) return Task.CompletedTask;

        item.Type = menuItem.Type;
        item.Name = menuItem.Name;
        item.Description = menuItem.Description;
        item.Price = menuItem.Price;

        return Task.CompletedTask;
    }

    public Task UpdateOrder(Order order)
    {
        throw new NotImplementedException();
    }
}
