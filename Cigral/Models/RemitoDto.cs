using System;
using System.Collections.Generic;

namespace Cigral.Models
{

    // Molde para enviar el ingreso completo con Remito en un solo viaje al sv.
    public class RemitoIngresoRequest
    {
        public int DepositoId { get; set; }                              //En qué depósito entra toda la mercadería.
        public int EntidadId { get; set; }                               //El Id del proveedor.
        public string NumeroRemito { get; set; }                         //Número del papel físico (remito).
        public string Observaciones { get; set; }                        //Texto extra de detalles.
        public List<RemitoDetalleRequest> Detalles { get; set; }         //Lista de los productos escaneados.
        public string ComprobanteAsociado { get; set; }
    }

    // Molde para cada renglón individual DENTRO de la caja grande del remito.
    public class RemitoDetalleRequest
    {
        public int ProductoId { get; set; }                              //Qué producto es.
        public string CodigoLote { get; set; }                           //El lote en formato texto (S/L si no tiene).
        public string NumeroSerie { get; set; }                          //Si el producto tiene número de serie. 
        public DateTime? FechaVencimiento { get; set; }                  //Fecha de vencimiento (acepta null).
        public int Cantidad { get; set; }                                //Cuántas unidades entraron.
    }



    //Molde para un nuevo código de barras.
    //Le mandamos esto para uqe lo dé de alta antes de sumar el stock.
    public class ProductoCreateRequest
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Gtin { get; set; }
        public string CodigoGenerico { get; set; }
        public bool EsUnitario { get; set; }
        public decimal Precio { get; set; }
        public string Marca { get; set; }
        public bool ProductoSinCodigo { get; set; }
    }




    //Molde para recibir el nuevo producto.
    public class ProductoCreateResponse
    {
        public int Id { get; set; }                                     //Id recien cocinado.
    }





    public class RemitoEgresoDetalleDto
    {
        public int productoId { get; set; }
        public string codigoLote { get; set; }
        public string numeroSerie { get; set; }
        public DateTime? fechaVencimiento { get; set; }
        public int cantidad { get; set; }
    }

    public class RemitoEgresoDto
    {
        public int depositoId { get; set; }
        public int entidadId { get; set; }
        public string numeroRemito { get; set; }
        public string observaciones { get; set; }
        public string comprobanteAsociado { get; set; }
        public List<RemitoEgresoDetalleDto> detalles { get; set; } = new List<RemitoEgresoDetalleDto>();
    }



   

    // Este es el molde para la lista que envuelve los remitos (paginación)
    public class RemitoPaginadoResponse
    {
        public List<RemitoHistorialDto> items { get; set; }
    }

    // Este es el molde de cada renglón que vamos a mostrar en la grilla
    public class RemitoHistorialDto
    {
        public int id { get; set; }
        public string numeroRemito { get; set; }
        public DateTime fecha { get; set; }

        public int depositoId { get; set; }
        public string depositoNombre { get; set; } // ¡Clave para que se lea bien!

        public int entidadId { get; set; }
        public string entidadNombre { get; set; }  // Proveedor o Cliente
        public string comprobanteAsociado { get; set; }

        public string observaciones { get; set; }
        // Los "detalles" los ignoramos acá porque no los necesitamos para la tabla principal
    }



}