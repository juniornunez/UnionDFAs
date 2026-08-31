using UnionDFAs.Controles;
using UnionDFAs.Estructuras;
using UnionDFAs.Modelo;

namespace UnionDFAs.Formularios
{
    public class FormDetalleAutomata : Form
    {
        private Automata automata;

        public FormDetalleAutomata(Automata automata)
        {
            this.automata = automata;
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Detalle del Automata: " + automata.Nombre;
            BackColor = Color.FromArgb(250, 250, 252);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(950, 1050);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(40, 42, 48);

            Label titulo = new Label();
            titulo.Text = automata.Nombre;
            titulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 15);
            Controls.Add(titulo);

            GroupBox grupoComponentes = new GroupBox();
            grupoComponentes.Text = "Componentes";
            grupoComponentes.Location = new Point(30, 65);
            grupoComponentes.Size = new Size(870, 130);
            grupoComponentes.ForeColor = Color.FromArgb(90, 92, 100);
            grupoComponentes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Controls.Add(grupoComponentes);

            string textoEstados = "Estados (" + automata.Estados.Cantidad + "): " + ConcatenarLista(automata.Estados);
            string textoAlfabeto = "Alfabeto (" + automata.Alfabeto.Cantidad + "): " + ConcatenarLista(automata.Alfabeto);
            string textoInicial = "Estado inicial: " + (automata.EstadoInicial != null ? automata.EstadoInicial : "(sin definir)");
            string textoFinales = "Estados finales (" + automata.EstadosFinales.Cantidad + "): " + ConcatenarLista(automata.EstadosFinales);

            Label lblEstados = CrearEtiquetaInfo(textoEstados, 15, 25);
            grupoComponentes.Controls.Add(lblEstados);

            Label lblAlfabeto = CrearEtiquetaInfo(textoAlfabeto, 15, 50);
            grupoComponentes.Controls.Add(lblAlfabeto);

            Label lblInicial = CrearEtiquetaInfo(textoInicial, 15, 75);
            lblInicial.ForeColor = Color.FromArgb(60, 100, 190);
            lblInicial.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grupoComponentes.Controls.Add(lblInicial);

            Label lblFinales = CrearEtiquetaInfo(textoFinales, 15, 98);
            lblFinales.ForeColor = Color.FromArgb(60, 150, 100);
            lblFinales.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grupoComponentes.Controls.Add(lblFinales);

            Label etiquetaGrafo = new Label();
            etiquetaGrafo.Text = "Vista del Automata (Grafo)";
            etiquetaGrafo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            etiquetaGrafo.AutoSize = true;
            etiquetaGrafo.Location = new Point(30, 210);
            Controls.Add(etiquetaGrafo);

            GroupBox grupoGrafo = new GroupBox();
            grupoGrafo.Location = new Point(30, 245);
            grupoGrafo.Size = new Size(870, 320);
            grupoGrafo.ForeColor = Color.FromArgb(90, 92, 100);
            Controls.Add(grupoGrafo);

            Panel contenedorScroll = new Panel();
            contenedorScroll.Location = new Point(10, 20);
            contenedorScroll.Size = new Size(850, 290);
            contenedorScroll.BorderStyle = BorderStyle.FixedSingle;
            contenedorScroll.AutoScroll = true;
            contenedorScroll.BackColor = Color.White;
            grupoGrafo.Controls.Add(contenedorScroll);

            GrafoAutomata grafoAutomata = new GrafoAutomata();
            grafoAutomata.Automata = automata;
            Size tamanoNecesario = grafoAutomata.CalcularTamanoNecesario();
            grafoAutomata.Size = new Size(Math.Max(tamanoNecesario.Width, contenedorScroll.Width - 20), Math.Max(tamanoNecesario.Height, contenedorScroll.Height - 20));
            grafoAutomata.Location = new Point(0, 0);
            contenedorScroll.Controls.Add(grafoAutomata);

            Label etiquetaTabla = new Label();
            etiquetaTabla.Text = "Tabla de Transiciones";
            etiquetaTabla.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            etiquetaTabla.AutoSize = true;
            etiquetaTabla.Location = new Point(30, 580);
            Controls.Add(etiquetaTabla);

            DataGridView tabla = new DataGridView();
            tabla.Location = new Point(30, 615);
            tabla.Size = new Size(870, 380);
            tabla.BackgroundColor = Color.White;
            tabla.BorderStyle = BorderStyle.None;
            tabla.GridColor = Color.FromArgb(228, 230, 234);
            tabla.EnableHeadersVisualStyles = false;
            tabla.RowHeadersVisible = false;
            tabla.ReadOnly = true;
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToDeleteRows = false;
            tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(246, 246, 248);
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(60, 62, 70);
            tabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabla.ColumnHeadersHeight = 34;
            tabla.RowTemplate.Height = 30;
            tabla.DefaultCellStyle.BackColor = Color.White;
            tabla.DefaultCellStyle.ForeColor = Color.FromArgb(40, 42, 48);
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            tabla.Columns.Add("estado", "Estado");
            for (int i = 0; i < automata.Alfabeto.Cantidad; i++)
            {
                tabla.Columns.Add("simbolo" + i, "Simbolo: " + automata.Alfabeto.Obtener(i));
            }

            for (int i = 0; i < automata.Estados.Cantidad; i++)
            {
                string estado = automata.Estados.Obtener(i);
                string marcador = "";
                if (estado == automata.EstadoInicial)
                {
                    marcador = marcador + " (inicial)";
                }
                if (automata.EsFinal(estado))
                {
                    marcador = marcador + " (final)";
                }

                object[] fila = new object[automata.Alfabeto.Cantidad + 1];
                fila[0] = estado + marcador;

                for (int j = 0; j < automata.Alfabeto.Cantidad; j++)
                {
                    string simbolo = automata.Alfabeto.Obtener(j);
                    string destino = automata.BuscarDestino(estado, simbolo);
                    fila[j + 1] = destino != null ? destino : "(sin definir)";
                }

                int indiceFila = tabla.Rows.Add(fila);

                if (estado == automata.EstadoInicial)
                {
                    tabla.Rows[indiceFila].DefaultCellStyle.BackColor = Color.FromArgb(235, 242, 255);
                }
                if (automata.EsFinal(estado))
                {
                    tabla.Rows[indiceFila].DefaultCellStyle.ForeColor = Color.FromArgb(60, 130, 90);
                    tabla.Rows[indiceFila].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }

            Controls.Add(tabla);
        }

        private Label CrearEtiquetaInfo(string texto, int x, int y)
        {
            Label etiqueta = new Label();
            etiqueta.Text = texto;
            etiqueta.Location = new Point(x, y);
            etiqueta.AutoSize = true;
            etiqueta.MaximumSize = new Size(830, 0);
            return etiqueta;
        }

        private string ConcatenarLista(ListaEnlazada<string> lista)
        {
            string resultado = "";
            for (int i = 0; i < lista.Cantidad; i++)
            {
                resultado = resultado + lista.Obtener(i);
                if (i < lista.Cantidad - 1)
                {
                    resultado = resultado + ", ";
                }
            }
            if (resultado == "")
            {
                resultado = "(vacio)";
            }
            return resultado;
        }
    }
}