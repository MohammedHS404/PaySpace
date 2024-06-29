using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaySpace.Calculator.API.Controllers;
using PaySpace.Calculator.API.Requests;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.API.Tests;

public class CalculatorControllerTests
{
    [Fact]
    public async Task CalculateAsync_WhenModelIsNotValid_ShouldReturnBadRequest()
    {
        // Arrange
        CalculatorController controller = CreateCalculatorController();

        controller.ModelState.AddModelError("Income", "Income is required");

        CalculateRequest request = new("1234", -1);

        // Act
        ActionResult<CalculateResult> result = await controller.CalculateAsync(request, CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CalculatorAsync_WhenCalculatorTypeIsNotFound_ShouldReturnBadRequest()
    {
        Mock<IPostalCodeService> postalCodeService = new();

        CalculatorType? calculatorType = null;

        CalculateRequest request = new("1234", 1000);

        postalCodeService.Setup(s => s.CalculatorTypeAsync(request.PostalCode, CancellationToken.None))
            .ReturnsAsync(calculatorType);

        // Arrange
        CalculatorController controller = CreateCalculatorController(
            postalCodeService: postalCodeService);

        ActionResult<CalculateResult> result = await controller.CalculateAsync(request, CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CalculateAsync_WhenCalculationIsSuccessful_ShouldReturnOk()
    {
        Mock<IPostalCodeService> postalCodeService = new();

        CalculatorType calculatorType = CalculatorType.FlatValue;

        postalCodeService.Setup(s => s.CalculatorTypeAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(calculatorType);

        Mock<ITaxCalculationService> taxCalculationService = new();

        CalculateRequest request = new("1234", 1000);

        CalculateTaxDto calculateTaxDto = new(request.PostalCode, calculatorType, request.Income);

        CalculateResultDto calculateResultDto = new(calculatorType, 10);

        taxCalculationService.Setup(s => s.CalculateTaxAsync(calculateTaxDto, CancellationToken.None))
            .ReturnsAsync(calculateResultDto);

        CalculatorController controller = CreateCalculatorController(
            postalCodeService: postalCodeService,
            taxCalculationService: taxCalculationService);


        ActionResult<CalculateResult> result = await controller.CalculateAsync(request, CancellationToken.None);

        Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact]
    public  async Task CalculateAsync_WhenCalculationFails_ShouldReturn500()
    {
        Mock<IPostalCodeService> postalCodeService = new();

        CalculatorType calculatorType = CalculatorType.FlatValue;

        postalCodeService.Setup(s => s.CalculatorTypeAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(calculatorType);

        Mock<ITaxCalculationService> taxCalculationService = new();

        CalculateRequest request = new("1234", 1000);

        CalculateTaxDto calculateTaxDto = new(request.PostalCode, calculatorType, request.Income);

        taxCalculationService.Setup(s => s.CalculateTaxAsync(calculateTaxDto, CancellationToken.None))
            .ThrowsAsync(new DomainException("An error occurred"));

        CalculatorController controller = CreateCalculatorController(
            postalCodeService: postalCodeService,
            taxCalculationService: taxCalculationService);

        ActionResult<CalculateResult> result = await controller.CalculateAsync(request, CancellationToken.None);

        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, (result.Result as ObjectResult)?.StatusCode);
    }

    private static CalculatorController CreateCalculatorController(
        Mock<IHistoryService>? historyService = null,
        Mock<IMapper>? mapper = null,
        Mock<ITaxCalculationService>? taxCalculationService = null,
        Mock<IPostalCodeService>? postalCodeService = null
    )
    {
        historyService ??= new();
        mapper ??= new();
        taxCalculationService ??= new();
        postalCodeService ??= new();

        Logger<CalculatorController> logger = new(new LoggerFactory());

        CalculatorController? controller = new(
            logger,
            historyService.Object,
            mapper.Object,
            taxCalculationService.Object,
            postalCodeService.Object);

        return controller;
    }
}