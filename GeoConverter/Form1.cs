using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace GeoConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        bool flag1=false,flag2=false;
        string[] files;
        string file,path=@"ConvertFiles\",format= ".geo";
       // string covertpath = @"Cenverts";

        private void выбратьФайлыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = null;
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                files = openFileDialog1.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    richTextBox1.Text += files[i] + "\n" + "\n";
                    openFileDialog1.InitialDirectory = Path.GetDirectoryName(files[i]);
                }
            }
            
            flag2 = true;
            flag1 = false;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

          //  richTextBox1.Text = null;
            if (flag1 && file != null)
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(files[files.Length-1]);
                Thread tre = new Thread(covertfile);
                tre.Start();
            }
            if (flag2&& files != null)
            {
                // Thread tres = new Thread(covertfiles);
                //tres.Start();
                covertfiles();
            }
            if(flag1==flag2) {
                richTextBox1.Text = "для работы програмы выберите файл или файлы";
            }
            //covertfile();
        }
        private void covertfile()
        {
            if (file.EndsWith(".json"))
            {
                List<string> strfile;
                string _lines, r; _lines = System.IO.File.ReadAllText(file, Encoding.GetEncoding(1251));
                int index1, index2;
                index1 = _lines.IndexOf("[[");
                r = _lines.Substring(index1);
                index2 = r.IndexOf("]]],");
                index2 = index2 + 1;
                r = r.Substring(0, index2);
                r = r.Replace("]]", " ;");
                r = r.Replace("]", "");
                r = r.Replace(",", " ");
                string[] g = r.Split(new char[] { '[', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] g1;
                string str = null;
                strfile = new List<string> { };
                StreamWriter f = new StreamWriter(path + "convert" + format);
                for (int i = 0, j = 0, num = 0, kol = 0; i < g.Length; i++)
                {
                    if (g[i].IndexOf(";") == -1)
                    {
                        if (j == 0 || j % 2 == 0)
                        {
                            str = g[i].Remove(0, 1);
                            j++;
                        }
                        else
                        {
                            num++;
                            strfile.Add(num + " " + g[i] + " " + str + " 100");
                            j++;
                            kol++;
                        }
                    }
                    else
                    {
                        g1 = strfile.ToArray<string>();
                        f.WriteLine(kol);
                        for (int b = 0; b < g1.Length; b++)
                        {
                            f.WriteLine(g1[b]);
                        }
                        strfile = new List<string> { };
                        kol = 0;
                    }

                }
                f.Close();
            }
            
        }

        private void covertfiles()
        {
            List<string> strfile;string r, _lines;
           
            for (int fi = 0; fi < files.Length; fi++)
            {
                if (files[fi].EndsWith(".json"))
                {
                    _lines = System.IO.File.ReadAllText(files[fi], Encoding.GetEncoding(1251));
                    int index1, index2;
                    index1 = _lines.IndexOf("[[");
                    r = _lines.Substring(index1);
                    index2 = r.IndexOf("]]],");
                    index2 = index2 + 1;
                    r = r.Substring(0, index2);
                    r = r.Replace("]]", " ;");
                    r = r.Replace("]", "");
                    r = r.Replace(",", " ");
                    string[] g = r.Split(new char[] { '[', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] g1;
                    string str = null;
                    strfile = new List<string> { };
                    string u = files[fi].Remove(0,files[fi].LastIndexOf("\\")+1);
                    u = u.Replace(".json", "");
                    StreamWriter f = new StreamWriter(path + u + format);
                    for (int i = 0, j = 0, num = 0, kol = 0; i < g.Length; i++)
                    {
                        if (g[i].IndexOf(";") == -1)
                        {
                            if (j == 0 || j % 2 == 0)
                            {
                                str = g[i].Remove(0, 1);
                                j++;
                            }
                            else
                            {
                                num++;
                                strfile.Add(num + " " + g[i] + " " + str + " 100");
                                j++;
                                kol++;
                            }
                        }
                        else
                        {
                            g1 = strfile.ToArray<string>();
                            f.WriteLine(kol);
                            for (int b = 0; b < g1.Length; b++)
                            {
                                f.WriteLine(g1[b]);
                            }
                            
                            strfile = new List<string> { };
                            kol = 0;
                        }

                    } f.Close();
                }
                
               
            }
            MessageBox.Show("Конвертирование завершено","Уведомление",MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
