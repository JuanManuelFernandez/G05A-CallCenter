using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public enum TipoUsuario
    {
        ADMIN = 1,
        EMPLEADO = 2,
        CLIENTE = 3,
    }
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }

        public Usuario(int tipo, string email, string clave)
        {
            TipoUsuario = tipo == 1 ? TipoUsuario.ADMIN : tipo == 2 ? TipoUsuario.EMPLEADO : TipoUsuario.CLIENTE;
            Email = email;
            Clave = clave;
        }
        public Usuario()
        {
            TipoUsuario = TipoUsuario.CLIENTE;
            Email = "user@mail.com";
            Clave = "password";
        }
    }
}
