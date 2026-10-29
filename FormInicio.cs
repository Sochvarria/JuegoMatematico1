using System;
using System.Drawing;
using System.Windows.Forms;

namespace JuegoMatematico
{
    public class FormInicio : Form
    {
        private TextBox txtNombre1;
        private TextBox txtNombre2;
        private RadioButton rbUno;
        private RadioButton rbDos;

        public FormInicio()
        {
            Text = "Juego matemático";
            Size = new Size(430, 330);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label titulo = new Label();
            titulo.Text = "JUEGO MATEMÁTICO";
            titulo.Font = new Font("Arial", 16, FontStyle.Bold);
            titulo.SetBounds(55, 20, 310, 35);
            Controls.Add(titulo);

            Label lbl1 = new Label();
            lbl1.Text = "Nombre del jugador 1:";
            lbl1.SetBounds(25, 80, 150, 25);
            Controls.Add(lbl1);

            txtNombre1 = new TextBox();
            txtNombre1.SetBounds(185, 80, 200, 25);
            Controls.Add(txtNombre1);

            Label lbl2 = new Label();
            lbl2.Text = "Nombre del jugador 2:";
            lbl2.SetBounds(25, 115, 150, 25);
            Controls.Add(lbl2);

            txtNombre2 = new TextBox();
            txtNombre2.SetBounds(185, 115, 200, 25);
            txtNombre2.Enabled = false;
            Controls.Add(txtNombre2);

            rbUno = new RadioButton();
            rbUno.Text = "Un jugador";
            rbUno.Checked = true;
            rbUno.SetBounds(65, 155, 120, 25);
            Controls.Add(rbUno);

            rbDos = new RadioButton();
            rbDos.Text = "Dos jugadores";
            rbDos.SetBounds(210, 155, 130, 25);
            Controls.Add(rbDos);

            rbDos.CheckedChanged += CambiarModo;

            Button btnJugar = new Button();
            btnJugar.Text = "Jugar";
            btnJugar.SetBounds(115, 200, 180, 35);
            btnJugar.Click += Empezar;
            Controls.Add(btnJugar);

            Button btnClasificacion = new Button();
            btnClasificacion.Text = "Ver clasificación";
            btnClasificacion.SetBounds(115, 242, 180, 30);
            btnClasificacion.Click += VerClasificacion;
            Controls.Add(btnClasificacion);
        }

        private void CambiarModo(object sender, EventArgs e)
        {
            txtNombre2.Enabled = rbDos.Checked;
        }

        private void Empezar(object sender, EventArgs e)
        {
            string nombre1 = txtNombre1.Text.Trim();
            string nombre2 = txtNombre2.Text.Trim();

            if (nombre1 == "")
            {
                MessageBox.Show("Escribe el nombre del jugador 1.");
                return;
            }

            if (rbDos.Checked && nombre2 == "")
            {
                MessageBox.Show("Escribe el nombre del jugador 2.");
                return;
            }

            Partida partida = new Partida(nombre1, nombre2, rbDos.Checked);

            using (FormJuego juego = new FormJuego(partida))
            {
                juego.ShowDialog();
            }
        }

        private void VerClasificacion(object sender, EventArgs e)
        {
            MessageBox.Show(Clasificacion.Mostrar(), "Clasificación");
        }
    }
}
