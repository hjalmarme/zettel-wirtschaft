using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using ZettelWirtschaft.Models;
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

    protected RadzenDataGrid<MenuItem> dataGrid;

    protected IEnumerable<MenuItem> menuItems;

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

    protected async Task OpenAddMenuItem()
    {
        var added = await DialogService.OpenAsync<AddMenuItem>($"Add Menu Item");

        if (added)
        {
            await dataGrid.Reload();
        }
    }
}
