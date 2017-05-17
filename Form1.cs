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

namespace MyNotePad
{
    public partial class FormMain : Form
    {
        string filename;

        public FormMain()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text Document|*.txt";
            saveFileDialog1.DefaultExt = "txt";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Modified == true)
            {
                if (MessageBox.Show("Do you want to save changes to this file?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                     == DialogResult.Yes)
                {
                    if (filename != null)
                    {
                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            sw.BaseStream.Seek(0, SeekOrigin.End);
                            sw.Write(textBox1.Text);
                        }
                    }
                    else
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            filename = saveFileDialog1.FileName;
                            using (StreamWriter sw = new StreamWriter(filename))
                            {
                                sw.Write(textBox1.Text);
                                this.Text = "MyNotePad - " + filename;
                            }
                        }
                    }
                    textBox1.Clear();
                    this.Text = "MyNotePad - Untitled";

                }
                else
                {
                    textBox1.Clear();
                    this.Text = "MyNotePad - Untitled";
                }
            }
            else
            {
                textBox1.Clear();
                this.Text = "MyNotePad - Untitled";
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Modified == true)
            {

                if (filename != null)
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        sw.BaseStream.Seek(0, SeekOrigin.End);
                        sw.Write(textBox1.Text);
                    }
                }
                else
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        filename = saveFileDialog1.FileName;
                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            sw.Write(textBox1.Text);
                            this.Text = "MyNotePad - " + filename;
                        }
                    }
                }
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Document";            
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string inValue = "", temp= "";
                using (StreamReader sr = new StreamReader(filename))
                {
                    this.Text = "MyNotePad - " + filename;
                    while ((inValue = sr.ReadLine()) != null)
                    {
                        temp += inValue;
                        temp += "\r\n";
                    }
                    sr.BaseStream.Seek(0, SeekOrigin.End);
                    textBox1.Text = temp;
                    string str = openFileDialog1.FileName;
                }
            }
        }

        private void undoStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.CanUndo)
            {
                textBox1.Undo();
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }


        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text += " - Untitled";
        }


        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to save this file before closing?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
     == DialogResult.Yes)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = saveFileDialog1.FileName;
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        sw.Write(textBox1.Text);
                        textBox1.Clear();
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is created by RRodelas.  Version 1.0.0", "About MyNotePad");
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.ForeColor = colorDialog1.Color;
            }
        }

    }
}
