using ClosedXML.Excel;
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

    protected record OrderItem
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    };

    protected RadzenDataGrid<OrderItem> dataGrid;

    protected List<OrderItem> orderItems = new();

    protected IEnumerable<Models.Order> orders;

    protected DateTime date = DateTime.Now.Date;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        orders = await DataService.GetOrders();

        await FilterOrders();
    }

    protected async Task FilterOrders()
    {
        orderItems.Clear();
        foreach (var order in orders.Where(x => x.Date.Date == date.Date))
        {
            foreach (var item in order.Items)
            {
                orderItems.Add(new OrderItem
                {
                    Id = order.Id,
                    Date = order.Date.ToString("yyyy-MM-dd_HH-mm"),
                    Name = item.Name,
                    Price = item.Price
                });
            }
        }

        await dataGrid.Reload();
    }

    protected async Task ExportXlsx()
    {
        // file name
        string fileName = $"DailyExport_{date:yyyy-MM-dd}.xlsx";
        string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        // workbook / worksheet
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sheet1");

        // header
        worksheet.Cell("A1").Value = "ID";
        worksheet.Cell("B1").Value = "Date";
        worksheet.Cell("C1").Value = "Name";
        worksheet.Cell("D1").Value = "Price";

        // content
        int row = 2;
        foreach (var item in orderItems)
        {
            worksheet.Cell($"A{row}").Value = item.Id;
            worksheet.Cell($"B{row}").Value = item.Date;
            worksheet.Cell($"C{row}").Value = item.Name;
            worksheet.Cell($"D{row}").Value = item.Price;
            row++;
        }

        // Convert workbook to a byte array
        byte[] byteArray;
        using (var memoryStream = new MemoryStream())
        {
            workbook.SaveAs(memoryStream);
            byteArray = memoryStream.ToArray();
        }

        // download
        await JSRuntime.InvokeVoidAsync("downloadFile", fileName, byteArray, fileType);
    }
}
