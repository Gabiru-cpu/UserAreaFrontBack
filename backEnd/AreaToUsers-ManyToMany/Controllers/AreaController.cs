using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AreaApi.Infrastructure.Data.Context;
using AreaApi.Domain.Models;
using AreaApi.Infrastructure.Data.Repositories;
using AreaApi.Domain.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace AreaApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _areaService;

        public AreaController(AreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet("list-areas")]
        public async Task<ActionResult> ListAreas()
        {
            try
            {
                List<Area> list = await _areaService.ListAreas();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-meus-Areas")]
        public async Task<ActionResult> ListMeusAreas()
        {
            try
            {
                List<Area> list = await _areaService.ListMeusAreas();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-area")]
        public async Task<ActionResult> GetArea([FromQuery] int areaId)
        {
            try
            {
                Area area = await _areaService.GetArea(areaId);

                return Ok(area);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("novo-area")]
        public async Task<ActionResult> NovoArea([FromBody] Area area)
        {
            try
            {
                area = await _areaService.NovoArea(area);

                return Ok(area);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("atualizar-area")]
        public async Task<ActionResult> AtualizarArea([FromBody] Area area)
        {
            try
            {
                await _areaService.AtualizarArea(area);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        // Controlador do adicionar usuário na área por ID
        [HttpPost("adicionar-usuario")]
        public async Task<ActionResult> AdicionarUsuario([FromQuery] int areaId, [FromQuery] string userIdAdd)
        {
            try
            {
                await _areaService.AdicionarUsuario(areaId, userIdAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}

