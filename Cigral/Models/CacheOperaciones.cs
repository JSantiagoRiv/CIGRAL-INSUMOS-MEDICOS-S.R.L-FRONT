using System.Collections.Generic;

namespace Cigral.Models
{
    // --- CACHÉ DE INGRESOS ---
    public class FilaIngresoCache
    {
        public int ProductoId { get; set; }
        public string Lote { get; set; }
        public string Vencimiento { get; set; }
        public string Serie { get; set; }
        public int Cantidad { get; set; }
        public string InfoAdicional { get; set; }
        public string GtinOculto { get; set; }
    }

    // CACHÉ DE EGRESOS
    public class FilaEgresoCache
    {
        public int ProductoId { get; set; }
        public string Lote { get; set; }
        public string Vencimiento { get; set; }
        public string Serie { get; set; }
        public int Cantidad { get; set; }
    }

    public static class CacheOperaciones
    {
        public static List<FilaIngresoCache> CacheIngresos { get; set; } = new List<FilaIngresoCache>();

        // LA LISTA PARA EGRESOS
        public static List<FilaEgresoCache> CacheEgresos { get; set; } = new List<FilaEgresoCache>();
    }
}