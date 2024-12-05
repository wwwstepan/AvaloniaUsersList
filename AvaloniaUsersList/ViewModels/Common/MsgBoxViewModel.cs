using Avalonia.Controls;
using ReactiveUI;
using System.Windows.Input;

namespace AvaloniaUsersList.ViewModels.Common;

public class MsgBoxViewModel : ViewModelBase
{
    public ICommand OkCommand { get; set; }

    public Window? Window { get; set; }

    private string? _message;
    public string? Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public MsgBoxViewModel(string message)
    {
        Message = message;
        OkCommand = ReactiveCommand.Create(() => Window?.Close());
    }
}
