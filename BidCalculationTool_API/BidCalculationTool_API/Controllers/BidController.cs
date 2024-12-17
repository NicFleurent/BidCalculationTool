using BidCalculationTool_API.Models;
using BidCalculationTool_API.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BidCalculationTool_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        // POST api/<BidController>
        [HttpPost]
        public IActionResult Post([FromBody] BidRequest bidRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Bid bid;
            if(bidRequest.BidType == "common")
            {
                bid = new CommonVehicleBid(bidRequest.BasePrice);
            }
            else if(bidRequest.BidType == "luxury")
            {
                bid = new LuxuryVehicleBid(bidRequest.BasePrice);
            }
            else
            {
                return BadRequest(ModelState);
            }

            BidModel response = bid.getBidModel();

            return Ok(response);
        }
    }
}
