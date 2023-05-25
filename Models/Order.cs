namespace ZettelWirtschaft.Models;

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int Number { get; set; } = 0;

    public string Name { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.Now;

    public bool IsOpen { get; set; } = true;

    public List<MenuItem> Items { get; set; } = new();
}
