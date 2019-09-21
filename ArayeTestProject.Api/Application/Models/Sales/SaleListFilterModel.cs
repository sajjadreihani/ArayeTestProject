namespace ArayeTestProject.Api.Application.Models.Sales
{
    public class SaleListFilterModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string CityName { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public long MinPrice { get; set; }
        public long MaxPrice { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
    }
}