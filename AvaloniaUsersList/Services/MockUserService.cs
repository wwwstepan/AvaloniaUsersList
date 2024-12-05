using AvaloniaUsersList.Models;
using System;
using System.Collections.Generic;

namespace AvaloniaUsersList.Services;

public class MockUserService : IUserService
{
    public ICollection<User> GetUsers()
    {
        return
        [
            new() {
                FirstName = "Иван",
                LastName = "Орлов",
                Login = "iorlov",
                Email = "orl@mail.ru",
                AccessLevel = AccessLevelEnum.User,
                Notes = "Boss",
            },
            new() {
                FirstName = "Сергей",
                LastName = "Воронов",
                Login = "svoronov",
                Email = "sv@yandex.ru",
                AccessLevel = AccessLevelEnum.Administrator,
                Notes = "Характер спокойный\nНе женат",
            },
            new() {
                FirstName = "Марина",
                LastName = "Галкина",
                Login = "mgalkina",
                Email = "marina2002@rambler.ru",
                AccessLevel = AccessLevelEnum.Moderator,
            },
        ];
    }

    public bool AddUser(User _) => true;

    public bool SaveUser(User _) => true;

    public bool DeleteUser(Guid _) => true;
}
