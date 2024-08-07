using AquaMania.Model;
using AquaMania.Model.SerVivo;
using AquaMania.Repository.Interface;
using AquaMania.Service.Interface;

namespace AquaMania.Service
{
    public class SerVivoService : ISerVivoService
    {
        private readonly ISerVivoRepository _serVivoRepository;

        public SerVivoService(ISerVivoRepository serVivoRepository)
        {
            _serVivoRepository = serVivoRepository;
        }

        public async Task<Response<SerVivoRequest>> Create(SerVivoRequest servivo)
        {
            try
            {
                var response = await _serVivoRepository.Create(servivo);

                return new Response<SerVivoRequest>
                {
                    Status = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Ser vivo criado com sucesso",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new Response<SerVivoRequest>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ex.Message}",
                    Data = null,
                    Error = "InternalServerError"
                };
            }
        }

        public async Task<Response<SerVivoRequest>> Delete(Guid id)
        {
            try
            {
                var response = await _serVivoRepository.Delete(id);

                if (response == null)
                {
                    return new Response<SerVivoRequest>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = $"Não há ser vivo com o id '{id}'",
                        Data = null
                    };
                }

                return new Response<SerVivoRequest>
                {
                    Status = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Ser vivo deletado com sucesso",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new Response<SerVivoRequest>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ex.Message}",
                    Data = null,
                    Error = "InternalServerError"
                };
            }
        }

        public async Task<Response<List<SerVivoResponse>>> GetAll()
        {
            try
            {
                var response = await _serVivoRepository.GetAll();

                if (response == null)
                {
                    return new Response<List<SerVivoResponse>>
                    {
                        Status = StatusCodes.Status200OK,
                        Success = true,
                        Message = "Não há seres vivos cadastrados",
                        Data = null
                    };
                }

                return new Response<List<SerVivoResponse>>
                {
                    Status = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Seres vivos retornados com sucesso",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new Response<List<SerVivoResponse>>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ex.Message}",
                    Data = null,
                    Error = "InternalServerError"
                };
            }
        }

        public async Task<Response<SerVivoResponse>> GetById(Guid id)
        {
            try
            {
                var response = await _serVivoRepository.GetById(id);

                if (response == null)
                {
                    return new Response<SerVivoResponse>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = $"Não há ser vivo cadastrado com o id '{id}'",
                        Data = null
                    };
                }

                return new Response<SerVivoResponse>
                {
                    Status = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Ser vivo retornado com sucesso",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new Response<SerVivoResponse>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ex.Message}",
                    Data = null,
                    Error = "InternalServerError"
                };
            }
        }

        public async Task<Response<SerVivoResponse>> Update(SerVivoRequest servivo)
        {
            try
            {
                var response = await _serVivoRepository.Update(servivo);

                if (response == null)
                {
                    return new Response<SerVivoResponse>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = $"Não há ser vivo cadastrado com o id '{servivo.Id}'",
                        Data = null
                    };
                }
                
                var getSerVivo = await _serVivoRepository.GetById(response.Id);

                return new Response<SerVivoResponse>
                {
                    Status = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Ser vivo atualizado com sucesso",
                    Data = getSerVivo
                };
            }
            catch (Exception ex)
            {
                return new Response<SerVivoResponse>
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
}
