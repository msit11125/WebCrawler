using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Http;
using HtmlAgilityPack;
using Crawler.Properties;
using iTextSharp.text.pdf.parser;
using System.Deployment.Application;
using Spire.Doc;

namespace Crawler
{
    public partial class WebCrawler : Form
    {
        List<String> urlList = new List<string>();
        List<KeyWords> keyWordsList = new List<KeyWords>();
        string exepath = "";
        //string path = "";
        int nums = 2;
        int errorCatch = 0;
        //Release版本
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\WebCrawler_RC\data\";

        //download
        //WebClient webclient = new WebClient();
        ////webclient.Proxy = proxy;
        //webclient.DownloadFile(url, fileName);
        public WebCrawler()
        {
            InitializeComponent();

            //string exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string path = exepath.Substring(0, exepath.LastIndexOf("WebCrawler.exe")) + @"data\";
            txtPath.Text = path;

        }
        async public Task<bool> tryHttpClient(string url,string html)
        {
            using (var httpClient = new HttpClient())  // 解決forbidden 403寫法 顯示html都是首頁的??
            {
                using (HttpResponseMessage respon = await httpClient.GetAsync(new Uri(url)))
                {
                    using (HttpContent content = respon.Content)
                    {
                        html = await content.ReadAsStringAsync();

                        Stream stream = await content.ReadAsStreamAsync();
                        StreamReader reader = new StreamReader(stream);
                        html = reader.ReadToEnd();

                        CountMostPopular(html, url);


                        WebClient webclient = new WebClient();
                        //webclient.Proxy = proxy;
                        webclient.DownloadFile(url, "data/"+"file.html");
                        return false;
                    }
                }
            }
        }

