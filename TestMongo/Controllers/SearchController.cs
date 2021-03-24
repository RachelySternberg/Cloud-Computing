using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMongo.Models;

namespace TestMongo.Controllers
{
    public class SearchController : Controller
    {
        private MongoContext ctx;
        public SearchController()
        {
            ctx = new MongoContext();
        }
        public ActionResult Index(string des = null, bool search_nutrients = false, float protein = float.MaxValue, float fats = float.MaxValue, float calories = float.MaxValue)
        {
            List<IceCream> list;
            if (des == null && search_nutrients == false)// you wants all the list
            {
                list = ctx._database.GetCollection<IceCream>("IceCream").FindAll().ToList();
                ViewBag.Error = "";
            }
            else if (search_nutrients == false) // filter by description
            {
                var description = des;
                list = ctx._database.GetCollection<IceCream>("IceCream").FindAll().Where(i => i.Description.Contains(description)).ToList();
                ViewBag.Error = "Results for: " + des;
                return View(list);
            }
            else // filter by  nutrients
            {
                list = ctx._database.GetCollection<IceCream>("IceCream").FindAll().Where(i => i.Protein <= protein && i.Fats <= fats && i.Calories <= calories).ToList();
                string p = protein.ToString(); string l = fats.ToString(); string e = calories.ToString();
                if (protein == float.MaxValue) { p = "None"; }
                if (fats == float.MaxValue) { l = "None"; }
                if (calories == float.MaxValue) { e = "None"; }
                ViewBag.Error = "Results for: Protein = " + p + ", Fats = " + l + ", Calories = " + e;
                //return View(list);
            }
            return View(list);
        }

    }
}
