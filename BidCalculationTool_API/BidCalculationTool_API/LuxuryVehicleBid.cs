namespace BidCalculationTool_API
{
    public class LuxuryVehicleBid : Bid
    {
        private const double MINIMUM_BASIC_FEE = 25.0;
        private const double MAXIMUM_BASIC_FEE = 200.0;
        private const double SPECIAL_FEE_RATE = 0.04;
        public LuxuryVehicleBid(double _basePrice) : base(_basePrice)
        {

        }

        public override double calculateBasicFee()
        {
            double basicFee = base.calculateBasicFee();

            if (basicFee < MINIMUM_BASIC_FEE)
                basicFee = MINIMUM_BASIC_FEE;
            else if (basicFee > MAXIMUM_BASIC_FEE)
                basicFee = MAXIMUM_BASIC_FEE;

            return basicFee;
        }
        public override double calculateSpecialFee()
        {
            double specialFee = basePrice * SPECIAL_FEE_RATE;
            return specialFee;
        }
    }
}
