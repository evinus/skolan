using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace kontroller
{
    class LabelTextbox : TextBox
    {
        private string ledText;

        [Category("Appearance"), Description("Label text."), DefaultValue("...")]
        public string LedText
        {
            get { return ledText; }
            set { ledText = value; Text = value; ForeColor = System.Drawing.Color.Gray; }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (this.Text == LedText)
            {
                this.Text = "";
                ForeColor = Color.Black;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (this.Text == "")
            {
                this.Text = LedText;
                ForeColor = Color.Gray;
            }
        }
    }
}
