using ArayeTestProject.Api.Application.Models.ExcelFile;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArayeTestProject.Api.Application.Commands {
    public class ImportExcelFileCommand : IRequest {
        public ImportExcelFileCommand (HttpContext context) {
            this.context = context;
        }

        public HttpContext context;
    }
}