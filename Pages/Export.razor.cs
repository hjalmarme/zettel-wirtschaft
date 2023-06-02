using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using ZettelWirtschaft.Services;

namespace ZettelWirtschaft.Pages;

public partial class Export
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

    [Inject]
    protected IExportService ExportService { get; set; }

    protected RadzenDataGrid<(Models.Order order, Models.MenuItem item)> dataGrid;

    protected List<(Models.Order order, Models.MenuItem item)> orderItems = new();

    protected IEnumerable<Models.Order> orders;

    protected DateTime date = DateTime.Now.Date;

    protected bool showClosed = true;

    protected bool showOpen = false;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        orders = await DataService.GetOrders();

        await FilterOrders();
    }

    protected async Task FilterOrders()
    {
        var filteredOrders = orders.Where(x =>
            x.Date.Date == date.Date &&
            ((showClosed && !x.IsOpen) || (showOpen && x.IsOpen))
        );

        orderItems.Clear();
        foreach (var order in filteredOrders)
        {
            foreach (var item in order.Items)
            {
                orderItems.Add((order, item));
            }
        }

        await dataGrid.Reload();
    }

    protected async Task ExportXlsx()
    {
        string fileName = $"DailyExport_{date:yyyy-MM-dd}.xlsx";

        var (content, fileType) = ExportService.CreateXlsx(orderItems);

        await JSRuntime.InvokeVoidAsync("downloadFile", fileName, content, fileType);
    }
}
