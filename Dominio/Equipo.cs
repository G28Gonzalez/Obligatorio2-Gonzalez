namespace Dominio
{
    public class Equipo
    {
        #region Propiedades
        public int Id { get; set; }
        public static int s_ultId { get; set; } = 1;
        public string NombreEquipo { get; set; }
        #endregion

        #region Constructores
        public Equipo(string nombre)
        {
            Id = s_ultId++;
            NombreEquipo = nombre;
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            return $"{Id} - {NombreEquipo}";
        }
        #endregion
    }
}
