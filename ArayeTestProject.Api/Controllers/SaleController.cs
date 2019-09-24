using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Commands;
using ArayeTestProject.Api.Application.Models.ErrorMessage;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Application.Queries;
using ArayeTestProject.Api.Presistences.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArayeTestProject.Api.Controllers {
    [Route ("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase {
        private readonly IMediator mediator;

        public SaleController (IMediator mediator) {
            this.mediator = mediator;
        }

        [HttpPost ("getlist")]
        public async Task<IActionResult> GetList (SaleListFilterModel filter) {
            return Ok (await mediator.Send (new GetSaleListQuery (filter)));
        }

        [HttpPost ("add")]
        public async Task<IActionResult> Add (SaleResource resource) {
            try {
                await mediator.Send (new AddSaleCommand (resource));
                return Ok ();
            } catch (ProductNotFoundException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 400,
                            ErrorMessage = "Product Not Found"
                    }
                });
            } catch (UserNameNotFoundException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 401,
                            ErrorMessage = "User Not Found"
                    }
                });
            } catch (CityNameNotFoundException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 402,
                            ErrorMessage = "City Not Found"
                    }
                });
            } catch (MaximumAmountThresholdException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 403,
                            ErrorMessage = "Price Is More Than 15% Of Last Price"
                    }
                });
            } catch (Exception ex) {
                return StatusCode (500, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 500,
                            ErrorMessage = ex.Message
                    }
                });
            }

        }

        [HttpPost ("update")]
        public async Task<IActionResult> Update (SaleResource resource) {
            try {
                await mediator.Send (new UpdateSaleCommand (resource));
                return Ok ();
            } catch (ProductNotFoundException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 400,
                            ErrorMessage = "Product Not Found"
                    }
                });
            } catch (UserNameNotFoundException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 401,
                            ErrorMessage = "User Not Found"
                    }
                });
            } catch (CityNameNotFoundException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 402,
                            ErrorMessage = "City Not Found"
                    }
                });
            } catch (MaximumAmountThresholdException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 403,
                            ErrorMessage = "Price Is More Than 15% Of Last Price"
                    }
                });
            } catch (SaleNotFoundException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 404,
                            ErrorMessage = "Sale Not Found"
                    }
                });
            } catch (Exception ex) {
                return StatusCode (500, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 500,
                            ErrorMessage = ex.Message
                    }
                });
            }

        }

        [HttpPost ("remove")]
        public async Task<IActionResult> Remove (SaleResource resource) {
            try {
                await mediator.Send (new RemoveSaleCommand (resource));
                return Ok ();
            } catch (SaleNotFoundException) {
                return StatusCode (400, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 404,
                            ErrorMessage = "Sale Not Found"
                    }
                });
            } catch (Exception ex) {
                return StatusCode (500, new ErrorMessageModel () {
                    Data = new ErrorData () {
                        ErrorCode = 500,
                            ErrorMessage = ex.Message
                    }
                });
            }

        }

    }
}