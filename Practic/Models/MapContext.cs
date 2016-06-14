using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyBlog.Models;
using DHTMLX.Common;
using System.Web.Mvc;
using System.Data;
using MyBlog.GoogleGeocoding;

namespace MyBlog.Models
{
    public class MapContext // : DbContext
    {
    //    private MyDBObjectsLINQDataContext _datacontext;
    //    // private CalendarContext dbC = new CalendarContext();

    //    public MapContext() : base("DbblogConnectionString")
    //    {
    //        Database.SetInitializer(new CreateDatabaseIfNotExists<MapContext>());
    //    }

    //    public IEnumerable<Location> GetAllMapPoint()
    //    {

    //        return _datacontext.Location.OrderBy(r => r.StartDate);


    //    }

    //    public MapContext(MyDBObjectsLINQDataContext _datacontext)
    //    {
    //        this._datacontext = _datacontext;
    //    }

    //    public System.Data.Entity.DbSet<AppointmentCalendar.Models.Appointment> Appointments { get; set; }


    //    public Location CreateMapPoint(string title, string PlaceName, int Price, string _startDate, string _endDate, string GeoLong, string GeoLat)
    //    {

    //        Location eve = new Location { Description = title, PlaceName = PlaceName, Price = Price, StartDate = _startDate, EndDate = _endDate, GeoLong = GeoLong, GeoLat = GeoLat };
    //        _datacontext.Location.InsertOnSubmit(eve);
    //        _datacontext.SubmitChanges();
    //        return eve;


    //    }


    //    public Location GetMapPoint(int id)
    //    {
    //        return _datacontext.Location.SingleOrDefault(t => t.Id == id);

    //    }

    //    //обновление статьи
    //    public void UpdateMapPoint(Location Myrec)
    //    {
    //        Location dbrec = GetMapPoint(Myrec.Id);
    //        dbrec.Description = Myrec.Description;
    //        dbrec.StartDate = Myrec.StartDate;
    //        dbrec.EndDate = Myrec.EndDate;

    //        _datacontext.SubmitChanges();


    //    }

    //    //удаление статьи
    //    public bool DeleteMapPoint(int recId)
    //    {
    //        Location rec = GetMapPoint(recId);
    //        _datacontext.Location.DeleteOnSubmit(rec);
    //        _datacontext.SubmitChanges();
    //        return true;
    //    }


    //    static public string[] Test(string address)

    //    {
    //        string[] coor = new string[2];
    //        var g = new vpGeo();
    //        g.Key = "AIzaSyDF5WCqlDZb3WOshmSDrJcEwruzVsAkPaU";
    //        var r = g.GoogleGeoCodeInfo(new Address { Address1 = address, City = "Харьков" });
    //        coor[0] = r.Result[0].Geometry.Location.Lat;
    //        coor[1] = r.Result[0].Geometry.Location.Long;
    //        return coor;

    //    }
    }
}
