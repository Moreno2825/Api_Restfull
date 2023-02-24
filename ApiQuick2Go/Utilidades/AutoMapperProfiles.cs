using ApiQuick2Go.DTOs;
using ApiQuick2Go.Entidades;
using AutoMapper;

namespace ApiQuick2Go.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CompradorCreacionDTO, Comprador>();
            CreateMap<Comprador, CompradorDTO>();
            CreateMap<PedidoCreacionDTO, Pedido>();
            CreateMap<Pedido, PedidoDTO>();
            CreateMap<ProductoCreacionDTO, Producto>();
            CreateMap<Producto, ProductoDTO>();
            CreateMap<VentaCreacionDTO,Venta>();
            CreateMap<Venta, VentaDTO>();
        }
    }
}
