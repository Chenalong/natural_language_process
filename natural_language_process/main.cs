using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;

namespace natural_language_process
{
    public partial class main : Form
    {
        public static string[] fileList = new string[100];
        public static string rootDirectory;
        public static int file_num = 0;
        public static int seed = 0;
        public main()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void main_Load(object sender, EventArgs e)
        {
            rootDirectory = System.Windows.Forms.Application.StartupPath;
            Console.WriteLine(rootDirectory);
            rootDirectory = rootDirectory.Substring(0,rootDirectory.LastIndexOf('\\'));
            rootDirectory = rootDirectory.Substring(0,rootDirectory.LastIndexOf('\\'));
            Console.WriteLine(rootDirectory);

            //初始画文件列表
            init_file_list();

            //初始化text_1,text_2,...这些lable
            init_text_lable();
        }

        private void init_file_list()
        {
            string file_root = rootDirectory + "\\文本集合\\";
            System.IO.DirectoryInfo theFolder = new System.IO.DirectoryInfo(file_root);
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                fileList[file_num++] = NextFolder.Name;
            }

            for (int i = 0; i < file_num; i++)
                Console.WriteLine(fileList[i]);
        }

        private void init_text_lable()
        {
            this.text_1.Text = fileList[(seed + 0) % file_num];
            this.text_2.Text = fileList[(seed + 1) % file_num];
            this.text_3.Text = fileList[(seed + 2) % file_num];
            this.text_4.Text = fileList[(seed + 3) % file_num];
            seed += 4;
        }

        private void bnt_change_question_Click(object sender, EventArgs e)
        {
            init_text_lable();
        }

        private string read_file_contend(string file_path)
        {
            StreamReader sr = new StreamReader(file_path, Encoding.Default);
            String line;
            String contend = "";
            while ((line = sr.ReadLine()) != null)
            {
                contend += line + "\r\n";
            }
            Console.WriteLine(contend);
            return contend;
        }
 

        private void text_1_Click_1(object sender, EventArgs e)
        {
            string file_name = ((System.Windows.Forms.Button)(sender)).Text;
            string file_path = rootDirectory + "\\文本集合\\" + file_name + "\\" + file_name + ".txt";
            string question_path = rootDirectory + "\\文本集合\\" + file_name + "\\" + file_name + "问题.txt";
            this.tx_contend.Text = read_file_contend(file_path);
            this.tb_question.Text = read_file_contend(question_path);
            //Console.WriteLine("_______________" + file_name);
        }

        private void bnt_test_Click(object sender, EventArgs e)
        {
            
            try 
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

                string api_key = "Z6a3L161atTsfa5bBbWr9JIvTAYsAsoyvo9JUqyi";
                string url_get_base = "http://api.ltp-cloud.com/analysis/?";
                // 国务院总理李克强调研上海外高桥时提出，支持上海积极探索新机制。
                string text = "国务院总理李克强调研上海外高桥时提出，支持上海积极探索新机制。";
                string pattern = "all";
                string format = "xml";
                string page_url = String.Format("{0}api_key={1}&text={2}&format={3}&pattern={4}", url_get_base, api_key, text, format, pattern);

                Byte[] pageData = MyWebClient.DownloadData(page_url); //从指定网站下载数据

                //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            

                string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句

                Console.WriteLine(pageHtml);//在控制台输入获取的内容

                
                
                /*
                using (StreamWriter sw = new StreamWriter("c:\\test\\ouput.html"))//将获取的内容写入文
                {
                    sw.Write(pageHtml);
                }
                 * */
                //Console.ReadLine(); //让控制台暂停,否则一闪而过了
            }
            catch(WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());

            }
        }
    }
}
