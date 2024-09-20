using CefSharp;
using CefSharp.DevTools.Network;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;

namespace SpiderFont
{
    public partial class Form1 : Form
    {
        private string _url;
        private string _data;
        private string _user;
        private string _password;
        private string _auth;
        public Form1()
        {
            InitializeComponent();
            _user = ConfigurationManager.AppSettings["user"];
            _password = ConfigurationManager.AppSettings["password"];
            
            statusUserName.Text=_user;
            statusMessage.Text = "";

            chromiumWebBrowser1.LoadUrl(System.Configuration.ConfigurationManager.AppSettings["p-url"]);
            chromiumWebBrowser1.MenuHandler = new CustomMenu();

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000;
            timer.Tick += OnTickup;
            timer.Enabled = true;
        }
        private void OnTickup(object sender, EventArgs e)
        {
            new Thread(async f =>
            {
                await chromiumWebBrowser1.GetMainFrame().EvaluateScriptAsync(@"
(function(){
var ele=[...document.head.childNodes].find(f=>f.innerHTML.indexOf('spider-font')>0);
if(ele==null||ele==undefined)
    return '';

var match=/url\(['""](?<data>.+?)['""]\)/.exec(ele.innerHTML);
if(match!=null)
    return match.groups['data'];

match = /src:\s*url\(data:application\/x-font-ttf;base64,(?<data>.*?)\)/.exec(ele.innerHTML);
if(match!=null)
    return match.groups['data'];

return '';
})();
").ContinueWith(s =>
            {
                var url = s.Result.Result.ToString();
                if (!string.IsNullOrEmpty(url))
                {
                    if (_url != url)
                    {
                        var client = new RestClient();
                        var request = new RestRequest(ConfigurationManager.AppSettings["d-url"], Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        new HttpBasicAuthenticator(_user, _password).Authenticate(client,request);
                        request.AddHeader("Accept", "*/*");
                        var body = $@"""{url}""";
                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                        var response = client.Execute(request);
                        if (response != null && response.StatusCode == HttpStatusCode.OK)
                        {
                            _url = url;
                            _data = response.Content;
                            
                            var json= new JsonDeserializer().Deserialize<dynamic>(response);
                            this.Invoke((MethodInvoker)delegate
                            {
                                statusMessage.Text = $"[{DateTime.Now:MM-dd HH:mm}]请求解密数据成功，剩余点数：{json["credit"]}";
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                statusMessage.Text = $"[{DateTime.Now:MM-dd HH:mm}]请求解密数据失败，请核实帐号是否有效(StatusCode:{response.StatusCode})。";
                            });
                            return;
                        }
                    }

                    chromiumWebBrowser1.GetMainFrame().EvaluateScriptAsync(@"
var data=JSON.parse('#1');
[...document.querySelectorAll('.__spider_font')].filter(s=>s.childElementCount == 0).forEach(s=>{
var text = s.innerHTML.split('').reduce((a,b)=>a + (data.result[b.charCodeAt()] ?? b), '');
s.innerHTML = text;
})".Replace("#1", _data));
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        statusMessage.Text = $"[{DateTime.Now:MM-dd HH:mm}]尚未找到需要解密的数据。";
                    });
                }
            });

            }).Start();

        }

        private void OnRefresh(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Refresh();
        }

        private void OnBackward(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Back();
        }

        private void OnForward(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Forward();
        }
    }
}
