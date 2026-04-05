using BLL.Bitacora.Interface;
using DAO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Bitacora
{
    public class BitacoraService : IBitacoraService
    {
        public BitacoraService() {  }

        public void GetAllBitacora(Guid idEntidad, string operacion, string detalle)
        {
            throw new NotImplementedException();
        }

        public void Registrar(Guid idEntidad, string operacion, string detalle)
        {
            throw new NotImplementedException();
        }
    }
}

//IMPLEMENTAR BITACORA DAO Y SQL 
