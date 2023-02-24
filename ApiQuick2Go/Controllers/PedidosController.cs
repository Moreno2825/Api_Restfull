using ApiQuick2Go.DTOs;
using ApiQuick2Go.Entidades;
using ApiQuick2Go.Utilidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace ApiQuick2Go.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PedidosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> Get()
        {

            //double subtotalPagar = 0;
            //
            //subtotalPagar += Convert.ToDouble( context.Productos.Where(x => x.CantidadProducto * x.Precio == subtotalPagar));

            return await context.Pedidos.Include(x => x.Compradores).Include(x => x.Productos).ToListAsync();
        }

        [HttpGet("Paginacion")]
        public async Task<ActionResult<List<PedidoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Pedidos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var pedidos = await queryable.OrderBy(pedido => pedido.CantidadProducto).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<PedidoDTO>>(pedidos);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoDTO>> Get(int id)
        {

            var ExistePedido = await context.Pedidos.AnyAsync(x => x.Id == id);

            if (!ExistePedido)
            {
                return NotFound($"No se encontró el pedido con id: {id}");
            }

            var pedido = await context.Pedidos.Include(x=> x.Compradores).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<PedidoDTO>(pedido);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(PedidoCreacionDTO pedidoCreacionDTO)
        {

            var pedido = mapper.Map<Pedido>(pedidoCreacionDTO);


            var producto = await context.Productos.FirstOrDefaultAsync(x => x.Id == pedidoCreacionDTO.IdProducto);
            pedido.SubTotal = pedido.CantidadProducto * producto.Precio;

            context.Add(pedido);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Pedido pedido, int id)
        {
            if (pedido.Id != id)
            {
                return BadRequest("El id del comprador no coincide con el id de la URL");
            }
            context.Update(pedido);
            await context.SaveChangesAsync();
            return Ok();
        }

        /*
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Pedidos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"No se ha encontrado el pedido con id: {id}");
            }

            context.Remove(new Pedido() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        } */
    }
}
