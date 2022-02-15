using ApiCustomization.JsonHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiCustomization.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage MyApiTestingMethod(string data)
        {
            if (data != "")
            {
                // perform some operation

                return JsonHelper.GetJsonObject(new { success = true, message = "Everything is Fine" });
            }

            return JsonHelper.GetJsonObject(new { success = false, message = "Something is Fine" });
        }
    }

}
