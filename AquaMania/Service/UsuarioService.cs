using AquaMania.Model;
using AquaMania.Model.Usuario;
using AquaMania.Repository.Interface;
using AquaMania.Service.Interface;
using AquaMania.Utils;

namespace AquaMania.Service;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly SystemUtils _utils = new SystemUtils();

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Response<UsuarioResponse>> Create(UsuarioRequest usuario)
    {
        try
        {
            usuario.Senha = _utils.HashPassword(usuario.Senha);
            var response = await _usuarioRepository.Create(usuario);

            return new Response<UsuarioResponse>
            {
                Status = StatusCodes.Status201Created,
                Success = true,
                Message = "Usuário criado com sucesso",
                Data = response
            };
        }
        catch (Exception ex)
        {
            return new Response<UsuarioResponse>
            {
                Status = StatusCodes.Status500InternalServerError,
                Success = false,
                Message = $"{ex.Message}",
                Data = null,
                Error = "InternalServerError"
            };
        }
    }

    public async Task<Response<UsuarioResponse>> Delete(Guid id)
    {
        try
        {
            var response = await _usuarioRepository.Delete(id);

            if (response == null)
            {
                return new Response<UsuarioResponse>
                {
                    Status = StatusCodes.Status204NoContent,
                    Success = true,
                    Message = $"Não foi encontrado usuário com o id '{id}'",
                    Data = null
                };
            }

            return new Response<UsuarioResponse>
            {
                Status = StatusCodes.Status200OK,
                Success = true,
                Message = "Usuário deletado com sucesso!",
                Data = response
            };
        }
        catch (Exception ex)
        {

            return new Response<UsuarioResponse>
            {
                Status = StatusCodes.Status500InternalServerError,
                Success = false,
                Message = $"{ex.Message}",
                Data = null,
                Error = "InternalServerError"
            };
        }
    }

    public async Task<Response<UsuarioResponse>> GetById(Guid id)
    {
        try
        {
            var response = await _usuarioRepository.GetById(id);

            if (response == null)
            {
                return new Response<UsuarioResponse>
                {
                    Status = StatusCodes.Status204NoContent,
                    Success = true,
                    Message = $"Não foi encontrado usuário com o id '{id}'",
                    Data = null
                };
            }

            return new Response<UsuarioResponse>
            {
                Status = StatusCodes.Status200OK,
                Success = true,
                Message = "Usuário retornado com sucesso!",
                Data = response
            };
        }
        catch (Exception ex)
        {

            return new Response<UsuarioResponse>
            {
                Status = StatusCodes.Status500InternalServerError,
                Success = false,
                Message = $"{ex.Message}",
                Data = null,
                Error = "InternalServerError"
            };
        }
    }

    public async Task<Response<List<UsuarioResponse>>> GetAll()
    {
        try
        {
            var response = await _usuarioRepository.GetAll();

            if (response == null) 
            {
                return new Response<List<UsuarioResponse>>
                {
                    Status = StatusCodes.Status204NoContent,
                    Success = true,
                    Message = "Não há usuários cadastrados",
                    Data = null
                };
            }

            return new Response<List<UsuarioResponse>>
            {
                Status = StatusCodes.Status200OK,
                Success = true,
                Message = "Lista de usuários retornada com sucesso!",
                Data = response
            };
        }
        catch (Exception ex)
        {
            return new Response<List<UsuarioResponse>>
            {
                Status = StatusCodes.Status500InternalServerError,
                Success = false,
                Message = $"{ex.Message}",
                Data = null,
                Error = "InternalServerError"
            };
        }
    }

    public async Task<Response<UsuarioResponse>> Update(UsuarioRequest usuario)
    {
        try
        {
            usuario.Senha = _utils.HashPassword(usuario.Senha);
            var response = await _usuarioRepository.Update(usuario);

            if(response == null)
            {
                return new Response<UsuarioResponse>
                {
                    Status = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = $"Não foi encontrado usuário com o id '{usuario.Id}'",
                    Data = null
                };
            }

            return new Response<UsuarioResponse>
            {
                Status = StatusCodes.Status200OK,
                Success = true,
                Message = "Usuário atualizado com sucesso",
                Data = response
            };
        }
        catch (Exception ex)
        {
            return new Response<UsuarioResponse>
            {
                Status = StatusCodes.Status500InternalServerError,
                Success = false,
                Message = $"{ex.Message}",
                Data = null,
                Error = "InternalServerError"
            };
        }
    }
}
