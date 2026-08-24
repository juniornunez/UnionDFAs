using UnionDFAs.Controles;
using UnionDFAs.Modelo;

namespace UnionDFAs.Formularios
{
    public class FormSeleccionUnion : Form
    {
        private ComboBox cmbAutomata1;
        private ComboBox cmbAutomata2;
        private ListBox lstErrores;
        private Label lblResultado;

        public FormSeleccionUnion()
        {
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Seleccionar Automatas para la Union";
            BackColor = Color.FromArgb(250, 250, 252);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(600, 420);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(40, 42, 48);

            Label titulo = new Label();
            titulo.Text = "Seleccionar Automatas para la Union";
            titulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);
            Controls.Add(titulo);

            Label lbl1 = new Label();
            lbl1.Text = "Automata 1:";
            lbl1.Location = new Point(30, 80);
            lbl1.AutoSize = true;
            Controls.Add(lbl1);

            cmbAutomata1 = new ComboBox();
            cmbAutomata1.Location = new Point(150, 76);
            cmbAutomata1.Size = new Size(250, 28);
            cmbAutomata1.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(cmbAutomata1);

            Label lbl2 = new Label();
            lbl2.Text = "Automata 2:";
            lbl2.Location = new Point(30, 120);
            lbl2.AutoSize = true;
            Controls.Add(lbl2);

            cmbAutomata2 = new ComboBox();
            cmbAutomata2.Location = new Point(150, 116);
            cmbAutomata2.Size = new Size(250, 28);
            cmbAutomata2.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(cmbAutomata2);

            for (int i = 0; i < Sesion.Automatas.Cantidad; i++)
            {
                string nombre = Sesion.Automatas.Obtener(i).Nombre;
                cmbAutomata1.Items.Add(nombre);
                cmbAutomata2.Items.Add(nombre);
            }

            BotonRedondeado btnContinuar = new BotonRedondeado("Verificar y Continuar", true);
            btnContinuar.Location = new Point(30, 170);
            btnContinuar.Size = new Size(200, 40);
            btnContinuar.Click += BtnContinuar_Click;
            Controls.Add(btnContinuar);

            lblResultado = new Label();
            lblResultado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResultado.Location = new Point(30, 225);
            lblResultado.AutoSize = true;
            Controls.Add(lblResultado);

            lstErrores = new ListBox();
            lstErrores.Location = new Point(30, 255);
            lstErrores.Size = new Size(530, 120);
            lstErrores.BackColor = Color.White;
            lstErrores.BorderStyle = BorderStyle.FixedSingle;
            lstErrores.ForeColor = Color.FromArgb(190, 60, 70);
            Controls.Add(lstErrores);
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            lstErrores.Items.Clear();

            if (cmbAutomata1.SelectedItem == null || cmbAutomata2.SelectedItem == null)
            {
                MessageBox.Show("Selecciona ambos automatas");
                return;
            }

            string nombre1 = cmbAutomata1.SelectedItem.ToString();
            string nombre2 = cmbAutomata2.SelectedItem.ToString();

            if (nombre1 == nombre2)
            {
                MessageBox.Show("Selecciona dos automatas diferentes");
                return;
            }

            Automata automata1 = BuscarPorNombre(nombre1);
            Automata automata2 = BuscarPorNombre(nombre2);

            bool alfabetosCoinciden = VerificarAlfabetos(automata1, automata2, out string[] listaErrores);

            if (alfabetosCoinciden)
            {
                lblResultado.Text = "Los alfabetos coinciden, listos para la union";
                lblResultado.ForeColor = Color.FromArgb(60, 150, 100);
            }
            else
            {
                lblResultado.Text = "Los alfabetos no coinciden";
                lblResultado.ForeColor = Color.FromArgb(190, 60, 70);
                for (int i = 0; i < listaErrores.Length; i++)
                {
                    lstErrores.Items.Add(listaErrores[i]);
                }
            }
        }

        private Automata BuscarPorNombre(string nombre)
        {
            for (int i = 0; i < Sesion.Automatas.Cantidad; i++)
            {
                Automata automata = Sesion.Automatas.Obtener(i);
                if (automata.Nombre == nombre)
                    return automata;
            }
            return null;
        }

        private bool VerificarAlfabetos(Automata automata1, Automata automata2, out string[] errores)
        {
            bool coincide = true;
            string[] listaTemporal = new string[automata1.Alfabeto.Cantidad + automata2.Alfabeto.Cantidad];
            int contadorErrores = 0;

            for (int i = 0; i < automata1.Alfabeto.Cantidad; i++)
            {
                string simbolo = automata1.Alfabeto.Obtener(i);
                if (!automata2.Alfabeto.Existe(simbolo))
                {
                    listaTemporal[contadorErrores] = "El simbolo '" + simbolo + "' esta en " + automata1.Nombre + " pero no en " + automata2.Nombre;
                    contadorErrores++;
                    coincide = false;
                }
            }

            for (int i = 0; i < automata2.Alfabeto.Cantidad; i++)
            {
                string simbolo = automata2.Alfabeto.Obtener(i);
                if (!automata1.Alfabeto.Existe(simbolo))
                {
                    listaTemporal[contadorErrores] = "El simbolo '" + simbolo + "' esta en " + automata2.Nombre + " pero no en " + automata1.Nombre;
                    contadorErrores++;
                    coincide = false;
                }
            }

            if (automata1.Alfabeto.Cantidad != automata2.Alfabeto.Cantidad && coincide)
            {
                coincide = false;
            }

            errores = new string[contadorErrores];
            for (int i = 0; i < contadorErrores; i++)
            {
                errores[i] = listaTemporal[i];
            }

            return coincide;
        }
    }
}