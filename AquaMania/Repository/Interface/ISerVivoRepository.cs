using AquaMania.Model.SerVivo;

namespace AquaMania.Repository.Interface;

public interface ISerVivoRepository
{
    Task<List<SerVivoResponse>> GetAll();
    Task<SerVivoResponse> GetById(Guid id);
    Task<SerVivoRequest> Create(SerVivoRequest servivo);
    Task<SerVivoResponse> Update(SerVivoRequest servivo);
    Task<SerVivoRequest> Delete(Guid id);
}
