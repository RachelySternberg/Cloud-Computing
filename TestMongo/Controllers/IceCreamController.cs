



using aaProject.Tools;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMongo.Models;

namespace TestMongo.Controllers
{
    public class IceCreamController : Controller
    {

        static int counter = 0;
        private MongoContext ctx;

        public IceCreamController()
        {
            ctx = new MongoContext();
            counter++;
        }
        // GET: IceCream
        public ActionResult Index()
        {
            var IceCreameDetails = ctx._database.GetCollection<IceCream>("IceCream").FindAll().ToList();
            return View(IceCreameDetails);

        }

        // GET: IceCream/Details/5
        public ActionResult Details(string id)
        {
            var IceCreamId = Query<IceCream>.EQ(i => i.Id, new ObjectId(id));
            var CurrentIC = ctx._database.GetCollection<IceCream>("IceCream").FindOne(IceCreamId);
            return View(CurrentIC);
        }


        // GET: IceCream/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(IceCream Icecream)
        {
            var document = ctx._database.GetCollection<BsonDocument>("IceCream");
            var query = Query.And(Query.EQ("Calories", Icecream.Calories), Query.EQ("Description", Icecream.Description));

            var count = document.FindAs<IceCream>(query).Count();
            await Nutritions.NutAsync(Icecream.NutritionalValues);
            Icecream.Calories = Nutritions.Calories;
            Icecream.Protein = Nutritions.Proteins;
            Icecream.Fats = Nutritions.Fats;
            if (count == 0)
            {
                var result = document.Insert(Icecream);
            }
            else
            {
                ViewBag.Error = "ice cream Already Exist";
                return View("Create");
            }
            return RedirectToAction("Index");
        }


        // POST: IceCream/Create



        public ActionResult Create(FormCollection collection)
        {
            return View();

            //try
            //{
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: IceCream/Edit/5
        public ActionResult Edit(string id)
        {
            var document = ctx._database.GetCollection<IceCream>("IceCream");
            var IceCreamDetailscount = document.FindAs<IceCream>(Query.EQ("_id", new ObjectId(id))).Count();

            if (IceCreamDetailscount > 0)
            {
                var Objectid = Query<IceCream>.EQ(p => p.Id, new ObjectId(id));

                var detail = ctx._database.GetCollection<IceCream>("IceCream").FindOne(Objectid);

                return View(detail);
            }
            return RedirectToAction("Index");
        }

        // POST: IceCream/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, IceCream iceCream)
        {
            iceCream.Id = new ObjectId(id);
            var IceCreamObjectId = Query<IceCream>.EQ(p => p.Id, new ObjectId(id));
            var collection = ctx._database.GetCollection<IceCream>("IceCream");
            var result = collection.Update(IceCreamObjectId, Update.Replace(iceCream), UpdateFlags.None);
            return RedirectToAction("Index");

        }
    }
}

// GET: IceCream/Delete/5
// public ActionResult Delete(string id)
//{
//return View();

//    TempData["Message"] = "Are you sure you want to delete this icecream? :(";
// return RedirectToAction("Index");
//}

// POST: IceCream/Delete/5
//  [HttpPost]
// public ActionResult Delete(string id, FormCollection collection)
// {
//   try
//   {
// TODO: Add delete logic here

//      ViewBag.Message = "Are you sure you want to delete this icecream? :(";
//      return RedirectToAction("Index");
//  }
// catch
//  {
// return View();
//  }
//  }

