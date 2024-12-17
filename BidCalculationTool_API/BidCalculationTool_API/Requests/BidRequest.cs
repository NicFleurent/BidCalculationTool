using System.ComponentModel.DataAnnotations;

namespace BidCalculationTool_API.Requests
{
    public class BidRequest
    {
        [Range(1, double.MaxValue, ErrorMessage = "The base price must be greater than or equal to 1.")]
        public double BasePrice { get; set; }

        [Required(ErrorMessage = "The bid type is required.")]
        [RegularExpression(@"^(common|luxury)$", ErrorMessage = "The bid type must be 'common' or 'luxury'.")]
        public string BidType { get; set; }
    }
}
