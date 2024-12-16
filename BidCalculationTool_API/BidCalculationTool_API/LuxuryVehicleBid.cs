namespace BidCalculationTool_API
{
    public class LuxuryVehicleBid : Bid
    {
        public LuxuryVehicleBid(double _basePrice) : base(_basePrice)
        {
            minimumBasicFee = 25.0;
            maximumBasicFee = 200.0;
            specialFeeRate = 0.04;
        }
    }
}
