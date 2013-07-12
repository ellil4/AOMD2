using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOMD2Analyzer
{
    public partial class ProgressWnd : Form
    {
        public ProgressWnd(int max)
        {
            InitializeComponent();
            amProgBar.Maximum = max;
        }

        public void SetProgress()
        {
            amProgBar.Value += 100;
        }
    }
}
