using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crudAuthApp.Model
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }

        public string DeptName { get; set; }

        public string DeptCode { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("Manager")]
        public virtual Guid ManagerId
        {
            get;
            set;
        }

        public Manager Manager { get; set; }
    }
}
