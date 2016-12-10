using System;
using System.Windows.Forms;
namespace Pixol
{
    class Authenticator:Form
    {
        public static string access_token="";
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private static bool reps = true; 
        public Authenticator()
        {
            InitializeComponent();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            //
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(477, 396);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("https://www.instagram.com/oauth/authorize/?client_id=4fc653ac0f854e3aa3b2cdaf2194" +
        "1ea7&redirect_uri=http://codenya.pk/&response_type=token", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // Authenticator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 396);
            this.Controls.Add(this.webBrowser1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Authenticator";
            this.Text = "Authenticator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Authenticator_FormClosing);
            this.ResumeLayout(false);
            this.Opacity = 0;
            this.ShowInTaskbar = false;
        }
        private void Authenticator_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                access_token = webBrowser1.Url.ToString().Substring(webBrowser1.Url.ToString().IndexOf("=") + 1);
            }
            catch
            {
                Console.WriteLine("Error: Couldn't load site, properly!");
            }
        }
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            
            if (webBrowser1.Url.ToString().Contains("access_token"))
            {
                //deleting cookies
                System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 2");
                this.Close();
            }
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Uri login= new Uri("https://www.instagram.com/accounts/login/?force_classic_login=&next=/oauth/authorize/%3Fclient_id%3D4fc653ac0f854e3aa3b2cdaf21941ea7%26redirect_uri%3Dhttp%3A//codenya.pk/%26response_type%3Dtoken");
            if (webBrowser1.Url== login)
            {
                if (reps == true)
                {
                    reps = false;
                    webBrowser1.Document.GetElementById("id_username").SetAttribute("value", Credentials.username);
                    webBrowser1.Document.GetElementById("id_password").SetAttribute("value", Credentials.password);
                    var elem = webBrowser1.Document.GetElementById("");
                    foreach (var element in webBrowser1.Document.GetElementsByTagName("input"))
                    {
                        var a = (HtmlElement)element;
                        a.InvokeMember("click");
                    }
                }
                else
                {
                    this.Close();
                }
            }
            Uri allowing = new Uri("https://www.instagram.com/oauth/authorize/?client_id=4fc653ac0f854e3aa3b2cdaf21941ea7&redirect_uri=http://codenya.pk/&response_type=token");
            if (webBrowser1.Url==allowing)
            {
                var elem = webBrowser1.Document.GetElementById("");
                foreach (var element in webBrowser1.Document.GetElementsByTagName("input"))
                {
                    var a = (HtmlElement)element;
                    if (a.Name == "allow")
                    {
                        a.InvokeMember("click");
                    }
                }
            }
        }
    }
}
