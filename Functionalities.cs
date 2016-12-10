using System;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
namespace Pixol
{
    partial class Program
    {
        public static void getPublicPhotos(string targetprofile,int likes=0)
        {
            try
            {
                string path = Path.Combine(Path.GetFullPath(System.IO.Directory.GetCurrentDirectory()), targetprofile);
                DirectoryInfo newdir = new DirectoryInfo(path);
                string maxid = "";
                bool more = true;
                do
                {
                    String url = String.Format(ConfigurationManager.AppSettings["InstaPublic_URL"] + maxid, targetprofile);
                    var client = new RestClient(url);
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    if (response.ErrorException != null)
                    {
                        throw new System.InvalidOperationException(response.ErrorException.Message);
                    }
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new System.InvalidOperationException("couldn't find \"" + url + "\"");
                    }
                    var instajson = response.Content;
                    Console.WriteLine(instajson);
                    RootObject obj = JsonConvert.DeserializeObject<RootObject>(instajson);
                    Console.WriteLine("Status: " + obj.status.ToString());
                    foreach (Item item in obj.items)
                    {
                        if (item.type == "image")
                        {
                            if (item.likes.count >= likes)
                            {
                                Console.WriteLine("Downloading: Picture " + item.created_time + "from: " + item.images.standard_resolution.url.ToString());
                                var clienttmp = new RestClient(item.images.standard_resolution.url);
                                var bytes = clienttmp.DownloadData(request);
                                if (bytes == null)
                                {
                                    throw new System.InvalidOperationException("couldn't resolve \"" + item.images.standard_resolution.url + "\"");
                                }
                                if (newdir.Exists == false)
                                {
                                    newdir.Create();
                                }
                                using (var stream = new MemoryStream(bytes))
                                {
                                    Bitmap bitmap = new Bitmap(stream);
                                    bitmap.Save(Path.Combine(path, item.created_time + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                bytes = null;
                            }
                        }
                    }
                    if (obj.more_available == false)
                    {
                        more = false;
                    }
                    else
                    {
                        maxid = obj.items.Last<Item>().id;
                    }
                } while (more);
            }
            catch
            {
                throw;
            }
        }
        static string getAccessToken(string username,string password)
        {
            Credentials.username = username;
            Credentials.password = password;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form frm = new Authenticator();
            Application.Run(frm);
            return Authenticator.access_token;
        }
        public static void getPrivatePhotos(string username, string password,int likes=0)
        {
            try
            {
                Console.WriteLine("Authenticating...");         
                string access=getAccessToken(username, password);
                String url = String.Format(ConfigurationManager.AppSettings["InstaPrivate_URL"] , access);
                bool more = true;
                do
                {
                    Console.WriteLine(url);
                    var client = new RestClient(url);
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);
                    if (response.ErrorException != null)
                    {
                        Console.WriteLine(response.ErrorMessage);
                    }
                    if (response.ErrorException != null)
                    {
                        throw response.ErrorException;
                    }
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new Exception("ErrorCode: 400\nErrorMessage: Couldn't Authenticate!");
                    }
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new System.InvalidOperationException("couldn't find \"" + url + "\"");
                    }
                    Console.WriteLine(response.Content);
                    RootObject obj = JsonConvert.DeserializeObject<RootObject>(response.Content);
                    Console.WriteLine("Status: " + obj.meta.code.ToString());
                    string path = Path.Combine(Path.GetFullPath(System.IO.Directory.GetCurrentDirectory()),obj.data.First().user.username);
                    DirectoryInfo newdir = new DirectoryInfo(path);
                    foreach (Datum item in obj.data)
                    {
                        if (item.type == "image")
                        {
                            if (item.likes.count >= likes)
                            {
                                Console.WriteLine("Downloading: Picture " + item.created_time + "from: " + item.images.standard_resolution.url.ToString());
                                var clienttmp = new RestClient(item.images.standard_resolution.url);
                                var bytes = clienttmp.DownloadData(request);
                                if (bytes == null)
                                {
                                    throw new System.InvalidOperationException("couldn't resolve \"" + item.images.standard_resolution.url + "\"");
                                }
                                if (newdir.Exists == false)
                                {
                                    newdir.Create();
                                }
                                using (var stream = new MemoryStream(bytes))
                                {
                                    Bitmap bitmap = new Bitmap(stream);
                                    bitmap.Save(Path.Combine(path, item.created_time + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                bytes = null;
                            }
                        }
                    }
                    if (obj.pagination.next_url==null)
                    {
                        more = false;
                    }
                    else
                    {
                        url = obj.pagination.next_url.ToString();
                    }
                } while (more);
            }
            catch
            {
                throw;
            }
        }
    }
}
