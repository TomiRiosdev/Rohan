using System;
using System.Collections.Generic;

namespace Service.DomainModel.Composite
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool Habilitado { get; set; }   
        public DateTime Fecha { get; set; }
        public Guid? IdSucursal { get; set; }
   

        public List<Component> Privilegios { get; set; } = new List<Component>();
        public List<Patente> Patentes
        {
            get
            {
                List<Patente> patentes = new List<Patente>();
                RecorrerFamilias(patentes, Privilegios);
                return patentes;
            }
        }

        

        private void RecorrerFamilias(List<Patente> patentes, List<Component> componentes)
        {
            foreach (var componente in componentes)
            {
                if (componente is Patente patente)
                {
                    if (!patentes.Exists(p => p.Id == patente.Id))
                        patentes.Add(patente);
                }
                else if (componente is Familia familia)
                {
                    RecorrerFamilias(patentes, familia.GetHijos());
                }
            }
        }

        public Usuario(Guid id, string username, string nombre, string email, string password,
                    string telefono, DateTime fecha, bool habilitado, Guid? idSucursal)
        {
            this.IdUsuario = id;
            this.Username = username;
            this.Nombre = nombre;
            this.Email = email;
            this.Password = password;
            this.Telefono = telefono;
            this.Fecha = fecha;
            this.Habilitado = habilitado;
            this.IdSucursal = idSucursal;

        }

        public Usuario(Guid idUsuario,string username, string nombre, string email,string telefono ,string password, Guid? idSucursal = null, bool habilitado = true) 
        {
            IdUsuario = idUsuario;
        }

        public Usuario()
        {

        }
    }
}
