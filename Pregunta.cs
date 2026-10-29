using System;

namespace JuegoMatematico
{
    public class Pregunta
    {
        public string Texto;
        public int Respuesta;

        public Pregunta(int nivel, Random azar)
        {
            int max = nivel * 5 + 5;
            int a = azar.Next(1, max + 1);
            int b = azar.Next(1, max + 1);
            int operacion = azar.Next(1, 5);

            if (operacion == 1)
            {
                Texto = a + " + " + b + " = ?";
                Respuesta = a + b;
            }
            else if (operacion == 2)
            {
                Texto = a + " - " + b + " = ?";
                Respuesta = a - b;
            }
            else if (operacion == 3)
            {
                Texto = a + " * " + b + " = ?";
                Respuesta = a * b;
            }
            else
            {
                int resultado = azar.Next(1, max + 1);
                b = azar.Next(1, max + 1);
                a = resultado * b;

                Texto = a + " / " + b + " = ?";
                Respuesta = resultado;
            }
        }
    }
}
