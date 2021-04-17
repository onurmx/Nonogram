using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Nonogram
{
    public class Logic
    {
        public static Control Lcontrol;
        public static bool Return = false;

        public static int SizeX = 10;
        public static int SizeY = 10;

        public static List<Button> Buttons = new List<Button>();
        public static List<Label> HorizontalLabels = new List<Label>();
        public static List<Label> VerticalLabels = new List<Label>();
        public static List<bool> Solution = new List<bool>();
        public static List<bool> Answer = new List<bool>();

        public static Label CongratulationsLabel = new Label();

        public static void CreateButtons(Control control)
        {
            Random rand = new Random();
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    Button newButton = new Button();
                    control.Controls.Add(newButton);
                    newButton.Text = "";
                    newButton.Location = new Point((control.ClientSize.Width / 2 - ((SizeX * 30) / 2)) + i * 30,
                                                   (control.ClientSize.Height / 2 - ((SizeY * 30) / 2)) + j * 30);
                    newButton.Size = new Size(30, 30);
                    if (rand.Next(0, 2) < 1)
                    {
                        Solution.Add(true);
                    }
                    else
                    {
                        Solution.Add(false);
                    }
                    Answer.Add(false);
                    newButton.BackColor = Color.White;
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.FlatAppearance.BorderColor = Color.Black;
                    newButton.FlatAppearance.BorderSize = 1;
                    newButton.MouseDown += Button_MouseDown;
                    Buttons.Add(newButton);
                }
            }
        }

        public static void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (((Button)sender).BackColor == Color.White && ((Button)sender).BackgroundImage == null)
                {
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).BackgroundImage = null;
                    Answer[Buttons.IndexOf(((Button)sender))] = true;
                }
                else if (((Button)sender).BackColor == Color.Black && ((Button)sender).BackgroundImage == null)
                {
                    ((Button)sender).BackColor = Color.White;
                    ((Button)sender).BackgroundImage = null;
                    Answer[Buttons.IndexOf(((Button)sender))] = false;
                }
                else if (((Button)sender).BackColor == Color.White && ((Button)sender).BackgroundImage != null)
                {
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).BackgroundImage = null;
                    Answer[Buttons.IndexOf(((Button)sender))] = true;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (((Button)sender).BackColor == Color.White && ((Button)sender).BackgroundImage == null)
                {
                    ((Button)sender).BackColor = Color.White;
                    ((Button)sender).BackgroundImage = Nonogram.Properties.Resources.cross;
                    Answer[Buttons.IndexOf(((Button)sender))] = false;
                }
                else if (((Button)sender).BackColor == Color.Black && ((Button)sender).BackgroundImage == null)
                {
                    ((Button)sender).BackColor = Color.White;
                    ((Button)sender).BackgroundImage = Nonogram.Properties.Resources.cross;
                    Answer[Buttons.IndexOf(((Button)sender))] = false;
                }
                else if (((Button)sender).BackColor == Color.White && ((Button)sender).BackgroundImage != null)
                {
                    ((Button)sender).BackColor = Color.White;
                    ((Button)sender).BackgroundImage = null;
                    Answer[Buttons.IndexOf(((Button)sender))] = false;
                }
            }
            CheckIfGameEnd();
        }

        public static void CreateLabels(Control control)
        {
            List<List<int>> Horizontal = new List<List<int>>();
            List<string> HorizontalLabelStrings = new List<string>();
            int HorizontalCounter;
            for (int j = 0; j < SizeY; j++)
            {
                List<int> tmp = new List<int>();
                HorizontalCounter = 0;
                for (int i = 0; i < SizeX; i++)
                {
                    if (Solution[SizeY * i + j] == true)
                    {
                        HorizontalCounter++;
                        if (i == SizeX - 1)
                        {
                            tmp.Add(HorizontalCounter);
                        }
                    }
                    else
                    {
                        if (HorizontalCounter != 0)
                        {
                            tmp.Add(HorizontalCounter);
                        }
                        HorizontalCounter = 0;
                    }
                }
                Horizontal.Add(tmp);
            }
            List<List<int>> Vertical = new List<List<int>>();
            List<string> VerticalLabelStrings = new List<string>();
            int VerticalCounter;
            for (int i = 0; i < SizeX; i++)
            {
                List<int> tmp2 = new List<int>();
                VerticalCounter = 0;
                for (int j = 0; j < SizeY; j++)
                {

                    if (Solution[i * SizeY + j] == true)
                    {
                        VerticalCounter++;
                        if (j == SizeY - 1)
                        {
                            tmp2.Add(VerticalCounter);
                        }
                    }
                    else
                    {
                        if (VerticalCounter != 0)
                        {
                            tmp2.Add(VerticalCounter);
                        }
                        VerticalCounter = 0;
                    }
                }
                Vertical.Add(tmp2);
            }
            foreach (var h in Horizontal)
            {
                string str = "";
                foreach (var e in h)
                {
                    str += e.ToString() + " ";
                }
                HorizontalLabelStrings.Add(str);
            }
            foreach (var v in Vertical)
            {
                string str = "";
                foreach (var e in v)
                {
                    str += e.ToString() + "\n";
                }
                VerticalLabelStrings.Add(str);
            }
            for (int i = 0; i < SizeY; i++)
            {
                Label NewHorizontalLabel = new Label();
                control.Controls.Add(NewHorizontalLabel);
                NewHorizontalLabel.AutoSize = true;
                NewHorizontalLabel.Text = HorizontalLabelStrings[i] == "" ? "0" : HorizontalLabelStrings[i];
                NewHorizontalLabel.Location = new Point(Buttons[i].Location.X - NewHorizontalLabel.Size.Width,
                                                        Buttons[i].Location.Y + Buttons[i].Size.Height / 2 - NewHorizontalLabel.Size.Height / 2);
                HorizontalLabels.Add(NewHorizontalLabel);
            }
            for (int i = 0; i < SizeX; i++)
            {
                Label NewVerticalLabel = new Label();
                control.Controls.Add(NewVerticalLabel);
                NewVerticalLabel.AutoSize = true;
                NewVerticalLabel.Text = VerticalLabelStrings[i] == "" ? "0" : VerticalLabelStrings[i];
                NewVerticalLabel.Location = new Point(Buttons[i * SizeY].Location.X + Buttons[i * SizeY].Size.Width / 2 - NewVerticalLabel.Size.Width / 2,
                                                      Buttons[i * SizeY].Location.Y - NewVerticalLabel.Size.Height);
                VerticalLabels.Add(NewVerticalLabel);
            }
        }

        public static void CheckIfGameEnd()
        {
            if (Solution.SequenceEqual(Answer))
            {
                foreach (var b in Buttons)
                {
                    b.Enabled = false;
                }
                CongratulationsLabel = new Label();
                Lcontrol.Controls.Add(CongratulationsLabel);
                CongratulationsLabel.AutoSize = true;
                CongratulationsLabel.Text = "Congratulations!";
                CongratulationsLabel.Font = new Font("Arial", 25, FontStyle.Regular);
                CongratulationsLabel.Location = new Point(Lcontrol.ClientSize.Width / 2 - CongratulationsLabel.Size.Width / 2,
                                                          Lcontrol.ClientSize.Height - CongratulationsLabel.Size.Height);
            }
        }

        public static void LoadLogic(int SizeX, int SizeY, List<bool> Solution, Control control)
        {
            Logic.SizeX = SizeX;
            Logic.SizeY = SizeY;
            Logic.Solution = Solution;
            Answer.AddRange(Enumerable.Repeat(false, Logic.SizeX * Logic.SizeY));
            Return = false;
            LoadButtons(control);
            CreateLabels(control);
        }

        public static void LoadButtons(Control control)
        {
            Random rand = new Random();
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    Button newButton = new Button();
                    control.Controls.Add(newButton);
                    newButton.Text = "";
                    newButton.Location = new Point((control.ClientSize.Width / 2 - ((SizeX * 30) / 2)) + i * 30,
                                                   (control.ClientSize.Height / 2 - ((SizeY * 30) / 2)) + j * 30);
                    newButton.Size = new Size(30, 30);
                    newButton.BackColor = Color.White;
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.FlatAppearance.BorderColor = Color.Black;
                    newButton.FlatAppearance.BorderSize = 1;
                    newButton.MouseDown += Button_MouseDown;
                    Buttons.Add(newButton);
                }
            }
        }

        public static void RemoveButtons(Control control)
        {
            foreach (var b in Buttons)
            {
                b.MouseClick -= Button_MouseDown;
                control.Controls.Remove(b);
                b.Dispose();
            }
            Buttons.Clear();
        }

        public static void RemoveLabels(Control control)
        {
            foreach (var l in HorizontalLabels)
            {
                control.Controls.Remove(l);
                l.Dispose();
            }
            HorizontalLabels.Clear();
            foreach (var l in VerticalLabels)
            {
                control.Controls.Remove(l);
                l.Dispose();
            }
            VerticalLabels.Clear();
        }

        public static void RemoveCongratulationsLabel(Control control)
        {
            control.Controls.Remove(CongratulationsLabel);
            CongratulationsLabel.Dispose();
        }

        public static void ClearSolutionAndAnswer()
        {
            Solution.Clear();
            Answer.Clear();
        }
    }
}
