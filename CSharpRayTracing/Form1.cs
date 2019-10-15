using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeavyRayTracing
{
    public partial class Form1 : Form
    {
        public Form1(Bitmap pic)
        {
            // display a render of the scene
            InitializeComponent();
            pictureBox1.Image = pic;
        }
    }
}
