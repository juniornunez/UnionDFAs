// Este es un control personalizado que dibujamos nosotros mismos, usado como tarjeta de menu
// en la pantalla principal, con un titulo, un subtitulo y un efecto de color cuando el mouse pasa encima.
using System.Drawing.Drawing2D;

namespace UnionDFAs.Controles
{
    public class BotonMenu : Panel
    {
        private string titulo;
        private string subtitulo;
        private Color colorAcento;
        private bool hover;

        public event EventHandler? AccionClick;

        public BotonMenu(string titulo, string subtitulo, Color colorAcento)
        {
            this.titulo = titulo;
            this.subtitulo = subtitulo;
            this.colorAcento = colorAcento;
            hover = false;

            DoubleBuffered = true;
            Size = new Size(260, 140);
            Cursor = Cursors.Hand;

            MouseEnter += (s, e) => { hover = true; Invalidate(); };
            MouseLeave += (s, e) => { hover = false; Invalidate(); };
            Click += (s, e) => AccionClick?.Invoke(this, EventArgs.Empty);
            Paint += BotonMenu_Paint;
        }

        private void BotonMenu_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color fondo = hover ? Color.FromArgb(248, 248, 250) : Color.White;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radio = 18;

            using (GraphicsPath ruta = ObtenerRutaRedondeada(rect, radio))
            {
                using (SolidBrush brochaFondo = new SolidBrush(fondo))
                {
                    g.FillPath(brochaFondo, ruta);
                }
                using (Pen lapiz = new Pen(hover ? Color.FromArgb(150, 152, 160) : Color.FromArgb(224, 226, 230), 1.5F))
                {
                    g.DrawPath(lapiz, ruta);
                }
            }

            using (SolidBrush brochaBarra = new SolidBrush(colorAcento))
            {
                g.FillRectangle(brochaBarra, 20, 24, 4, 32);
            }

            using (Font fuenteTitulo = new Font("Segoe UI", 13F, FontStyle.Bold))
            using (SolidBrush brochaTitulo = new SolidBrush(Color.FromArgb(35, 37, 43)))
            {
                g.DrawString(titulo, fuenteTitulo, brochaTitulo, new PointF(36, 20));
            }

            using (Font fuenteSubtitulo = new Font("Segoe UI", 9.5F))
            using (SolidBrush brochaSubtitulo = new SolidBrush(Color.FromArgb(130, 133, 145)))
            {
                Rectangle areaTexto = new Rectangle(24, 62, Width - 48, Height - 74);
                g.DrawString(subtitulo, fuenteSubtitulo, brochaSubtitulo, areaTexto);
            }
        }

        private GraphicsPath ObtenerRutaRedondeada(Rectangle rect, int radio)
        {
            GraphicsPath ruta = new GraphicsPath();
            int d = radio * 2;
            ruta.AddArc(rect.X, rect.Y, d, d, 180, 90);
            ruta.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            ruta.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            ruta.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            ruta.CloseFigure();
            return ruta;
        }
    }
}