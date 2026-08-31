// Esta pantalla deja escribir una cadena y probarla en una union ya guardada, mostrando la
// trazabilidad paso a paso, la notacion formal delta gorrito para los 2 automatas originales
// y para la union, y el veredicto de si la cadena fue aceptada en cada uno de los 3.

using UnionDFAs.Controles;
using UnionDFAs.Estructuras;
using UnionDFAs.Logica;
using UnionDFAs.Modelo;

namespace UnionDFAs.Formularios
{
    public class FormPruebaCadenas : Form
    {
        private ComboBox cmbUnion;
        private TextBox txtCadena;
        private FlowLayoutPanel panelTrazabilidad;
        private TextBox txtNotacionAutomata1;
        private TextBox txtNotacionAutomata2;
        private TextBox txtNotacionUnion;
        private Label lblTituloNotacion1;
        private Label lblTituloNotacion2;
        private Label lblVeredicto1;
        private Label lblVeredicto2;
        private Label lblVeredictoUnion;
        private Label lblMensajeError;
        private GroupBox grupoVeredicto1;
        private GroupBox grupoVeredicto2;
        private UnionGuardada unionSeleccionada;

        public FormPruebaCadenas()
        {
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Prueba de Cadenas";
            BackColor = Color.FromArgb(250, 250, 252);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 900);
            AutoScroll = true;
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(40, 42, 48);

            Label titulo = new Label();
            titulo.Text = "Prueba de Cadenas";
            titulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 15);
            Controls.Add(titulo);

            Label lblUnion = new Label();
            lblUnion.Text = "Union a evaluar:";
            lblUnion.Location = new Point(30, 60);
            lblUnion.AutoSize = true;
            Controls.Add(lblUnion);

            cmbUnion = new ComboRedondeado();
            cmbUnion.Location = new Point(150, 56);
            cmbUnion.Size = new Size(350, 28);
            cmbUnion.SelectedIndexChanged += CmbUnion_SelectedIndexChanged;
            Controls.Add(cmbUnion);

            for (int i = 0; i < Sesion.Uniones.Cantidad; i++)
            {
                cmbUnion.Items.Add(Sesion.Uniones.Obtener(i).AutomataResultante.Nombre);
            }

            Label lblEntrada = new Label();
            lblEntrada.Text = "Cadena a evaluar:";
            lblEntrada.Location = new Point(30, 105);
            lblEntrada.AutoSize = true;
            Controls.Add(lblEntrada);

            txtCadena = new TextBox();
            txtCadena.Location = new Point(180, 101);
            txtCadena.Size = new Size(300, 28);
            txtCadena.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(txtCadena);

            BotonRedondeado btnEvaluar = new BotonRedondeado("Evaluar Cadena", true);
            btnEvaluar.Location = new Point(500, 97);
            btnEvaluar.Size = new Size(170, 38);
            btnEvaluar.Click += BtnEvaluar_Click;
            Controls.Add(btnEvaluar);

            lblMensajeError = new Label();
            lblMensajeError.Text = "";
            lblMensajeError.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMensajeError.ForeColor = Color.FromArgb(190, 60, 70);
            lblMensajeError.Location = new Point(30, 145);
            lblMensajeError.AutoSize = true;
            Controls.Add(lblMensajeError);

            Label lblTrazabilidad = new Label();
            lblTrazabilidad.Text = "Trazabilidad en el Automata Union";
            lblTrazabilidad.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTrazabilidad.AutoSize = true;
            lblTrazabilidad.Location = new Point(30, 185);
            Controls.Add(lblTrazabilidad);

            GroupBox grupoTraza = new GroupBox();
            grupoTraza.Location = new Point(30, 220);
            grupoTraza.Size = new Size(900, 110);
            grupoTraza.ForeColor = Color.FromArgb(90, 92, 100);
            Controls.Add(grupoTraza);

            panelTrazabilidad = new FlowLayoutPanel();
            panelTrazabilidad.Location = new Point(10, 15);
            panelTrazabilidad.Size = new Size(880, 85);
            panelTrazabilidad.AutoScroll = true;
            panelTrazabilidad.BackColor = Color.White;
            grupoTraza.Controls.Add(panelTrazabilidad);

            Label lblNotacionUnionTitulo = new Label();
            lblNotacionUnionTitulo.Text = "Notacion Formal - Automata Union";
            lblNotacionUnionTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNotacionUnionTitulo.AutoSize = true;
            lblNotacionUnionTitulo.Location = new Point(30, 345);
            Controls.Add(lblNotacionUnionTitulo);

            txtNotacionUnion = CrearCajaNotacion(30, 375, 900, 150);
            Controls.Add(txtNotacionUnion);

            lblTituloNotacion1 = new Label();
            lblTituloNotacion1.Text = "Notacion Formal - Automata 1";
            lblTituloNotacion1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloNotacion1.AutoSize = true;
            lblTituloNotacion1.Location = new Point(30, 535);
            Controls.Add(lblTituloNotacion1);

