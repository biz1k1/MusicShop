using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicShop.Domain.Enums;
using MusicShop.Domain.Model.Aunth;
using Microsoft.Extensions.Options;

namespace MusicShop.Infrastructure.Data.Configuration
{


    //Role ++
    public  class RoleConfiguration : IEntityTypeConfiguration<RoleEntity> 
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x => x.Id);
            builder
                .HasMany(x => x.Permissions)
                .WithMany(x => x.Roles)
                
                .UsingEntity<RolePermissionsEntity>(
                x => x.HasOne<PermissionsEntity>().WithMany().HasForeignKey(x => x.PermissionsId),
                x => x.HasOne<RoleEntity>().WithMany().HasForeignKey(x => x.RoleId));
            var roles = Enum
                .GetValues<Role>()
                .Select(role => new RoleEntity
                {
                    Id = (int)role,
                    Name = role.ToString()
                });
            builder.HasData(roles);

        }
    }
    //Permission ++
    public class PermissionsConfiguration : IEntityTypeConfiguration<PermissionsEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionsEntity> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);
            var permissions = Enum
                .GetValues<Permissions>()
                .Select(permission => new PermissionsEntity
                {
                    Id = (int)permission,
                    Name = permission.ToString()
                });
            builder.HasData(permissions);
        }
    }
    // Permission + Role 
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionsEntity>
    {
        private readonly AuthorizeOptions _authorization;
        public RolePermissionConfiguration(AuthorizeOptions authorization)
        {
            _authorization = authorization;
        }
        public void Configure(EntityTypeBuilder<RolePermissionsEntity> builder)
        {
            builder.ToTable("RolePermissions");
            builder.HasKey(x => new { x.RoleId, x.PermissionsId });
            builder.HasData(RolePermissionsParses());
        }


        private RolePermissionsEntity[] RolePermissionsParses()
        {

            return _authorization.RolePermissions
                .SelectMany(x => x.Permissions
                .Select(d => new RolePermissionsEntity
                {
                    RoleId = (int)Enum.Parse<Role>(x.Role),
                    PermissionsId = (int)Enum.Parse<Permissions>(d)
                }))
                .ToArray();
        }
    }

}

