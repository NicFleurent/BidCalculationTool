using BidCalculationTool_API.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationTool_API.tests
{
    public class CommonVehicleTest
    {
        [Theory]
        [InlineData(398, 39.80)]
        [InlineData(501, 50)]
        [InlineData(57, 10)]
        [InlineData(1100, 50)]
        public void GivenCommonVehicleBid_WhenCalculateBasicFee_ReturnSpecialFee(double basePrice, double basicFee)
        {
            //Arrange
            Bid bid = new CommonVehicleBid(basePrice);

            //Act
            double result = bid.calculateBasicFee();
            double roundedResult = Math.Round(result, 2);

            //Assert
            roundedResult.Should().Be(basicFee);
        }

        [Theory]
        [InlineData(398, 7.96)]
        [InlineData(501, 10.02)]
        [InlineData(57, 1.14)]
        [InlineData(1100, 22)]
        public void GivenCommonVehicleBid_WhenCalculateSpecialFee_ReturnSpecialFee(double basePrice, double specialFee)
        {
            //Arrange
            Bid bid = new CommonVehicleBid(basePrice);

            //Act
            double result = bid.calculateSpecialFee();
            double roundedResult = Math.Round(result, 2);

            //Assert
            roundedResult.Should().Be(specialFee);
        }

        [Theory]
        [InlineData(398, 5)]
        [InlineData(501, 10)]
        [InlineData(57, 5)]
        [InlineData(1100, 15)]
        public void GivenCommonVehicleBid_WhenGetAssociationFee_ReturnAssociationFee(double basePrice, double associationFee)
        {
            //Arrange
            Bid bid = new CommonVehicleBid(basePrice);

            //Act
            double result = bid.getAssociationFee();

            //Assert
            result.Should().Be(associationFee);
        }

        [Theory]
        [InlineData(398, 550.76)]
        [InlineData(501, 671.02)]
        [InlineData(57, 173.14)]
        [InlineData(1100, 1287)]
        public void GivenCommonVehicleBid_WhenCalculateTotalPrice_ReturnTotalPrice(double basePrice, double totalPrice)
        {
            //Arrange
            Bid bid = new CommonVehicleBid(basePrice);

            //Act
            double result = bid.calculateTotalPrice();
            double roundedResult = Math.Round(result, 2);

            //Assert
            roundedResult.Should().Be(totalPrice);
        }

        [Theory]
        [InlineData(398, 39.800000000000004, 7.96, 5, 100, 550.76)]
        [InlineData(501, 50, 10.02, 10, 100, 671.02)]
        [InlineData(57, 10, 1.1400000000000001, 5, 100, 173.14)]
        [InlineData(1100, 50, 22, 15, 100, 1287)]
        public void GivenCommonVehicleBid_WhenGetBidModel_ReturnBidModel
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
            Bid bid = new CommonVehicleBid(basePrice);

            //Act
            BidModel result = bid.getBidModel();

            //Assert
            result.Should().BeEquivalentTo(bidModel);
        }
    }
}
