using System.Collections.Generic;

namespace JuegoMatematico
{
    public static class Clasificacion
    {
        public static List<string> Resultados = new List<string>();

        public static void Guardar(Partida partida)
        {
            for (int i = 0; i < partida.CantJugadores; i++)
            {
                string resultado = partida.Nombres[i] + " - " +
                    partida.Puntos[i] + " puntos";

                Resultados.Add(resultado);
            }
        }

        public static string Mostrar()
        {
            if (Resultados.Count == 0)
                return "Todavía no hay resultados.";

            string texto = "";

            for (int i = 0; i < Resultados.Count; i++)
                texto += (i + 1) + ". " + Resultados[i] + "\n";

            return texto;
        }
    }
}
