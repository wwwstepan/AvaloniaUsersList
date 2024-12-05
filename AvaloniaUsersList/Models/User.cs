using System;

namespace AvaloniaUsersList.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public AccessLevelEnum AccessLevel { get; set; }
    public string Notes { get; set; } = string.Empty;

    public User Clone() => new()
    {
        Id = this.Id,
        FirstName = this.FirstName,
        LastName = this.LastName,
        Login = this.Login,
        Password = this.Password,
        Email = this.Email,
        AccessLevel = this.AccessLevel,
        Notes = this.Notes,
    };
}
