using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Text.Json;
using ZettelWirtschaft.Services;

namespace ZettelWirtschaft.Pages;

public partial class MenuItem
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
    public Models.MenuItem Item { get; set; } = new();

    private void OnSubmit(Models.MenuItem item)
    {
        Console.WriteLine($"Submit: {JsonSerializer.Serialize(item, new JsonSerializerOptions() { WriteIndented = true })}");

        DialogService.Close(item);
    }

    private void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
    {
        Console.WriteLine($"InvalidSubmit: {JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true })}");
    }
}
