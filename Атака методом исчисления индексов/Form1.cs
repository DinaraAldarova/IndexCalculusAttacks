using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Атака_методом_исчисления_индексов
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Culc culc = new Culc();
            richTextBox1.Text = culc.report;
        }
    }
}
