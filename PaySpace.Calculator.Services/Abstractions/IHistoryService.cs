﻿using PaySpace.Calculator.Data.Dtos;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions;

public interface IHistoryService
{
    Task AddAndSaveAsync(CalculatorHistory history, CancellationToken cancellationToken);

    Task<List<CalculatorHistory>> GetHistoryAsync(PaginationDto pagination, CancellationToken cancellationToken);

}