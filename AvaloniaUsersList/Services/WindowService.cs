using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using AvaloniaUsersList.ViewModels;
using AvaloniaUsersList.ViewModels.Common;
using AvaloniaUsersList.Views;
using AvaloniaUsersList.Views.Common;
using System.Threading.Tasks;

namespace AvaloniaUsersList.Services;

public class WindowService
{
    private static Window? _mainWindow;

    public static Window? GetMainWindow()
    {
        if (_mainWindow is not null)
            return _mainWindow;

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            _mainWindow = desktop.MainWindow;

        return _mainWindow;
    }

    public static void ShowMsgBox(string message, Window? ownerWindow = null)
    {
        Task.Run(async () =>
            Dispatcher.UIThread.InvokeAsync(() => ShowMsgBoxAsync(message, ownerWindow))
        ).Wait();
    }

    public static async Task ShowMsgBoxAsync(string message, Window? ownerWindow = null)
    {
        var vm = new MsgBoxViewModel(message);
        var wnd = new MsgBoxView { DataContext = vm };
        vm.Window = wnd;

        await wnd.ShowDialog(ownerWindow ?? GetMainWindow());
    }
}
