using ApiQuick2Go.DTOs;
using ApiQuick2Go.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiQuick2Go.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VentasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<VentaDTO>>> Get(int id)
        {
            var existeVenta = await context.Ventas.AnyAsync(x=> x.Id == id);
            if(!existeVenta)
            {
                return NotFound();
            }

            else
            {
                var venta = await context.Ventas.ToListAsync();
                return mapper.Map<List<VentaDTO>>(venta);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post(VentaCreacionDTO ventaCreacionDTO)
        {
            var venta = mapper.Map<Venta>(ventaCreacionDTO);

            var pedido = await context.Pedidos.FirstOrDefaultAsync(x => x.Id == ventaCreacionDTO.IdPedido);
            venta.TotalPago = 1.16 * pedido.SubTotal;

            context.Add(venta);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Ventas.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound("Id no encontrado");
            }
            context.Remove(new Venta() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
