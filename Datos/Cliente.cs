namespace Datos
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public CategoriasCliente Categoria { get; set; }
        public Usuario Usuario { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
    }
}
