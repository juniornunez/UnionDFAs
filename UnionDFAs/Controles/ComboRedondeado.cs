namespace UnionDFAs.Controles
{
    public class ComboRedondeado : ComboBox
    {
        public ComboRedondeado()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 24;
            BackColor = Color.White;
            ForeColor = Color.FromArgb(40, 42, 48);
            FlatStyle = FlatStyle.Flat;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 && SelectedIndex < 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                return;
            }

            Color colorFondo = Color.White;
            Color colorTexto = Color.FromArgb(40, 42, 48);

            bool esAreaCerrada = (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit;

            if (!esAreaCerrada && (e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                colorFondo = Color.FromArgb(240, 241, 244);
            }

            using (SolidBrush brochaFondo = new SolidBrush(colorFondo))
            {
                e.Graphics.FillRectangle(brochaFondo, e.Bounds);
            }

            string texto;
            if (e.Index >= 0)
            {
                texto = Items[e.Index].ToString();
            }
            else
            {
                texto = SelectedItem != null ? SelectedItem.ToString() : "";
            }

            Rectangle areaTexto = e.Bounds;
            TextFormatFlags formato = TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix;

            if (esAreaCerrada)
            {
                areaTexto = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 18, e.Bounds.Height);
                formato = formato | TextFormatFlags.HorizontalCenter;
            }
            else
            {
                areaTexto = new Rectangle(e.Bounds.X + 10, e.Bounds.Y, e.Bounds.Width - 10, e.Bounds.Height);
                formato = formato | TextFormatFlags.Left;
            }

            TextRenderer.DrawText(e.Graphics, texto, Font, areaTexto, colorTexto, formato);

            e.DrawFocusRectangle();
        }
    }
}