using Google.Protobuf.WellKnownTypes;

namespace ISSA_IdentityService.Protos.Admin
{
    public partial class Admin
    {
        public Admin(Contract.Repository.Entity.Admin entity)
        {
            Id = entity.Id;
            IsDelete = entity.IsDelete;
            CreatedTime = Timestamp.FromDateTime(entity.CreatedTime);
            LastUpdatedTime = entity.LastUpdatedTime == null ? null : Timestamp.FromDateTime(entity.LastUpdatedTime.Value);
            if(entity.ApplicationUser != null)
            {
                ApplicationUser = entity.ApplicationUser;
            }
        }

        public static implicit operator Admin(Contract.Repository.Entity.Admin entity) => new(entity);
    }

    public partial class AdminPagi
    {
        public AdminPagi(Core.Models.Common.PaginatedList<Contract.Repository.Entity.Admin> pagination)
        {
            PageNumber = pagination.PageNumber;
            TotalPages = pagination.TotalPages;
            TotalCount = pagination.TotalCount;
            Items.Clear();
            pagination.Items.ToList().ForEach(x => Items.Add(new Admin(x)));
        }

        public static implicit operator AdminPagi(Core.Models.Common.PaginatedList<Contract.Repository.Entity.Admin> pagination) => new(pagination);
    }
}
