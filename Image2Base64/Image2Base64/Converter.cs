using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image2Base64
{
    public partial class Converter : Form
    {
        public Converter()
        {
            InitializeComponent();
        }

        private void Converter_Load(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        using (Image image = Image.FromFile(filePath))
                        {
                            using (MemoryStream m = new MemoryStream())
                            {
                                image.Save(m, image.RawFormat);
                                byte[] imageBytes = m.ToArray();
                                string base64String = Convert.ToBase64String(imageBytes);
                                SaveFileDialog savefile = new SaveFileDialog();
                                savefile.FileName = "Converted.base64";
                                savefile.Filter = "Text files (*.base64)|*.base64|All files (*.*)|*.*";

                                if (savefile.ShowDialog() == DialogResult.OK)
                                {
                                    using (StreamWriter sw = new StreamWriter(savefile.FileName))
                                        sw.WriteLine(base64String);
                                }
                                Application.Exit();
                            }
                        }
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}
