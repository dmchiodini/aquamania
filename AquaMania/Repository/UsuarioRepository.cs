using AquaMania.DataContext;
using AquaMania.Model.Usuario;
using AquaMania.Repository.Interface;
using Dapper;

namespace AquaMania.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DapperContext _dbContext;

    public UsuarioRepository(DapperContext context)
    {
        _dbContext = context;
    }

    public async Task<UsuarioResponse> Create(UsuarioRequest usuario)
    {
        try
        {
            var query = @$"INSERT INTO usuarios (
                 nome
                ,email
                ,senha
                ,data_nascimento
                ,cidade
                ,estado
                ,criado_em
                ,atualizado_em 
            ) OUTPUT INSERTED.* VALUES (
                 '{usuario.Nome}'                
                ,'{usuario.Email}'
                ,'{usuario.Senha}'
                ,'{usuario.Data_nascimento}'
                ,'{usuario.Cidade}'
                ,'{usuario.Estado}'
                ,'{usuario.Criado_em}'
                ,'{usuario.Atualizado_em}'
            )";

            using(var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QuerySingleOrDefaultAsync(query);

                response = new UsuarioResponse
                {
                    Id = response.id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Data_nascimento = usuario.Data_nascimento,
                    Cidade = usuario.Cidade,
                    Estado = usuario.Estado,
                };
                return response;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while creating user! {ex}");
        }

    }

    public async Task<UsuarioResponse> Delete(Guid id)
    {
        try
        {
            var query = @$"DELETE FROM usuarios OUTPUT DELETED.* WHERE id = '{id}'";

            using (var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QueryFirstOrDefaultAsync<UsuarioResponse>(query);
                return response;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while deleting users! {ex}");
        }
    }

    public async Task<List<UsuarioResponse>> GetAll()
    {
        try
        {
            var query = @"SELECT * FROM usuarios";

            using (var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QueryAsync<UsuarioResponse>(query);
                return response.ToList();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while retrieving users! {ex}");
        }
       
    }

    public async Task<UsuarioResponse> GetById(Guid id)
    {
        try
        {
            var query = @$"SELECT * FROM usuarios WHERE id = '{id}'";
            
            using(var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QueryFirstOrDefaultAsync<UsuarioResponse>(query);
                return response;
            }
        }
        catch (Exception ex)
        {

            throw new Exception($"Exception while retrieving user! {ex}");
        }
    }

    public async Task<UsuarioResponse> Update(UsuarioRequest usuario)
    {

        try
        {
            var query = @$"UPDATE usuarios SET
                 nome =  '{usuario.Nome}'      
                ,email = '{usuario.Email}'
                ,senha = '{usuario.Senha}'
                ,data_nascimento = '{usuario.Data_nascimento}'
                ,cidade = '{usuario.Cidade}'
                ,estado ='{usuario.Estado}'
                ,atualizado_em = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
             OUTPUT INSERTED.* WHERE id = '{usuario.Id}'";

            using (var connection = _dbContext.CreateConenction())
            {
                var response = await connection.QuerySingleOrDefaultAsync(query);

                response = new UsuarioResponse
                {
                    Id = response.id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Data_nascimento = usuario.Data_nascimento,
                    Cidade = usuario.Cidade,
                    Estado = usuario.Estado,
                };

                return response;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception while creating user! {ex}");
        }
    }
}
