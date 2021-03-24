using TestMongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestMongo.Controllers
{
    public class OpinionsController : Controller
    {
        MongoContext ctx;
        public OpinionsController()
        {
            ctx = new MongoContext();
        }
        // GET: Opinions
        public ActionResult Index()
        {
            var OpinionDetails = ctx._database.GetCollection<Opinions>("Opinions").FindAll().ToList();
            return View(OpinionDetails);

        }

        // GET: Opinions/Details/5
        public ActionResult Details(string id)
        {
            var opinionsId = Query<Opinions>.EQ(p => p.Id, new ObjectId(id));
            var opinionsDetails = ctx._database.GetCollection<Opinions>("Opinions").FindOne(opinionsId);

            return View(opinionsDetails);
        }

        // GET: Opinions/Create
        public ActionResult Create()
        {
            return View();
        }
        public class Tag
        {
            public double confidence { get; set; }
            public string tag { get; set; }
        }

        // POST: Opinions/Create
        [HttpPost]
        public ActionResult Create(Opinions opinions)
        {
            var document = ctx._database.GetCollection<BsonDocument>("Opinions");
            //לבדוק פה האם התמונה תקינה
            //var query = Query.EQ("ImageUrl", opinions.ImageUrl);



            //if (count == 0)
            //{
            var result = document.Insert(opinions);
            //}
            //else
            //{
            //אם התמונה לא תקינה אז תחזור לאותו מקום ותתקן
            //    ViewBag.Error = "The picture is not valid";
            //    return View("Create", opinions);
            //}

            return RedirectToAction("Index");
            //try
            //{
            //    // TODO: Add insert logic here

            //    
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Opinions/Edit/5
        public ActionResult Edit(string id)
        {
            var document = ctx._database.GetCollection<Opinions>("Opinions");

            var opinionsDetailscount = document.FindAs<Opinions>(Query.EQ("_id", new ObjectId(id))).Count();

            if (opinionsDetailscount > 0)
            {
                var opinionsObjectid = Query<Opinions>.EQ(p => p.Id, new ObjectId(id));

                var opinionsDetail = ctx._database.GetCollection<Opinions>("Opinions").FindOne(opinionsObjectid);

                return View(opinionsDetail);
            }
            return RedirectToAction("Index");
        }

        // POST: Opinions/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Opinions opinions)
        {
            try
            {
                opinions.Id = new ObjectId(id);
                //Mongo Query  
                var opinionsObjectId = Query<Opinions>.EQ(p => p.Id, new ObjectId(id));
                // Document Collections  
                var collection = ctx._database.GetCollection<Opinions>("Opinions");
                // Document Update which need Id and Data to Update  
                var result = collection.Update(opinionsObjectId, Update.Replace(opinions), UpdateFlags.None);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Opinions/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            //Mongo Query  
            var OpinionsObjectid = Query<Opinions>.EQ(p => p.Id, new ObjectId(id));
            var OpinionsDetail = ctx._database.GetCollection<Opinions>("Opinions").FindOne(OpinionsObjectid);
            return View(OpinionsDetail);
        }

        // POST: Opinions/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, Opinions opinions)
        {
            try
            {
                //Mongo Query  
                var opinionsObjectid = Query<Opinions>.EQ(p => p.Id, new ObjectId(id));
                // Document Collections  
                var collection = ctx._database.GetCollection<Opinions>("Opinions");
                // Document Delete which need ObjectId to Delete Data  
                var result = collection.Remove(opinionsObjectid, RemoveFlags.Single);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}


