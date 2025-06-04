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
         if (errorLog == null)
        throw new ArgumentNullException(nameof(errorLog));
    if (string.IsNullOrWhiteSpace(errorLog.ExceptionType) ||
        string.IsNullOrWhiteSpace(errorLog.StatusCode))
    {
        throw new ArgumentException("ExceptionType and StatusCode are required.");
    }
    await _errorLogRepository.UpsertErrorLogAsync(errorLog);
}

}
