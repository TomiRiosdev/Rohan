using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class StockPorSucursalDTO
    {
        // ==== CLAVES DE PERSISTENCIA Y ENTIDAD ====
        public Guid IdStockPorSucursal { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdProducto { get; set; }

        /// <summary>
        /// IMPORTANTE: Guarda siempre la cantidad física en UNIDADES/PAQUETES SUELTOS (ej: 14 latas o 2 bolsas).
        /// Toda la matemática de la Base de Datos se consolida acá.
        /// </summary>
        public int CantidadTotal { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }
        public bool Habilitado { get; set; }

        // ==== DATOS REQUERIDOS PARA LA CREACIÓN DEL LOTE (HISTORIAL) ====
        public decimal CostoUnitario { get; set; }
        public string? NumeroLote { get; set; }

        // ==== PROPIEDADES ENRIQUECIDAS (VIENEN DEL JOIN CON PRODUCTO) ====
        public string? ProductoNombre { get; set; }
        public int? CodigoSku { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public string UnidadMedidaNombre { get; set; } = string.Empty; 
        public Guid IdCategoria { get; set; }
        public Guid IdUnidadMedida { get; set; }

        // ==== COEFICIENTES LOGÍSTICOS DEL MAESTRO DE PRODUCTOS ====
        public int CantidadPorBulto { get; set; } // Ej: 6 (si viene en caja de 6) o 1 (si es una bolsa de harina suelta)
        public decimal ContenidoPorVenta { get; set; } // Lo que pesa cada unidad individual (ej: 500 gramos o 900 ml)
        public int IdTipoEnvase { get; set; } // El entero del Enum (Caja, Pack, Bolsa)
        public string TipoEnvaseNombre { get; set; } = "Unidad"; // Ej: "Caja", "Pack"

        // ==== PROPIEDADES OPERATIVAS PARA TRANSACCIONES MANUALES ====
        public int IdTipoMovimiento { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public bool EsIngresoPorBulto { get; set; } // True: El usuario digitó bultos cerrados. False: Unidades sueltas.

        public int? DiasVidaUtil { get; set; }
        public int? DiasAlertaVencimiento { get; set; }
        public bool TieneLotesVencidos { get; set; }


        //PROPIEDADES CALCULADAS INTELIGENTES PARA EL "EFECTO EXCEL" EN TU DGV
        /// <summary>
        /// Divide la CantidadTotal por lo que trae el bulto. Ej: 14 latas / 6 = 2 Cajas enteras.
        /// </summary>
        public int BultosCerrados => CantidadPorBulto > 0 ? CantidadTotal / CantidadPorBulto : 0;

        /// <summary>
        /// El residuo matemático. Ej: 14 latas % 6 = 2 latas sueltas en estantería.
        /// </summary>
        public int UnidadesSueltas => CantidadPorBulto > 0 ? CantidadTotal % CantidadPorBulto : 0;

        /// <summary>
        /// Multiplica el stock físico por el peso/volumen unitario. Ej: 14 latas * 250g = 3500 gramos netos de materia prima.
        /// </summary>
        public decimal ContenidoNetoTotal => CantidadTotal * ContenidoPorVenta;

        /// <summary>
        // "Envases / Bultos"
        /// Muestra limpiamente cuántas cajas/packs cerrados representa el stock técnico.
        /// </summary>
        public string BultosVisual
        {
            get
            {// Si no tiene envase o dice "Sin especificar", es un producto suelto puro
                if (string.IsNullOrWhiteSpace(TipoEnvaseNombre) || TipoEnvaseNombre == "Sin especificar")
                    return "-";

                // Si la cantidad por bulto es 0 (evitamos división por cero) o el stock neto es 0
                if (CantidadPorBulto <= 0 || CantidadTotal <= 0)
                    return $"0 {TipoEnvaseNombre}(s)";

                // Calcula los bultos enteros (para Balde con CantidadPorBulto = 1, dará el total neto directo)
                int bultosEnteros = CantidadTotal / CantidadPorBulto;

                return $"{bultosEnteros} {TipoEnvaseNombre}(s)";
            }
        }
        /// <summary>
        /// Muestra ÚNICAMENTE el desglose interno de lo que quedó suelto o abierto. Ej: "(1 cajas + 4 u. sueltas)"
        /// </summary>
        public string RemanenteSueltoVisual
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TipoEnvaseNombre) || TipoEnvaseNombre == "Sin especificar")
                    return "-";
  
                if (CantidadPorBulto == 1)
                    return "0 unidades";

                return $"{UnidadesSueltas} unidades";
            }
        }

        /// <summary>
        /// Total Stock Und
        /// Muestra la cantidad total de unidades físicas netas que componen el inventario.
        /// Ej: "14 u." (Sabiendo que es 1 pack de 10 + 4 sueltas)
        /// </summary>
        public string TotalStockUnidadesVisual => $"{CantidadTotal} unidades";


        /// <summary>
        /// Muestra el volumen neto real convertido a Kg o Lts si pasa los 1000. Ej: "16.00 Kg" o "50.00 Kg de 1 Kg"
        /// </summary>
        public string CantidadTotalVisual
        {
            get
            {
                string textoNeto = "";
                string unidadMinuscula = (UnidadMedidaNombre ?? "").ToLower().Trim();

                // Conversión de Gramos a Kilogramos
                if (unidadMinuscula.Contains("gramo") || unidadMinuscula == "gr" || unidadMinuscula == "g")
                {
                    if (ContenidoNetoTotal >= 1000)
                    {
                        double enKilogramos = (double)ContenidoNetoTotal / 1000.0;
                        textoNeto = $"{enKilogramos:N2} Kg";
                    }
                    else
                    {
                        textoNeto = $"{ContenidoNetoTotal} {UnidadMedidaNombre}";
                    }
                }
                // Conversión de Cc/Ml a Litros
                else if (unidadMinuscula == "cc" || unidadMinuscula.Contains("ml") || unidadMinuscula.Contains("mili") || unidadMinuscula.Contains("litro"))
                {
                    if (ContenidoNetoTotal >= 1000 || unidadMinuscula.Contains("litro"))
                    {
                        double enLitros = (double)ContenidoNetoTotal / 1000.0;
                        textoNeto = $"{enLitros:N2} Lts";
                    }
                    else
                    {
                        textoNeto = $"{ContenidoNetoTotal} {UnidadMedidaNombre}";
                    }
                }
                //  Unidades físicas fijas (ej: moldes, envases)
                else
                {
                    textoNeto = $"{ContenidoNetoTotal} {UnidadMedidaNombre}";
                }

                // Le concatenamos la aclaración del empaque base para que el operario sepa la equivalencia
                // Ej: "16.00 Kg (unidades de 1 Kg)"
                if (ContenidoPorVenta > 0)
                {
                    // Para no repetir "1000 Gramos", usamos la unidad base si es menor a 1000, o su equivalente simplificado
                    string pesoUnitarioTexto = ContenidoPorVenta >= 1000 && (unidadMinuscula.Contains("gramo") || unidadMinuscula == "gr")
                        ? $"{((double)ContenidoPorVenta / 1000.0):N0} Kg"
                        : $"{ContenidoPorVenta} {UnidadMedidaNombre}";

                    return $"{textoNeto} de {pesoUnitarioTexto}";
                }

                return textoNeto;
            }
        }
    }
}

