using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;

using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Common;

using AppointmentCalendar.Models;
using MyBlog.Core;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [CategoriesFilter]
    public class CalendarController : Controller
    {
        //
        // GET: /Home/
        private DataManager _datamanager;
       
        public CalendarController(DataManager datamanager)
        {
            _datamanager = datamanager;
        }
        private CalendarContext dbC = new CalendarContext();
       // private  CalendarContextdbC =  new CalendarContext();
        [HttpGet]
        public ActionResult Index()
        {
         
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Flat;
            scheduler.Config.isReadonly = true;
            scheduler.Config.first_hour = 6;
            scheduler.Config.last_hour = 23;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var apps = dbC.Appointments.ToList();
            return new SchedulerAjaxData(apps);
        }

        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = DHXEventsHelper.Bind<AppointmentCalendar.Models.Appointment>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        dbC.Appointments.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        dbC.Entry(changedEvent).State = EntityState.Deleted;
                        break;
                    default:// "update"  
                        dbC.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
                dbC.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbC.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
