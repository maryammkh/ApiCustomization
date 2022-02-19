using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public bool CheckGetMethod()
        {
            try
            {
                var url = $"https://localhost:44322/ARC_API/Values/API_GetMethod?data=test";
                var request = WebRequest.Create(url);
                request.Method = "GET";

                // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                Stream streamResponse = myHttpWebResponse.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                Char[] readBuff = new Char[256];
                int count = streamRead.Read(readBuff, 0, 256);
                string response = "";
                while (count > 0)
                {
                    String outputData = new String(readBuff, 0, count);
                    count = streamRead.Read(readBuff, 0, 256);
                    response += outputData;
                }
                
                streamResponse.Close();                                     // Close the Stream object.
                streamRead.Close();                                         // Close the Stream read object.
                myHttpWebResponse.Close();                                  // Release the HttpWebResponse Resource.
                JObject jObj = JObject.Parse(response);                     // Parse the object graph
                bool success = Convert.ToBoolean(jObj["success"]);          // Retrive value by key
                string message = jObj["message"].ToString();                // Retrive value by key
                return success;                                             // all ok, and return true
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckPostMethod(string firstName, string lastName)
        {
            try
            {
                var url = $"https://localhost:44322/ARC_API/Values/API_PostMethod";
                var request = WebRequest.Create(url);
                var postData = "FirstName=" + Uri.EscapeDataString(firstName);        // sending store url in form body
                postData += "&LastName=" + Uri.EscapeDataString(lastName);
                var data = Encoding.ASCII.GetBytes("\"" + Base64Encode(postData) + "\"");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream()) { stream.Write(data, 0, data.Length); }

                // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();

                Stream streamResponse = myHttpWebResponse.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                Char[] readBuff = new Char[256];
                int count = streamRead.Read(readBuff, 0, 256);
                string response = "";
                while (count > 0)
                {
                    String outputData = new String(readBuff, 0, count);
                    count = streamRead.Read(readBuff, 0, 256);
                    response += outputData;
                }
                // Close the Stream object.
                streamResponse.Close();
                streamRead.Close();
                // Release the HttpWebResponse Resource.
                myHttpWebResponse.Close();

                JObject jObj = JObject.Parse(response); // Parse the object graph
                bool success = Convert.ToBoolean(jObj["success"]); // Retrive value by key, it is a guid stored in generic attribute from main app and retrieved from remote app
                string message = jObj["message"].ToString(); // Retrive value by key, it is a guid stored in generic attribute from main app and retrieved from remote app
                return success;    // all ok, and return true
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public JsonResult SaveSeparateTaskDomain()
        {
            //CheckGetMethod();
            CheckPostMethod("Muhammad", "Arsalan");
            if (true)
            {
                return Json("Success");
            }
            else
            {
                return Json("Error");
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.Default.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.Default.GetString(base64EncodedBytes);
        }
    }
}