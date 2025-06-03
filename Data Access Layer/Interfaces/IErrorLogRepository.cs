using Core.Beans;

namespace Repositories.Interfaces;

public interface IErrorLogRepository
{
    public Task SaveErrorLogAsync(ErrorLogDTO errorLog);

}
