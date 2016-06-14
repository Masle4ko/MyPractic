using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.SearchEngine
{
    public class RecordFilter
    {
        Record Model;
        public RecordFilter(Record model)
        {
            this.Model = model;
        }
        public int Id
        {
            get { return this.Model.Id_Record; }
        }
        [DataType(DataType.MultilineText)]
        public string Title
        {
            get { return Model.Title; }

        }
        [DataType(DataType.MultilineText)]
        public string PreviewText
        {
            get { return Model.PreviewText; }

        }
        [DataType(DataType.MultilineText)]
        public string StartDate
        {
            get { return Model.StartDate; }

        }
        public string EndDate
        {
            get { return Model.EndDate; }

        }

        public int CategoryID
        {
            get { return Model.Category_Id; }
        }


        public int Price
        {
            get { return Model.Price; }
        }


        public int min
        {
            get { return Model.Price; }
        }


        public int max
        {
            get { return Model.Price; }
        }
        public string Location
        {
            get { return Model.Location; }

        }
        public string GeoLong
        {
            get { return Model.GeoLong; }

        }
        public string GeoLat
        {
            get { return Model.GeoLat; }

        }
    }
}