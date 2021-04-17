using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nonogram
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "10";
            textBox2.Text = "10";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                Logic.SizeX = Int32.Parse(textBox1.Text);
                Logic.SizeY = Int32.Parse(textBox2.Text);
                Logic.Return = true;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logic.Return = false;
            this.Close();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (Int32.Parse(textBox1.Text) < 2 || Int32.Parse(textBox1.Text) > 15)
            {
                errorProvider1.SetError(textBox1, "Width must be integer number in range 2-15");
                e.Cancel = true;
                return;
            }
            errorProvider1.SetError(textBox1, string.Empty);
            e.Cancel = false;
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (Int32.Parse(textBox2.Text) < 2 || Int32.Parse(textBox2.Text) > 15)
            {
                errorProvider2.SetError(textBox2, "Height must be integer number in range 2-15");
                e.Cancel = true;
                return;
            }
            errorProvider2.SetError(textBox2, string.Empty);
            e.Cancel = false;
        }
    }
}
