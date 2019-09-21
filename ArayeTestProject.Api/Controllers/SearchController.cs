using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Commands;
using ArayeTestProject.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArayeTestProject.Api.Controllers {
    [Route ("api/search")]
    [ApiController]
    public class SearchController : ControllerBase {
        private readonly IMediator mediator;

        public SearchController (IMediator mediator) {
            this.mediator = mediator;
        }

        [HttpGet ("username")]
        public async Task<IActionResult> GetUserName (string searchKey) {
            return Ok (await mediator.Send (new SearchUserNameQuery (searchKey)));
        }

        [HttpGet ("cityname")]
        public async Task<IActionResult> GetCityName (string searchKey) {
            return Ok (await mediator.Send (new SearchCityNameQuery (searchKey)));
        }

        [HttpGet ("productname")]
        public async Task<IActionResult> GetProductName (string searchKey) {
            return Ok (await mediator.Send (new SearchProductNameQuery (searchKey)));
        }
    }
}