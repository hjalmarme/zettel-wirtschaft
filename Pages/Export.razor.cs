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

    protected IEnumerable<Models.Order> orders;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        orders = await DataService.GetOrders();
    }

    public async Task DailyExport()
    {
        DateTime now = DateTime.Now;

        // file name
        string fileName = $"DailyExport_{now.ToString("yyyy-MM-dd_HH-mm")}.csv";

        // header
        string fileContent = "ID;Date;Name;Price;\n";

        // content
        foreach (var order in orders.Where(x => x.Date.Date == now.Date))
        {
            foreach (var item in order.Items)
            {
                fileContent += $"{order.Id};{order.Date.ToString("yyyy-MM-dd_HH-mm")};{item.Name};{item.Price};\n";
            }
        }

        // Call the JavaScript function to create and download the file
        await JSRuntime.InvokeVoidAsync("downloadFile", fileName, fileContent, "text/plain");
    }
}
