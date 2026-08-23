using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace UnionDFAs
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SuspendLayout();
            ClientSize = new Size(920, 620);
            Name = "Form1";
            Text = "Sistema de Automatas Finitos Deterministas";
            ResumeLayout(false);
        }
    }
}