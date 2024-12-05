using Avalonia.Controls;
using AvaloniaUsersList.Models;
using AvaloniaUsersList.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AvaloniaUsersList.ViewModels;

public class AddEditUserViewModel : ViewModelBase
{
    public ICommand SaveCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public bool IsOk { get; set; } = false;
    public Window? Window { get; set; }

    public AddEditUserViewModel(User? model = null)
    {
        _model = model ?? new();
        
        Title = _model is null ? "Добавление пользователя" : "Редактирование пользователя";

        AccessLevels = Enum.GetValues(typeof(AccessLevelEnum))
            .Cast<AccessLevelEnum>()
            .ToList();

        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(() => Close(false));
    }

    private string? _title;
    public string? Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    private User? _model;
    public User? Model
    {
        get => _model;
        set => this.RaiseAndSetIfChanged(ref _model, value);
    }

    private List<AccessLevelEnum>? _accessLevels;
    public List<AccessLevelEnum>? AccessLevels
    {
        get => _accessLevels;
        set => this.RaiseAndSetIfChanged(ref _accessLevels, value);
    }

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(Model?.FirstName) || Model?.FirstName.Length < 2)
        {
            WindowService.ShowMsgBox("Некорректное имя", Window);
            return;
        }

        if (string.IsNullOrWhiteSpace(Model?.LastName) || Model?.LastName.Length < 2)
        {
            WindowService.ShowMsgBox("Некорректная фамилия", Window);
            return;
        }

        if (!string.IsNullOrWhiteSpace(Model?.Email) && 
            (Model?.Email.Length < 6 || Model?.Email.IndexOf('@') < 1))
        {
            WindowService.ShowMsgBox("Некорректный адрес электронной почты", Window);
            return;
        }

        Close(true);
    }

    private void Close(bool isOk)
    {
        IsOk = isOk;
        Window?.Close();
    }
}
