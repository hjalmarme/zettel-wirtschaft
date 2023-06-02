using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using ZettelWirtschaft.Services;

namespace ZettelWirtschaft.Pages;

public partial class OrderOverview
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

    public IEnumerable<Models.Order> Orders { get; set; } = new List<Models.Order>();

    public Models.Order SelectedOrder { get; set; }

    protected bool IsChildSelected(Models.Order order) => SelectedOrder == order;

    protected void OnSelectOrder(Models.Order order) => SelectedOrder = order;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            Orders = await DataService.GetOrders();
        }
        catch (Exception)
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = "Unable to load"
            });
        }
    }

    protected void AddOrder() => NavigationManager.NavigateTo(NavigationManager.BaseUri + "order");

    protected void EditOrder() => NavigationManager.NavigateTo(NavigationManager.BaseUri + $"order/{SelectedOrder?.Id}");

    protected async Task RemoveOrder()
    {
        if (SelectedOrder is null) return;

        await DataService.RemoveOrder(SelectedOrder.Id);

        Orders = await DataService.GetOrders();

        StateHasChanged();
    }
    protected async Task CloseOrder()
    {
        if (SelectedOrder is null) return;

        SelectedOrder.IsOpen = false;

        await DataService.UpdateOrder(SelectedOrder);

        StateHasChanged();
    }
}
