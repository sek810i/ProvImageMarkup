using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProvImageMarkup
{
    public partial class PropMarkup : Form
    {
        public PropMarkup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                colorDialog1.ShowDialog();
                panel1.BackColor = colorDialog1.Color;
            }

        }

        private void PropMarkup_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Properties.Settings.Default.defColor;
            numericUpDown1.Value = Properties.Settings.Default.defBorder;
            panel2.BackColor = Properties.Settings.Default.defTextColor;
            Fo = Properties.Settings.Default.defFont;
            textBox1.Text = Properties.Settings.Default.defOtst.ToString();
        }

        public static Font Fo;
        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.defColor = panel1.BackColor;
            Properties.Settings.Default.defBorder = numericUpDown1.Value;
            Properties.Settings.Default.defTextColor = panel2.BackColor;
            Properties.Settings.Default.defFont = Fo;
            Properties.Settings.Default.defOtst = Convert.ToInt32(textBox1.Text);

            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colordialog;
            colordialog = new ColorDialog();
            colorDialog1.ShowDialog();
            panel2.BackColor = colorDialog1.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowEffects = false;
            fontDialog1.Font = Fo;
            fontDialog1.ShowDialog();
            Fo = fontDialog1.Font;
        }
    }
}
