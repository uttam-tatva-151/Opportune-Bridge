using Core.Beans;
using Npgsql;

namespace WebApi.Extensions;

public static class NpgsqlTypeRegistrationExtensions
{
     /// <summary>
    /// Registers all PostgreSQL composite types needed for the application.
    /// </summary>
    public static NpgsqlDataSourceBuilder RegisterAppComposites(this NpgsqlDataSourceBuilder builder)
    {
        builder.MapComposite<ErrorLogDTO>("error_log_dto");
        // Add more composite mappings as needed.
        return builder;
    }
}
