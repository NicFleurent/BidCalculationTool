using System.Xml.Linq;

namespace BidCalculationTool_API
{
    public class CommonVehicleBid : Bid
    {
        public CommonVehicleBid(double _basePrice) : base(_basePrice) 
        {
            minimumBasicFee = 10.0;
            maximumBasicFee = 50.0;
            specialFeeRate = 0.02;
        }
    }
}
