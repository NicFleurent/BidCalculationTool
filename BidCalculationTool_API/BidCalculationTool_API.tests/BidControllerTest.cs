using BidCalculationTool_API.Controllers;
using BidCalculationTool_API.Models;
using BidCalculationTool_API.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculationTool_API.tests
{
    public class BidControllerTest
    {
        private readonly BidController _controller;

        public BidControllerTest()
        {
            _controller = new BidController();
        }

        [Fact]
        public void GivenModelStateIsInvalid_WhenPost_ReturnBadRequest()
        {
            //Arrange
            BidRequest request = new BidRequest();

            //I don't really like hardcoding the errors of the model state but I didn't find a way to test it otherwise
            //https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-9.0
            _controller.ModelState.AddModelError("BasePrice", "The base price must be greater than or equal to 1.");
            _controller.ModelState.AddModelError("BidType", "The bid type is required.");

            //Act
            var result = _controller.Post(request);

            //Asert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void GivenMissingBasePrice_WhenPost_ReturnBadRequest()
        {
            //Arrange
            BidRequest request = new BidRequest
            {
                BidType = "luxury"
            };

            //I don't really like hardcoding the errors of the model state but I didn't find a way to test it otherwise
            //https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-9.0
            _controller.ModelState.AddModelError("BasePrice", "The base price must be greater than or equal to 1.");

            //Act
            var result = _controller.Post(request);

            //Asert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void GivenInvalidBasePrice_WhenPost_ReturnBadRequest()
        {
            //Arrange
            BidRequest request = new BidRequest
            {
                BasePrice = 0.5,
                BidType = "luxury"
            };

            //I don't really like hardcoding the errors of the model state but I didn't find a way to test it otherwise
            //https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-9.0
            _controller.ModelState.AddModelError("BasePrice", "The base price must be greater than or equal to 1.");

            //Act
            var result = _controller.Post(request);

            //Asert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void GivenMissingBidType_WhenPost_ReturnBadRequest()
        {
            //Arrange
            BidRequest request = new BidRequest
            {
                BasePrice = 1100
            };

            //I don't really like hardcoding the errors of the model state but I didn't find a way to test it otherwise
            //https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-9.0
            _controller.ModelState.AddModelError("BidType", "The bid type is required.");

            //Act
            var result = _controller.Post(request);

            //Asert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void GivenInvalidBidType_WhenPost_ReturnBadRequest()
        {
            //Arrange
            BidRequest request = new BidRequest
            {
                BasePrice = 1100,
                BidType = "invalid"
            };

            //I don't really like hardcoding the errors of the model state but I didn't find a way to test it otherwise
            //https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-9.0
            _controller.ModelState.AddModelError("BidType", "The bid type must be 'common' or 'luxury'.");

            //Act
            var result = _controller.Post(request);

            //Asert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Theory]
        [InlineData(398, "common", 39.800000000000004, 7.96, 5, 100, 550.76)]
        [InlineData(501, "common", 50, 10.02, 10, 100, 671.02)]
        [InlineData(57, "common", 10, 1.1400000000000001, 5, 100, 173.14)]
        [InlineData(1100, "common", 50, 22, 15, 100, 1287)]
        [InlineData(1800, "luxury", 180, 72, 15, 100, 2167)]
        [InlineData(1000000, "luxury", 200, 40000, 20, 100, 1040320)]
        public void GivenValidRequest_WhenPost_ReturnOKWithBidModel
            (
                double basePrice,
                string bidType,
                double basicFee,
                double specialFee,
                double associationFee,
                double storageFee,
                double totalPrice
            )
        {
            //Arrange
            BidRequest request = new BidRequest
            {
                BasePrice = basePrice,
                BidType = bidType
            };

            BidModel bidModel = new BidModel
            {
                BasePrice = basePrice,
                BasicFee = basicFee,
                SpecialFee = specialFee,
                AssociationFee = associationFee,
                StorageFee = storageFee,
                TotalPrice = totalPrice,
            };

            //Act
            var result = _controller.Post(request);

            //Asert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<BidModel>(okResult.Value);
            response.Should().BeEquivalentTo(bidModel);
        }
    }
}
