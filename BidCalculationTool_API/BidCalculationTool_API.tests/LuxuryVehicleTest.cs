using BidCalculationTool_API.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationTool_API.tests
{
    public class LuxuryVehicleTest
    {
        [Theory]
        [InlineData(1800, 180)]
        [InlineData(1000000, 200)]
        public void GivenLuxuryVehicleBid_WhenCalculateBasicFee_ReturnSpecialFee(double basePrice, double basicFee)
        {
            //Arrange
            Bid bid = new LuxuryVehicleBid(basePrice);

            //Act
            double result = bid.calculateBasicFee();
            double roundedResult = Math.Round(result, 2);

            //Assert
            roundedResult.Should().Be(basicFee);
        }

        [Theory]
        [InlineData(1800, 72)]
        [InlineData(1000000, 40000)]
        public void GivenLuxuryVehicleBid_WhenCalculateSpecialFee_ReturnSpecialFee(double basePrice, double specialFee)
        {
            //Arrange
            Bid bid = new LuxuryVehicleBid(basePrice);

            //Act
            double result = bid.calculateSpecialFee();
            double roundedResult = Math.Round(result, 2);

            //Assert
            roundedResult.Should().Be(specialFee);
        }

        [Theory]
        [InlineData(1800, 15)]
        [InlineData(1000000, 20)]
        public void GivenLuxuryVehicleBid_WhenGetAssociationFee_ReturnAssociationFee(double basePrice, double associationFee)
        {
            //Arrange
            Bid bid = new LuxuryVehicleBid(basePrice);

            //Act
            double result = bid.getAssociationFee();

            //Assert
            result.Should().Be(associationFee);
        }

        [Theory]
        [InlineData(1800, 2167)]
        [InlineData(1000000, 1040320)]
        public void GivenLuxuryVehicleBid_WhenCalculateTotalPrice_ReturnTotalPrice(double basePrice, double totalPrice)
        {
            //Arrange
            Bid bid = new LuxuryVehicleBid(basePrice);

            //Act
            double result = bid.calculateTotalPrice();
            double roundedResult = Math.Round(result, 2);

            //Assert
            roundedResult.Should().Be(totalPrice);
        }

        [Theory]
        [InlineData(1800, 180, 72, 15, 100, 2167)]
        [InlineData(1000000, 200, 40000, 20, 100, 1040320)]
        public void GivenLuxuryVehicleBid_WhenGetBidModel_ReturnBidModel
            (
                double basePrice, 
                double basicFee, 
                double specialFee, 
                double associationFee, 
                double storageFee, 
                double totalPrice
            )
        {
            //Arrange
            BidModel bidModel = new BidModel
            {
                BasePrice = basePrice,
                BasicFee = basicFee,
                SpecialFee = specialFee,
                AssociationFee = associationFee,
                StorageFee = storageFee,
                TotalPrice = totalPrice,
            };
            Bid bid = new LuxuryVehicleBid(basePrice);

            //Act
            BidModel result = bid.getBidModel();

            //Assert
            result.Should().BeEquivalentTo(bidModel);
        }
    }
}
