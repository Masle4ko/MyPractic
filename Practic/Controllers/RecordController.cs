using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using System.Collections;
using MyBlog.Core;

namespace WebSiteBlog.Controllers
{
    [CategoriesFilter]
    public class RecordController : Controller
    {
        private DataManager _datamanager;

        public RecordController(DataManager datamanager)
        {

            _datamanager = datamanager;
        }

        //
        // GET: /Record/

        public ActionResult Index()
        {

            return View();
        }

        public int returnCommentCount(int id)
        {
            return _datamanager.Comment.GetCommentsForRecord(id).Count();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult GetRecords()
        {
            ViewData["DateCreate"] = "MyTitle";
            ViewData["list"] = _datamanager.Record.GetAllRecords();
            ViewData["Action"] = "GetRecords";
            return View();

        }
        //действие для перехода на страничку с отдельной статьей
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetRecord(int id)
        {
            ViewData.Model = _datamanager.Record.GetRecord(id);
            ViewData["comments"] = _datamanager.Comment.GetCommentsForRecord(id);
            ViewData["Action"] = "GetRecord";
            Record query = _datamanager.Record.GetMapPoint(id);
            ViewBag.GeoLat = query.GeoLat;
            ViewBag.GeoLong = query.GeoLong;

            //List<Comment> lst = IEnumerable(ViewData["comments"]);

            //ViewData["user"]=_datamanager.Comment.getUser();

            return View();

        }



        public ActionResult DeleteRecord(int id)
        {
            foreach (var item in _datamanager.Comment.GetCommentsForRecord(id))
            {
                _datamanager.Comment.DeleteComment(item.Id_Comment);
            }
            if (_datamanager.Record.DeleteRecord(id) && (_datamanager.calendar.DeleteAppointment(id)))
            {
                return RedirectToAction("GetRecords");
            }

            return View("GetRecord");

        }




        [AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        public ActionResult AddComment(int idcomment)
        {

            if (Request.IsAjaxRequest())
            {
                if (Request.IsAuthenticated)
                {
                    if (Request.Form["textarea"].Length > 0)
                    {

                        _datamanager.Comment.CreateComment(idcomment, Request.Form["textarea"]);
                        ViewData["comments"] = _datamanager.Comment.GetCommentsForRecord(idcomment);
                        return PartialView("_commentsview");

                    }
                    else return RedirectToAction("GetRecord", "Record", new { id = idcomment });

                }
                else return Content("Оставлять комментарии могут только зарегистрированные пользаватели!");
            }

            return RedirectToAction("Index", "Home", new { id = idcomment });
        }

















        //--------------------------------------------------------------------------
        //РЕДАКТИРОВАНИЕ СТАТЕЙ
        //определяем способ передачи данных этого метода(по методу Get)
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditRecord(int id)
        {
            ViewData.Model = _datamanager.Record.GetRecord(id);
            //ViewData.Model = _datamanager.calendar.GetAppointment(id-40);
            return View();
        }

        //определяем способ передачи данных этого метода(по методу Post)
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditRecord(int id, Record rec, Appointments eve)
        {
            eve.Id = id;
            eve.StartDate = Convert.ToDateTime(Request.Form["textar3"]);
            eve.EndDate   = Convert.ToDateTime(Request.Form["textar4"]);
            eve.Description = Request.Form["textar2"];
            rec.Id_Record = id;
            rec.DateEdit = DateTime.Now;
            rec.Text = Request.Form["textar"];
            rec.PreviewText = Request.Form["textar2"];
            rec.Price = Convert.ToInt32(Request.Form["textar5"]);
            rec.Location = Request.Form["textar6"];
            rec.StartDate= Request.Form["textar3"];
            rec.EndDate= Request.Form["textar4"];

            if (ModelState.IsValid)
            {

                //проверка на Ajax- запрос
                _datamanager.calendar.UpdateAppointment(eve);
                _datamanager.Record.UpdateRecord(rec);
                return RedirectToAction("GetRecords");


            }
            return RedirectToAction("EditRecord");


        }
        //--------------------------------------------------------------------------------

        //ДОБАВЛНИЕ НОВОЙ СТАТЬИ
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddRecord()
        {
            ViewData["ListCategory"] = new SelectList(_datamanager.Category.GetAllCategories(), "Id_Category", "NameCategory");
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult AddRecord(string title, string textar, string PreviewText, int Id_Category, string StartDate, string EndDate, int Price, string Location)
        {
            if (title.Length < 3)
                ModelState.AddModelError("Title", "Поле заголовка введено не корректно!");
            if (Request.Form["textarea"] != null)
                ModelState.AddModelError("Text", "Поле содержания введено не корректно!");
            if (ModelState.IsValid)
            {
                string[] coor = RecordRepository.Test(Location);
                _datamanager.Record.CreateRecord(title, Request.Form["textar"], PreviewText, Id_Category, StartDate, EndDate, Price, Location, coor[0], coor[1]);
                _datamanager.calendar.CreateEvent( title,Convert.ToDateTime(StartDate),Convert.ToDateTime(EndDate));
               // string [] coor = MapContext.Test(Location);
               // _datamanager.mapRep.CreateMapPoint(title, Location, Price, StartDate, EndDate, coor[0], coor[1]);
                return RedirectToAction("GetRecords");
            }
            return View();


        }





































    }
}
