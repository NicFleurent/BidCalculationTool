using BidCalculationTool_API.Models;

namespace BidCalculationTool_API
{
    public abstract class Bid
    {
        private static readonly double[] ASSOCIATION_MINIMUM_PRICES = { 1.0, 500.0, 1000.0, 3000.0 };
        private static readonly double[] ASSOCIATION_MINIMUM_FEES = { 5.0, 10.0, 15.0, 20.0 };

        private const double BASIC_FEE_RATE = 0.1;
        private const double STORAGE_FEE = 100.0;

        protected double basePrice;
        protected double minimumBasicFee;
        protected double maximumBasicFee;
        protected double specialFeeRate;

        public Bid(double _basePrice)
        {
            setBasePrice(_basePrice);
        }

        /* To respect the OCP principle, I feel like I should make the methods
         * below abstract and define them in the children classes. That code for
         * the both of them would be identical and I assume that they would be pretty
         * similar I we added a new type of bid. For that reason, I prefered not to
         * copy paste code and leave it here.
         */
        public void setBasePrice(double _basePrice)
        {
            if(_basePrice < 1)
                this.basePrice = 1;
            else
                this.basePrice = _basePrice;
        }

        public BidModel getBidModel()
        {
            BidModel bidModel = new BidModel
            {
                BasePrice = basePrice,
                BasicFee = calculateBasicFee(),
                SpecialFee = calculateSpecialFee(),
                AssociationFee = getAssociationFee(),
                StorageFee = STORAGE_FEE,
                TotalPrice = calculateTotalPrice(),
            };

            return bidModel;
        }

        public double calculateTotalPrice()
        {
            double basicFee = calculateBasicFee();
            double specialFee = calculateSpecialFee();
            double associationFee = getAssociationFee();

            double totalPrice = basePrice + basicFee + specialFee + associationFee + STORAGE_FEE;

            return totalPrice;
        }

        public double calculateBasicFee()
        {
            double basicFee = basePrice * BASIC_FEE_RATE;

            if (basicFee < minimumBasicFee)
                basicFee = minimumBasicFee;
            else if (basicFee > maximumBasicFee)
                basicFee = maximumBasicFee;

            return basicFee;
        }

        public double calculateSpecialFee()
        {
            double specialFee = basePrice * specialFeeRate;
            return specialFee;
        }

        public double getAssociationFee()
        {
            double associationFee = ASSOCIATION_MINIMUM_FEES[0];
            for(int i = 0; i < ASSOCIATION_MINIMUM_PRICES.Length; i++)
            {
                if(basePrice > ASSOCIATION_MINIMUM_PRICES[i])
                {
                    associationFee = ASSOCIATION_MINIMUM_FEES[i];
                }
            }
            return associationFee;
        }
    }
}
