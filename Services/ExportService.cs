using ClosedXML.Excel;
using System.Globalization;

namespace ZettelWirtschaft.Services
{
    public class ExportService : IExportService
    {
        public (byte[] content, string fileType) CreateXlsx(IEnumerable<(Models.Order order, Models.MenuItem item)> orderItems)
        {
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
                worksheet.Cell($"A{row}").Value = item.order.Id;
                worksheet.Cell($"B{row}").Value = item.order.Date.ToString(CultureInfo.CurrentCulture);
                worksheet.Cell($"C{row}").Value = item.item.Name;
                worksheet.Cell($"D{row}").Value = item.item.Price;
                row++;
            }

            // Convert workbook to a byte array
            byte[] byteArray;
            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                byteArray = memoryStream.ToArray();
            }

            return (byteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
