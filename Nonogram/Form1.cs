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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Logic.Lcontrol = this;
            CreateLogic.CLcontrol = this;

            Logic.CreateButtons(Logic.Lcontrol);
            Logic.CreateLabels(Logic.Lcontrol);
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if (Logic.Return)
            {
                Logic.RemoveButtons(Logic.Lcontrol);
                Logic.RemoveLabels(Logic.Lcontrol);
                Logic.ClearSolutionAndAnswer();
                Logic.Return = false;
                Logic.RemoveCongratulationsLabel(Logic.Lcontrol);

                CreateLogic.RemoveButtons(CreateLogic.CLcontrol);
                CreateLogic.RemoveLabels(CreateLogic.CLcontrol);
                CreateLogic.ClearSolution();
                CreateLogic.Return = false;
                CreateLogic.RemoveUserControl(CreateLogic.CLcontrol);


                Logic.CreateButtons(Logic.Lcontrol);
                Logic.CreateLabels(Logic.Lcontrol);
            }
        }

        private void createPuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
            if (CreateLogic.Return)
            {
                Logic.RemoveButtons(Logic.Lcontrol);
                Logic.RemoveLabels(Logic.Lcontrol);
                Logic.ClearSolutionAndAnswer();
                Logic.Return = false;
                Logic.RemoveCongratulationsLabel(Logic.Lcontrol);

                CreateLogic.RemoveButtons(CreateLogic.CLcontrol);
                CreateLogic.RemoveLabels(CreateLogic.CLcontrol);
                CreateLogic.ClearSolution();
                CreateLogic.Return = false;
                CreateLogic.RemoveUserControl(CreateLogic.CLcontrol);


                CreateLogic.CreateButtons(CreateLogic.CLcontrol);
                CreateLogic.CreateLabels(CreateLogic.CLcontrol);
                CreateLogic.CreateUserControl(CreateLogic.CLcontrol);
            }
        }

        private void loadPuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Load Puzzle";
            openFileDialog.Filter = "XML Save File|*.xml";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!openFileDialog.FileName.Equals(""))
                {
                    FileStream fileStream = null;
                    try
                    {
                        using (fileStream = new FileStream($"{openFileDialog.FileName}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            Logic.RemoveButtons(Logic.Lcontrol);
                            Logic.RemoveLabels(Logic.Lcontrol);
                            Logic.ClearSolutionAndAnswer();
                            Logic.Return = false;
                            Logic.RemoveCongratulationsLabel(Logic.Lcontrol);

                            CreateLogic.RemoveButtons(CreateLogic.CLcontrol);
                            CreateLogic.RemoveLabels(CreateLogic.CLcontrol);
                            CreateLogic.ClearSolution();
                            CreateLogic.Return = false;
                            CreateLogic.RemoveUserControl(CreateLogic.CLcontrol);


                            XmlSerializer s = new XmlSerializer(typeof(SaveLogicStruct_t));
                            SaveLogicStruct_t tmp = (SaveLogicStruct_t)s.Deserialize(fileStream);
                            Logic.LoadLogic(tmp.SizeX, tmp.SizeY, tmp.Solution, Logic.Lcontrol);
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

        private void choosePuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
    }
}
