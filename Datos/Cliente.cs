﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int IdCategoria { get; set; }
        public Usuario Usuario { get; set; } // Contiene Email y Clave
        //public string Contraseña { get; set; }
        //public string Email { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public bool Eliminado { get; set; }
    }
}
