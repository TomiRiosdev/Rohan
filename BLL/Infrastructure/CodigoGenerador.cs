using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public static class CodigoGenerador
    {
        private static readonly Random _random = new Random();

        public static string GenerarCodigoOcAlfanumerico()
        {
            // Resultado ejemplo: OC-202606-A8F3
            string añoMes = DateTime.Now.ToString("yyyyMM");
            string hashAleatorio = _random.Next(0x1000, 0xFFFF).ToString("X4"); // Genera 4 caracteres hexadecimales únicos
            return $"OC-{añoMes}-{hashAleatorio}";
        }

        public static int GenerarNumeroOcUnicoNumerico()
        {

            string formatoCorto = DateTime.Now.ToString("yyMMddHHmm");

            if (long.Parse(formatoCorto) > int.MaxValue)
            {
                formatoCorto = DateTime.Now.ToString("ddHHmmss");
            }

            return int.Parse(formatoCorto);
        }
    }
}
