using System.ComponentModel.DataAnnotations;

namespace ArayeTestProject.Api.Application.Models.Domain {
    public class Sale {
        [Key]
        public long Id { get; set; }

        [StringLength (50)]
        public string UserName { get; set; }

        [StringLength (50)]
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public long Price { get; set; }

        public long CityId { get; set; }
        public City City { get; set; }
    }
}