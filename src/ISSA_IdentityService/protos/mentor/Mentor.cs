using Google.Protobuf.WellKnownTypes;

namespace ISSA_IdentityService.Protos.Mentor
{
    public partial class Mentor
    {
        public Mentor(Contract.Repository.Entity.Mentor entity)
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

        public static implicit operator Mentor(Contract.Repository.Entity.Mentor entity) => new(entity);
    }

    public partial class MentorPagi
    {
        public MentorPagi(Core.Models.Common.PaginatedList<Contract.Repository.Entity.Mentor> pagination)
        {
            PageNumber = pagination.PageNumber;
            TotalPages = pagination.TotalPages;
            TotalCount = pagination.TotalCount;
            Items.Clear();
            pagination.Items.ToList().ForEach(x => Items.Add(new Mentor(x)));
        }

        public static implicit operator MentorPagi(Core.Models.Common.PaginatedList<Contract.Repository.Entity.Mentor> pagination) => new(pagination);
    }

}
