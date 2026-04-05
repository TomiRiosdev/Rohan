using BLL.DomainDtos;


namespace BLL.GestiónProducto.Interface
{
    public interface ICategoriaService
    {
        // Métodos CRUD 
        void AgregarCategoria(CategoriaDTO dto);
        void ModificarCategoria(CategoriaDTO dto);
        void DeshabilitarCategoria(Guid id);
        void HabilitarCategoria(Guid id);

        // Métodos de consulta
        CategoriaDTO GetById(Guid id);
        List<CategoriaDTO> GetHabilitados();


    }
}
