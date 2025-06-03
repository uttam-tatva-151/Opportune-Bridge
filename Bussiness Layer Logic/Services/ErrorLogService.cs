using Core.Beans;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class ErrorLogService : IErrorLogService
{
    private readonly IErrorLogRepository _errorLogRepository;
    public ErrorLogService (IErrorLogRepository errorLogRepository)
    {
        _errorLogRepository = errorLogRepository;
    }
    public async Task SaveErrorLogAsync(ErrorLogDTO errorLog)
    {
        await _errorLogRepository.SaveErrorLogAsync(errorLog);
    }

}
