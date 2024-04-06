namespace MusicShop.Domain.Model.Aunth
{
    public class AuthorizeOptions
    {
       public RolePermissions[] RolePermissions { get; set; } = [];
    }
    public class RolePermissions
    {
        public string Role { get; set; } = string.Empty;
        public string[] Permissions { get; set; } = [];
    }
}
