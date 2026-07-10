namespace EcoMeal.Constants;

public static class AppRoles
{
    public const string Admin = "admin";
    public const string User = "user"; // alternatively, "customer"
    public const string Manager = "manager"; // alternatively, "business manager"

    public static readonly string[] AllRoles = [Admin, User, Manager];
}
