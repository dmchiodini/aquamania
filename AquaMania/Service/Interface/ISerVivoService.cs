using AquaMania.Model;
using AquaMania.Model.SerVivo;

namespace AquaMania.Service.Interface
{
    public interface ISerVivoService
    {
        Task<Response<List<SerVivoResponse>>> GetAll();
        Task<Response<SerVivoResponse>> GetById(Guid id);
        Task<Response<SerVivoRequest>> Create(SerVivoRequest servivo);
        Task<Response<SerVivoResponse>> Update(SerVivoRequest servivo);
        Task<Response<SerVivoRequest>> Delete(Guid id);
    }
}
