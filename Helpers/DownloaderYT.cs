using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroTube.Helpers
{
    public static class DownloaderYT
    {
        public static bool EsNumero(this string numero)
        {
            return float.TryParse(numero, out float numero2);
        }

        public static int? ConvertirEntero(this string numero)
        {
            if (numero.EsNumero())
            {
                return (int)float.Parse(numero);
            }
            return null;
        }
    }
}
