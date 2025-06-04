using Core.Beans;

namespace Repositories.Interfaces;

public interface IErrorLogRepository
{
    public Task UpsertErrorLogAsync(ErrorLogDTO errorLog);

}
