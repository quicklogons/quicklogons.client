using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.NancyFx.Website.Models
{
    public class TitleModel
    {
        public string Layout { get; set; }
        public string Title { get; set; }

        public TitleModel()
        {
            Layout = "Master.cshtml";
        }
    }
}