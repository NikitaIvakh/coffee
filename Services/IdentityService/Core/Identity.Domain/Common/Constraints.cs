namespace Identity.Domain.Common;

public struct Constraints
{
    public const int MaxLength = 100;
    public const int MinLength = 1;

    public const int MaxPasswordLenght = 500;
    public const int MinPasswordLenght = 5;
    
    public const int MaxRefreshTokenMaxLength = 1000;
    public const int MinRefreshTokenMaxLength = 1;
}