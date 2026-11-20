namespace GestionPeliculas.Data
{
    public class Conexion
    {
        public string Servidor { get; set; } = "localhost";
        public string Puerto { get; set; } = "3306";
        public string BaseDatos { get; set; } = "gestion_peliculas";
        public string Usuario { get; set; } = "root";
        public string Clave { get; set; } = "";

        public string ObtenerCadenaConexion()
        {
            return $"server={Servidor};port={Puerto};database={BaseDatos};user={Usuario};password={Clave};";
        }
    }
}