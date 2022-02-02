using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using TruckApplication.Messages;
using TruckApplication.Models;
using TruckApplication.Queries;

namespace TruckApplication.Controllers
{
    [ApiController]
    [Route("/truck/")]
    public class TruckController : ControllerBase
    {
        private IMediator _mediator { get; }

        public TruckController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("random")]
        public async Task<TruckModel> GetTruckInfo([FromRoute] string truckName, CancellationToken ct)
        {
            var truckModel = new TruckModel()
            {
                Name = truckName
            };
            var message = new TruckMessage { Truck = truckModel };
            var truck = await _mediator.Send(new GetTruckInfoQuery { Message = message }, CancellationToken.None).ConfigureAwait(false);
            //return truck;
            return truck;
        }
    }
}