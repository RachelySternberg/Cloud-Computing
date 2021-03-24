//using TestMongo.App_Start;
using TestMongo.Models;
using TestMongo.Tools;
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
    public class NoteController : Controller
    {
        private MongoContext ctx;
        private Imagga img;
        public static string IcId;
        public NoteController()
        {
            ctx = new MongoContext();
            img = new Imagga();
        }
        // GET: Note
        public ActionResult Index()
        {
            var Details = ctx._database.GetCollection<Note>("Notes").FindAll().ToList();
            return View("UserLandingView", Details);
        }

        // GET: Note/Details/5
        public ActionResult Details(string id)
        {
            var Id = Query<Note>.EQ(i => i.Id, new ObjectId(id));
            var Current = ctx._database.GetCollection<Note>("Notes").FindOne(Id);
            return View("Details", "Note", Current);
        }

        // GET: Note/Create
        //public ActionResult Create()
        //{
        //    return View("Create","Note");
        //}
        public ActionResult Create(FormCollection collection)
        {
            return View();
        }

        // POST: Note/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(Note note)
        {
            try
            {
                var document = ctx._database.GetCollection<BsonDocument>("Notes");
                var query = Query.And(Query.EQ("StoreName", note.StoreName), Query.EQ("Image", note.Iamge));

                var count = document.FindAs<Note>(query).Count();
                var x = await Imagga.ImgCheckAsync(note.Iamge);//img.ImgCheckAsync(note.Iamge);
                if (count != 0)
                {
                    ViewBag.Error = "This note Already Exists";
                    return RedirectToAction("Create", "Note");
                }
                if (x != true)
                {
                    ViewBag.Error = "Try again! the worng image";
                    return RedirectToAction("Create", "Note");
                }
                else
                {
                    ViewBag.Error = "Thank you =)";
                    var result = document.Insert(note);
                }
                var doc = ctx._database.GetCollection<Note>("Notes").FindAll().ToList(); ;
                return View("UserLandingView", doc);
            }
            catch
            {
                return RedirectToAction("Create", "Note");
            }
        }

        // GET: Note/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                var document = ctx._database.GetCollection<Note>("Notes");
                var StoreDetailscount = document.FindAs<Note>(Query.EQ("_id", new ObjectId(id))).Count();

                if (StoreDetailscount > 0)
                {
                    var Objectid = Query<Note>.EQ(p => p.Id, new ObjectId(id));

                    var detail = ctx._database.GetCollection<Note>("Notes").FindOne(Objectid);

                    return View(detail);
                }
                var doc = ctx._database.GetCollection<Note>("Notes").FindAll().ToList();
                return View("UserLandingView", "Shared", doc);
            }
            catch
            {
                return RedirectToAction("Edit", "Note");
            }
        }

        // POST: Note/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Note note)
        {
            try
            {
                note.Id = new ObjectId(id);
                var StoreObjectId = Query<Note>.EQ(p => p.Id, new ObjectId(id));
                var collection = ctx._database.GetCollection<Note>("Notes");
                var result = collection.Update(StoreObjectId, Update.Replace(note), UpdateFlags.None);
                var doc = collection.FindAll().ToList();
                return View("UserLandingView", doc);

            }
            catch
            {
                return RedirectToAction("Edit", "Note");
            }
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Note/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("UserLandingView");
            }
            catch
            {
                return View();
            }
        }
    }
}
