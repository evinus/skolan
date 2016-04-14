using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kontroller
{
    class EmailAdrresTextbox : TextBox
    {
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            char[] texten = this.Text.ToCharArray();
            bool hasAt = false;
            bool hasDot = false;
            for(int i=0;i<texten.Length;i++)
            {
                if (texten[i] == '@' || hasAt)
                {
                    hasAt = true;
                    if (texten[i] == '.' || hasDot)
                    {
                        hasDot = true;
                        if (texten[i] == '.')
                        {
                        }
                        else
                            this.BackColor = System.Drawing.Color.Green;
                    }

                }
                else
                    BackColor = System.Drawing.Color.Red;
            }

        }
    }
}
