using System;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Commands;
using ArayeTestProject.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArayeTestProject.Api.Controllers {
    [Route ("api/excel")]
    [ApiController]
    public class ExcelController : ControllerBase {
        private readonly IMediator mediator;

        public ExcelController (IMediator mediator) {
            this.mediator = mediator;
        }

        // GET api/excel/generate
        [HttpGet ("generate")]
        public async Task<IActionResult> Get () {
            var command = await mediator.Send (new GenerateExcelFileQuery ());
            return Ok (command.Url);
        }

        [HttpPost ("importfile")]
        [RequestFormLimits (MultipartBodyLengthLimit = int.MaxValue)]
        [RequestSizeLimit (int.MaxValue)]
        public async Task<IActionResult> ImportFile () {
            try {

                await mediator.Send (new ImportExcelFileCommand (HttpContext));
                return Ok ();
            } catch (Exception ex) {
                return BadRequest (ex);
            }
        }

    }
}