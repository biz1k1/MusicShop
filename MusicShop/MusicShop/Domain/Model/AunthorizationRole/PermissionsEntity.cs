namespace MusicShop.Domain.Model.Aunth
{
    public class PermissionsEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
