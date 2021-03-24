using TestMongo;
using TestMongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMongo.Controllers
{
    public class StoreController : Controller
    {
        private MongoContext ctx;
        public static string IcId;
        public StoreController()
        {
            ctx = new MongoContext();
            //ViewBag.IsAdmin = false;
            //ViewBag.In = false;
        }
        // GET: Store
        public ActionResult Index()
        {
            var StoreDetails = ctx._database.GetCollection<Store>("Store").FindAll().ToList();
            return View(StoreDetails);
        }

        // GET: Store/Details/5
        public ActionResult Details(string id)
        {
            var StoreId = Query<Store>.EQ(i => i.Id, new ObjectId(id));
            var CurrentStore = ctx._database.GetCollection<Store>("Store").FindOne(StoreId);
            return View(CurrentStore);
        }

        // GET: Store/Create
        [HttpPost]
        public ActionResult Create(Store store)
        {
            try
            {
                var document = ctx._database.GetCollection<BsonDocument>("Store");
                var query = Query.And(Query.EQ("Name", store.Name), Query.EQ("Adress", store.Adress));

                var count = document.FindAs<Store>(query).Count();

                if (count == 0)
                {
                    store.IceCreams = ViewBag.iceCreamsList;
                    var result = document.Insert(store);
                    return RedirectToAction("AddMenu", new { id = store.Id });
                }
                else
                {
                    ViewBag.Error = "This store Already Exists";
                    return View("Create");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Store/Create
        //[HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            return View();
        }
        public ActionResult AddMenu(string id)
        {
            if (id != null)
            {
                IcId = id;
            }
            return View();
        }
        public ActionResult AddIceCream(string id)
        {
            try
            {
                var storeId = Query<Store>.EQ(i => i.Id, new ObjectId(IcId));
                var currentStore = ctx._database.GetCollection<Store>("Store").FindOne(storeId);
                var IceCreamId = Query<Store>.EQ(i => i.Id, new ObjectId(id));
                var currentIceCream = ctx._database.GetCollection<IceCream>("IceCream").FindOne(IceCreamId);
                if (currentStore.IceCreams == null)
                {
                    currentStore.IceCreams = new List<IceCream>();
                }
                currentStore.IceCreams.Add(currentIceCream);
                var collection = ctx._database.GetCollection<Store>("Store");
                var result = collection.Update(storeId, Update.Replace(currentStore), UpdateFlags.None);
                return RedirectToAction("AddMenu");
            }
            catch
            {

                return RedirectToAction("Index");
            }
        }

        // GET: Store/Edit/5
        public ActionResult Edit(string id)
        {
            var document = ctx._database.GetCollection<Store>("Store");
            var StoreDetailscount = document.FindAs<Store>(Query.EQ("_id", new ObjectId(id))).Count();

            if (StoreDetailscount > 0)
            {
                var Objectid = Query<Store>.EQ(p => p.Id, new ObjectId(id));

                var detail = ctx._database.GetCollection<Store>("Store").FindOne(Objectid);

                return View(detail);
            }
            return RedirectToAction("Index");
        }

        // POST: Store/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Store store)
        {
            store.Id = new ObjectId(id);
            var StoreObjectId = Query<Store>.EQ(p => p.Id, new ObjectId(id));
            var collection = ctx._database.GetCollection<Store>("Store");
            var result = collection.Update(StoreObjectId, Update.Replace(store), UpdateFlags.None);
            return RedirectToAction("Index");

        }

        //// GET: Store/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Store/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
