using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DomainModel.Composite
{
    public class Familia : Component
    {

        private List<Component> _hijos = new List<Component>();

        public string Nombre { get; set;}

        public Familia()
        {

        }

        public override void Add(Component component)
        {

            _hijos.Add(component);
        }

        public void AddRange(IEnumerable<Component> components)
        {
            if (components != null)
            {
                _hijos.AddRange(components);
            }
        }

        public override void Remove(Component component)
        {
            _hijos.Remove(component);
        }

        public List<Component> GetHijos()
        {
            return _hijos;
        }

        public override int GetCount()
        {
            int total = 0;
            foreach (var hijo in _hijos)
            {
                if (hijo is Patente) total++;
                else total += hijo.GetCount(); 
            }
            return total;
        }
    }
}
