using UnionDFAs.Controles;
using UnionDFAs.Modelo;

namespace UnionDFAs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            BackColor = Color.FromArgb(20, 22, 28);
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(920, 620);
            Font = new Font("Segoe UI", 9F);

            Label etiquetaTitulo = new Label();
            etiquetaTitulo.Text = "Sistema de Automatas Finitos Deterministas";
            etiquetaTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            etiquetaTitulo.ForeColor = Color.FromArgb(240, 240, 245);
            etiquetaTitulo.AutoSize = true;
            etiquetaTitulo.Location = new Point(50, 50);
            Controls.Add(etiquetaTitulo);

            Label etiquetaSubtitulo = new Label();
            etiquetaSubtitulo.Text = "Validacion estricta y operacion de union sobre estructuras propias";
            etiquetaSubtitulo.Font = new Font("Segoe UI", 11F);
            etiquetaSubtitulo.ForeColor = Color.FromArgb(150, 152, 165);
            etiquetaSubtitulo.AutoSize = true;
            etiquetaSubtitulo.Location = new Point(52, 92);
            Controls.Add(etiquetaSubtitulo);

            Panel barraAcento = new Panel();
            barraAcento.BackColor = Color.FromArgb(94, 92, 230);
            barraAcento.Size = new Size(70, 5);
            barraAcento.Location = new Point(52, 130);
            Controls.Add(barraAcento);

            Color colorMorado = Color.FromArgb(94, 92, 230);
            Color colorTeal = Color.FromArgb(45, 212, 191);
            Color colorNaranja = Color.FromArgb(240, 165, 90);
            Color colorRosa = Color.FromArgb(230, 92, 150);

            BotonMenu botonAutomata1 = new BotonMenu("Automata 1", "Crear y validar el primer DFA", colorMorado);
            botonAutomata1.Location = new Point(50, 170);
            botonAutomata1.AccionClick += (s, e) => AbrirCapturaAutomata(1);
            Controls.Add(botonAutomata1);

            BotonMenu botonAutomata2 = new BotonMenu("Automata 2", "Crear y validar el segundo DFA", colorTeal);
            botonAutomata2.Location = new Point(330, 170);
            botonAutomata2.AccionClick += (s, e) => AbrirCapturaAutomata(2);
            Controls.Add(botonAutomata2);

            BotonMenu botonUnion = new BotonMenu("Operacion de Union", "Generar el DFA resultante de la union", colorNaranja);
            botonUnion.Location = new Point(50, 330);
            botonUnion.AccionClick += (s, e) => AbrirUnion();
            Controls.Add(botonUnion);

            BotonMenu botonPrueba = new BotonMenu("Prueba de Cadenas", "Evaluar cadenas con trazabilidad paso a paso", colorRosa);
            botonPrueba.Location = new Point(330, 330);
            botonPrueba.AccionClick += (s, e) => AbrirPruebaCadenas();
            Controls.Add(botonPrueba);
        }

        private void AbrirCapturaAutomata(int numero)
        {
            MessageBox.Show("Formulario de captura del Automata " + numero + " en construccion");
        }

        private void AbrirUnion()
        {
            MessageBox.Show("Formulario de union en construccion");
        }

        private void AbrirPruebaCadenas()
        {
            MessageBox.Show("Formulario de prueba de cadenas en construccion");
        }
    }
}