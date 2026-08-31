// Esta pantalla muestra el resultado de una union: la lista de componentes, el grafo del
// automata resultante y la tabla completa de transiciones.
using UnionDFAs.Controles;
using UnionDFAs.Modelo;

namespace UnionDFAs.Formularios
{
    public class FormResultadoUnion : Form
    {
        private Automata automataUnion;

        public FormResultadoUnion(Automata automataUnion)
        {
            this.automataUnion = automataUnion;
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Resultado de la Union: " + automataUnion.Nombre;
            BackColor = Color.FromArgb(250, 250, 252);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 1050);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(40, 42, 48);

            Label titulo = new Label();
            titulo.Text = "Automata Union Generado";
            titulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 15);
            Controls.Add(titulo);

            GroupBox grupoComponentes = new GroupBox();
            grupoComponentes.Text = "Componentes";
            grupoComponentes.Location = new Point(30, 60);
            grupoComponentes.Size = new Size(900, 110);
            grupoComponentes.ForeColor = Color.FromArgb(90, 92, 100);
            grupoComponentes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Controls.Add(grupoComponentes);

            string textoEstados = "Estados (" + automataUnion.Estados.Cantidad + "): " + ConcatenarLista(automataUnion.Estados);
            string textoAlfabeto = "Alfabeto: " + ConcatenarLista(automataUnion.Alfabeto);
            string textoInicial = "Estado inicial: " + automataUnion.EstadoInicial;
            string textoFinales = "Estados finales (" + automataUnion.EstadosFinales.Cantidad + "): " + ConcatenarLista(automataUnion.EstadosFinales);

            Label lblEstados = CrearEtiquetaInfo(textoEstados, 15, 25);
            grupoComponentes.Controls.Add(lblEstados);

            Label lblAlfabeto = CrearEtiquetaInfo(textoAlfabeto, 15, 48);
            grupoComponentes.Controls.Add(lblAlfabeto);

            Label lblInicial = CrearEtiquetaInfo(textoInicial, 15, 68);
            grupoComponentes.Controls.Add(lblInicial);

            Label lblFinales = CrearEtiquetaInfo(textoFinales, 15, 88);
            grupoComponentes.Controls.Add(lblFinales);

            Label etiquetaGrafo = new Label();
            etiquetaGrafo.Text = "Vista del Automata Union (Grafo)";
            etiquetaGrafo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            etiquetaGrafo.AutoSize = true;
            etiquetaGrafo.Location = new Point(30, 185);
            Controls.Add(etiquetaGrafo);

            GroupBox grupoGrafo = new GroupBox();
            grupoGrafo.Location = new Point(30, 220);
            grupoGrafo.Size = new Size(900, 350);
            grupoGrafo.ForeColor = Color.FromArgb(90, 92, 100);
            Controls.Add(grupoGrafo);

            Panel contenedorScroll = new Panel();
            contenedorScroll.Location = new Point(10, 20);
            contenedorScroll.Size = new Size(880, 320);
            contenedorScroll.BorderStyle = BorderStyle.FixedSingle;
            contenedorScroll.AutoScroll = true;
            contenedorScroll.BackColor = Color.White;
            grupoGrafo.Controls.Add(contenedorScroll);

            GrafoAutomata grafoAutomata = new GrafoAutomata();
            grafoAutomata.Automata = automataUnion;
            Size tamanoNecesario = grafoAutomata.CalcularTamanoNecesario();
            grafoAutomata.Size = new Size(Math.Max(tamanoNecesario.Width, contenedorScroll.Width - 20), Math.Max(tamanoNecesario.Height, contenedorScroll.Height - 20));
            grafoAutomata.Location = new Point(0, 0);
            contenedorScroll.Controls.Add(grafoAutomata);

            Label etiquetaTabla = new Label();
            etiquetaTabla.Text = "Tabla de Transiciones";
            etiquetaTabla.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            etiquetaTabla.AutoSize = true;
            etiquetaTabla.Location = new Point(30, 590);
            Controls.Add(etiquetaTabla);

            DataGridView tabla = new DataGridView();
            tabla.Location = new Point(30, 625);
            tabla.Size = new Size(900, 380);
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
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            tabla.Columns.Add("estado", "Estado");
            for (int i = 0; i < automataUnion.Alfabeto.Cantidad; i++)
            {
                tabla.Columns.Add("simbolo" + i, "Simbolo: " + automataUnion.Alfabeto.Obtener(i));
            }

            for (int i = 0; i < automataUnion.Estados.Cantidad; i++)
            {
                string estado = automataUnion.Estados.Obtener(i);
                string marcador = "";
                if (estado == automataUnion.EstadoInicial)
                {
                    marcador = marcador + " (inicial)";
                }
                if (automataUnion.EsFinal(estado))
                {
                    marcador = marcador + " (final)";
                }

                object[] fila = new object[automataUnion.Alfabeto.Cantidad + 1];
                fila[0] = estado + marcador;

                for (int j = 0; j < automataUnion.Alfabeto.Cantidad; j++)
                {
                    string simbolo = automataUnion.Alfabeto.Obtener(j);
                    string destino = automataUnion.BuscarDestino(estado, simbolo);
                    fila[j + 1] = destino;
                }

                int indiceFila = tabla.Rows.Add(fila);

                if (estado == automataUnion.EstadoInicial)
                {
                    tabla.Rows[indiceFila].DefaultCellStyle.BackColor = Color.FromArgb(235, 242, 255);
                }
                if (automataUnion.EsFinal(estado))
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
            etiqueta.ForeColor = Color.FromArgb(60, 62, 70);
            etiqueta.MaximumSize = new Size(860, 0);
            return etiqueta;
        }

        private string ConcatenarLista(Estructuras.ListaEnlazada<string> lista)
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