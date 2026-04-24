using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DomainModel.Composite
{
    public class Familia : Component
    {

        private List<Component> hijos = new List<Component>();

        public string Nombre { get; set; }

        public Familia()
        {

        }

        public override void Add(Component component)
        {

            hijos.Add(component);
        }

        public void AddRange(IEnumerable<Component> components)
        {
            hijos.AddRange(components);
        }

        public override void Remove(Component component)
        {
            component.Remove(component);
        }

        public List<Component> GetHijos()
        {
            return hijos;
        }

    }
}
