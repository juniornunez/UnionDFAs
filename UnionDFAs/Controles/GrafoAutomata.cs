using System.ComponentModel;
using UnionDFAs.Modelo;

namespace UnionDFAs.Controles
{
    public class GrafoAutomata : Panel
    {
        private Automata automata;
        private Rectangle[] cajas;
        private const int AnchoMinimo = 70;
        private const int AltoCaja = 40;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Automata Automata
        {
            get { return automata; }
            set { automata = value; Invalidate(); }
        }

        public GrafoAutomata()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (automata == null || automata.Estados.Cantidad == 0)
            {
                DibujarMensajeVacio(e.Graphics);
                return;
            }

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            CalcularCajas(g);
            DibujarAristas(g);
            DibujarFlechaInicial(g);
            DibujarCajas(g);
        }

        private void DibujarMensajeVacio(Graphics g)
        {
            using (Font fuente = new Font("Consolas", 10F))
            using (SolidBrush brocha = new SolidBrush(Color.FromArgb(150, 152, 165)))
            {
                string mensaje = "Agrega estados y transiciones para ver el grafo";
                SizeF tamano = g.MeasureString(mensaje, fuente);
                g.DrawString(mensaje, fuente, brocha, (Width - tamano.Width) / 2, (Height - tamano.Height) / 2);
            }
        }

        private const int RadioMinimoBase = 90;
        private const int RadioPorEstadoExtra = 22;

        public Size CalcularTamanoNecesario()
        {
            if (automata == null || automata.Estados.Cantidad <= 1)
                return new Size(400, 300);

            int n = automata.Estados.Cantidad;
            int radio = RadioMinimoBase + n * RadioPorEstadoExtra;
            int margen = 100;
            int lado = radio * 2 + margen;
            return new Size(lado, lado);
        }

        private void CalcularCajas(Graphics g)
        {
            int n = automata.Estados.Cantidad;
            cajas = new Rectangle[n];

            using (Font fuente = new Font("Consolas", 10F, FontStyle.Bold))
            {
                int cx = Width / 2;
                int cy = Height / 2;

                if (n == 1)
                {
                    string estadoUnico = automata.Estados.Obtener(0);
                    SizeF tamanoTexto = g.MeasureString(estadoUnico, fuente);
                    int anchoCaja = Math.Max(AnchoMinimo, (int)tamanoTexto.Width + 26);
                    cajas[0] = new Rectangle(cx - anchoCaja / 2, cy - AltoCaja / 2, anchoCaja, AltoCaja);
                    return;
                }

                int radio = RadioMinimoBase + n * RadioPorEstadoExtra;

                for (int i = 0; i < n; i++)
                {
                    string estado = automata.Estados.Obtener(i);
                    SizeF tamanoTexto = g.MeasureString(estado, fuente);
                    int anchoCaja = Math.Max(AnchoMinimo, (int)tamanoTexto.Width + 26);

                    double angulo = -Math.PI / 2 + i * (2 * Math.PI / n);
                    int x = cx + (int)(radio * Math.Cos(angulo));
                    int y = cy + (int)(radio * Math.Sin(angulo));

                    cajas[i] = new Rectangle(x - anchoCaja / 2, y - AltoCaja / 2, anchoCaja, AltoCaja);
                }
            }
        }

        private void DibujarCajas(Graphics g)
        {
            using (Font fuente = new Font("Consolas", 10F, FontStyle.Bold))
            using (Pen lapizNormal = new Pen(Color.Black, 1f))
            using (Pen lapizInicial = new Pen(Color.Black, 2f))
            using (SolidBrush brochaTexto = new SolidBrush(Color.Black))
            {
                for (int i = 0; i < automata.Estados.Cantidad; i++)
                {
                    string estado = automata.Estados.Obtener(i);
                    Rectangle caja = cajas[i];
                    bool esFinal = automata.EsFinal(estado);
                    bool esInicial = estado == automata.EstadoInicial;

                    using (SolidBrush brochaFondo = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(brochaFondo, caja);
                    }

                    Pen lapizUsado = esInicial ? lapizInicial : lapizNormal;
                    g.DrawRectangle(lapizUsado, caja);

                    if (esFinal)
                    {
                        Rectangle cajaInterna = new Rectangle(caja.X + 4, caja.Y + 4, caja.Width - 8, caja.Height - 8);
                        g.DrawRectangle(lapizNormal, cajaInterna);
                    }

                    SizeF tamanoTexto = g.MeasureString(estado, fuente);
                    float tx = caja.X + (caja.Width - tamanoTexto.Width) / 2;
                    float ty = caja.Y + (caja.Height - tamanoTexto.Height) / 2;
                    g.DrawString(estado, fuente, brochaTexto, tx, ty);
                }
            }
        }

