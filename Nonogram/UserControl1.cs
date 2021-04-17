using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Nonogram
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveLogic.RetrieveInformation(textBox1.Text,
                                          textBox2.Text,
                                          CreateLogic.SizeX,
                                          CreateLogic.SizeY,
                                          CreateLogic.Solution);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Created Game";
            saveFileDialog.Filter = "XML Save File|*.xml";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!saveFileDialog.FileName.Equals(""))
                {
                    System.IO.FileStream fileStream = null;
                    try
                    {
                        using (fileStream = (System.IO.FileStream)saveFileDialog.OpenFile())
                        {
                            XmlSerializer s = new XmlSerializer(typeof(SaveLogicStruct_t));
                            s.Serialize(fileStream, SaveLogic.SaveLogicStruct);
                        }
                    }
                    finally
                    {
                        if (fileStream != null)
                        {
                            fileStream.Dispose();
                        }
                    }
                }
            }
        }
    }
}
