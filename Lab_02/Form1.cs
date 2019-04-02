using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var path = Path.GetFullPath(textBox1.Text);
            DateTime dateTimeNow = DateTime.Now;

            dateTimeNow = dateTimeNow = new DateTime(
                dateTimeNow.Ticks - (dateTimeNow.Ticks % TimeSpan.TicksPerSecond),
                dateTimeNow.Kind);

            var zipPath = $@"C:\FormOfDirectory-{dateTimeNow.ToString("dd/MM/yyyy-H/mm/ss")}.zip";

            ZipFile.CreateFromDirectory(Path.GetFullPath(textBox1.Text), zipPath, CompressionLevel.Fastest, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
