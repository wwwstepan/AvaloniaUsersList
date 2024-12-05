using AvaloniaUsersList.Models;
using System;
using System.Collections.Generic;

namespace AvaloniaUsersList.Services;

public interface IUserService
{
    ICollection<User> GetUsers();

    bool AddUser(User user);

    bool SaveUser(User user);

    bool DeleteUser(Guid id);
}
