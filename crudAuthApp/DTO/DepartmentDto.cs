namespace crudAuthApp.DTO
{
    public class DepartmentDto
    {

        public string DeptName { get; set; }

        public string DeptCode { get; set; }

        public Guid CreatedBy { get; set; }

        public virtual Guid ManagerId { get; set; }
    }
}
