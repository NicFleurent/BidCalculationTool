namespace BidCalculationTool_API.Models
{
    public class BidModel
    {
        public double BasePrice { get; set; }
        public double BasicFee { get; set; }
        public double SpecialFee { get; set; }
        public double AssociationFee { get; set; }
        public double StorageFee { get; set; }
        public double TotalPrice { get; set; }
    }
}
