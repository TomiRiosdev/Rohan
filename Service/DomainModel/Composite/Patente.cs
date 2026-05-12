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
        public string Descripcion { get; set; }
        public TipoAcceso TipoAcceso { get; set; }

        public override void Add(Component c)
        {
            throw new InvalidOperationException("No se puede agregar elementos a una Patente.");
        }

        public override void Remove(Component c)
        {
            throw new InvalidOperationException("No se puede quitar elementos de una Patente.");
        }

        public override int GetCount()
        {
            return 0;
        }
    }

    public enum TipoAcceso
    {
        Lectura = 1,
        Escritura = 2,
        ControlTotal = 3
    }
}