        private void DibujarFlechaInicial(Graphics g)
        {
            if (automata.EstadoInicial == null)
                return;

            int indiceInicial = automata.Estados.ObtenerPosicion(automata.EstadoInicial);
            if (indiceInicial < 0)
                return;

            Rectangle caja = cajas[indiceInicial];
            Point centro = new Point(caja.X + caja.Width / 2, caja.Y + caja.Height / 2);
            int cx = Width / 2;
            int cy = Height / 2;

            float dx = centro.X - cx;
            float dy = centro.Y - cy;
            float distancia = (float)Math.Sqrt(dx * dx + dy * dy);
            if (distancia < 0.01f)
            {
                dx = 0;
                dy = -1;
                distancia = 1;
            }

            float dirX = dx / distancia;
            float dirY = dy / distancia;

            Point puntoBorde = InterseccionConCaja(caja, centro, new Point(centro.X + (int)(dirX * 100), centro.Y + (int)(dirY * 100)));
            Point puntoExterno = new Point(centro.X + (int)(dirX * (Math.Max(caja.Width, caja.Height) / 2 + 40)), centro.Y + (int)(dirY * (Math.Max(caja.Width, caja.Height) / 2 + 40)));

            using (Pen lapiz = new Pen(Color.Black, 1f))
            {
                g.DrawLine(lapiz, puntoExterno, puntoBorde);
            }
            DibujarPuntaFlechaSimple(g, puntoExterno, puntoBorde);
        }

        private void DibujarAristas(Graphics g)
        {
            int n = automata.Estados.Cantidad;
            string[,] etiquetas = new string[n, n];

            for (int k = 0; k < automata.Transiciones.Cantidad; k++)
            {
                Transicion t = automata.Transiciones.Obtener(k);
                int origen = automata.Estados.ObtenerPosicion(t.EstadoOrigen);
                int destino = automata.Estados.ObtenerPosicion(t.EstadoDestino);
                if (origen < 0 || destino < 0)
                    continue;

                if (etiquetas[origen, destino] == null)
                    etiquetas[origen, destino] = t.Simbolo;
                else
                    etiquetas[origen, destino] = etiquetas[origen, destino] + "," + t.Simbolo;
            }

            for (int i = 0; i < n; i++)
            {
                if (etiquetas[i, i] != null)
                {
                    DibujarAutociclo(g, i, etiquetas[i, i]);
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    bool tieneIJ = etiquetas[i, j] != null;
                    bool tieneJI = etiquetas[j, i] != null;

                    if (!tieneIJ && !tieneJI)
                        continue;

                    Point centroI = new Point(cajas[i].X + cajas[i].Width / 2, cajas[i].Y + cajas[i].Height / 2);
                    Point centroJ = new Point(cajas[j].X + cajas[j].Width / 2, cajas[j].Y + cajas[j].Height / 2);

                    float dx = centroJ.X - centroI.X;
                    float dy = centroJ.Y - centroI.Y;
                    float distancia = (float)Math.Sqrt(dx * dx + dy * dy);
                    if (distancia < 0.01f)
                        distancia = 0.01f;

                    float perpX = -(dy / distancia);
                    float perpY = dx / distancia;

                    if (tieneIJ)
                    {
                        int offset = tieneJI ? 10 : 0;
                        DibujarArista(g, i, j, etiquetas[i, j], perpX, perpY, offset);
                    }
                    if (tieneJI)
                    {
                        int offset = tieneIJ ? 10 : 0;
                        DibujarArista(g, j, i, etiquetas[j, i], perpX, perpY, -offset);
                    }
                }
            }
        }

        private Point InterseccionConCaja(Rectangle caja, Point centro, Point haciaAfuera)
        {
            float dx = haciaAfuera.X - centro.X;
            float dy = haciaAfuera.Y - centro.Y;

            if (Math.Abs(dx) < 0.001f && Math.Abs(dy) < 0.001f)
                return centro;

            float mitadAncho = caja.Width / 2f;
            float mitadAlto = caja.Height / 2f;

            float escalaX = dx != 0 ? mitadAncho / Math.Abs(dx) : float.MaxValue;
            float escalaY = dy != 0 ? mitadAlto / Math.Abs(dy) : float.MaxValue;
            float escala = Math.Min(escalaX, escalaY);

            return new Point((int)(centro.X + dx * escala), (int)(centro.Y + dy * escala));
        }

