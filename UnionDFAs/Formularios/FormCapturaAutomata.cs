using UnionDFAs.Controles;
using UnionDFAs.Estructuras;
using UnionDFAs.Logica;
using UnionDFAs.Modelo;

namespace UnionDFAs.Formularios
{
    public class FormCapturaAutomata : Form
    {
        private Automata automataActual;

        private TextBox txtNombre;
        private TextBox txtEstado;
        private TextBox txtSimbolo;
        private ListBox lstEstados;
        private ListBox lstAlfabeto;
        private ComboBox cmbEstadoInicial;
        private CheckedListBox chkFinales;
        private ComboBox cmbOrigen;
        private ComboBox cmbSimboloTransicion;
        private ComboBox cmbDestino;
        private DataGridView dgvTransiciones;
        private ListBox lstErrores;
        private Label lblResultado;
        private BotonRedondeado btnGuardar;

        public FormCapturaAutomata()
        {
            automataActual = new Automata(Sesion.GenerarNombreDisponible());
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Captura de Automata";
            BackColor = Color.FromArgb(250, 250, 252);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 760);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(40, 42, 48);

            Label titulo = new Label();
            titulo.Text = "Definicion de Automata";
            titulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(35, 37, 43);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 15);
            Controls.Add(titulo);

            Label lblNombre = CrearEtiqueta("Nombre del automata:", 30, 60);
            Controls.Add(lblNombre);

            txtNombre = CrearCajaTexto(190, 56, 200);
            txtNombre.Text = automataActual.Nombre;
            Controls.Add(txtNombre);

            GroupBox grupoEstados = CrearGrupo("Estados", 30, 100, 280, 160);
            Controls.Add(grupoEstados);

            txtEstado = CrearCajaTexto(15, 30, 150);
            grupoEstados.Controls.Add(txtEstado);

            BotonRedondeado btnAgregarEstado = new BotonRedondeado("Agregar");
            btnAgregarEstado.Location = new Point(175, 28);
            btnAgregarEstado.Size = new Size(90, 32);
            btnAgregarEstado.Click += BtnAgregarEstado_Click;
            grupoEstados.Controls.Add(btnAgregarEstado);

            lstEstados = CrearLista(15, 65, 250, 80);
            grupoEstados.Controls.Add(lstEstados);

            GroupBox grupoAlfabeto = CrearGrupo("Alfabeto (1 letra o 1 numero)", 330, 100, 280, 160);
            Controls.Add(grupoAlfabeto);

            txtSimbolo = CrearCajaTexto(15, 30, 150);
            txtSimbolo.MaxLength = 1;
            txtSimbolo.KeyPress += TxtSimbolo_KeyPress;
            grupoAlfabeto.Controls.Add(txtSimbolo);

            BotonRedondeado btnAgregarSimbolo = new BotonRedondeado("Agregar");
            btnAgregarSimbolo.Location = new Point(175, 28);
            btnAgregarSimbolo.Size = new Size(90, 32);
            btnAgregarSimbolo.Click += BtnAgregarSimbolo_Click;
            grupoAlfabeto.Controls.Add(btnAgregarSimbolo);

            lstAlfabeto = CrearLista(15, 65, 250, 80);
            grupoAlfabeto.Controls.Add(lstAlfabeto);

            GroupBox grupoInicialFinales = CrearGrupo("Estado Inicial y Finales", 630, 100, 300, 160);
            Controls.Add(grupoInicialFinales);

            Label lblInicial = CrearEtiqueta("Estado inicial:", 15, 28);
            grupoInicialFinales.Controls.Add(lblInicial);

            cmbEstadoInicial = CrearCombo(120, 25, 150);
            grupoInicialFinales.Controls.Add(cmbEstadoInicial);

            Label lblFinales = CrearEtiqueta("Estados finales:", 15, 60);
            grupoInicialFinales.Controls.Add(lblFinales);

            chkFinales = new CheckedListBox();
            chkFinales.Location = new Point(15, 85);
            chkFinales.Size = new Size(255, 65);
            chkFinales.BackColor = Color.White;
            chkFinales.ForeColor = Color.FromArgb(40, 42, 48);
            chkFinales.BorderStyle = BorderStyle.FixedSingle;
            grupoInicialFinales.Controls.Add(chkFinales);

            GroupBox grupoTransiciones = CrearGrupo("Transiciones", 30, 280, 900, 200);
            Controls.Add(grupoTransiciones);

            Label lblOrigen = CrearEtiqueta("Origen:", 15, 32);
            grupoTransiciones.Controls.Add(lblOrigen);

            cmbOrigen = CrearCombo(70, 28, 120);
            grupoTransiciones.Controls.Add(cmbOrigen);

            Label lblSimbolo = CrearEtiqueta("Simbolo:", 210, 32);
            grupoTransiciones.Controls.Add(lblSimbolo);

            cmbSimboloTransicion = CrearCombo(275, 28, 100);
            grupoTransiciones.Controls.Add(cmbSimboloTransicion);

            Label lblDestino = CrearEtiqueta("Destino:", 395, 32);
            grupoTransiciones.Controls.Add(lblDestino);

