using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceKiller
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnSelectSourceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "选择源文件";
            fileDialog.Filter = "文本文件 |*.xml;*.txt;*.csv|" +
                                "所有文件 |*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSourceFile.Text = fileDialog.FileName;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            string strSourceFile = txtSourceFile.Text;
            string strRegText = txtRegex.Text;
            string strReplaceText = txtReplaceString.Text;

            if (string.IsNullOrWhiteSpace(strSourceFile))
            {
                MessageBox.Show("请先选择源文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string strExtName = Path.GetExtension(strSourceFile);
            string filter = string.Format("{0}文件| *{0}", string.IsNullOrEmpty(strExtName) ? "*" : strExtName);
            saveFileDialog.Filter = filter;
            saveFileDialog.OverwritePrompt = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string strSaveFilePath = saveFileDialog.FileName;
                Encoding fileEncoding = TextFileUtil.GetFileEncodeType(strSourceFile);
                string fileContent = File.ReadAllText(strSourceFile, fileEncoding);
                string targetContent = Regex.Replace(fileContent, strRegText, strReplaceText);

                File.WriteAllText(strSaveFilePath, targetContent, fileEncoding);
            }
        }
    }
}
