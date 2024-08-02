using Microsoft.Data.SqlClient;
using System.Data;

namespace AquaMania.DataContext;

public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConenction()
            => new SqlConnection(_configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value);
}
