using System.ComponentModel;

namespace Identity.Domain.Enums;

public enum Role
{
    [Description(nameof(User))]
    User = 1,
    
    [Description(nameof(Administrator))]
    Administrator = 2,
}