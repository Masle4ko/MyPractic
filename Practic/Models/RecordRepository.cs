using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using MyBlog.GoogleGeocoding;

namespace MyBlog.Models
{
    public class RecordRepository
    {
        private MyDBObjectsLINQDataContext _datacontext;


        public RecordRepository(MyDBObjectsLINQDataContext datacontext)
        {

            _datacontext = datacontext;

        }


        //возвращает все записи
        public IEnumerable<Record> GetAllRecords()
        {

            return _datacontext.Record.OrderBy(r => r.DateEdit);

        }

        public static string CutLongText(string value)
        {


            return HttpUtility.HtmlDecode(value.Substring(0, 200) + "...");


        }

        public Record GetMapPoint(int id)
        {
            return _datacontext.Record.SingleOrDefault(t => t.Id_Record == id);

        }
        public IEnumerable<Record> GetRecordsFromCategory(int id)
        {

            return _datacontext.Record.Where(r => r.Category_Id == id);

        }

        //добавление новой статьи
        public Record CreateRecord(string title, string text, string textprev, int Id_Category, string _startDate, string _endDate, int price, string location, string GeoLong, string GeoLat)
        {

            Record rec = new Record { DateCreate = DateTime.Now, Title = title, Text = text, PreviewText = textprev, Category_Id = Id_Category, StartDate=_startDate, EndDate=_endDate, Price=price, Location=location, GeoLat=GeoLat, GeoLong=GeoLong};
            _datacontext.Record.InsertOnSubmit(rec);
            _datacontext.SubmitChanges();
            return rec;
        }

        //выбираем одну статью по id
        public Record GetRecord(int id)
        {
            return _datacontext.Record.SingleOrDefault(t => t.Id_Record == id);

        }

        //обновление статьи
        public void UpdateRecord(Record Myrec)
        {
            Record dbrec = GetRecord(Myrec.Id_Record);
            dbrec.DateEdit = DateTime.Now;
            dbrec.Title = Myrec.Title;
            dbrec.Text = Myrec.Text;
            dbrec.PreviewText = Myrec.PreviewText;
            dbrec.Price = Myrec.Price;
            dbrec.Location = dbrec.Location;
            dbrec.StartDate = dbrec.StartDate;
            dbrec.EndDate = dbrec.EndDate;

            _datacontext.SubmitChanges();


        }

        //удаление статьи
        public bool DeleteRecord(int recId)
        {
            Record rec = GetRecord(recId);
            _datacontext.Record.DeleteOnSubmit(rec);
            _datacontext.SubmitChanges();
            return true;
        }





        static public string[] Test(string address)

        {
            string[] coor = new string[2];
            var g = new vpGeo();
            g.Key = "AIzaSyDF5WCqlDZb3WOshmSDrJcEwruzVsAkPaU";
            var r = g.GoogleGeoCodeInfo(new Address { Address1 = address, City = "Харьков" });
            coor[0] = r.Result[0].Geometry.Location.Lat;
            coor[1] = r.Result[0].Geometry.Location.Long;
            return coor;

        }








    }
}