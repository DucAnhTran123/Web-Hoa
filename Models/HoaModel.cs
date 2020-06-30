using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Web_Hoa.Models
{
    public class HoaModel
    {
        WEB_HOA1Entities db = null;
        public HoaModel()
        {
            db = new WEB_HOA1Entities();

        }
        public Hoa LayHoaByID(int id)
        {
            return db.Hoas.Find(id);
        }
    }
}