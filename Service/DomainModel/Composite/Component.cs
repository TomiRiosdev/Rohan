using System;

namespace Service.DomainModel.Composite
{
    public abstract class Component
    {

        public Guid Id { get; set; }
        public Component()
        {

        }
   
        public abstract void Add(Component component);
   
        public abstract void Remove(Component component);

    }
}
