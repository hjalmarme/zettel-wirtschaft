using ZettelWirtschaft.Models;

namespace ZettelWirtschaft.Services;

public interface IDataService
{
    public Task AddMenuItem(MenuItem menuItem);

    public Task<MenuItem> GetMenuItem(string id);

    public Task<IEnumerable<MenuItem>> GetMenuItems();

    public Task UpdateMenuItem(MenuItem menuItem);

    public Task RemoveMenuItem(MenuItem menuItem);
}
