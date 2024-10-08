namespace Identity.Domain.Common;

public struct Constraints
{
    public const int MaxLength = 100;
    public const int MinLength = 3;

    public const int MaxPasswordLenght = 500;
    public const int MinPasswordLenght = 3;
    
    public const int MaxRefreshTokenMaxLength = 1000;
    public const int MinRefreshTokenMaxLength = 3;
}