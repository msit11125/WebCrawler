using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace VocabularyCrawler.Controllers
{
    public class VocabularySearchController : Controller
    {
        public ActionResult Index(string word)
        {
            string searchUrl = "https://tw.voicetube.com/definition/" + word;

            // From Web
            var url = searchUrl;
            var web = new HtmlWeb();
            var doc = web.Load(url);


            var definSingleNode = doc.DocumentNode.SelectSingleNode("//div[@id='definition']");

            if(definSingleNode == null)
            {
                ViewBag.Definition = $"<h1>沒有找到相關的單字</h1>";
                return View();
            }

            foreach (var node in definSingleNode.Descendants())
            {
                if(node.Name == "a")
                {
                    node.Attributes["href"].Value = "http://localhost:59674/VocabularySearch?word=" + node.InnerText;
                }
            }

            // 加入標題
            HtmlNode titleNode = HtmlNode.CreateNode($"<h1>{word}</h1>");
            definSingleNode.PrependChild(titleNode);

            ViewBag.Definition = definSingleNode.InnerHtml;


            return View();
        }



        async public Task<string> tryHttpClient(string url)
        {
            using (var client = new HttpClient())  
            {
                client.DefaultRequestHeaders
                          .Accept
                          .Add(new MediaTypeWithQualityHeaderValue("text/html")); //ACCEPT header
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.125 Safari/537.36");

                string html;
                try
                {
                    var respon = await client.GetAsync(new Uri(url));
                    html = await respon.Content.ReadAsStringAsync();

                }
                catch (Exception)
                {
                    html = "";
                }
                return html;
            }
        }


    }
}