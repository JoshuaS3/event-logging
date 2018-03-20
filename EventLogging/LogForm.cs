using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace EventLogging
{
    class LogForm
    {
        public static Form Window = null;

        public static void InitializeWindow()
        {

            Window = new Form();
            Window.Size = new Size(500, 500);
            MenuStrip menu = new MenuStrip();
            menu.Location = new Point(0, 0);
            menu.Size = new Size(500, 0);
            ToolStripItem fileButton = menu.Items.Add("File");
            ToolStripItem editbutton = menu.Items.Add("Edit");
            ToolStripItem helpbutton = menu.Items.Add("Help");

            TextBox context = new TextBox();
            context.Parent = Window;
            context.Location = new Point(0, 24);
            context.Size = new Size(500, 486);
            context.Multiline = true;


            menu.Parent = Window;
            Window.ShowDialog();
        }
    }
}