        int websitesCount = 0;
        int totalsites = 0;
        private async Task<bool> urlToList(string url, CancellationToken token)
        {
            await Task.Delay(20);
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    btnCrawler.InvokeIfRequired(() =>
                    {
                        btnCrawler.Enabled = true;
                    });
                    resultMsg.InvokeIfRequired(() =>
                    {
                        resultMsg.Text += Environment.NewLine + "※ 已停止搜尋動作.............." + Environment.NewLine;
                        resultMsg.SelectionStart = resultMsg.Text.Length;
                        resultMsg.ScrollToCaret();
                    });
                    return true;
                }
                try
                {
                    String html = "";
                    if (chkBuildHttpClient.Checked)
                    {
                        if (url.GetLast(4) == ".pdf" || url.GetLast(4) ==".doc" || url.GetLast(5) == ".docx")
                        {
                            try
                            {
                                WebClient webClient = new WebClient();
                                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)");
                                string fileName = toFileName(url);
                                if(!File.Exists(path+fileName)) //檢查是否已下載
                                     webClient.DownloadFile(url, path + fileName);


                                string text = "";
                                if (url.EndsWith(".doc") /*|| url.EndsWith(".docx")*/)  //處理word文件
                                {
                                    Spire.Doc.Document document = new Spire.Doc.Document();
                                    Stream stream = File.OpenRead(path + fileName);
                                    document.LoadFromStream(stream, FileFormat.Doc);
                                    text = document.GetText();
                                }
                                else
                                {
                                    StringBuilder sb = new StringBuilder();
                                    Encoding enc = Encoding.Default;

                                    StreamReader file = new StreamReader(path + fileName, enc);

                                    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(path + fileName);
                                    for (int i = 1; i <= reader.NumberOfPages; i++)
                                    {
                                        sb.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                                    }
                                    text += sb.ToString();
                                    file.Close();
                                }
                                CountMostPopular(text, url);
                                return false;
                            }
                            catch (Exception ex)
                            {
                                resultMsg.InvokeIfRequired(() =>
                                {
                                    resultMsg.Text += Environment.NewLine + ex.Message + Environment.NewLine;
                                    resultMsg.SelectionStart = resultMsg.Text.Length;
                                    resultMsg.ScrollToCaret();
                                });
                            }
                        }
                        else
                        {
                            #region HttpClient方法
                            return await tryHttpClient(url, html);
                            
                        }
                        #endregion
                    }
                    else
                    {
                        //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; //基礎連接已關閉: 無法為 SSL/TLS 安全通道建立信任關係。
                        CookieContainer cookieJar = new CookieContainer();

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.CookieContainer = cookieJar;
                        request.Accept = @"text/html, application/xhtml+xml, */*";
                        //request.Referer = @"http://www.somesite.com/";
                        request.Headers.Add("Accept-Language", "en-GB");
                        request.UserAgent = @"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
                        //request.Host = @"www.somesite.com";
                        request.Timeout = 10000;
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            html = reader.ReadToEnd();
                        }
                        response.Close();

                    }

                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(html);
                    var h4s = htmlDocument.DocumentNode.Descendants("h4")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("title")).ToList();
                    var spanContent = htmlDocument.DocumentNode.Descendants("span")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("description")).ToList();

                    if (h4s.Count == 0) //單一網頁，非使用搜尋引擎
                    {
                        CountMostPopular(html, url);
                        return false;
                    }
                    foreach (var h4 in h4s)
                    {
                        string titleUrl = h4.Descendants("a")
                            .FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;

                        if (!urlList.Contains(titleUrl))
                        {
                            urlList.Add(titleUrl);
                            resultMsg.InvokeIfRequired(() =>
                            {
                                resultMsg.Text += Environment.NewLine +"[" +nums++ +"] "+ titleUrl;
                                resultMsg.SelectionStart = resultMsg.Text.Length;
                                resultMsg.ScrollToCaret();
                            });

                        }

                    }
                    string nextPage = "";
                    try
                    {
                        nextPage = htmlDocument.DocumentNode.Descendants("a")
                            .Where(node => node.GetAttributeValue("class", "").
                            Equals("next_page")).FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;
                        url = "https://search.uspto.gov" + nextPage;

                    }
                    catch (Exception ex)
                    {
                        cts.Cancel();

                        resultMsg.InvokeIfRequired(() =>
                        {
                            resultMsg.Text += Environment.NewLine + "Search Ending.........................";
                            resultMsg.SelectionStart = resultMsg.Text.Length;
                            resultMsg.ScrollToCaret();
                        });
                        //cts2 = new CancellationTokenSource();
                        nums = 1; //包含自己的連線網址
                        return true;
                        //Task t = Task.Run(() => EndPage_StartHtmlCounting(cts2));
                    }





                }
                catch (Exception ex)
                {

                    resultMsg.InvokeIfRequired(() =>
                    {
                        resultMsg.Text += Environment.NewLine + ex.Message;
                        resultMsg.SelectionStart = resultMsg.Text.Length;
                        resultMsg.ScrollToCaret();
                    });
                }

            }
        }
        /// <summary>
        /// 開始HTML搜尋
        /// </summary>
        public void EndPage_StartHtmlCounting(CancellationTokenSource cts2)
        {
            while (true)
            {

                foreach (var url in urlList)
                {
                    if (cts2.IsCancellationRequested)
                    {
                        btnCrawler.InvokeIfRequired(() =>
                            {
                                btnCrawler.Enabled = true;
                            });
                        resultMsg.InvokeIfRequired(() =>
                        {
                            resultMsg.Text += Environment.NewLine + "※ 已停止搜尋動作.............." + Environment.NewLine;
                            resultMsg.SelectionStart = resultMsg.Text.Length;
                            resultMsg.ScrollToCaret();
                        });
                        return;
                    }
                    string html = "";
                    if (url.GetLast(4) == ".pdf" || url.GetLast(4) == ".doc" || url.GetLast(5) == ".docx")
                    {
                        try
                        {

                            WebClient webClient = new WebClient();
                            webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)");
                            string fileName = toFileName(url);
                            if (!File.Exists(path + fileName)) //檢查是否已下載
                                  webClient.DownloadFile(url, path + fileName);


                            string text = "";
                            if (url.EndsWith(".doc") /*|| url.EndsWith(".docx")*/)  //處理word文件
                            {
                                Spire.Doc.Document document = new Spire.Doc.Document();
                                Stream stream = File.OpenRead(path + fileName);
                                document.LoadFromStream(stream, FileFormat.Doc);
                                text = document.GetText();
                            }
                            else
                            {
                                Encoding enc = Encoding.Default;

                                StreamReader file = new StreamReader(path + fileName, enc);

                                StringBuilder sb = new StringBuilder();
                                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(path + fileName);
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    sb.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                                }
                                text += sb.ToString();
                                file.Close();
                            }
                            CountMostPopular(text, url);
                            
                        }
                        catch (Exception ex)
                        {
                            resultMsg.InvokeIfRequired(() =>
                            {
                                resultMsg.Text += Environment.NewLine + "[" + nums++ + "] " + ex.Message + Environment.NewLine;
                                resultMsg.SelectionStart = resultMsg.Text.Length;
                                resultMsg.ScrollToCaret();
                            });
                            websitesCount++;
                            pointErrorCount.InvokeIfRequired(() =>
                            {
                                pointErrorCount.Text = "Error Catch: " + ++errorCatch;
                            });
                        }
                    }
                    else
                    {
                        try
                        {
                            //處理一般html
                            CookieContainer cookieJar = new CookieContainer();
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            request.CookieContainer = cookieJar;

                            request.Accept = @"text/html, application/xhtml+xml, */*";
                            //request.Referer = @"http://www.somesite.com/";
                            request.Headers.Add("Accept-Language", "en-GB");
                            request.UserAgent = @"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
                            //request.Host = @"www.somesite.com";
                            request.Timeout = 10000;

                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                html = reader.ReadToEnd();
                            }
                            response.Close();

                            CountMostPopular(html, url);

                        }
                        catch (Exception ex)
                        {
                            //逾時10秒
                            resultMsg.InvokeIfRequired(() =>
                            {
                                resultMsg.Text += Environment.NewLine + "[" + nums++ + "] " + ex.Message + Environment.NewLine;
                                resultMsg.SelectionStart = resultMsg.Text.Length;
                                resultMsg.ScrollToCaret();
                            });
                            websitesCount++;
                            pointErrorCount.InvokeIfRequired(() =>
                            {
                                pointErrorCount.Text = "Error Catch: " + ++errorCatch;
                            });
                        }
                    }
                }//foreach url end....
                btnCrawler.Enabled = true;

            }
        }



        //TODO HtmlToPlainText
        //去html tags處理
        public static string StripHTML(string html)
        {
            return Regex.Replace(html, "<.*?>", " ");
        }

        public void CountMostPopular(string html, string url)
        {
            var newhtml = html.ToLower();
            string TextMining = "";
            string[] stripChars = { "&nbsp;", "#", "*" };
            foreach (string character in stripChars)
            {
                newhtml = newhtml.Replace(character, " ");
            }

            if (url.GetLast(4) != ".pdf" && url.GetLast(4)!=".doc" && url.GetLast(5)!=".docx")
            {
                MatchCollection mc = Regex.Matches(newhtml,
                    @"<p[^>]*?>(.*?)</p>|<span[^>]*?>(.*?)</span>|<h[0-9][^>]*?>(.*?)</h[0-9]>|<td[^>]*?>(.*?)</td>"

                 );
                //------------------
                foreach (Match text in mc)
                {
                    TextMining += text + Environment.NewLine;
                }
                //經過去tags處理
                TextMining = StripHTML(TextMining);
            }
            else
            {
                TextMining = newhtml;
            }



            resaultMsgDetail.InvokeIfRequired(() =>
            {
                resaultMsgDetail.Text = "○ 搜尋網址 : " + url + Environment.NewLine + Environment.NewLine + html;

                //resaultMsgDetail.SelectionStart = resaultMsgDetail.Text.Length;
                //resaultMsgDetail.ScrollToCaret();
            });
            resaultMsgDetail_BeforeRegex.InvokeIfRequired(() =>
            {
                resaultMsgDetail_BeforeRegex.Text = Environment.NewLine + "● 資料篩選後結果 : " + Environment.NewLine + Environment.NewLine
                + TextMining + Environment.NewLine;
            });

            resultMsg.InvokeIfRequired(() =>
            {
                resultMsg.Text += Environment.NewLine + "[" + nums++ + "] " + "網址: " + url + Environment.NewLine;
                resultMsg.SelectionStart = resultMsg.Text.Length;
                resultMsg.ScrollToCaret();
            });
            //string[] keywords = txtImport.Text.ToLower().Split('&');

            foreach (var key in keyWordsList)
            {
                //可以計算次數
                string strReplacce = TextMining.Replace(key.Keywords, "");
                int times = (TextMining.Length - strReplacce.Length) / key.Keywords.Length;

                resultMsg.InvokeIfRequired(() =>
                {
                    resultMsg.Text += "△ " + key.Keywords + " ,找到 " + times + "筆相符結果......." + Environment.NewLine;
                    resultMsg.SelectionStart = resultMsg.Text.Length;
                    resultMsg.ScrollToCaret();
                });
                if (times >= numTimes.Value) //搜尋到此網頁的關鍵字次數大於多少
                    keyWordsList.Where(k => k.Keywords == key.Keywords).FirstOrDefault().Related += 1;


            }
            dataGridView1.InvokeIfRequired(() =>
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = keyWordsList;

            });

            #region ---- 單字最多出現次數演算法 ----
            //string[] stripChars = { ";", ",", ".", "-", "_", "^", "(", ")", "[", "]",
            //            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "\n", "\t", "\r" };
            //foreach (string character in stripChars)
            //{
            //    TextMining = TextMining.Replace(character, " ");
            //}

            //List<string> wordList = TextMining.Split(' ').ToList();

            //string[] stopwords = new string[] { "and", "the", "she", "for", "this", "you", "but", "class", "resault", "that", "our" };
            //foreach (string word in stopwords)
            //{
            //    while (wordList.Contains(word))
            //    {
            //        wordList.Remove(word);
            //    }
            //}
            //Dictionary<string, int> dictionary = new Dictionary<string, int>();

            //foreach (string word in wordList)
            //{
            //    if (word.Length >= 3)
            //    {
            //        if (dictionary.ContainsKey(word))
            //        {
            //            dictionary[word]++;
            //        }
            //        else
            //        {
            //            dictionary[word] = 1;
            //        }

            //    }

            //}
            //var sortedDict = (from entry in dictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

            //int count = 1;
            //resultMsg.InvokeIfRequired(() =>
            //{
            //    resultMsg.Text += Environment.NewLine + "---- Most Frequent Terms in the File:  ----" + Environment.NewLine;
            //});

            //foreach (KeyValuePair<string, int> pair in sortedDict)
            //{
            //    resultMsg.InvokeIfRequired(() =>
            //    {
            //        resultMsg.Text += count + "\t" + pair.Key + "\t" + pair.Value + Environment.NewLine;
            //        resultMsg.SelectionStart = resultMsg.Text.Length;
            //        resultMsg.ScrollToCaret();
            //    });
            //    count++;
            //    if (count > 10)
            //    {
            //        break;
            //    }
            //}
            #endregion


            websitesCount++;
            totalsites = urlList.Count();
            resultMsg.InvokeIfRequired(() =>
            {
                pointCrawlerCount.Text = websitesCount + " / " + totalsites + " Files Has Crawlered";
                resultMsg.SelectionStart = resultMsg.Text.Length;
                resultMsg.ScrollToCaret();
            });
        }




        async private void btnCrawler_Click(object sender, EventArgs e)
        {
            
            btnCrawler.Enabled = false;
            //初始化
            txtImport_TextChanged(sender, e);
            websitesCount = 0; totalsites = 0;
            pointCrawlerCount.Text = websitesCount + " / " + totalsites + " Files Has Crawlered";
            nums = 2;
            errorCatch = 0;
            pointErrorCount.Text = "Error Catch : 0";
            resultMsg.Text = "";
            resaultMsgDetail.Text = "";
            urlList = new List<string>();

            if (txtUrl.Text == "" || txtImport.Text == "")
                return;

            urlList.Add(txtUrl.Text); //出發點網址

            cts = new CancellationTokenSource();
            // Task t = Task.Run(() => craw(cts.Token));
            bool nextPage = await Task.Run(() => urlToList(txtUrl.Text, cts.Token));
            if (nextPage)
            {
                cts2 = new CancellationTokenSource();

                Task t = Task.Run(() => EndPage_StartHtmlCounting(cts2));
            }
            else
            {
                btnCrawler.Enabled = true;
            }

        }

        CancellationTokenSource cts;
        CancellationTokenSource cts2;

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                cts.Cancel();
                cts2.Cancel();
            }
            catch (Exception ex) { }
            //btnCrawler.Enabled = true;
        }

        private void htmlDetail_Click(object sender, EventArgs e)
        {
            resultMsg.Visible = !resultMsg.Visible;
            resaultMsgDetail.Visible = !resaultMsgDetail.Visible;
            resaultMsgDetail_BeforeRegex.Visible = !resaultMsgDetail_BeforeRegex.Visible;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. 關鍵字輸入多個使用「&」以及需再關鍵字前加「空格」做間隔，注意較容易出現的字母需再兩側加空白例如:「 car 」。\n\n2. Times:搜尋到此網頁的關鍵字次數大於多少，列入搜尋累計次數加一。\n\n3. 使用uspto.gov時搜尋單頁或搜尋引擎時，不可勾選HttpClient連線。\n\n4. 搜尋任意單一網頁或文件檔(.pdf |.doc)需勾選HttpClient連線。");
        }

        private void txtImport_TextChanged(object sender, EventArgs e)
        {
            keyWordsList.Clear();
            string[] keywords = txtImport.Text.ToLower().Split('&');
            foreach (var key in keywords)
            {
                KeyWords kw = new KeyWords();
                kw.Keywords = key;
                kw.Related = 0;
                keyWordsList.Add(kw);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = keyWordsList;

        }

        private void WebCrawler_Load(object sender, EventArgs e)
        {
            txtImport_TextChanged(sender, e);
        }


        /// <summary>
        /// 產生檔名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string toFileName(string url)
        {
            String fileName = url.Replace('?', '_');
            fileName = fileName.Replace('/', '_');
            fileName = fileName.Replace('&', '_');
            fileName = fileName.Replace(':', '_');
            fileName = fileName.ToLower();
            if (fileName.EndsWith(".pdf"))
            {
                if(!fileName.EndsWith(".pdf"))
                   fileName = fileName + ".pdf";
            }
            else if (fileName.EndsWith(".doc"))
            {
               if(!fileName.EndsWith(".doc"))
                    fileName = fileName + ".doc";
            }
            return fileName;
        }
    }







    //擴充方法
    public static class Extension
    {
        //非同步委派更新UI
        public static void InvokeIfRequired(
            this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }

    public static class StringExtension
    {
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
            {
                return source;
            }
            return source.Substring(source.Length - tail_length);
        }
    }
    //==================================




    public class KeyWords
    {
        public string Keywords { get; set; }
        public int Related { get; set; }
    }


}
