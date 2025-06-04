using System.Data.Common;
using Core.Beans;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class ErrorLogRepository : IErrorLogRepository
{
    private readonly AppDbContext _appDbContext;
    public ErrorLogRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task UpsertErrorLogAsync(ErrorLogDTO errorLog)
    {
        DbConnection connection = _appDbContext.Database.GetDbConnection();
        if (connection.State != System.Data.ConnectionState.Open)
            await connection.OpenAsync();
        NpgsqlConnection npgsqlConn = (NpgsqlConnection)connection;
        using NpgsqlCommand cmd = new("SELECT upsert_error_log(@in_log)", npgsqlConn);
        cmd.Parameters.AddWithValue("in_log", errorLog);

        await cmd.ExecuteNonQueryAsync();
    }

}
