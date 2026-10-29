using System;

namespace JuegoMatematico
{
    public class Partida
    {
        public string[] Nombres;
        public int[] Puntos;
        public int[] Vidas;
        public int[] Niveles;
        public int[] CantPreguntas;

        public int CantJugadores;
        public int Turno;
        public Pregunta PreguntaActual;

        private Random azar = new Random();

        public Partida(string nombre1, string nombre2, bool dosJugadores)
        {
            CantJugadores = dosJugadores ? 2 : 1;

            Nombres = new string[CantJugadores];
            Puntos = new int[CantJugadores];
            Vidas = new int[CantJugadores];
            Niveles = new int[CantJugadores];
            CantPreguntas = new int[CantJugadores];

            Nombres[0] = nombre1;

            if (dosJugadores)
                Nombres[1] = nombre2;

            for (int i = 0; i < CantJugadores; i++)
            {
                Vidas[i] = 3;
                Niveles[i] = 1;
            }

            Turno = 0;
            NuevaPregunta();
        }

        public void NuevaPregunta()
        {
            PreguntaActual = new Pregunta(Niveles[Turno], azar);
        }

        public bool RevisarRespuesta(int respuesta)
        {
            int jugador = Turno;
            bool correcta = respuesta == PreguntaActual.Respuesta;

            CantPreguntas[jugador]++;

            if (correcta)
            {
                Puntos[jugador] += 10;

                if (CantPreguntas[jugador] % 3 == 0)
                    Niveles[jugador]++;
            }
            else
            {
                Vidas[jugador]--;
            }

            return correcta;
        }

        public bool JugadorSigue(int jugador)
        {
            return Vidas[jugador] > 0 && CantPreguntas[jugador] < 10;
        }

        public void CambiarTurno()
        {
            if (CantJugadores == 2)
            {
                int otro = 1 - Turno;

                if (JugadorSigue(otro))
                    Turno = otro;
                else if (!JugadorSigue(Turno) && JugadorSigue(otro))
                    Turno = otro;
            }
        }

        public bool Termino()
        {
            for (int i = 0; i < CantJugadores; i++)
            {
                if (JugadorSigue(i))
                    return false;
            }

            return true;
        }

        public int Ganador()
        {
            if (CantJugadores == 1)
                return 0;

            if (Puntos[0] == Puntos[1])
                return -1;

            return Puntos[0] > Puntos[1] ? 0 : 1;
        }
    }
}
