using AquaMania.Model;
using AquaMania.Model.Usuario;

namespace AquaMania.Service.Interface;

public interface IUsuarioService
{
    Task<Response<List<UsuarioResponse>>> GetAll();
    Task<Response<UsuarioResponse>> GetById(Guid id);
    Task<Response<UsuarioResponse>> Create(UsuarioRequest usuario);
    Task<Response<UsuarioResponse>> Update(UsuarioRequest usuario);
    Task<Response<UsuarioResponse>> Delete(Guid id);
}
