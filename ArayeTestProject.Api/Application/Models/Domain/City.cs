using System.ComponentModel.DataAnnotations;

namespace ArayeTestProject.Api.Application.Models.Domain {
    public class City {
        [Key]
        public long Id { get; set; }

        [StringLength (50)]
        public string Name { get; set; }
    }
}