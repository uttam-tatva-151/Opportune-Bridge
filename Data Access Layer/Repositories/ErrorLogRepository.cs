using Core.Beans;
using Core.Mappers;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class ErrorLogRepository : IErrorLogRepository
{
    private  readonly AppDbContext _appDbContext;
    public ErrorLogRepository(AppDbContext appDbContext){
        _appDbContext = appDbContext;
    }
    public async Task SaveErrorLogAsync(ErrorLogDTO errorLog)
    {
        ErrorLog errorLogEntity = ErrorLogMapper.ToEntity(errorLog);
        await _appDbContext.ErrorLogs.AddAsync(errorLogEntity);
        await _appDbContext.SaveChangesAsync();
    }

}
