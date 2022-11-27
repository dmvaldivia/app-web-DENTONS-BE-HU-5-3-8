using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityLoginResponse
    {
        public int IdUsuario { get; set; }
        public string IdPerfil { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string token { get; set; }
    }
}
