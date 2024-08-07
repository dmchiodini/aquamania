using AquaMania.Model;
using AquaMania.Model.SerVivo;
using AquaMania.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AquaMania.Controllers;

/// <summary>
/// Controlador responsável por lidar com operações relacionadas aos seres vivos.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
[Consumes("application/json")]
[Produces("application/json")]
public class SerVivoController : ControllerBase
{
   private readonly ISerVivoService _serVivoService;

    public SerVivoController(ISerVivoService service)
    {
        _serVivoService = service;
    }


    /// <summary>
    /// Responsável por criar ser vivo.
    /// </summary>
    /// <returns>O ser vivo cadastrado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<SerVivoRequest>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<SerVivoRequest>>> Create(SerVivoRequest servivo)
    {
        var response = await _serVivoService.Create(servivo);
        return Ok(response);
    }

    /// <summary>
    /// Responsável por buscar seres vivos.
    /// </summary>
    /// <returns>Os seres vivos cadastrados no sistema</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<List<SerVivoResponse>>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<SerVivoResponse>>>> GetAll()
    {
        var response = await _serVivoService.GetAll();
        return Ok(response);
    }


    /// <summary>
    /// Responsável por buscar ser vivo através do id.
    /// </summary>
    /// <returns>O ser vivo cadastrados no sistema</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<SerVivoResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<SerVivoResponse>>> GetById(Guid id)
    {
        var response = await _serVivoService.GetById(id);
        return Ok(response);
    }

    /// <summary>
    /// Responsável por deletar ser vivo através do id.
    /// </summary>
    /// <returns>O ser vivo deletado do sistema</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<SerVivoRequest>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<SerVivoRequest>>> Delete(Guid id)
    {
        var response = await _serVivoService.Delete(id);
        return Ok(response);
    }

    /// <summary>
    /// Responsável por atualizar ser vivo.
    /// </summary>
    /// <returns>O ser vivo atualizado no sistema</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<SerVivoResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<SerVivoResponse>>> Update(SerVivoRequest servivo)
    {
        var response = await _serVivoService.Update(servivo);
        return Ok(response);
    }
}
