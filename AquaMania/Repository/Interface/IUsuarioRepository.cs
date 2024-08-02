using AquaMania.Model.Usuario;

namespace AquaMania.Repository.Interface;

public interface IUsuarioRepository
{
    Task<List<UsuarioResponse>> GetAll();
    Task<UsuarioResponse> GetById(Guid id); 
    Task<UsuarioResponse> Create(UsuarioRequest usuario);
    Task<UsuarioResponse> Update(UsuarioRequest usuario);
    Task<UsuarioResponse> Delete(Guid id);  
}
