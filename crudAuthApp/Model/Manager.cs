using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crudAuthApp.Model
{
    public class Manager
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }
        public int LocationCode { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
