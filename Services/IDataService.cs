using ZettelWirtschaft.Client.Models;

namespace ZettelWirtschaft.Client.Services;

public interface IDataService
{
    public void AddMenuItem(MenuItem menuItem);

    public MenuItem GetMenuItem(string id);

    public IEnumerable<MenuItem> GetMenuItems();

    public void UpdateMenuItem(MenuItem menuItem);

    public void RemoveMenuItem(MenuItem menuItem);
}
