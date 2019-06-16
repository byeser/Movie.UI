using Movie.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Movie.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public ActionResult Index()
        {
            IEnumerable<Film> list = null;
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                                                                               (se, cert, chain, sslerror) =>
                                                                               {
                                                                                   return true;
                                                                               };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                response = client.GetAsync("api/Films/GetAll").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(content.Result).ToString();
                    string k = result.Replace("{\r\n  \"films\":", "").Replace("\r\n}", "");
                    list = JsonConvert.DeserializeObject<IEnumerable<Film>>(k);
                    return View(list);

                }
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            IEnumerable<Film> list = null;
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                                                                               (se, cert, chain, sslerror) =>
                                                                               {
                                                                                   return true;
                                                                               };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                if (name == string.Empty)
                    response = client.GetAsync("api/Films/GetAll").Result;
                else
                    response = client.GetAsync("api/Films/Get/" + name).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync();
                    if (content.Result == null)
                        return View(list);
                    var result = JsonConvert.DeserializeObject<dynamic>(content.Result).ToString();
                    string k = result.Replace("{\r\n  \"films\":", "").Replace("\r\n}", "");
                    list = JsonConvert.DeserializeObject<IEnumerable<Film>>(k);
                    return View(list);

                }
            }
            return View(list);
        }

        public JsonResult UpdateData()
        {
            IEnumerable<Film> list = null;
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                                                                               (se, cert, chain, sslerror) =>
                                                                               {
                                                                                   return true;
                                                                               };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                response = client.GetAsync("api/Films/Put").Result;
                response.EnsureSuccessStatusCode();
                return Json(response.EnsureSuccessStatusCode(),JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}