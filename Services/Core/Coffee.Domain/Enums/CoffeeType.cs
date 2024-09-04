using System.ComponentModel;

namespace Coffee.Domain.Enums;

public enum CoffeeType
{
    [Description("Brazil")]
    Brazil = 1,

    [Description("Kenya")]
    Kenya = 2,

    [Description("Columbia")]
    Columbia = 3
}