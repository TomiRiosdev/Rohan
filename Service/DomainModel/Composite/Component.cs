using System;

namespace Service.DomainModel.Composite
{
    public abstract class Component
    {

        public Guid Id { get; set; }
        public string Nombre { get; set; }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract int GetCount();

    }
}
