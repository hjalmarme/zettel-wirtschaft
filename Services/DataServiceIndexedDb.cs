using Microsoft.JSInterop;
using ZettelWirtschaft.Models;

namespace ZettelWirtschaft.Services;

public class DataServiceIndexedDb : IDataService
{
    private readonly IJSRuntime _jsRuntime;

    public DataServiceIndexedDb(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;

    public async Task AddMenuItem(MenuItem menuItem)
    {
        await _jsRuntime.InvokeVoidAsync("window.idbWrapper.add", "menuItems", menuItem);
    }

    public async Task<MenuItem> GetMenuItem(string id)
    {
        return await _jsRuntime.InvokeAsync<MenuItem>("window.idbWrapper.get", "menuItems", id);
    }

    public async Task<IEnumerable<MenuItem>> GetMenuItems()
    {
        return await _jsRuntime.InvokeAsync<IEnumerable<MenuItem>>("window.idbWrapper.getAll", "menuItems");
    }

    public async Task UpdateMenuItem(MenuItem menuItem)
    {
        await _jsRuntime.InvokeVoidAsync("window.idbWrapper.put", "menuItems", menuItem);
    }

    public async Task RemoveMenuItem(string id)
    {
        await _jsRuntime.InvokeVoidAsync("window.idbWrapper.delete", "menuItems", id);
    }

    public async Task AddOrder(Order order)
    {
        await _jsRuntime.InvokeVoidAsync("window.idbWrapper.add", "orders", order);
    }

    public async Task<Order> GetOrder(string id)
    {
        return await _jsRuntime.InvokeAsync<Order>("window.idbWrapper.get", "orders", id);
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _jsRuntime.InvokeAsync<IEnumerable<Order>>("window.idbWrapper.getAll", "orders");
    }

    public async Task UpdateOrder(Order order)
    {
        await _jsRuntime.InvokeVoidAsync("window.idbWrapper.put", "orders", order);
    }

    public async Task RemoveOrder(string id)
    {
        await _jsRuntime.InvokeVoidAsync("window.idbWrapper.delete", "orders", id);
    }
}
