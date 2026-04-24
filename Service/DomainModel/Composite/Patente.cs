using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DomainModel.Composite
{
    public class Patente : Component
    {

        public string DataKey { get; set; }

        public TipoAcceso TipoAcceso { get; set; }
        public Patente()
        {

        }

        public override void Add(Component component)
        {
            throw new Exception("No se pueden agregar elementos en un hijo tipo hoja");
        }

        public override void Remove(Component component)
        {
            throw new Exception("No se pueden eliminar elementos en un hijo tipo hoja");
        }

    }

    public enum TipoAcceso
    {
        Pantalla,
        CasoUso,
        Servicio,
        SP,
        Tabla
    }
}
