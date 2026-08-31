// Esta es la tarjeta visual que representa un automata guardado en la pantalla principal,
// mostrando su nombre y su informacion basica, con un boton de eliminar y la opcion de
// hacer clic para ver el detalle completo.
using System.Drawing.Drawing2D;
using UnionDFAs.Modelo;

namespace UnionDFAs.Controles
{
    public class TarjetaAutomata : Panel
    {
        private Automata automata;
        private bool hover;
        private Rectangle areaBotonEliminar;

        public event EventHandler? SolicitudEliminar;
        public event EventHandler? SolicitudVerDetalle;

        public Automata Automata => automata;

        public TarjetaAutomata(Automata automata)
        {
            this.automata = automata;
            hover = false;

            DoubleBuffered = true;
            Size = new Size(220, 110);
            Margin = new Padding(10);
            Cursor = Cursors.Hand;

            MouseEnter += (s, e) => { hover = true; Invalidate(); };
            MouseLeave += (s, e) => { hover = false; Invalidate(); };
            MouseClick += TarjetaAutomata_MouseClick;
            Paint += TarjetaAutomata_Paint;
        }

        private void TarjetaAutomata_MouseClick(object sender, MouseEventArgs e)
        {
            if (areaBotonEliminar.Contains(e.Location))
            {
                SolicitudEliminar?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                SolicitudVerDetalle?.Invoke(this, EventArgs.Empty);
            }
        }

        private void TarjetaAutomata_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radio = 14;

            using (GraphicsPath ruta = ObtenerRutaRedondeada(rect, radio))
            {
                Color fondo = hover ? Color.FromArgb(248, 248, 250) : Color.White;
                using (SolidBrush brochaFondo = new SolidBrush(fondo))
                {
                    g.FillPath(brochaFondo, ruta);
                }
                using (Pen lapiz = new Pen(Color.FromArgb(224, 226, 230), 1.5F))
                {
                    g.DrawPath(lapiz, ruta);
                }
            }

            using (Font fuenteNombre = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (SolidBrush brochaNombre = new SolidBrush(Color.FromArgb(35, 37, 43)))
            {
                g.DrawString(automata.Nombre, fuenteNombre, brochaNombre, new PointF(16, 14));
            }

            string infoEstados = "Estados: " + automata.Estados.Cantidad;
            string infoAlfabeto = "Alfabeto: " + automata.Alfabeto.Cantidad;
            string infoInicial = "Inicial: " + automata.EstadoInicial;
            string infoFinales = "Finales: " + automata.EstadosFinales.Cantidad;

            using (Font fuenteInfo = new Font("Segoe UI", 9F))
            using (SolidBrush brochaInfo = new SolidBrush(Color.FromArgb(130, 133, 145)))
            {
                g.DrawString(infoEstados, fuenteInfo, brochaInfo, new PointF(16, 46));
                g.DrawString(infoAlfabeto, fuenteInfo, brochaInfo, new PointF(16, 64));
                g.DrawString(infoInicial, fuenteInfo, brochaInfo, new PointF(120, 46));
                g.DrawString(infoFinales, fuenteInfo, brochaInfo, new PointF(120, 64));
            }

            areaBotonEliminar = new Rectangle(Width - 34, 10, 22, 22);
            using (SolidBrush brochaX = new SolidBrush(Color.FromArgb(190, 60, 70)))
            using (Font fuenteX = new Font("Segoe UI", 10F, FontStyle.Bold))
            {
                g.DrawString("x", fuenteX, brochaX, new PointF(areaBotonEliminar.X + 6, areaBotonEliminar.Y));
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