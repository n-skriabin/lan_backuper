using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_02_server
{
    public partial class Form1 : Form
    {
        int BytesPerRead = 1024;//bytes to read and then write
        private bool _ServerIsWorking = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                ServerListener();
            }).Start();
        }

        public void ServerListener()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 100);//bypass Compiler

            if (!_ServerIsWorking)
            {
                listener.Stop();

                this.Invoke(new MethodInvoker(delegate ()
                {
                    button1.Text = "Выключить сервер";
                }));
                _ServerIsWorking = true;

                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);

                //wait for sender
                listener.Start();
                while (_ServerIsWorking)
                {
                    if (listener.Pending())
                    {
                        break;
                    }
                }

                if (_ServerIsWorking)
                {
                    DateTime dateTimeNow = DateTime.Now;

                    dateTimeNow = dateTimeNow = new DateTime(
                        dateTimeNow.Ticks - (dateTimeNow.Ticks % TimeSpan.TicksPerSecond),
                        dateTimeNow.Kind);

                    using (var client = listener.AcceptTcpClient())
                    using (var stream = client.GetStream())
                    using (var output = File.Create($@"C:\serverFolder\dataBACKUP-{dateTimeNow.ToString("dd/MM/yyyy_H/mm/ss")}.zip"))
                    {

                        var buffer = new byte[BytesPerRead];
                        int bytesRead;
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                        }
                    }
                }
                else
                {
                    listener.Stop();
                }
            }
            else
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    button1.Text = "Включить сервер";
                }));
                _ServerIsWorking = false;
            }
        }
    }
}
