using System;
using System.Collections.Generic;


namespace Cigral.Models
{
    
    
    //Proveedores.
    //Molde para recibir datos basicos de un proveeder desde la db.
    public class ProveedorDto
    {
        public int Id { get; set; }                        // Identificador único en la db.
        public string RazonSocial { get; set; }            // Nombre de la empresa.
        public string Gln { get; set; }                    // GLN del Proveedor.
        public string Email { get; set; }                  // Email de contacto.
        public string Cuit { get; set; }                   // Cuit del Proveedor.
        public string Telefono { get; set; }               // Teléfono de contacto.
        public string Direccion { get; set; }              // Dirección física.
    }




    //Utilidades y respuestas genéricas.
    //Molde maestro para anrir cualquier respuesta del sv que venga dividida en páginas.
    public class PaginadoResponse<T>
    {
        public List<T> items { get; set; }
        public int totalCount { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public bool hasPreviousPage { get; set; }
        public bool hasNextPage { get; set; }
    }




    //Parser
    //Molde para recibir el resultado de la traducción del codigo de barras.
    public class ParserResponse
    {
        public string Gtin { get; set; }                    //Código de barras universal del producto.
        public string Lote { get; set; }                    //Código alfanúmerico del lote.
        public string NumeroSerie { get; set; }             //Si el producto es unitario, trae su número de serie.
        public DateTime? FechaVencimiento { get; set; }     //El '?' permite que la fecha sea nula (null) si el escáner no encontró vencimiento.
        public int Cantidad { get; set; }                   //Cuántas unidades vienen en ese escaneo.
        public bool ExisteProducto { get; set; }            //True si la DB ya lo conoce, False si es nuevo
        public int? ProductoId { get; set; }                //El ? permite que el Id sea nulo si el producto no existe ne la DB todavía.
        public string NombreProducto { get; set; }          //El nombre real. 
        public string InformacionAdicional { get; set; }    //Info
    }




    //Depósitos
    //Molde para recibir los datos de un depósito individual.
    public class DepositoDto
    {
        public int Id { get; set; }                        //Id del depósito.
        public string Nombre { get; set; }                 //El nombre visible.
        public string Codigo { get; set; }                 //Código interno
        public bool Activo { get; set; }                   //Para saber si el depósito esta habilitado.
    }

    // Caja exterior específica que manda el back cuando le solicito la lista de depósitos.
    public class DepositosResponse
    {
        public List<DepositoDto> Items { get; set; }       //Lista que contiene los depósitos.
    }




    //Exsitstencias
    //Molde para aumentar existencias.
    public class AumentarExistenciaRequest
    {
        public int DepositoId { get; set; }                //Depósito en el que lo vamos a guardar.
        public int ProductoId { get; set; }                //Que producto se esta guardando.
        public string NumSerie { get; set; }               //Se manda el string vacio si no tiene serie.
        public string CodigoLote { get; set; }                   //El ? permite mandar null para que no haya conflicto con la DB.
        public DateTime? FechaVencimiento { get; set; }    //El ? permite mandar null si el producto no vence.
        public int Cantidad { get; set; }                  //Cantidad de stock que vamos a sumar.
    }



    //Existencias (stock)
    //Molde para recibir cada renglón de stock desde el sv.
    public class ExistenciaDto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public string ProductoGtin { get; set; }
        public int DepositoId { get; set; }
        public string DepositoNombre { get; set; }

        public int? LoteId { get; set; }                  //Le ponemos '?' por las dudas de que venga vacío.
        public string CodigoLote { get; set; }           //Ej: "105G283" o "Sin Código de Lote"
        public string NumSerie { get; set; }
        public DateTime? FechaVencimiento { get; set; }   //Puede ser null si no tiene vencimiento
        public int Cantidad { get; set; }
        public string ProductoCodigoGenerico { get; set; }
    }

    public class DisminuirStockDto
    {
        public int depositoId { get; set; }
        public int productoId { get; set; }
        public string numSerie { get; set; }
        public string codigoLote { get; set; }
        public DateTime? fechaVencimiento { get; set; }
        public int cantidad { get; set; }
    }

    public class ClienteDto
    {
        public int id { get; set; }
        public string razonSocial { get; set; }
        public string gln { get; set; }
        public string email { get; set; }
        public string cuit { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
    }


    // El molde para cada renglón de la auditoría
    public class AuditoriaItemDto
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public DateTime fechaMovimiento { get; set; }
        public int? productoId { get; set; }
        public string productoNombre { get; set; }
        public int? depositoId { get; set; }
        public string depositoNombre { get; set; }
        public int? loteId { get; set; }
        public string codigoLote { get; set; }
        public string numeroSerie { get; set; }
        public int cantidad { get; set; }
        public int stockAnterior { get; set; }
        public int stockNuevo { get; set; }
        public int? remitoIngresoId { get; set; }
        public int? remitoEgresoId { get; set; }
        public string usuario { get; set; }
        public string observaciones { get; set; }
    }

    // La "cajita" que envuelve a la lista (así no nos tira el error de la otra vez)
    public class AuditoriaResponseDto
    {
        public List<AuditoriaItemDto> items { get; set; }
        public int totalCount { get; set; }
    }

    public class VencimientoDto
    {
        public int existenciaId { get; set; }
        public int productoId { get; set; }
        public string productoNombre { get; set; }
        public string productoGtin { get; set; }
        public int depositoId { get; set; }
        public string depositoNombre { get; set; }
        public int loteId { get; set; }
        public string codigoLote { get; set; }
        public string numeroSerie { get; set; }
        public DateTime? fechaVencimiento { get; set; }
        public int diasParaVencer { get; set; }
        public int cantidad { get; set; }
    }

    public class DomainErrorResponse
    {
        public string error { get; set; }
        public string code { get; set; }
        public int codeValue { get; set; }
        public string message { get; set; }
    }

    // El molde de lo que viene adentro de "items"
    public class ProductoResponseDto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string gtin { get; set; }
        public string codigoGenerico { get; set; }
    }

    // El molde principal que ataja toda la respuesta del servidor
    public class ProductoPaginadoResponse
    {
        public List<ProductoResponseDto> items { get; set; }
    }

    public class ValidationErrorResponse
    {
        public string mensaje { get; set; }
        public List<DetalleError> errores { get; set; }
    }

    public class DetalleError
    {
        public int orden { get; set; }
        public string mensaje { get; set; }
    }


}
