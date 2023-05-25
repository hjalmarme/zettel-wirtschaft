using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using ZettelWirtschaft.Services;

namespace ZettelWirtschaft.Pages;

public partial class Menu
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
    public IDataService DataService { get; set; }

    protected RadzenDataGrid<Models.MenuItem> dataGrid;

    protected IEnumerable<Models.MenuItem> menuItems;
    protected IList<Models.MenuItem> selectedItems = new List<Models.MenuItem>();

    protected int menuItemsCount;

    protected async Task MenuItemsLoadData(LoadDataArgs args)
    {
        try
        {
            var result = await DataService.GetMenuItems();

            menuItems = result;
            menuItemsCount = result.Count();
        }
        catch (Exception)
        {
            NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
        }
    }

    protected async Task AddMenuItem()
    {
        var item = await DialogService.OpenAsync<MenuItem>("Add Menu Item");

        if (item is not null)
        {
            await DataService.AddMenuItem(item);
            await dataGrid.Reload();
        }
    }

    protected async Task EditMenuItem()
    {
        var item = selectedItems.FirstOrDefault();
        if (item is null) return;

        item = await DialogService.OpenAsync<MenuItem>("Edit Menu Item", new() { { "Item", item } });

        if (item is not null)
        {
            await DataService.UpdateMenuItem(item);
            await dataGrid.Reload();
        }
    }

    protected async Task RemoveMenuItem()
    {
        var item = selectedItems.FirstOrDefault();

        bool remove = await DialogService.Confirm("Are you sure?", "Remove Menu Item", new() { OkButtonText = "Yes", CancelButtonText = "No" }) ?? false;

        if (remove && item is not null)
        {
            await DataService.RemoveMenuItem(item.Id);
            await dataGrid.Reload();
        }
    }

    protected async Task AddDemoData()
    {
        var data = new List<Models.MenuItem>()
        {
            new Models.MenuItem()
            {
                Name = "Kola",
                Description = "0,33",
                Price = "3",
                Type = "Drinks"
            },
            new Models.MenuItem()
            {
                Name = "Orange",
                Description = "0,33",
                Price = "3",
                Type = "Drinks"
            },
            new Models.MenuItem()
            {
                Name = "Zitrone",
                Description = "0,33",
                Price = "3",
                Type = "Drinks"
            },
            new Models.MenuItem()
            {
                Name = "Jever",
                Description = "0,33",
                Price = "3",
                Type = "Drinks"
            },
            new Models.MenuItem()
            {
                Name = "Pulleken",
                Description = "0,33",
                Price = "3",
                Type = "Drinks"
            },
            new Models.MenuItem()
            {
                Name = "Aperol",
                Description = "alk",
                Price = "3",
                Type = "Drinks"
            },
            new Models.MenuItem()
            {
                Name = "Burger",
                Description = "with cheese",
                Price = "5",
                Type = "Food"
            },
            new Models.MenuItem()
            {
                Name = "Pizza",
                Description = "vegan",
                Price = "5",
                Type = "Food"
            },
            new Models.MenuItem()
            {
                Name = "Panini",
                Description = "with rocktes",
                Price = "5",
                Type = "Food"
            },
            new Models.MenuItem()
            {
                Name = "Banana Bread",
                Description = "vegan",
                Price = "5",
                Type = "Food"
            },
            new Models.MenuItem()
            {
                Name = "Pretzel",
                Description = "with butter",
                Price = "5",
                Type = "Food"
            },
            new Models.MenuItem()
            {
                Name = "Nuts",
                Description = "allergic",
                Price = "5",
                Type = "Food"
            },
        };

        foreach (var i in data)
        {
            await DataService.AddMenuItem(i);
        }

        await dataGrid.Reload();
    }
}
