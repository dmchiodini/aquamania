using AquaMania.DataContext;
using AquaMania.Model.SerVivo;
using AquaMania.Repository.Interface;
using Dapper;

namespace AquaMania.Repository;

public class SerVivoRepository : ISerVivoRepository
{
    private readonly DapperContext _dbContext;

    public SerVivoRepository(DapperContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SerVivoRequest> Create(SerVivoRequest servivo)
    {
        try
        {
            var query = @$"INSERT INTO seresVivos (
                           nome,
                           nome_cientifico,
                           local,
                           tamanho,
                           expectativa_vida,
                           ph,
                           temperatura,
                           informacoes_adicionais,
                           tipo_agua_id,
                           categoria_id,
                           comportamento_id,
                           servivo_url,
                           criado_em,
                           atualizado_em
                        ) OUTPUT INSERTED.* VALUES (
                            '{servivo.Nome}',
                            '{servivo.Nome_cientifico}',
                            '{servivo.Local}',
                            '{servivo.Tamanho}',
                            '{servivo.Expectativa_vida}',
                            '{servivo.Ph}',
                            '{servivo.Temperatura}',
                            '{servivo.Informacoes_adicionais}',
                            '{servivo.Tipo_agua_id}',
                            '{servivo.Categoria_id}',
                            '{servivo.Comportamento_id}',
                            '{servivo.Servivo_url}',
                            '{servivo.Criado_em}',
                            '{servivo.Atualizado_em}'
                        )";

            using(var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QuerySingleOrDefaultAsync<SerVivoRequest>(query);
                return response;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while creating serVivo! {ex}");

        }
    }

    public async Task<SerVivoRequest> Delete(Guid id)
    {
        try
        {
            var query = @$"DELETE FROM seresVivos OUTPUT DELETED.* WHERE id = '{id}'";

            using (var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QueryFirstOrDefaultAsync<SerVivoRequest>(query);
                return response;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while deleting ser vivo! {ex}");
        }
    }

    public async Task<List<SerVivoResponse>> GetAll()
    {
        try
        {
            var query = @$"SELECT s.*, t.tipo Tipo_agua, c.nome Categoria, cp.tipo Comportamento 
                            FROM seresVivos s JOIN tiposAgua t ON t.id = s.tipo_agua_id 
                            JOIN categorias c ON c.id = s.categoria_id 
                            JOIN comportamentos cp ON cp.id = s.comportamento_id ";

            using (var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QueryAsync<SerVivoResponse>(query);
                return response.ToList();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while retrieving serVivo! {ex}");
        }

    }

    public async Task<SerVivoResponse> GetById(Guid id)
    {
        try
        {
            var query = @$"SELECT s.*, t.tipo Tipo_agua, c.nome Categoria, cp.tipo Comportamento 
                            FROM seresVivos s JOIN tiposAgua t ON t.id = s.tipo_agua_id 
                            JOIN categorias c ON c.id = s.categoria_id 
                            JOIN comportamentos cp ON cp.id = s.comportamento_id WHERE s.id = '{id}'";

            using (var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QueryFirstOrDefaultAsync<SerVivoResponse>(query);
                return response;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while retrieving serVivo! {ex}");
        }
    }

    public async Task<SerVivoResponse> Update(SerVivoRequest servivo)
    {
        try
        {
            var query = @$"UPDATE seresVivos SET 
                            nome = '{servivo.Nome}',
                            nome_cientifico = '{servivo.Nome_cientifico}',
                            local = '{servivo.Local}',
                            tamanho = '{servivo.Tamanho}',
                            expectativa_vida = '{servivo.Expectativa_vida}',
                            ph = '{servivo.Ph}',
                            temperatura = '{servivo.Temperatura}',
                            informacoes_adicionais = '{servivo.Informacoes_adicionais}',
                            tipo_agua_id = '{servivo.Tipo_agua_id}',
                            categoria_id = '{servivo.Categoria_id}',
                            comportamento_id = '{servivo.Comportamento_id}',
                            servivo_url = '{servivo.Servivo_url}',
                            atualizado_em = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                            OUTPUT INSERTED.* WHERE id = '{servivo.Id}'
                          ";

            using(var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QueryFirstOrDefaultAsync<SerVivoResponse>(query);
                return response;
            }

        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while updating serVivo! {ex}");
        }
    }
}
