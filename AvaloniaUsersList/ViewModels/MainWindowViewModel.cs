using AvaloniaUsersList.Models;
using AvaloniaUsersList.Services;
using AvaloniaUsersList.Views;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaUsersList.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IUserService _userService;

    public ICommand AddUserCommand { get; set; }
    public ICommand EditUserCommand { get; set; }
    public ICommand DeleteUserCommand { get; set; }

    public MainWindowViewModel()
    {
        _userService = new MockUserService();

        AddUserCommand = ReactiveCommand.CreateFromTask(AddUser);

        EditUserCommand = ReactiveCommand.CreateFromTask(EditUser, 
            this.WhenAnyValue(x => x.SelectedUser, (User? x) => x != null));

        DeleteUserCommand = ReactiveCommand.Create(DeleteUser, 
            this.WhenAnyValue(x => x.SelectedUser, (User? x) => x != null));

        Load();
    }

    private void Load()
    {
        var users = _userService.GetUsers();
        Users = new(users);
    }

    private ObservableCollection<User> _users = new();
    public ObservableCollection<User> Users
    {
        get => _users;
        set => this.RaiseAndSetIfChanged(ref _users, value);
    }

    private User? _selectedUser;
    public User? SelectedUser
    {
        get => _selectedUser;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedUser, value);
            UsersNotes = value?.Notes;
        }
    }

    private string? _usersNotes;
    public string? UsersNotes
    {
        get => _usersNotes;
        set => this.RaiseAndSetIfChanged(ref _usersNotes, value);
    }

    private async Task AddUser()
    {
        var vm = new AddEditUserViewModel();
        var wnd = new AddEditUserView { DataContext = vm };
        vm.Window = wnd;

        var mainWindow = WindowService.GetMainWindow();
        if (mainWindow is null)
            return;

        await wnd.ShowDialog(mainWindow);

        if (vm.IsOk && vm.Model is not null)
        {
            if (_userService.AddUser(vm.Model))
            {
                Users.Add(vm.Model);
                SelectedUser = vm.Model;
            }
        }
    }

    private async Task EditUser()
    {
        if (SelectedUser is null)
            return;

        var vm = new AddEditUserViewModel(SelectedUser.Clone());
        var wnd = new AddEditUserView { DataContext = vm };
        vm.Window = wnd;

        var mainWindow = WindowService.GetMainWindow();
        if (mainWindow is null)
            return;

        await wnd.ShowDialog(mainWindow);

        if (vm.IsOk && vm.Model is not null)
        {
            if (_userService.SaveUser(vm.Model))
            {
                var changedUser = Users.FirstOrDefault(x => x.Id == vm.Model.Id);

                if (changedUser is not null)
                {
                    var indexOfChangedUser = Users.IndexOf(changedUser);

                    if (indexOfChangedUser >= 0)
                    {
                        Users[indexOfChangedUser] = vm.Model;
                        SelectedUser = vm.Model;
                    }
                }
            }
        }
    }

    private void DeleteUser()
    {
        if (SelectedUser is null)
            return;

        if (_userService.DeleteUser(SelectedUser.Id))
            Users.Remove(SelectedUser);
    }
}
