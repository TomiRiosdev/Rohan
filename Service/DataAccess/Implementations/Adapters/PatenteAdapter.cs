using System;
using Service.DateAccess.Interface;
using Service.DomainModel.Composite;

namespace Service.DataAccess.Implementations.Adapters
{
    internal class PatenteAdapter : IAdapter<Patente>
    {
        #region Singleton
        private readonly static PatenteAdapter _instance = new PatenteAdapter();

        public static PatenteAdapter Current
        {
            get { return _instance; }
        }

        private PatenteAdapter()
        {
            // Inicialización del singleton si fuera necesaria
        }
        #endregion

        public Patente Get(object[] values)
        {
            Patente patente = new Patente();
            patente.Id = Guid.Parse(values[0].ToString());
            patente.DataKey = values[1].ToString();
            patente.Nombre = values[1].ToString(); // Mapeamos el nombre para cumplir con Component
            patente.TipoAcceso = (TipoAcceso)Enum.Parse(typeof(TipoAcceso), values[2].ToString());

            return patente;
        }
    }
}
