using ApiQuick2Go.DTOs;
using ApiQuick2Go.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiQuick2Go.Controllers
{
    [ApiController]
    [Route("api/compradores")]
    public class CompradoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CompradoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<CompradorDTO>>> Get()
        {

            var compradores = await context.Compradores.ToListAsync();
            return mapper.Map<List<CompradorDTO>>(compradores);
        }

        /*
        [HttpGet("{apellido}")]
        public async Task<ActionResult<List<CompradorDTO>>> Get([FromRoute] string apellido)
        {
            var comprador = await context.Compradores.Where(a => a.Apellidos.Contains(apellido)).ToListAsync();
            return mapper.Map<List<CompradorDTO>>(comprador);
        }
        */

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(CompradorCreacionDTO compradorCreacionDTO)
        {
            var comprador = mapper.Map<Comprador>(compradorCreacionDTO);
            context.Add(comprador);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(Comprador comprador, int id)
        {
            if(comprador.Id != id)
            {
                return BadRequest("El id del comprador no coincide con el id de la URL");
            }
            context.Update(comprador);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Compradores.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound($"No se ha encontrado al comprador con id: {id}");
            }

            context.Remove(new Comprador() { Id = id});
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
