using Google.Protobuf.WellKnownTypes;

namespace ISSA_IdentityService.Protos.Student
{
    public partial class Student
    {
        public Student(Contract.Repository.Entity.Student entity)
        {
            Id = entity.Id;
            IsDelete = entity.IsDelete;
            CreatedTime = Timestamp.FromDateTime(entity.CreatedTime);
            LastUpdatedTime = entity.LastUpdatedTime == null ? null : Timestamp.FromDateTime(entity.LastUpdatedTime.Value);
            if (entity.ApplicationUser != null)
            {
                ApplicationUser = entity.ApplicationUser;
            }
        }

        public static implicit operator Student(Contract.Repository.Entity.Student entity) => new(entity);
    }

    public partial class StudentPagi
    {
        public StudentPagi(Core.Models.Common.PaginatedList<Contract.Repository.Entity.Student> pagination)
        {
            PageNumber = pagination.PageNumber;
            TotalPages = pagination.TotalPages;
            TotalCount = pagination.TotalCount;
            Items.Clear();
            pagination.Items.ToList().ForEach(x => Items.Add(new Student(x)));
        }

        public static implicit operator StudentPagi(Core.Models.Common.PaginatedList<Contract.Repository.Entity.Student> pagination) => new(pagination);
    }

}
