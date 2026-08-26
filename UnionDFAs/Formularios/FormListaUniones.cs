using UnionDFAs.Controles;
using UnionDFAs.Logica;
using UnionDFAs.Modelo;

namespace UnionDFAs.Formularios
{
    public class FormListaUniones : Form
    {
        private FlowLayoutPanel panelTarjetas;

        public FormListaUniones()
        {
            ConstruirInterfaz();
            CargarTarjetas();
        }

        private void ConstruirInterfaz()
        {
            Text = "Uniones Generadas";
            BackColor = Color.FromArgb(250, 250, 252);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(950, 650);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(40, 42, 48);

            Label titulo = new Label();
            titulo.Text = "Uniones Generadas";
            titulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);
            Controls.Add(titulo);

            Label subtitulo = new Label();
            subtitulo.Text = "Haz clic en una tarjeta para ver el detalle de esa union";
            subtitulo.ForeColor = Color.FromArgb(130, 133, 145);
            subtitulo.AutoSize = true;
            subtitulo.Location = new Point(30, 55);
            Controls.Add(subtitulo);

            panelTarjetas = new FlowLayoutPanel();
            panelTarjetas.Location = new Point(30, 90);
            panelTarjetas.Size = new Size(890, 520);
            panelTarjetas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelTarjetas.AutoScroll = true;
            panelTarjetas.BackColor = Color.FromArgb(250, 250, 252);
            Controls.Add(panelTarjetas);
        }

        private void CargarTarjetas()
        {
            panelTarjetas.Controls.Clear();

            for (int i = 0; i < Sesion.Uniones.Cantidad; i++)
            {
                UnionGuardada union = Sesion.Uniones.Obtener(i);
                TarjetaUnion tarjeta = new TarjetaUnion(union);
                tarjeta.SolicitudVerDetalle += (s, e) => VerDetalle(union);
                tarjeta.SolicitudEliminar += (s, e) => EliminarUnion(union);
                panelTarjetas.Controls.Add(tarjeta);
            }
        }

        private void VerDetalle(UnionGuardada union)
        {
            FormResultadoUnion formulario = new FormResultadoUnion(union.AutomataResultante);
            formulario.ShowDialog();
        }

        private void EliminarUnion(UnionGuardada union)
        {
            DialogResult respuesta = MessageBox.Show("Eliminar la union '" + union.AutomataResultante.Nombre + "'?", "Confirmar", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                Sesion.Uniones.EliminarPorValor(union);
                RepositorioArchivos.Guardar();
                CargarTarjetas();
            }
        }
    }
}