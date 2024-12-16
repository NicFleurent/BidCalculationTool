using System.Xml.Linq;

namespace BidCalculationTool_API
{
    public class CommonVehicleBid : Bid
    {
        private const double MINIMUM_BASIC_FEE = 10.0;
        private const double MAXIMUM_BASIC_FEE = 50.0;
        private const double SPECIAL_FEE_RATE = 0.02;
        public CommonVehicleBid(double _basePrice) : base(_basePrice) 
        { 
            
        }

        public override double calculateBasicFee()
        {
            double basicFee = base.calculateBasicFee();

            if(basicFee < MINIMUM_BASIC_FEE)
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
