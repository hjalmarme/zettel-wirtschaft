namespace ZettelWirtschaft.Services
{
    public interface IExportService
    {
        public (byte[] content, string fileType) CreateXlsx(IEnumerable<(Models.Order order, Models.MenuItem item)> orderItems);
    }
}