            txtNotacionAutomata1 = CrearCajaNotacion(30, 565, 900, 140);
            Controls.Add(txtNotacionAutomata1);

            lblTituloNotacion2 = new Label();
            lblTituloNotacion2.Text = "Notacion Formal - Automata 2";
            lblTituloNotacion2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloNotacion2.AutoSize = true;
            lblTituloNotacion2.Location = new Point(30, 715);
            Controls.Add(lblTituloNotacion2);

            txtNotacionAutomata2 = CrearCajaNotacion(30, 745, 900, 140);
            Controls.Add(txtNotacionAutomata2);

            Label etiquetaVeredicto = new Label();
            etiquetaVeredicto.Text = "Veredicto de Aceptacion";
            etiquetaVeredicto.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            etiquetaVeredicto.AutoSize = true;
            etiquetaVeredicto.Location = new Point(30, 900);
            Controls.Add(etiquetaVeredicto);

            grupoVeredicto1 = CrearTarjetaVeredicto("Automata 1", 30, 940, out lblVeredicto1);
            Controls.Add(grupoVeredicto1);

            grupoVeredicto2 = CrearTarjetaVeredicto("Automata 2", 320, 940, out lblVeredicto2);
            Controls.Add(grupoVeredicto2);

            GroupBox grupoVeredictoUnion = CrearTarjetaVeredicto("Automata Union", 610, 940, out lblVeredictoUnion);
            Controls.Add(grupoVeredictoUnion);

