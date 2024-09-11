namespace Coffee.WebAPI.Utils;

public static class StaticDetails
{
    public const string RoleUser = "User";
    public const string RoleAdministrator = "Administrator";

    public const string UserPolicy = "UserPolicy";
    public const string AdministratorPolicy = "AdministratorPolicy";
    public const string UserAndAdministratorPolicy = "UserAndAdministratorPolicy";
    
    public const string AdministratorOrUser = RoleAdministrator + "," + RoleUser; 
}