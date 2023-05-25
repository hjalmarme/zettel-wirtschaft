using ZettelWirtschaft.Models;

namespace ZettelWirtschaft.Services;

public interface IDataService
{
    public Task AddMenuItem(MenuItem menuItem);

    public Task<MenuItem> GetMenuItem(string id);

    public Task<IEnumerable<MenuItem>> GetMenuItems();

    public Task UpdateMenuItem(MenuItem menuItem);

    public Task RemoveMenuItem(string id);

    public Task AddOrder(Order order);

    public Task<Order> GetOrder(string id);

    public Task<IEnumerable<Order>> GetOrders();

    public Task UpdateOrder(Order order);

    public Task RemoveOrder(string id);
}
