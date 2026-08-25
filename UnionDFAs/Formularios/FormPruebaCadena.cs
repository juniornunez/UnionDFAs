using UnionDFAs.Controles;
using UnionDFAs.Estructuras;
using UnionDFAs.Logica;
using UnionDFAs.Modelo;

namespace UnionDFAs.Formularios
{
    public class FormPruebaCadenas : Form
    {
        private TextBox txtCadena;
        private Label lblTrazabilidad;
        private FlowLayoutPanel panelTrazabilidad;
        private Label lblVeredicto1;
        private Label lblVeredicto2;
        private Label lblVeredictoUnion;
        private Label lblMensajeError;

        public FormPruebaCadenas()
        {
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Prueba de Cadenas";
            BackColor = Color.FromArgb(250, 250, 252);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(950, 650);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(40, 42, 48);

            Label titulo = new Label();
            titulo.Text = "Prueba de Cadenas sobre el Automata Union";
            titulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 15);
            Controls.Add(titulo);

            Label subtitulo = new Label();
            subtitulo.Text = "Union evaluada: " + Sesion.AutomataOrigenUnion1.Nombre + " y " + Sesion.AutomataOrigenUnion2.Nombre;
            subtitulo.Font = new Font("Segoe UI", 10F);
            subtitulo.ForeColor = Color.FromArgb(130, 133, 145);
            subtitulo.AutoSize = true;
            subtitulo.Location = new Point(30, 55);
            Controls.Add(subtitulo);

            Label lblEntrada = new Label();
            lblEntrada.Text = "Cadena a evaluar:";
            lblEntrada.Location = new Point(30, 100);
            lblEntrada.AutoSize = true;
            Controls.Add(lblEntrada);

            txtCadena = new TextBox();
            txtCadena.Location = new Point(180, 96);
            txtCadena.Size = new Size(300, 28);
            txtCadena.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(txtCadena);

            BotonRedondeado btnEvaluar = new BotonRedondeado("Evaluar Cadena", true);
            btnEvaluar.Location = new Point(500, 92);
            btnEvaluar.Size = new Size(170, 38);
            btnEvaluar.Click += BtnEvaluar_Click;
            Controls.Add(btnEvaluar);

            lblMensajeError = new Label();
            lblMensajeError.Text = "";
            lblMensajeError.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMensajeError.ForeColor = Color.FromArgb(190, 60, 70);
            lblMensajeError.Location = new Point(30, 140);
            lblMensajeError.AutoSize = true;
            Controls.Add(lblMensajeError);

            lblTrazabilidad = new Label();
            lblTrazabilidad.Text = "Trazabilidad en el Automata Union";
            lblTrazabilidad.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTrazabilidad.AutoSize = true;
            lblTrazabilidad.Location = new Point(30, 180);
            Controls.Add(lblTrazabilidad);

            GroupBox grupoTraza = new GroupBox();
            grupoTraza.Location = new Point(30, 215);
            grupoTraza.Size = new Size(870, 130);
            grupoTraza.ForeColor = Color.FromArgb(90, 92, 100);
            Controls.Add(grupoTraza);

            panelTrazabilidad = new FlowLayoutPanel();
            panelTrazabilidad.Location = new Point(10, 20);
            panelTrazabilidad.Size = new Size(850, 100);
            panelTrazabilidad.AutoScroll = true;
            panelTrazabilidad.BackColor = Color.White;
            grupoTraza.Controls.Add(panelTrazabilidad);

            Label etiquetaVeredicto = new Label();
            etiquetaVeredicto.Text = "Veredicto de Aceptacion";
            etiquetaVeredicto.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            etiquetaVeredicto.AutoSize = true;
            etiquetaVeredicto.Location = new Point(30, 365);
            Controls.Add(etiquetaVeredicto);

            GroupBox grupoVeredicto1 = CrearTarjetaVeredicto(Sesion.AutomataOrigenUnion1.Nombre, 30, 405, out lblVeredicto1);
            Controls.Add(grupoVeredicto1);

            GroupBox grupoVeredicto2 = CrearTarjetaVeredicto(Sesion.AutomataOrigenUnion2.Nombre, 320, 405, out lblVeredicto2);
            Controls.Add(grupoVeredicto2);

            GroupBox grupoVeredictoUnion = CrearTarjetaVeredicto("Automata Union", 610, 405, out lblVeredictoUnion);
            Controls.Add(grupoVeredictoUnion);
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

        private void BtnEvaluar_Click(object sender, EventArgs e)
        {
            string cadena = txtCadena.Text.Trim();
            lblMensajeError.Text = "";
            panelTrazabilidad.Controls.Clear();

            EvaluadorCadenas evaluador = new EvaluadorCadenas();

            ResultadoEvaluacion resultadoUnion = evaluador.Evaluar(Sesion.AutomataUnion, cadena);

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

            ResultadoEvaluacion resultado1 = evaluador.Evaluar(Sesion.AutomataOrigenUnion1, cadena);
            ResultadoEvaluacion resultado2 = evaluador.Evaluar(Sesion.AutomataOrigenUnion2, cadena);

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