        private void DibujarArista(Graphics g, int origen, int destino, string etiqueta, float perpX, float perpY, int offsetPerp)
        {
            Rectangle cajaA = cajas[origen];
            Rectangle cajaB = cajas[destino];
            Point centroA = new Point(cajaA.X + cajaA.Width / 2, cajaA.Y + cajaA.Height / 2);
            Point centroB = new Point(cajaB.X + cajaB.Width / 2, cajaB.Y + cajaB.Height / 2);

            Point centroAAjustado = new Point((int)(centroA.X + perpX * offsetPerp), (int)(centroA.Y + perpY * offsetPerp));
            Point centroBAjustado = new Point((int)(centroB.X + perpX * offsetPerp), (int)(centroB.Y + perpY * offsetPerp));

            Point inicio = InterseccionConCaja(cajaA, centroA, centroBAjustado);
            Point fin = InterseccionConCaja(cajaB, centroB, centroAAjustado);

            Point puntoMedio = new Point((inicio.X + fin.X) / 2 + (int)(perpX * offsetPerp * 2), (inicio.Y + fin.Y) / 2 + (int)(perpY * offsetPerp * 2));

            using (Pen lapiz = new Pen(Color.Black, 1f))
            {
                if (offsetPerp == 0)
                {
                    g.DrawLine(lapiz, inicio, fin);
                }
                else
                {
                    g.DrawLine(lapiz, inicio, puntoMedio);
                    g.DrawLine(lapiz, puntoMedio, fin);
                }
            }

            DibujarPuntaFlechaSimple(g, offsetPerp == 0 ? inicio : puntoMedio, fin);

            using (Font fuenteEtiqueta = new Font("Consolas", 9F, FontStyle.Bold))
            using (SolidBrush brochaFondo = new SolidBrush(Color.White))
            using (SolidBrush brochaTexto = new SolidBrush(Color.Black))
            {
                Point puntoEtiquetaBase = offsetPerp == 0 ? new Point((inicio.X + fin.X) / 2, (inicio.Y + fin.Y) / 2) : puntoMedio;
                SizeF tamano = g.MeasureString(etiqueta, fuenteEtiqueta);
                PointF puntoEtiqueta = new PointF(puntoEtiquetaBase.X - tamano.Width / 2, puntoEtiquetaBase.Y - tamano.Height / 2);
                RectangleF fondoEtiqueta = new RectangleF(puntoEtiqueta.X - 3, puntoEtiqueta.Y - 1, tamano.Width + 6, tamano.Height + 2);
                g.FillRectangle(brochaFondo, fondoEtiqueta);
                g.DrawString(etiqueta, fuenteEtiqueta, brochaTexto, puntoEtiqueta);
            }
        }

        private void DibujarAutociclo(Graphics g, int indice, string etiqueta)
        {
            Rectangle caja = cajas[indice];
            Point cimaCaja = new Point(caja.X + caja.Width / 2, caja.Y);

            int mitadAncho = 16;
            int alto = 22;

            Point p1 = new Point(cimaCaja.X - mitadAncho, cimaCaja.Y);
            Point p2 = new Point(cimaCaja.X - mitadAncho, cimaCaja.Y - alto);
            Point p3 = new Point(cimaCaja.X + mitadAncho, cimaCaja.Y - alto);
            Point p4 = new Point(cimaCaja.X + mitadAncho, cimaCaja.Y);

            using (Pen lapiz = new Pen(Color.Black, 1f))
            {
                g.DrawLine(lapiz, p1, p2);
                g.DrawLine(lapiz, p2, p3);
                g.DrawLine(lapiz, p3, p4);
            }

            DibujarPuntaFlechaSimple(g, p3, p4);

            using (Font fuenteEtiqueta = new Font("Consolas", 9F, FontStyle.Bold))
            using (SolidBrush brochaTexto = new SolidBrush(Color.Black))
            {
                SizeF tamano = g.MeasureString(etiqueta, fuenteEtiqueta);
                g.DrawString(etiqueta, fuenteEtiqueta, brochaTexto, cimaCaja.X - tamano.Width / 2, cimaCaja.Y - alto - tamano.Height - 2);
            }
        }

        private void DibujarPuntaFlechaSimple(Graphics g, Point desde, Point hacia)
        {
            float dx = hacia.X - desde.X;
            float dy = hacia.Y - desde.Y;
            float distancia = (float)Math.Sqrt(dx * dx + dy * dy);
            if (distancia < 0.01f)
                return;

            float dirX = dx / distancia;
            float dirY = dy / distancia;
            float perpX = -dirY;
            float perpY = dirX;

            int tamanoFlecha = 7;
            Point lado1 = new Point((int)(hacia.X - dirX * tamanoFlecha + perpX * tamanoFlecha * 0.6f), (int)(hacia.Y - dirY * tamanoFlecha + perpY * tamanoFlecha * 0.6f));
            Point lado2 = new Point((int)(hacia.X - dirX * tamanoFlecha - perpX * tamanoFlecha * 0.6f), (int)(hacia.Y - dirY * tamanoFlecha - perpY * tamanoFlecha * 0.6f));

            using (Pen lapiz = new Pen(Color.Black, 1f))
            {
                g.DrawLine(lapiz, hacia, lado1);
                g.DrawLine(lapiz, hacia, lado2);
            }
        }
    }
}