            if (cmbUnion.Items.Count > 0)
            {
                cmbUnion.SelectedIndex = 0;
            }
        }

        private TextBox CrearCajaNotacion(int x, int y, int ancho, int alto)
        {
            TextBox caja = new TextBox();
            caja.Location = new Point(x, y);
            caja.Size = new Size(ancho, alto);
            caja.Multiline = true;
            caja.ReadOnly = true;
            caja.ScrollBars = ScrollBars.Vertical;
            caja.Font = new Font("Consolas", 10F);
            caja.BackColor = Color.White;
            caja.BorderStyle = BorderStyle.FixedSingle;
            caja.ForeColor = Color.FromArgb(40, 42, 48);
            return caja;
        }

        private GroupBox CrearTarjetaVeredicto(string titulo, int x, int y, out Label etiquetaResultado)
        {
            GroupBox grupo = new GroupBox();
            grupo.Text = titulo;
            grupo.Location = new Point(x, y);
            grupo.Size = new Size(280, 90);
            grupo.ForeColor = Color.FromArgb(90, 92, 100);
            grupo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            Label etiqueta = new Label();
            etiqueta.Text = "Sin evaluar";
            etiqueta.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            etiqueta.ForeColor = Color.FromArgb(130, 133, 145);
            etiqueta.AutoSize = true;
            etiqueta.Location = new Point(15, 35);
            grupo.Controls.Add(etiqueta);

            etiquetaResultado = etiqueta;
            return grupo;
        }

        private void CmbUnion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUnion.SelectedIndex < 0)
                return;

            unionSeleccionada = Sesion.Uniones.Obtener(cmbUnion.SelectedIndex);
            grupoVeredicto1.Text = unionSeleccionada.NombreOrigen1;
            grupoVeredicto2.Text = unionSeleccionada.NombreOrigen2;
            lblTituloNotacion1.Text = "Notacion Formal - " + unionSeleccionada.NombreOrigen1;
            lblTituloNotacion2.Text = "Notacion Formal - " + unionSeleccionada.NombreOrigen2;

            lblVeredicto1.Text = "Sin evaluar";
            lblVeredicto1.ForeColor = Color.FromArgb(130, 133, 145);
            lblVeredicto2.Text = "Sin evaluar";
            lblVeredicto2.ForeColor = Color.FromArgb(130, 133, 145);
            lblVeredictoUnion.Text = "Sin evaluar";
            lblVeredictoUnion.ForeColor = Color.FromArgb(130, 133, 145);
            panelTrazabilidad.Controls.Clear();
            txtNotacionUnion.Text = "";
            txtNotacionAutomata1.Text = "";
            txtNotacionAutomata2.Text = "";
            lblMensajeError.Text = "";
        }

        private Automata BuscarAutomataPorNombre(string nombre)
        {
            for (int i = 0; i < Sesion.Automatas.Cantidad; i++)
            {
                Automata automata = Sesion.Automatas.Obtener(i);
                if (automata.Nombre == nombre)
                    return automata;
            }
            return null;
        }

        private void BtnEvaluar_Click(object sender, EventArgs e)
        {
            if (unionSeleccionada == null)
            {
                MessageBox.Show("Selecciona una union para evaluar");
                return;
            }

            Automata automataOrigen1 = BuscarAutomataPorNombre(unionSeleccionada.NombreOrigen1);
            Automata automataOrigen2 = BuscarAutomataPorNombre(unionSeleccionada.NombreOrigen2);

            if (automataOrigen1 == null || automataOrigen2 == null)
            {
                MessageBox.Show("No se encontraron los automatas originales de esta union");
                return;
            }

            string cadena = txtCadena.Text.Trim();
            lblMensajeError.Text = "";
            panelTrazabilidad.Controls.Clear();
            txtNotacionUnion.Text = "";
            txtNotacionAutomata1.Text = "";
            txtNotacionAutomata2.Text = "";

            EvaluadorCadenas evaluador = new EvaluadorCadenas();

            ResultadoEvaluacion resultadoUnion = evaluador.Evaluar(unionSeleccionada.AutomataResultante, cadena);

            if (!resultadoUnion.CadenaValida)
            {
                lblMensajeError.Text = resultadoUnion.MensajeError;
                lblVeredicto1.Text = "Sin evaluar";
                lblVeredicto1.ForeColor = Color.FromArgb(130, 133, 145);
                lblVeredicto2.Text = "Sin evaluar";
                lblVeredicto2.ForeColor = Color.FromArgb(130, 133, 145);
                lblVeredictoUnion.Text = "Sin evaluar";
                lblVeredictoUnion.ForeColor = Color.FromArgb(130, 133, 145);
                return;
            }

            MostrarTrazabilidad(resultadoUnion.SecuenciaEstados, cadena);

            txtNotacionUnion.Text = GeneradorNotacionFormal.Generar(unionSeleccionada.AutomataResultante.EstadoInicial, cadena, resultadoUnion.SecuenciaEstados);

            ResultadoEvaluacion resultado1 = evaluador.Evaluar(automataOrigen1, cadena);
            ResultadoEvaluacion resultado2 = evaluador.Evaluar(automataOrigen2, cadena);

            if (resultado1.CadenaValida)
            {
                txtNotacionAutomata1.Text = GeneradorNotacionFormal.Generar(automataOrigen1.EstadoInicial, cadena, resultado1.SecuenciaEstados);
            }
            else
            {
                txtNotacionAutomata1.Text = "No se pudo calcular: " + resultado1.MensajeError;
            }

            if (resultado2.CadenaValida)
            {
                txtNotacionAutomata2.Text = GeneradorNotacionFormal.Generar(automataOrigen2.EstadoInicial, cadena, resultado2.SecuenciaEstados);
            }
            else
            {
                txtNotacionAutomata2.Text = "No se pudo calcular: " + resultado2.MensajeError;
            }

            AplicarVeredicto(lblVeredicto1, resultado1);
            AplicarVeredicto(lblVeredicto2, resultado2);
            AplicarVeredicto(lblVeredictoUnion, resultadoUnion);
        }

        private void AplicarVeredicto(Label etiqueta, ResultadoEvaluacion resultado)
        {
            if (!resultado.CadenaValida)
            {
                etiqueta.Text = "No evaluable";
                etiqueta.ForeColor = Color.FromArgb(190, 60, 70);
                return;
            }

            if (resultado.Aceptada)
            {
                etiqueta.Text = "Aceptada";
                etiqueta.ForeColor = Color.FromArgb(60, 150, 100);
            }
            else
            {
                etiqueta.Text = "Rechazada";
                etiqueta.ForeColor = Color.FromArgb(190, 60, 70);
            }
        }

        private void MostrarTrazabilidad(ListaEnlazada<string> secuencia, string cadena)
        {
            for (int i = 0; i < secuencia.Cantidad; i++)
            {
                string estado = secuencia.Obtener(i);

                Label etiquetaEstado = new Label();
                etiquetaEstado.Text = estado;
                etiquetaEstado.AutoSize = true;
                etiquetaEstado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                etiquetaEstado.ForeColor = Color.FromArgb(40, 42, 48);
                etiquetaEstado.BackColor = Color.FromArgb(240, 241, 244);
                etiquetaEstado.Padding = new Padding(10, 6, 10, 6);
                etiquetaEstado.Margin = new Padding(4, 10, 4, 10);
                panelTrazabilidad.Controls.Add(etiquetaEstado);

                if (i < secuencia.Cantidad - 1)
                {
                    Label etiquetaFlecha = new Label();
                    etiquetaFlecha.Text = "--( " + cadena[i] + " )-->";
                    etiquetaFlecha.AutoSize = true;
                    etiquetaFlecha.Font = new Font("Segoe UI", 9.5F);
                    etiquetaFlecha.ForeColor = Color.FromArgb(130, 133, 145);
                    etiquetaFlecha.Margin = new Padding(4, 16, 4, 10);
                    panelTrazabilidad.Controls.Add(etiquetaFlecha);
                }
            }
        }
    }
}