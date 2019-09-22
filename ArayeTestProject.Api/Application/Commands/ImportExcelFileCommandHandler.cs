using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Extensions;
using ArayeTestProject.Api.Presistences.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ArayeTestProject.Api.Application.Commands {
    public class ImportExcelFileCommandHandler : IRequestHandler<ImportExcelFileCommand> {
        private readonly IMainRepository repository;
        private readonly IHostingEnvironment hostingEnvironment;
        private static readonly FormOptions _defaultFormOptions = new FormOptions ();
        public ImportExcelFileCommandHandler (IMainRepository repository, IHostingEnvironment hostingEnvironment) {
            this.repository = repository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<Unit> Handle (ImportExcelFileCommand request, CancellationToken cancellationToken) {

            string fullPath = null;
            List<City> cities = new List<City> ();
            List<Sale> sales = new List<Sale> ();

            var boundary = MultipartRequestHelper.GetBoundary (
                MediaTypeHeaderValue.Parse (request.context.Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader (boundary, request.context.Request.Body);

            var section = await reader.ReadNextSectionAsync ();
            while (section != null) {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse (section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader) {
                    if (MultipartRequestHelper.HasFileContentDisposition (contentDisposition)) {
                        string folderName = "TempExcels";
                        string webRootPath = hostingEnvironment.ContentRootPath;
                        string newPath = Path.Combine (webRootPath, folderName);
                        if (!Directory.Exists (newPath)) {
                            Directory.CreateDirectory (newPath);
                        }
                        fullPath = Path.Combine (newPath, "testdata.xlsx");
                        var targetStream = System.IO.File.Create (fullPath);
                        await section.Body.CopyToAsync (targetStream);
                        ISheet citySheet, saleSheet;
                        await targetStream.FlushAsync();
                        targetStream.Seek(0,SeekOrigin.Begin);
                        XSSFWorkbook hssfwb = new XSSFWorkbook (targetStream); //This will read 2007 Excel format  
                        citySheet = hssfwb.GetSheet ("Cities");
                        saleSheet = hssfwb.GetSheet ("Sales"); //get first sheet from workbook  
                        for (int i = (citySheet.FirstRowNum + 1); i <= citySheet.LastRowNum; i++) //Read Excel File
                        {

                            IRow row = citySheet.GetRow (i);
                            if (row == null) continue;
                            cities.Add (new City () {
                                Name = row.GetCell (0).StringCellValue
                            });
                        }
                        cities = await repository.AddCities (cities);
                        for (int i = (saleSheet.FirstRowNum + 1); i <= saleSheet.LastRowNum; i++) //Read Excel File
                        {

                            IRow row = saleSheet.GetRow (i);
                            if (row == null) continue;
                            sales.Add (new Sale () {
                                CityId = cities.First (c => c.Name == row.GetCell (0).StringCellValue).Id,
                                    UserName = row.GetCell (1).StringCellValue,
                                    ProductName = row.GetCell (2).StringCellValue,
                                    ProductId = row.GetCell (3).StringCellValue,
                                    Price = (long) row.GetCell (4).NumericCellValue,
                            });
                        }
                        if (sales.Count > 0)
                            await repository.AddSales (sales);

                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync ();
            }

            return Unit.Value;
        }
    }

}