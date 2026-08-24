using UnionDFAs.Controles;
using UnionDFAs.Formularios;
using UnionDFAs.Modelo;

namespace UnionDFAs
{
    public partial class Form1 : Form
    {
        private FlowLayoutPanel panelTarjetas;

        public Form1()
        {
            InitializeComponent();
            ConstruirInterfaz();
            CargarTarjetas();
        }

        private void ConstruirInterfaz()
        {
            BackColor = Color.FromArgb(250, 250, 252);
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(920, 620);
            Font = new Font("Segoe UI", 9F);

            Label etiquetaTitulo = new Label();
            etiquetaTitulo.Text = "Sistema de Automatas Finitos Deterministas";
            etiquetaTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            etiquetaTitulo.ForeColor = Color.FromArgb(35, 37, 43);
            etiquetaTitulo.AutoSize = true;
            etiquetaTitulo.Location = new Point(40, 30);
            Controls.Add(etiquetaTitulo);

            Label etiquetaSubtitulo = new Label();
            etiquetaSubtitulo.Text = "Crea, valida y une automatas sobre estructuras propias";
            etiquetaSubtitulo.Font = new Font("Segoe UI", 10.5F);
            etiquetaSubtitulo.ForeColor = Color.FromArgb(130, 133, 145);
            etiquetaSubtitulo.AutoSize = true;
            etiquetaSubtitulo.Location = new Point(42, 68);
            Controls.Add(etiquetaSubtitulo);

            BotonRedondeado btnNuevo = new BotonRedondeado("Nuevo Automata", true);
            btnNuevo.Location = new Point(40, 105);
            btnNuevo.Size = new Size(180, 40);
            btnNuevo.Click += (s, e) => AbrirCapturaAutomata();
            Controls.Add(btnNuevo);

            BotonRedondeado btnUnir = new BotonRedondeado("Unir Automatas");
            btnUnir.Location = new Point(230, 105);
            btnUnir.Size = new Size(180, 40);
            btnUnir.Click += (s, e) => AbrirUnion();
            Controls.Add(btnUnir);

            BotonRedondeado btnPrueba = new BotonRedondeado("Prueba de Cadenas");
            btnPrueba.Location = new Point(420, 105);
            btnPrueba.Size = new Size(180, 40);
            btnPrueba.Click += (s, e) => AbrirPruebaCadenas();
            Controls.Add(btnPrueba);

            Label etiquetaLista = new Label();
            etiquetaLista.Text = "Automatas creados";
            etiquetaLista.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            etiquetaLista.ForeColor = Color.FromArgb(35, 37, 43);
            etiquetaLista.AutoSize = true;
            etiquetaLista.Location = new Point(42, 165);
            Controls.Add(etiquetaLista);

            panelTarjetas = new FlowLayoutPanel();
            panelTarjetas.Location = new Point(40, 200);
            panelTarjetas.Size = new Size(840, 380);
            panelTarjetas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelTarjetas.AutoScroll = true;
            panelTarjetas.BackColor = Color.FromArgb(250, 250, 252);
            Controls.Add(panelTarjetas);
        }

        private void CargarTarjetas()
        {
            panelTarjetas.Controls.Clear();

            for (int i = 0; i < Sesion.Automatas.Cantidad; i++)
            {
                Automata automata = Sesion.Automatas.Obtener(i);
                TarjetaAutomata tarjeta = new TarjetaAutomata(automata);
                tarjeta.SolicitudEliminar += (s, e) => EliminarAutomata(automata);
                panelTarjetas.Controls.Add(tarjeta);
            }
        }

        private void EliminarAutomata(Automata automata)
        {
            DialogResult respuesta = MessageBox.Show("Eliminar el automata '" + automata.Nombre + "'?", "Confirmar", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                Sesion.Automatas.EliminarPorValor(automata);
                CargarTarjetas();
            }
        }

        private void AbrirCapturaAutomata()
        {
            FormCapturaAutomata formulario = new FormCapturaAutomata();
            formulario.ShowDialog();
            CargarTarjetas();
        }

        private void AbrirUnion()
        {
            if (Sesion.Automatas.Cantidad < 2)
            {
                MessageBox.Show("Necesitas al menos 2 automatas guardados para realizar la union");
                return;
            }
            FormSeleccionUnion formulario = new FormSeleccionUnion();
            formulario.ShowDialog();
        }

        private void AbrirPruebaCadenas()
        {
            MessageBox.Show("Formulario de prueba de cadenas en construccion");
        }
    }
}