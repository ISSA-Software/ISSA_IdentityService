using Google.Protobuf.WellKnownTypes;

namespace ISSA_IdentityService.Protos
{
    public sealed partial class ApplicationUser
    {
        public ApplicationUser (Contract.Repository.Entity.IdentityModels.ApplicationUser entity )
        {
            Id = entity.Id;
            UserName = entity.UserName;
            NormalizedUserName = entity.NormalizedUserName;
            Email = entity.Email;
            NormalizedEmail = entity.NormalizedEmail;
            EmailConfirmed = entity.EmailConfirmed;
            PasswordHash = entity.PasswordHash;
            SecurityStamp = entity.SecurityStamp;
            ConcurrencyStamp = entity.ConcurrencyStamp;
            PhoneNumber = entity.PhoneNumber;
            PhoneNumberConfirmed = entity.PhoneNumberConfirmed;
            TwoFactorEnabled = entity.TwoFactorEnabled;
            LockoutEnd = entity.LockoutEnd == null ? null : Timestamp.FromDateTimeOffset(entity.LockoutEnd.Value);
            LockoutEnabled = entity.LockoutEnabled;
            AccessFailedCount = entity.AccessFailedCount;
            Name = entity.Name;
            ImageUrl = entity.ImageUrl;
            IsDelete = entity.IsDelete;
            CreatedTime = Timestamp.FromDateTimeOffset(entity.CreatedTime);
            LastUpdatedTime = entity.LastUpdatedTime == null ? null : Timestamp.FromDateTimeOffset(entity.LastUpdatedTime.Value);
        }

        public static implicit operator ApplicationUser(Contract.Repository.Entity.IdentityModels.ApplicationUser entity) => new(entity);
    }
}
