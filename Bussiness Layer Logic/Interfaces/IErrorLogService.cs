using Core.Beans;

namespace Services.Interfaces;

public interface IErrorLogService
{
    Task SaveErrorLogAsync(ErrorLogDTO errorLog);
}
