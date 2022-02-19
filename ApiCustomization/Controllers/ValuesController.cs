using ApiCustomization.JsonHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ApiCustomization.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage API_GetMethod(string data)
        {
            if (data != "")
            {
                // perform some operation

                return JsonHelper.GetJsonObject(new { success = true, message = "Everything is Fine" });
            }

            return JsonHelper.GetJsonObject(new { success = false, message = "Something is Fine" });
        }

        [HttpPost]
        public HttpResponseMessage API_PostMethod([FromBody] string data)
        {
            try
            {
                var qs = HttpUtility.ParseQueryString(Base64Decode(data));
                var Guid = qs.Get("FirstName");
                var Domain = qs.Get("LastName");
                return JsonHelper.GetJsonObject(new { success = false, message = "Something is Fine" });
            }
            catch (Exception exc)
            {
                return JsonHelper.GetJsonObject(new { success = false, message = exc.Message, dbGuid = "" });
            }
        }

        [System.Web.Http.NonAction]
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.Default.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        [System.Web.Http.NonAction]
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.Default.GetString(base64EncodedBytes);
        }
    }

}
