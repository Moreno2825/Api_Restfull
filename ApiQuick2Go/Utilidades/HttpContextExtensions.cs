using Microsoft.EntityFrameworkCore;

namespace ApiQuick2Go.Utilidades
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext == null) { throw new ArgumentException(nameof(httpContext)); }
            double cantidad = await queryable.CountAsync();
            httpContext.Response.Headers.Add("CantidadTotalRegistros", cantidad.ToString());
        }
    }
}
