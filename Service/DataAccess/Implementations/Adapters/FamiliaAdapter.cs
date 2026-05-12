using System;
using Service.DateAccess.Interface;
using Service.DomainModel.Composite;

namespace Service.DataAccess.Implementations.Adapters
{
    internal class FamiliaAdapter : IAdapter<Familia>
    {
        #region Singleton
        private readonly static FamiliaAdapter _instance = new FamiliaAdapter();

        public static FamiliaAdapter Current
        {
            get { return _instance; }
        }

        private FamiliaAdapter() { }
        #endregion

        public Familia Get(object[] values)
        {
            Familia familia = new Familia();
            familia.Id = Guid.Parse(values[0].ToString());
            familia.Nombre = values[1].ToString();

            familia.AddRange(new FamiliaFamiliaRepository().GetByObject(familia));
            familia.AddRange(new FamiliaPatenteRepository().GetByObject(familia));

            return familia;
        }
    }
}
