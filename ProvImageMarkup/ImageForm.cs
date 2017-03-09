using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProvImageMarkup
{
    public partial class ImageForm : Form
    { 
        public ImageForm()
        {
            InitializeComponent();
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
                      

        }

        public Image NewMarkup (Image img, person pers) {
            Pen pen = new Pen(Color.FromArgb(255, Properties.Settings.Default.defColor))
            {
                Width = (float) Properties.Settings.Default.defBorder
            };
            using (var rectangle = Graphics.FromImage(img))
            {
                rectangle.DrawRectangle(pen, new Rectangle(pers.x, pers.y, pers.w, pers.h));
                rectangle.DrawString(pers.Fam + " " + pers.Name + " " + pers.Otch, Properties.Settings.Default.defFont, new SolidBrush(Properties.Settings.Default.defTextColor), new Point(pers.w / 2, pers.y+ Properties.Settings.Default.defOtst));
            }
            return img;
        } 

        public void ImageForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown && e.Control) {
                try
                {
                    if (CurImage == Records.Count)
                    {
                        MessageBox.Show(@"Это последний образ");
                    }
                    else
                    {
                        CurImage++;
                        string imgpath = Records[CurImage].persons[0].FilePath;
                        var img = Image.FromFile(imgpath);
                        Box.Size = new Size(Convert.ToInt32(img.Size.Width) / 4, Convert.ToInt32(img.Size.Height) / 4);
                        if (Records[CurImage].persons.Count != 0)
                        {
                            foreach (var pers in Records[CurImage].persons)
                            {
                                img = NewMarkup(img, pers);
                            }
                        }
                        Box.Image = img;
                        Form.Text = @"Образ - " + imgpath + @"; ID = " + Records[CurImage].pid;
                        GC.Collect();
                        e.Handled = true;
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show(@"База не загруженна из за ошибки", @"Ошибка");
                }
                
            }
            if (e.KeyCode == Keys.PageUp && e.Control)
            {
                try
                {
                    if (CurImage == 0)
                    {
                        MessageBox.Show(@"Это последний образ");
                    }
                    else
                    {
                        CurImage--;
                        string imgpath = Records[CurImage].persons[0].FilePath;
                        var img = Image.FromFile(imgpath);
                        Box.Size = new Size(Convert.ToInt32(img.Size.Width) / 4, Convert.ToInt32(img.Size.Height) / 4);
                        if (Records[CurImage].persons.Count != 0)
                        {
                            foreach (var pers in Records[CurImage].persons)
                            {
                                img = NewMarkup(img, pers);
                            }
                        }
                        Box.Image = img;
                        Form.Text = @"Образ - " + imgpath + @"; ID = " + Records[CurImage].pid;
                        GC.Collect();
                        e.Handled = true;
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show(@"База не загруженна из за ошибки", @"Ошибка");
                }
                
            }
        }

        public List<Record> Records;
        public ImageForm Form;
        public PictureBox Box;
        public int CurImage;

        public void LoadImage(List<Record> rec, ImageForm imgForm, PictureBox imgBox,int selectId) {
            CurImage = selectId;
            var imgpath = rec[CurImage].persons[0].FilePath;
            try
            {
                var img = Image.FromFile(imgpath);
                imgForm.AutoScroll = true;
                imgBox.Location = new Point(0, 0);
                if (rec[CurImage].persons.Count != 0)
                {
                    foreach (var pers in rec[CurImage].persons)
                    {
                        img = NewMarkup(img, pers);
                    }
                }
                imgBox.Image = img;
                imgBox.SizeMode = PictureBoxSizeMode.Zoom;
                imgBox.Size = new Size(Convert.ToInt32(img.Size.Width) / 4, Convert.ToInt32(img.Size.Height) / 4);
                Records = rec;
                Form = imgForm;
                Box = imgBox;
                Form.Text = @"Образ - " + imgpath + @"; ID = " + rec[CurImage].pid;
            }
            catch (Exception)
            {
                MessageBox.Show(@"По пути "+imgpath+@" нет образа", @"Ошибка");
            }
            
        }

    }
}
