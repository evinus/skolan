﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fontstyle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontStyle stil = FontStyle.Regular;

            if (cbxFet.Checked) stil = stil | FontStyle.Bold;
            if (cbxKursiv.Checked) stil = stil | FontStyle.Italic;
            if (cbxUnderstryken.Checked) stil = stil | FontStyle.Underline;
            if (cbxÖverstruken.Checked) stil = stil | FontStyle.Strikeout;
           
        }
    }
}
