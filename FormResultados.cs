using System.Drawing;
using System.Windows.Forms;

namespace JuegoMatematico
{
    public class FormResultados : Form
    {
        public FormResultados(Partida partida)
        {
            Text = "Resultados";
            Size = new Size(400, 270);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label titulo = new Label();
            titulo.Text = "RESULTADOS FINALES";
            titulo.Font = new Font("Arial", 15, FontStyle.Bold);
            titulo.TextAlign = ContentAlignment.MiddleCenter;
            titulo.SetBounds(20, 20, 345, 35);
            Controls.Add(titulo);

            Label datos = new Label();
            datos.SetBounds(30, 75, 330, 100);

            for (int i = 0; i < partida.CantJugadores; i++)
            {
                datos.Text += partida.Nombres[i] + ": " +
                    partida.Puntos[i] + " puntos\n";
            }

            if (partida.CantJugadores == 2)
            {
                int ganador = partida.Ganador();

                if (ganador == -1)
                    datos.Text += "\nEmpate.";
                else
                    datos.Text += "\nGanó " + partida.Nombres[ganador] + ".";
            }

            Controls.Add(datos);

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.SetBounds(120, 185, 130, 35);
            btnCerrar.Click += (s, e) => Close();
            Controls.Add(btnCerrar);
        }
    }
}
