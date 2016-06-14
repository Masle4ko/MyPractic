using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyBlog.Models;
using DHTMLX.Common;
using System.Web.Mvc;
using System.Data;

namespace AppointmentCalendar.Models
{
    public class CalendarContext : DbContext
    {
        private MyDBObjectsLINQDataContext _datacontext;
       // private CalendarContext dbC = new CalendarContext();

        public CalendarContext() : base("DbblogConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CalendarContext>());
        }

        public CalendarContext(MyDBObjectsLINQDataContext _datacontext)
        {
            this._datacontext = _datacontext;
        }

        public System.Data.Entity.DbSet<AppointmentCalendar.Models.Appointment> Appointments { get; set; }


        public Appointments CreateEvent( string title,  DateTime _startDate, DateTime _endDate)
        {

            Appointments eve = new Appointments { Description = title, StartDate = _startDate, EndDate = _endDate };
            _datacontext.Appointments.InsertOnSubmit(eve);
            _datacontext.SubmitChanges();
            return eve;


        }


        public Appointments GetAppointment(int id)
        {
            return _datacontext.Appointments.SingleOrDefault(t => t.Id == id);

        }

        //обновление статьи
        public void UpdateAppointment(Appointments Myrec)
        {
            Appointments dbrec = GetAppointment(Myrec.Id);
            dbrec.Description = Myrec.Description;
            dbrec.StartDate = Myrec.StartDate;
            dbrec.EndDate = Myrec.EndDate;

            _datacontext.SubmitChanges();


        }

        //удаление статьи
        public bool DeleteAppointment(int recId)
        {
            Appointments rec = GetAppointment(recId);
            _datacontext.Appointments.DeleteOnSubmit(rec);
            _datacontext.SubmitChanges();
            return true;
        }



    }



}