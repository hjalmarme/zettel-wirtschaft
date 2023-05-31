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
        string fileName = $"DailyExport_{now:yyyy-MM-dd_HH-mm}.xlsx";

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
        foreach (var order in orders.Where(x => x.Date.Date == now.Date))
        {
            foreach (var item in order.Items)
            {
                worksheet.Cell($"A{row}").Value = order.Id;
                worksheet.Cell($"B{row}").Value = order.Date.ToString("yyyy-MM-dd_HH-mm");
                worksheet.Cell($"C{row}").Value = item.Name;
                worksheet.Cell($"D{row}").Value = item.Price;
                row++;
            }
        }

        // Convert workbook to a byte array
        byte[] byteArray;
        using (var memoryStream = new MemoryStream())
        {
            workbook.SaveAs(memoryStream);
            byteArray = memoryStream.ToArray();
        }

        // download
        await JSRuntime.InvokeVoidAsync("downloadFile", fileName, byteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }
}
