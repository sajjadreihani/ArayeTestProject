using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.ExcelFile;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ArayeTestProject.Api.Application.Queries {
    public class GenerateExcelFileQueryHandler : IRequestHandler<GenerateExcelFileQuery, GenerateExcelFileResource> {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContext;

        public GenerateExcelFileQueryHandler (IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContext) {
            this.hostingEnvironment = hostingEnvironment;
            this.httpContext = httpContext;
        }

        public Task<GenerateExcelFileResource> Handle (GenerateExcelFileQuery  request, CancellationToken cancellationToken) {

            string folderName = "TempExcels";
            string webRootPath = hostingEnvironment.ContentRootPath;
            string newPath = Path.Combine (webRootPath, folderName);
            string sFileName = @"TestSample.xlsx";
            string filePath = Path.Combine (newPath, sFileName);
            string URL = string.Format ("{0}://{1}/{2}/{3}", httpContext.HttpContext.Request.Scheme, httpContext.HttpContext.Request.Host, folderName, sFileName);

            if (!Directory.Exists (newPath)) {
                Directory.CreateDirectory (newPath);
            }
            if (File.Exists (filePath)) {
                File.Delete (filePath);
            }
            var output = new GenerateExcelFileResource ();
            var memory = new MemoryStream ();
            using (var fs = new FileStream (filePath, FileMode.Create, FileAccess.Write)) {
                IWorkbook workbook;
                workbook = new XSSFWorkbook ();
                ISheet citySheet = workbook.CreateSheet ("Cities");
                IRow cityRow = citySheet.CreateRow (0);
                cityRow.CreateCell (0).SetCellValue ("نام شهر");
                var cities = new Faker<City> (locale: "fa")
                    .RuleFor (c => c.Name, opt => opt.Lorem.Word ()).Generate (1000);
                cities = cities.GroupBy (c => c.Name).Select (c => c.First ()).ToList ();
                int cityRowCountr = 1;
                foreach (var city in cities) {
                    cityRow = citySheet.CreateRow (cityRowCountr);

                    cityRow.CreateCell (0).SetCellValue (city.Name);
                    cityRowCountr++;
                }

                ISheet saleSheet = workbook.CreateSheet ("Sales");
                IRow saleRow = saleSheet.CreateRow (0);
                saleRow.CreateCell (0).SetCellValue ("نام شهر");
                saleRow.CreateCell (1).SetCellValue ("نام فرد");
                saleRow.CreateCell (2).SetCellValue ("نام محصول");
                saleRow.CreateCell (3).SetCellValue ("کد محصول");
                saleRow.CreateCell (4).SetCellValue ("قیمت محصول");
                var sale = new Faker<Sale> (locale: "fa")
                    .RuleFor (s => s.UserName, opt => opt.Lorem.Word ())
                    .RuleFor (s => s.ProductName, opt => opt.Lorem.Word ())
                    .RuleFor (s => s.Price, opt => opt.Random.Long (1000, 50000))
                    .Generate (1040000);

                var faker = new Faker ();
                    var groupSale = sale.GroupBy(s=>s.ProductName);
                    foreach (var item in groupSale)
                    {
                        var ProductId = faker.Random.Int(1,1400000).ToString();
                        while(sale.Any(s=>s.ProductId==ProductId))
                        ProductId = faker.Random.Int(1,1400000).ToString();
                        sale.Where(s=>s.ProductName==item.Key).ToList().ForEach(s=>s.ProductId=ProductId);
                    }
                int saleRowCounter = 1;
                var cityNames = cities.Select (c => c.Name).ToList ();
                foreach (var item in sale) {
                    saleRow = saleSheet.CreateRow (saleRowCounter);
                    saleRow.CreateCell (0).SetCellValue (faker.PickRandom (cityNames));
                    saleRow.CreateCell (1).SetCellValue (item.UserName);
                    saleRow.CreateCell (2).SetCellValue (item.ProductName);
                    saleRow.CreateCell (3).SetCellValue (item.ProductId);
                    saleRow.CreateCell (4).SetCellValue (item.Price);
                    saleRowCounter++;
                }
                workbook.Write (fs);
            }
            using (var stream = new FileStream (filePath, FileMode.Open)) {
                stream.CopyTo (memory);
            }
            memory.Position = 0;
            output.Url = URL;
            return Task.FromResult (output);
        }

    }
}