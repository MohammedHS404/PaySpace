using PaySpace.Calculator.Business.Models;

namespace PaySpace.Calculator.API.Responses;

public class CalculateResultResponse
{
    public string Calculator { get; set; }
    
    public decimal Tax { get; set; }
    
    public CalculateResultResponse(string calculator, decimal tax)
    {
        Calculator = calculator;
        Tax = tax;
    }
    
    public CalculateResultResponse(CalculateTaxResultDto result)
    {
        Calculator = result.Calculator.ToString();
        Tax = result.Tax;
    }
}