            cmbDestino = CrearCombo(450, 28, 120);
            grupoTransiciones.Controls.Add(cmbDestino);

            BotonRedondeado btnAgregarTransicion = new BotonRedondeado("Agregar Transicion");
            btnAgregarTransicion.Location = new Point(590, 26);
            btnAgregarTransicion.Size = new Size(170, 34);
            btnAgregarTransicion.Click += BtnAgregarTransicion_Click;
            grupoTransiciones.Controls.Add(btnAgregarTransicion);

            dgvTransiciones = new DataGridView();
            dgvTransiciones.Location = new Point(15, 65);
            dgvTransiciones.Size = new Size(865, 120);
            dgvTransiciones.BackgroundColor = Color.White;
            dgvTransiciones.BorderStyle = BorderStyle.None;
            dgvTransiciones.GridColor = Color.FromArgb(228, 230, 234);
            dgvTransiciones.EnableHeadersVisualStyles = false;
            dgvTransiciones.RowHeadersVisible = false;
            dgvTransiciones.ReadOnly = true;
            dgvTransiciones.AllowUserToAddRows = false;
            dgvTransiciones.AllowUserToDeleteRows = false;
            dgvTransiciones.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(246, 246, 248);
            dgvTransiciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(60, 62, 70);
            dgvTransiciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvTransiciones.DefaultCellStyle.BackColor = Color.White;
            dgvTransiciones.DefaultCellStyle.ForeColor = Color.FromArgb(40, 42, 48);
            dgvTransiciones.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 241, 244);
            dgvTransiciones.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 42, 48);
            dgvTransiciones.ColumnCount = 3;
            dgvTransiciones.Columns[0].Name = "Origen";
            dgvTransiciones.Columns[1].Name = "Simbolo";
            dgvTransiciones.Columns[2].Name = "Destino";
            grupoTransiciones.Controls.Add(dgvTransiciones);

            BotonRedondeado btnValidar = new BotonRedondeado("Validar Automata", true);
            btnValidar.Location = new Point(30, 500);
            btnValidar.Size = new Size(190, 40);
            btnValidar.Click += BtnValidar_Click;
            Controls.Add(btnValidar);

            lblResultado = new Label();
            lblResultado.Text = "";
            lblResultado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResultado.Location = new Point(240, 510);
            lblResultado.AutoSize = true;
            Controls.Add(lblResultado);

            lstErrores = new ListBox();
            lstErrores.Location = new Point(30, 550);
            lstErrores.Size = new Size(900, 130);
            lstErrores.BackColor = Color.White;
            lstErrores.BorderStyle = BorderStyle.FixedSingle;
            lstErrores.ForeColor = Color.FromArgb(190, 60, 70);
            Controls.Add(lstErrores);

            btnGuardar = new BotonRedondeado("Guardar Automata", true);
            btnGuardar.Location = new Point(30, 695);
            btnGuardar.Size = new Size(190, 40);
            btnGuardar.Enabled = false;
            btnGuardar.Click += BtnGuardar_Click;
            Controls.Add(btnGuardar);
        }

        private void TxtSimbolo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (txtSimbolo.Text.Length >= 1)
            {
                e.Handled = true;
            }
        }

        private GroupBox CrearGrupo(string texto, int x, int y, int ancho, int alto)
        {
            GroupBox grupo = new GroupBox();
            grupo.Text = texto;
            grupo.Location = new Point(x, y);
            grupo.Size = new Size(ancho, alto);
            grupo.ForeColor = Color.FromArgb(90, 92, 100);
            grupo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            return grupo;
        }

        private Label CrearEtiqueta(string texto, int x, int y)
        {
            Label etiqueta = new Label();
            etiqueta.Text = texto;
            etiqueta.Location = new Point(x, y);
            etiqueta.AutoSize = true;
            etiqueta.ForeColor = Color.FromArgb(90, 92, 100);
            return etiqueta;
        }

        private TextBox CrearCajaTexto(int x, int y, int ancho)
        {
            TextBox caja = new TextBox();
            caja.Location = new Point(x, y);
            caja.Size = new Size(ancho, 28);
            caja.BorderStyle = BorderStyle.FixedSingle;
            caja.BackColor = Color.White;
            caja.ForeColor = Color.FromArgb(40, 42, 48);
            return caja;
        }

        private ComboBox CrearCombo(int x, int y, int ancho)
        {
            ComboBox combo = new ComboBox();
            combo.Location = new Point(x, y);
            combo.Size = new Size(ancho, 28);
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.BackColor = Color.White;
            combo.ForeColor = Color.FromArgb(40, 42, 48);
            combo.FlatStyle = FlatStyle.Flat;
            return combo;
        }

        private ListBox CrearLista(int x, int y, int ancho, int alto)
        {
            ListBox lista = new ListBox();
            lista.Location = new Point(x, y);
            lista.Size = new Size(ancho, alto);
            lista.BackColor = Color.White;
            lista.ForeColor = Color.FromArgb(40, 42, 48);
            lista.BorderStyle = BorderStyle.FixedSingle;
            return lista;
        }

        private void BtnAgregarEstado_Click(object sender, EventArgs e)
        {
            string estado = txtEstado.Text.Trim();
            if (estado == "")
            {
                MessageBox.Show("Escribe un nombre de estado antes de agregar");
                return;
            }
            if (automataActual.Estados.Existe(estado))
            {
                MessageBox.Show("El estado ya existe en la lista");
                return;
            }
            automataActual.Estados.Agregar(estado);
            lstEstados.Items.Add(estado);
            txtEstado.Clear();
            ActualizarCombosDeEstados();
        }

        private void BtnAgregarSimbolo_Click(object sender, EventArgs e)
        {
            string simbolo = txtSimbolo.Text.Trim();
            if (simbolo == "")
            {
                MessageBox.Show("Escribe un simbolo antes de agregar");
                return;
            }
            if (simbolo.Length != 1 || !char.IsLetterOrDigit(simbolo[0]))
            {
                MessageBox.Show("El simbolo debe ser una sola letra o un solo numero");
                return;
            }
            if (automataActual.Alfabeto.Existe(simbolo))
            {
                MessageBox.Show("El simbolo ya existe en el alfabeto");
                return;
            }
            automataActual.Alfabeto.Agregar(simbolo);
            lstAlfabeto.Items.Add(simbolo);
            txtSimbolo.Clear();
            ActualizarComboSimbolos();
        }

        private void ActualizarCombosDeEstados()
        {
            cmbEstadoInicial.Items.Clear();
            cmbOrigen.Items.Clear();
            cmbDestino.Items.Clear();
            chkFinales.Items.Clear();

            for (int i = 0; i < automataActual.Estados.Cantidad; i++)
            {
                string estado = automataActual.Estados.Obtener(i);
                cmbEstadoInicial.Items.Add(estado);
                cmbOrigen.Items.Add(estado);
                cmbDestino.Items.Add(estado);
                chkFinales.Items.Add(estado);
            }
        }

        private void ActualizarComboSimbolos()
        {
            cmbSimboloTransicion.Items.Clear();
            for (int i = 0; i < automataActual.Alfabeto.Cantidad; i++)
            {
                cmbSimboloTransicion.Items.Add(automataActual.Alfabeto.Obtener(i));
            }
        }

        private void BtnAgregarTransicion_Click(object sender, EventArgs e)
        {
            if (cmbOrigen.SelectedItem == null || cmbSimboloTransicion.SelectedItem == null || cmbDestino.SelectedItem == null)
            {
                MessageBox.Show("Selecciona origen, simbolo y destino para agregar la transicion");
                return;
            }

            string origen = cmbOrigen.SelectedItem.ToString();
            string simbolo = cmbSimboloTransicion.SelectedItem.ToString();
            string destino = cmbDestino.SelectedItem.ToString();

            for (int i = 0; i < automataActual.Transiciones.Cantidad; i++)
            {
                Transicion existente = automataActual.Transiciones.Obtener(i);
                if (existente.EstadoOrigen == origen && existente.Simbolo == simbolo)
                {
                    MessageBox.Show("Ya existe una transicion para ese estado y simbolo");
                    return;
                }
            }

            Transicion nueva = new Transicion(origen, simbolo, destino);
            automataActual.Transiciones.Agregar(nueva);
            dgvTransiciones.Rows.Add(origen, simbolo, destino);
        }

        private void BtnValidar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (nombre == "")
            {
                MessageBox.Show("Escribe un nombre para el automata");
                return;
            }
            automataActual.Nombre = nombre;
            automataActual.EstadoInicial = cmbEstadoInicial.SelectedItem != null ? cmbEstadoInicial.SelectedItem.ToString() : null;

            ListaEnlazada<string> finalesSeleccionados = new ListaEnlazada<string>();
            for (int i = 0; i < chkFinales.Items.Count; i++)
            {
                if (chkFinales.GetItemChecked(i))
                {
                    finalesSeleccionados.Agregar(chkFinales.Items[i].ToString());
                }
            }
            automataActual.EstadosFinales = finalesSeleccionados;

            Validador validador = new Validador();
            ResultadoValidacion resultado = validador.Validar(automataActual);

            lstErrores.Items.Clear();

            if (resultado.EsValido)
            {
                lblResultado.Text = "Automata valido";
                lblResultado.ForeColor = Color.FromArgb(60, 150, 100);
                btnGuardar.Enabled = true;
            }
            else
            {
                lblResultado.Text = "Automata invalido, revisa los errores";
                lblResultado.ForeColor = Color.FromArgb(190, 60, 70);
                btnGuardar.Enabled = false;

                for (int i = 0; i < resultado.Errores.Cantidad; i++)
                {
                    lstErrores.Items.Add(resultado.Errores.Obtener(i));
                }
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (Sesion.ExisteNombre(automataActual.Nombre))
            {
                MessageBox.Show("Ya existe un automata guardado con ese nombre, cambia el nombre antes de guardar");
                return;
            }

            Sesion.Automatas.Agregar(automataActual);
            MessageBox.Show("Automata '" + automataActual.Nombre + "' guardado correctamente");
            Close();
        }
    }
}