using Anfx.Profuturo.Domain.Entities;
using Anfx.Profuturo.Sofom.Application.Common.Interfaces;
using Anfx.Profuturo.Sofom.Infrastructure.Persitence.Extensions;
using Anfx.Profuturo.Sofom.Infrastructure.Persitence.Interfases;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence;

public partial class ApplicationDbContextProcedures : IApplicationDbContextProcedures
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextProcedures(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<int> usp_ProcesaSaldoInsolutoAsync(int? idContrato, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new[]
        {
                new SqlParameter
                {
                    ParameterName = "IdContrato",
                    Value = idContrato ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
        var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[usp_ProcesaSaldoInsoluto] @IdContrato = @IdContrato", sqlParameters, cancellationToken);

        returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }
    public virtual async Task<List<usp_RC_IniciaReestructuracionIMSSResult>> usp_RC_IniciaReestructuracionIMSSAsync(int? idContrato, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new[]
        {
                new SqlParameter
                {
                    ParameterName = "IdContrato",
                    Value = idContrato ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
        var _ = await _context.SqlQueryAsync<usp_RC_IniciaReestructuracionIMSSResult>("EXEC @returnValue = [dbo].[usp_RC_IniciaReestructuracionIMSS] @IdContrato = @IdContrato", sqlParameters, cancellationToken);

        returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }

    public virtual async Task<List<usp_SaldoLiquidacionResult>> usp_SaldoLiquidacionAsync(int? idContrato, DateTime? fecha, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new[]
        {
                new SqlParameter
                {
                    ParameterName = "IdContrato",
                    Value = idContrato ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "Fecha",
                    Value = fecha ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.DateTime,
                },
                parameterreturnValue,
            };
        var _ = await _context.SqlQueryAsync<usp_SaldoLiquidacionResult>("EXEC @returnValue = [dbo].[usp_SaldoLiquidacion] @IdContrato = @IdContrato, @Fecha = @Fecha", sqlParameters, cancellationToken);

        returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }
}
