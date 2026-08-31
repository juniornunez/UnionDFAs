// Este es un boton dibujado por nosotros con las esquinas bien redondeadas, para que se vea
// mas moderno que un boton normal de Windows Forms.
using System.Drawing.Drawing2D;

namespace UnionDFAs.Controles
{
    public class BotonRedondeado : Panel
    {
        private bool esPrimario;
        private bool hover;
        private Label etiqueta;

        public BotonRedondeado(string texto, bool esPrimario = false)
        {
            this.esPrimario = esPrimario;
            hover = false;

            DoubleBuffered = true;
            Size = new Size(140, 36);
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;

            etiqueta = new Label();
            etiqueta.Text = texto;
            etiqueta.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            etiqueta.TextAlign = ContentAlignment.MiddleCenter;
            etiqueta.Dock = DockStyle.Fill;
            etiqueta.BackColor = Color.Transparent;
            etiqueta.Cursor = Cursors.Hand;
            Controls.Add(etiqueta);

            MouseEnter += (s, e) => { hover = true; Invalidate(); };
            MouseLeave += (s, e) => { hover = false; Invalidate(); };
            etiqueta.MouseEnter += (s, e) => { hover = true; Invalidate(); };
            etiqueta.MouseLeave += (s, e) => { hover = false; Invalidate(); };
            etiqueta.Click += (s, e) => OnClick(EventArgs.Empty);
            EnabledChanged += (s, e) => { ActualizarColores(); Invalidate(); };
            Paint += BotonRedondeado_Paint;

            ActualizarColores();
        }

        private void ActualizarColores()
        {
            if (!Enabled)
            {
                etiqueta.ForeColor = Color.FromArgb(180, 182, 188);
            }
            else if (esPrimario)
            {
                etiqueta.ForeColor = Color.White;
            }
            else
            {
                etiqueta.ForeColor = Color.FromArgb(50, 52, 58);
            }
        }

        private void BotonRedondeado_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radio = Height / 2;

            using (GraphicsPath ruta = ObtenerRutaRedondeada(rect, radio))
            {
                Color colorFondo;
                if (!Enabled)
                {
                    colorFondo = Color.FromArgb(235, 236, 239);
                }
                else if (esPrimario)
                {
                    colorFondo = hover ? Color.FromArgb(60, 62, 70) : Color.FromArgb(35, 37, 43);
                }
                else
                {
                    colorFondo = hover ? Color.FromArgb(240, 241, 244) : Color.White;
                }

                using (SolidBrush brocha = new SolidBrush(colorFondo))
                {
                    g.FillPath(brocha, ruta);
                }

                if (!esPrimario)
                {
                    using (Pen lapiz = new Pen(Color.FromArgb(210, 212, 217), 1.5F))
                    {
                        g.DrawPath(lapiz, ruta);
                    }
                }
            }
        }

        private GraphicsPath ObtenerRutaRedondeada(Rectangle rect, int radio)
        {
            GraphicsPath ruta = new GraphicsPath();
            int d = radio * 2;
            ruta.AddArc(rect.X, rect.Y, d, d, 90, 180);
            ruta.AddArc(rect.Right - d, rect.Y, d, d, 270, 180);
            ruta.CloseFigure();
            return ruta;
        }
    }
}