using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaySpace.Calculator.API.Controllers;
using PaySpace.Calculator.API.Requests;
using PaySpace.Calculator.Business.Abstractions;
using PaySpace.Calculator.Business.Dtos;
using PaySpace.Calculator.Business.Exceptions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.API.Tests;

public class CalculatorControllerTests
{
    [Fact]
    public async Task CalculateAsync_WhenModelIsNotValid_ShouldReturnBadRequest()
    {
        // Arrange
        CalculatorController controller = CreateCalculatorController();

        controller.ModelState.AddModelError("Income", "Income is required");

        CalculateTaxRequest taxRequest = new("1234", -1);

        // Act
        ActionResult<CalculateTaxResultDto> result = await controller.CalculateAsync(taxRequest, CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CalculatorAsync_WhenCalculatorTypeIsNotFound_ShouldReturnBadRequest()
    {
        Mock<IPostalCodeService> postalCodeService = new();

        CalculatorType? calculatorType = null;

        CalculateTaxRequest taxRequest = new("1234", 1000);

        postalCodeService.Setup(s => s.GetCalculatorTypeByPostalCodeAsync(taxRequest.PostalCode, CancellationToken.None))
            .ReturnsAsync(calculatorType);

        // Arrange
        CalculatorController controller = CreateCalculatorController(
            postalCodeService: postalCodeService);

        ActionResult<CalculateTaxResultDto> result = await controller.CalculateAsync(taxRequest, CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CalculateAsync_WhenCalculationIsSuccessful_ShouldReturnOk()
    {
        Mock<IPostalCodeService> postalCodeService = new();

        CalculatorType calculatorType = CalculatorType.FlatValue;

        postalCodeService.Setup(s => s.GetCalculatorTypeByPostalCodeAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(calculatorType);

        Mock<ITaxCalculationService> taxCalculationService = new();

        CalculateTaxRequest taxRequest = new("1234", 1000);

        CalculateTaxDto calculateTaxDto = new(taxRequest.PostalCode, calculatorType, taxRequest.Income);

        CalculateTaxResultDto calculateTaxResultDto = new(calculatorType, 10);

        taxCalculationService.Setup(s => s.CalculateTaxAsync(calculateTaxDto, CancellationToken.None))
            .ReturnsAsync(calculateTaxResultDto);

        CalculatorController controller = CreateCalculatorController(
            postalCodeService: postalCodeService,
            taxCalculationService: taxCalculationService);


        ActionResult<CalculateTaxResultDto> result = await controller.CalculateAsync(taxRequest, CancellationToken.None);

        Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact]
    public  async Task CalculateAsync_WhenCalculationFails_ShouldReturn500()
    {
        Mock<IPostalCodeService> postalCodeService = new();

        CalculatorType calculatorType = CalculatorType.FlatValue;

        postalCodeService.Setup(s => s.GetCalculatorTypeByPostalCodeAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(calculatorType);

        Mock<ITaxCalculationService> taxCalculationService = new();

        CalculateTaxRequest taxRequest = new("1234", 1000);

        CalculateTaxDto calculateTaxDto = new(taxRequest.PostalCode, calculatorType, taxRequest.Income);

        taxCalculationService.Setup(s => s.CalculateTaxAsync(calculateTaxDto, CancellationToken.None))
            .ThrowsAsync(new DomainException("An error occurred"));

        CalculatorController controller = CreateCalculatorController(
            postalCodeService: postalCodeService,
            taxCalculationService: taxCalculationService);

        ActionResult<CalculateTaxResultDto> result = await controller.CalculateAsync(taxRequest, CancellationToken.None);

        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, (result.Result as ObjectResult)?.StatusCode);
    }

    private static CalculatorController CreateCalculatorController(
        Mock<IHistoryService>? historyService = null,
        Mock<ITaxCalculationService>? taxCalculationService = null,
        Mock<IPostalCodeService>? postalCodeService = null
    )
    {
        historyService ??= new();
        taxCalculationService ??= new();
        postalCodeService ??= new();

        Logger<CalculatorController> logger = new(new LoggerFactory());

        CalculatorController? controller = new(
            logger,
            historyService.Object,
            taxCalculationService.Object,
            postalCodeService.Object);

        return controller;
    }
}