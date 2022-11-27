using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityImage : EntityBase
    {
        public int idImagen { get; set; }
        public string nombre { get; set; }
        public string ruta { get; set; }
        public int idProyecto { get; set; }
        public int idDepartamento { get; set; }
        public string tipo { get; set; }
    }
}