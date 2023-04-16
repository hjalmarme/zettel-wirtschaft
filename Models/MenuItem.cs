namespace ZettelWirtschaft.Client.Models;

public class MenuItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Type { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Price { get; set; }
}
