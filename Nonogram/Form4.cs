using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Nonogram
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ChooseLogic.Files.Clear();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (!folderBrowserDialog.SelectedPath.Equals(""))
                {
                    // I added these extra enabling functionalities because it makes form look more user friendly :)
                    listView1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    textBox1.Text = folderBrowserDialog.SelectedPath;
                    string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.xml");
                    foreach (var file in files)
                    {
                        FileStream fileStream = null;
                        try
                        {
                            using (fileStream = new FileStream($"{file}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                XmlSerializer s = new XmlSerializer(typeof(SaveLogicStruct_t));
                                SaveLogicStruct_t tmp = (SaveLogicStruct_t)s.Deserialize(fileStream);
                                string[] row = { tmp.PuzzleTitle, tmp.SizeX.ToString(), tmp.SizeY.ToString(), tmp.Difficulty };
                                var listViewItem = new ListViewItem(row);
                                listView1.Items.Add(listViewItem);
                                ChooseLogic.Files.Add(fileStream.Name);
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

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ChooseLogic.Files.Clear();
            string[] files = Directory.GetFiles(textBox1.Text, "*.xml");
            foreach (var file in files)
            {
                FileStream fileStream = null;
                try
                {
                    using (fileStream = new FileStream($"{file}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(SaveLogicStruct_t));
                        SaveLogicStruct_t tmp = (SaveLogicStruct_t)s.Deserialize(fileStream);
                        string[] row = { tmp.PuzzleTitle, tmp.SizeX.ToString(), tmp.SizeY.ToString(), tmp.Difficulty };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                        ChooseLogic.Files.Add(fileStream.Name);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Logic.RemoveButtons(Logic.Lcontrol);
            Logic.RemoveLabels(Logic.Lcontrol);
            Logic.ClearSolutionAndAnswer();
            Logic.Return = false;
            Logic.RemoveCongratulationsLabel(Logic.Lcontrol);

            CreateLogic.RemoveButtons(Logic.Lcontrol);
            CreateLogic.RemoveLabels(Logic.Lcontrol);
            CreateLogic.ClearSolution();
            CreateLogic.Return = false;
            CreateLogic.RemoveUserControl(Logic.Lcontrol);


            FileStream fileStream = new FileStream(ChooseLogic.Files[listView1.Items.IndexOf(listView1.SelectedItems[0])],
                                                   FileMode.Open,
                                                   FileAccess.Read,
                                                   FileShare.ReadWrite);
            XmlSerializer s = new XmlSerializer(typeof(SaveLogicStruct_t));
            SaveLogicStruct_t tmp = (SaveLogicStruct_t)s.Deserialize(fileStream);
            Logic.LoadLogic(tmp.SizeX, tmp.SizeY, tmp.Solution, Logic.Lcontrol);
            this.Close();
        }
    }
}