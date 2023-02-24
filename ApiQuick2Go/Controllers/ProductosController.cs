using ApiQuick2Go.DTOs;
using ApiQuick2Go.Entidades;
using ApiQuick2Go.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApiQuick2Go.Utilidades;

namespace ApiQuick2Go.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await context.Productos.ToListAsync();
        }

        [HttpGet("Paginacion")]
        public async Task<ActionResult<List<ProductoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Productos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var productos = await queryable.OrderBy(producto => producto.NombreProducto).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ProductoDTO>>(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var ExisteProducto = await context.Productos.AnyAsync(x => x.Id == id);

            if(!ExisteProducto)
            {
                return NotFound($"No se encontró el producto con id: {id}");
            }

            //checar si no dejé nada de reseñas
            var producto = await context.Productos
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<ProductoDTO>(producto);
        }

        [HttpGet("{categoria}")]
        public async Task<ActionResult<List<ProductoDTO>>> Get([FromRoute] string categoria)
        {
            var existeProducto = await context.Productos.AnyAsync(x => x.Categoria == categoria);
            if(!existeProducto)
            {
                return NotFound("No se encontró la categoría.");
            }

            var producto = await context.Productos.Where(x=> x.Categoria.Contains(categoria)).ToListAsync();
            return mapper.Map<List<ProductoDTO>>(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductoCreacionDTO productoCreacionDTO)
        {
            var producto = mapper.Map<Producto>(productoCreacionDTO);
            context.Add(producto);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Productos.AnyAsync(x=> x.Id == id);

            if(!existe)
            {
                return NotFound("Id no encontrado");
            }
            context.Remove(new Producto() { Id = id});
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
