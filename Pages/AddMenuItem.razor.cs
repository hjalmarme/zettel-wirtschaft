using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Text.Json;
using ZettelWirtschaft.Client.Models;
using ZettelWirtschaft.Client.Services;

namespace ZettelWirtschaft.Client.Pages;

public partial class AddMenuItem
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

    protected MenuItem Item = new();

    private void OnSubmit(MenuItem item)
    {
        Console.WriteLine($"Submit: {JsonSerializer.Serialize(item, new JsonSerializerOptions() { WriteIndented = true })}");

        DataService.AddMenuItem(item);

        DialogService.Close(true);
    }

    private void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
    {
        Console.WriteLine($"InvalidSubmit: {JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true })}");
    }
}
