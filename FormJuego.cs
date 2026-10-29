using System;
using System.Drawing;
using System.Windows.Forms;

namespace JuegoMatematico
{
    public class FormJuego : Form
    {
        private Partida partida;
        private Label lblDatos;
        private Label lblPregunta;
        private TextBox txtRespuesta;
        private Button btnResponder;
        private bool terminado = false;

        public FormJuego(Partida p)
        {
            partida = p;

            Text = "Partida";
            Size = new Size(560, 410);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            lblDatos = new Label();
            lblDatos.SetBounds(20, 15, 500, 100);
            lblDatos.Font = new Font("Arial", 10);
            Controls.Add(lblDatos);

            lblPregunta = new Label();
            lblPregunta.SetBounds(20, 120, 500, 60);
            lblPregunta.Font = new Font("Arial", 26, FontStyle.Bold);
            lblPregunta.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(lblPregunta);

            txtRespuesta = new TextBox();
            txtRespuesta.SetBounds(170, 200, 200, 35);
            txtRespuesta.Font = new Font("Arial", 16);
            txtRespuesta.TextAlign = HorizontalAlignment.Center;
            txtRespuesta.KeyPress += EscribirNumero;
            Controls.Add(txtRespuesta);

            btnResponder = new Button();
            btnResponder.Text = "Responder";
            btnResponder.SetBounds(170, 250, 200, 35);
            btnResponder.Click += Revisar;
            Controls.Add(btnResponder);
            AcceptButton = btnResponder;

            MostrarPregunta();
        }

        private void EscribirNumero(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void MostrarPregunta()
        {
            lblDatos.Text = "";

            for (int i = 0; i < partida.CantJugadores; i++)
            {
                lblDatos.Text += partida.Nombres[i] +
                    " | Puntos: " + partida.Puntos[i] +
                    " | Vidas: " + partida.Vidas[i] +
                    " | Nivel: " + partida.Niveles[i] +
                    " | Preguntas: " + partida.CantPreguntas[i] + "/10\n";
            }

            lblDatos.Text += "\nTurno de: " + partida.Nombres[partida.Turno];
            lblPregunta.Text = partida.PreguntaActual.Texto;
            txtRespuesta.Clear();
            txtRespuesta.Focus();
        }

        private void Revisar(object sender, EventArgs e)
        {
            int respuesta;

            if (!int.TryParse(txtRespuesta.Text, out respuesta))
            {
                MessageBox.Show("Escribe un número entero.");
                return;
            }

            int respuestaCorrecta = partida.PreguntaActual.Respuesta;
            bool correcta = partida.RevisarRespuesta(respuesta);

            string mensaje;

            if (correcta)
                mensaje = "¡Correcto! Ganaste 10 puntos.";
            else
                mensaje = "Incorrecto. La respuesta era " + respuestaCorrecta + ".";

            partida.CambiarTurno();

            if (partida.Termino())
            {
                terminado = true;
                Clasificacion.Guardar(partida);

                MessageBox.Show(mensaje, "Resultado");

                using (FormResultados resultados = new FormResultados(partida))
                {
                    resultados.ShowDialog();
                }

                Close();
            }
            else
            {
                MessageBox.Show(mensaje, "Resultado");
                partida.NuevaPregunta();
                MostrarPregunta();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!terminado)
            {
                DialogResult respuesta = MessageBox.Show(
                    "¿Quieres salir de la partida?",
                    "Salir",
                    MessageBoxButtons.YesNo);

                if (respuesta == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }
    }
}
