using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace ProvImageMarkup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<Record> Records;
        public List<Record> RecordsAll;
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
            var dbcon = new Dbconnect {DbPath = openFileDialog1.FileName};
            Records = dbcon.ReadDb(textBox4.Text);
            Records = Record.fullPersons(Records, checkBox1.Checked);
            var fio = dbcon.ReadFio();
            Records = Record.addFIO(fio, Records);
            foreach (var rec in Records) {
                comboBox1.Items.Add(rec.pid);
            }
            if (Records.Count == 0) { MessageBox.Show(@"База не загруженна из за ошибки",@"Ошибка"); }
            else { MessageBox.Show(@"База загружена"); }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = textBox1.Text;
                openFileDialog1.RestoreDirectory = false;
                openFileDialog1.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int sid = 0;
                foreach (var rec in Records)
                {
                    if (Convert.ToInt32(comboBox1.SelectedItem) == rec.pid)
                    {
                        break;
                    }
                    sid++;
                }
                RecordsAll = Records;
                foreach (var rec in RecordsAll)
                {
                    foreach (var per in rec.persons)
                    {
                        if (textBox2.Text == "")
                        {
                            per.FilePath = textBox3.Text + per.FilePath;
                            per.FilePath = per.FilePath.Replace(@"/", @"\");
                        }
                        else
                        {
                            per.FilePath = per.FilePath.Replace(textBox2.Text, textBox3.Text);
                            per.FilePath = per.FilePath.Replace(@"/", @"\");
                        }
                        
                    }
                }

                var imgForm = new ImageForm();
                PictureBox pBox = new PictureBox();
                imgForm.Controls.Add(pBox);
                imgForm.LoadImage(RecordsAll, imgForm, pBox, sid);
                imgForm.Show();
                // MessageBox.Show(FIO);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(@"Некорректная ссылка на образ: 
                                  "+ ex.Message, @"Ошибка");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(@"База не загруженна из за ошибки", @"Ошибка");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            RecordsAll = Records;
            foreach (var rec in RecordsAll) {
                foreach (var per in rec.persons) {
                    if (textBox2.Text == "")
                    {
                        per.FilePath = textBox3.Text + per.FilePath;
                        per.FilePath = per.FilePath.Replace(@"/", @"\");
                    }
                    else
                    {
                        per.FilePath = per.FilePath.Replace(textBox2.Text, textBox3.Text);
                        per.FilePath = per.FilePath.Replace(@"/", @"\");
                    }
                }
            }

            var imgForm = new ImageForm();
            PictureBox pBox = new PictureBox();
            imgForm.Controls.Add(pBox);
            imgForm.LoadImage(RecordsAll, imgForm, pBox,0);
            imgForm.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.defDbPath;
            textBox2.Text = Properties.Settings.Default.defCur;
            textBox3.Text = Properties.Settings.Default.defReplace;
            textBox4.Text = Properties.Settings.Default.defcolname;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var prop = new PropMarkup();
            prop.Show();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            int index = comboBox1.FindString(comboBox1.Text);
            comboBox1.SelectedIndex = index;
        }
    }
}
