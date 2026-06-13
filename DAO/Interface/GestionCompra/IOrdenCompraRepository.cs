using Models;
using System;

namespace DAO.Interface.GestionCompra
{
    public interface IOrdenCompraRepository
    {
        // CRUD Básico
        void Add(OrdenCompra entity);
        void Update(OrdenCompra entity);
        OrdenCompra GetById(Guid idOc);
        IEnumerable<OrdenCompra> GetAll();

        // Filtros del Listado Operativo
        IEnumerable<OrdenCompra> GetByEstado(int idEstadoOc);
        IEnumerable<OrdenCompra> GetByProveedor(Guid idProveedor);

        // Calcular el correlativo de la OC (Ej: OC-0001)
        int ObtenerUltimoNumeroOc();
    }
}
