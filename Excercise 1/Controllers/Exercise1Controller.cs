using Excercise_1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Excercise_1.Controllers
{
    public class Exercise1Controller : Controller
    {
        private const string Key = "countriesList";
        private List<SelectListItem> countriesList = new List<SelectListItem>();

        [HttpPost]
        public IActionResult AddCountry(IFormCollection formCollection)

        {



            countriesList = JsonConvert.DeserializeObject<List<SelectListItem>>(HttpContext.Session.GetString(Key));

         

            string felt1 = formCollection["Felt1"];
            string felt2 = formCollection["Felt2"];

            countriesList.Add(new SelectListItem { Text = felt1, Value = felt2 });
            HttpContext.Session.SetString(Key, JsonConvert.SerializeObject(countriesList));

            ViewBag.Countries = countriesList;

            Utilities.SortSelectList(ViewBag.Countries, "");
            return View("Index");

        }



        [HttpPost]
        public IActionResult Index(String Countries)
        {



            ViewBag.Countries = JsonConvert.DeserializeObject<List<SelectListItem>>(HttpContext.Session.GetString(Key));
            ViewBag.Country = Countries;
            Utilities.SortSelectList(ViewBag.Countries, Countries);
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(Key) == null)
            {
                countriesList.Add(new SelectListItem { Text = "China", Value = "CN" });
                countriesList.Add(new SelectListItem { Text = "Denmark", Value = "DK" });
                countriesList.Add(new SelectListItem { Text = "England", Value = "UK" });
                HttpContext.Session.SetString(Key, JsonConvert.SerializeObject(countriesList));

            }
            else
            {
                countriesList = JsonConvert.DeserializeObject<List<SelectListItem>>(HttpContext.Session.GetString(Key));
                //Utilities.SortSelectList(countriesList);
            }

            ViewBag.Countries = countriesList;




            return View();
        }
    }
}
