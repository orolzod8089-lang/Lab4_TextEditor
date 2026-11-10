using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Lab4_TextEditor
{
    public partial class frmRichTextEditor : Form
    {
        private void SaveAsFile()
        {
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|Rich Text Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentFile = saveFileDialog1.FileName;

                if (Path.GetExtension(currentFile).ToLower() == ".rtf")
                {
                    richTextBox1.SaveFile(currentFile);
                }
                else
                {
                    File.WriteAllText(currentFile, richTextBox1.Text);
                }

                this.Text = Path.GetFileName(currentFile) + " - My Rich Text Editor";
            }
        }
        private void SaveCurrentFile()
        {
            if (string.IsNullOrEmpty(currentFile))
            {
                SaveAsFile();
            }
            else
            {
                if (Path.GetExtension(currentFile).ToLower() == ".rtf")
                {
                    richTextBox1.SaveFile(currentFile);
                }
                else
                {
                    File.WriteAllText(currentFile, richTextBox1.Text);
                }
            }
        }

        private void FindNextText()
        {
            int start = richTextBox1.SelectionStart
                      + richTextBox1.SelectionLength;

            if (start >= richTextBox1.TextLength)
            {
                start = 0;
            }

            int index = richTextBox1.Find(
                findText,
                start,
                RichTextBoxFinds.None);

            if (index >= 0)
            {
                richTextBox1.Select(index, findText.Length);
                richTextBox1.Focus();
            }
            else
            {
                MessageBox.Show("Хайсан үг олдсонгүй!");
            }
        }
        private string currentFile = "";
        private string findText = "";
        public frmRichTextEditor()
        {
            InitializeComponent();
            this.Text = "My Rich Text Editor";

            pasteToolStripMenuItem.Enabled = false;

            richTextBox1.SelectionChanged += richTextBox1_SelectionChanged;
            richTextBox1.TextChanged += richTextBox1_TextChanged;
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            int position = richTextBox1.SelectionStart;

            int line = richTextBox1.GetLineFromCharIndex(position);

            int firstChar = richTextBox1.GetFirstCharIndexFromLine(line);

            int column = position - firstChar;

            lblPosition.Text = "Мөр: " + (line + 1) +
                               ", Багана: " + (column + 1);
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            lblCount.Text = "Тэмдэгт: " + richTextBox1.TextLength;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ZoomFactor < 4)
            {
                richTextBox1.ZoomFactor += 0.2f;
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                openFileDialog1.Filter =
                    "Rich Text Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    currentFile = openFileDialog1.FileName;

                    string ext = Path.GetExtension(currentFile).ToLower();

                    if (ext == ".rtf")
                    {
                        richTextBox1.LoadFile(currentFile,
                            RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        richTextBox1.LoadFile(currentFile,
                            RichTextBoxStreamType.PlainText);
                    }

                    this.Text = Path.GetFileName(currentFile)
                                + " - My Rich Text Editor";
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFile == "")
            {
                SaveAsFile();
            }
            else
            {
                SaveCurrentFile();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ZoomFactor > 0.6f)
            {
                richTextBox1.ZoomFactor -= 0.2f;
            }
        }

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionColor = colorDialog1.Color;
                }
            }
        }

        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void alignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();

            pasteToolStripMenuItem.Enabled = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findText = Interaction.InputBox(
      "Хайх үгээ оруулна уу:",
      "Find");

            if (findText != "")
            {
                richTextBox1.SelectionStart = 0;

                FindNextText();
            }
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (findText != "")
            {
                FindNextText();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void seperatorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}