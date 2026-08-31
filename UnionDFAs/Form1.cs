// Esta es la pantalla principal del programa, donde salen los botones para crear un automata
// nuevo, unir automatas, ver las uniones generadas y probar cadenas. Tambien muestra las
// tarjetas de todos los automatas que ya se han creado.
using UnionDFAs.Controles;
using UnionDFAs.Formularios;
using UnionDFAs.Logica;
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
            btnNuevo.Size = new Size(170, 40);
            btnNuevo.Click += (s, e) => AbrirCapturaAutomata();
            Controls.Add(btnNuevo);

            BotonRedondeado btnUnir = new BotonRedondeado("Unir Automatas");
            btnUnir.Location = new Point(220, 105);
            btnUnir.Size = new Size(170, 40);
            btnUnir.Click += (s, e) => AbrirUnion();
            Controls.Add(btnUnir);

            BotonRedondeado btnVerUniones = new BotonRedondeado("Ver Uniones");
            btnVerUniones.Location = new Point(400, 105);
            btnVerUniones.Size = new Size(170, 40);
            btnVerUniones.Click += (s, e) => AbrirListaUniones();
            Controls.Add(btnVerUniones);

            BotonRedondeado btnPrueba = new BotonRedondeado("Prueba de Cadenas");
            btnPrueba.Location = new Point(580, 105);
            btnPrueba.Size = new Size(170, 40);
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
                tarjeta.SolicitudVerDetalle += (s, e) => VerDetalleAutomata(automata);
                panelTarjetas.Controls.Add(tarjeta);
            }
        }

        private void VerDetalleAutomata(Automata automata)
        {
            FormDetalleAutomata formulario = new FormDetalleAutomata(automata);
            formulario.ShowDialog();
        }

        private void EliminarAutomata(Automata automata)
        {
            DialogResult respuesta = MessageBox.Show("Eliminar el automata '" + automata.Nombre + "'?", "Confirmar", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                Sesion.Automatas.EliminarPorValor(automata);
                RepositorioArchivos.Guardar();
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

        private void AbrirListaUniones()
        {
            if (Sesion.Uniones.Cantidad == 0)
            {
                MessageBox.Show("Todavia no has generado ninguna union");
                return;
            }
            FormListaUniones formulario = new FormListaUniones();
            formulario.ShowDialog();
        }

        private void AbrirPruebaCadenas()
        {
            if (Sesion.Uniones.Cantidad == 0)
            {
                MessageBox.Show("Primero debes generar al menos una union antes de probar cadenas");
                return;
            }
            FormPruebaCadenas formulario = new FormPruebaCadenas();
            formulario.ShowDialog();
        }
    }
}