using AquaMania.Model;
using AquaMania.Model.Usuario;
using AquaMania.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace AquaMania.Controllers;

/// <summary>
/// Controlador responsável por lidar com operações relacionadas aos usuários.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }


    /// <summary>
    /// Responsável por buscar usuários.
    /// </summary>
    /// <returns>Os usuários cadastrados no sistema</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<List<UsuarioResponse>>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<UsuarioResponse>>>> GetAll()
    {
        var response = await _usuarioService.GetAll();
        return Ok(response);
    }

    /// <summary>
    /// Responsável por buscar usuário através do id.
    /// </summary>
    /// <returns>O usuários cadastrado com o id informado.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<UsuarioResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<UsuarioResponse>>> GetById(Guid id)
    {
        var response = await _usuarioService.GetById(id);
        return Ok(response);
    }


    /// <summary>
    /// Responsável por criar usuário.
    /// </summary>
    /// <returns>O usuários cadastrado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<UsuarioResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<UsuarioResponse>>> Create(UsuarioRequest usuario)
    {
        var response = await _usuarioService.Create(usuario);
        return Ok(response);
    }

    /// <summary>
    /// Responsável por atualizar usuário.
    /// </summary>
    /// <returns>O usuários atualizado.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<UsuarioResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<UsuarioResponse>>> Update(UsuarioRequest usuario)
    {
        var response = await _usuarioService.Update(usuario);
        return Ok(response);
    }

    /// <summary>
    /// Responsável por deletar usuário.
    /// </summary>
    /// <returns>O usuários deletado.</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<UsuarioResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<UsuarioResponse>>> Delete(Guid id)
    {
        var response = await _usuarioService.Delete(id);
        return Ok(response);
    }
}