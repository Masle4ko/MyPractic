using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using MyBlog.Core;
//using MyBlog.GoogleGeocoding;
using MyBlog.SearchEngine;
using System.ServiceModel.Syndication;
using System.Collections;
using MongoDB.Bson;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DotNetOpenAuth.Messaging;
using Newtonsoft.Json;



namespace MyBlog.Controllers
{
    [CategoriesFilter]
    public class HomeController : Controller
    {
        //Реализуем паттерн DependencyInjection (впрыскивание в класс-контроллер 
        //обьекта для работы с классами данных(репозиториями))
        //1 - создаем поле
        //2- создаем конструктор и присваиваем в поле сылку на класс данных
        private DataManager _datamanager;

        public HomeController(DataManager datamanager)
        {
            _datamanager = datamanager;

        }

        //////////////////////////////////////////////////

        public ActionResult Index()
        {

            ViewData["Title"] = "MyTitle";
            ViewData["list"] = _datamanager.Record.GetAllRecords();
            RecordIdForComment r = new RecordIdForComment();
            ViewData["commentCount"] = r.recordforcomment((IEnumerable<Record>)ViewData["list"]);
            IEnumerable<Record> query = _datamanager.Record.GetAllRecords().OrderByDescending(record => record.Id_Record);
          
            ViewData["SHIT"] = query;
            return View();
        }


        public ActionResult StartPage()
        {


            return View();
        }


        public ActionResult Search(string term)
        {
            List<SearchItem> result = new List<SearchItem>();
            var recordresultsDate = _datamanager.Record.GetAllRecords().Where(x => x.StartDate.Contains(term) || x.StartDate.Contains(term))
            .Take(5)
            .ToList()
            .Select(x => new RecordSearch(x));
        
            var recordresults = _datamanager.Record.GetAllRecords().Where(x => x.Title.Contains(term) || x.Title.Contains(term))
            .Take(5)
            .ToList()
            .Select(x => new RecordSearch(x));
 
            if (recordresults.Any())
            {


                result.AddRange(recordresults);
            }

            if (recordresultsDate.Any())
            {


                result.AddRange(recordresultsDate);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
    }


        public ActionResult Rss()
        {
            SyndicationFeed feed = new SyndicationFeed("Лента статей", "Читайте мои новые статьи!", new Uri(Request.Url.AbsoluteUri));
            feed.Items = (from t in _datamanager.Record.GetAllRecords()
                          orderby t.DateCreate
                          select
                          new SyndicationItem(t.Title, t.Text, new Uri(Request.Url.AbsoluteUri))
                            );
            return new RssViewResult(feed);
        }

        
        public ActionResult FilterRecord()
        {
            ViewData["ListCategory"] = new SelectList(_datamanager.Category.GetAllCategories(), "Id_Category", "NameCategory");
            return View();
        }
 //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FilterRecordPost(string title="null", string startDate="null", int Id_Category=7, int min=1, int max=9999)
        {
            
           
            List <RecordFilter> result = new List<RecordFilter>();

            var recordresultsSearchTitle = _datamanager.Record.GetAllRecords()
                .Where((x => x.Title.Contains(title) || x.Title.Contains(title)))
                .Where((x => x.StartDate.Contains(startDate) || x.StartDate.Contains(startDate)))
                .Where(x => x.Category_Id.Equals(Id_Category))
                .Where(x => x.Price > min && x.Price < max)
                .ToList()
                .OrderBy(x => x.StartDate)
             .Select(x => new RecordFilter(x));

            if (recordresultsSearchTitle.Any())
            {


                result.AddRange(recordresultsSearchTitle);
            }
            //IEnumerable<RecordSearch> query = result;
            ViewData["SHOT"] = result;
            return PartialView(result);
           // return Json(result, JsonRequestBehavior.AllowGet);
        }





        public ActionResult Google()
        {
            Record query = _datamanager.Record.GetMapPoint(1);
            ViewBag.GeoLat = query.GeoLat;
            ViewBag.GeoLong = query.GeoLong;
            return View(ViewBag);
        }

        public JsonResult GetData()
        {
            // создадим список данных
            //Location stations = _datamanager.mapRep.GetMapPoint(id);
            List<RecordFilter> result = new List<RecordFilter>();
            var recordresultsSearchTitle = _datamanager.Record.GetAllRecords()
            .ToList()
            .Select(x => new RecordFilter(x));

            if (recordresultsSearchTitle.Any())
            {


                result.AddRange(recordresultsSearchTitle);
            }
            // return View(stations);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        

    }
}
