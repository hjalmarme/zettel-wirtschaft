using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Text.Json;
using ZettelWirtschaft.Services;

namespace ZettelWirtschaft.Shared;

public partial class OrderComponent
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

    [Parameter]
    public Models.Order Order { get; set; } = new();

    [Parameter]
    public EventCallback OnClick { get; set; }

    [Parameter]
    public Action<Models.MenuItem> OnClickItem { get; set; }

    [Parameter]
    public bool IsSelected { get; set; }

    protected int CalculateTotal()
    {
        if (!Order.Items.Any())
        {
            return 0;
        }

        return Order.Items.Select(x => Convert.ToInt32(x.Price)).Aggregate((x, y) => x + y);
    }
}
