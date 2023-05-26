using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Text.Json;
using ZettelWirtschaft.Services;

namespace ZettelWirtschaft.Pages;

public partial class Order
{
    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected TooltipService TooltipService { get; set; }

    [Inject]
    protected ContextMenuService ContextMenuService { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    [Inject]
    protected IDataService DataService { get; set; }

    [Parameter]
    public string OrderId { get; set; }

    public List<string> Types { get; set; } = new();

    public IEnumerable<Models.MenuItem> MenuItems { get; set; } = null;

    public Models.Order CurrentOrder { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        MenuItems = await DataService.GetMenuItems();

        Types = MenuItems.Select(x => x.Type).Distinct().ToList();

        var orders = await DataService.GetOrders();

        if (OrderId is null)
        {
            CurrentOrder.Number = orders.Select(x => x.Number).OrderDescending().FirstOrDefault() + 1;
        }
        else
        {
            CurrentOrder = orders.FirstOrDefault(x => x.Id == OrderId);
        }
    }

    protected async void OnSave()
    {
        if (OrderId is null)
        {
            await DataService.AddOrder(CurrentOrder);
        }
        else
        {
            await DataService.UpdateOrder(CurrentOrder);
        }

        NavigationManager.NavigateTo(NavigationManager.BaseUri + "orderoverview");
    }

    protected void OnExit()
    {
        NavigationManager.NavigateTo(NavigationManager.BaseUri + "orderoverview");
    }

    public void AddToOrder(Models.MenuItem item)
    {
        CurrentOrder.Items.Add(item);
        StateHasChanged();
    }

    protected void RemoveItem(Models.MenuItem item) => CurrentOrder.Items.Remove(item);
}
