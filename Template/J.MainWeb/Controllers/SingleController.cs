﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using J.Entities;

namespace J.MainWeb.Controllers
{
    public class SingleController : Controller
    {
        private DBEntities db = new DBEntities();

        //
        // GET: /Single/

        public ActionResult Index()
        {
            return View(db.Singles.OrderBy(s => s.ID).ToList());
        }

        //
        // GET: /Single/Details/5

        public ActionResult Details(int id)
        {
            J.Entities.Single single = db.Singles.Single(s => s.ID == id);
            return View(single);
        }

        //
        // GET: /Single/Create

        public ActionResult Create(int singleIntNumber, int singleIntEnum, decimal singleMoney, DateTime singleDatetime,
            string singleVarchar, string singleLongVarchar, int singleBit, int singleTinyintBool, int singleTinyintEnum, string singleText)
        {
            try
            {
                J.Entities.Single sg = new J.Entities.Single();
                sg.SingleIntNumber = singleIntNumber;
                sg.SingleIntEnum = (ESingleIntEnum)singleIntEnum;
                sg.SingleMoney = singleMoney;
                sg.SingleDatetime = singleDatetime;
                sg.SingleVarchar = singleVarchar;
                sg.SingleLongVarchar = singleLongVarchar;
                sg.SingleBit = singleBit==1?true:false;
                sg.SingleTinyintBool = singleTinyintBool!=0?true:false;
                sg.SingleTinyintEnum = (ESingleTinyintEnum)singleTinyintEnum;
                sg.SingleText = singleText;
                db.Singles.Add(sg);
                db.SaveChanges();
                return Content("Y");
            }
            catch
            {
                return Content("N");
            }
        }


        //
        // GET: /Single/Edit/5

        public ActionResult Edit(int id, int singleIntNumber, ESingleIntEnum singleIntEnum, decimal singleMoney, DateTime singleDatetime,
            string singleVarchar, string singleLongVarchar, bool singleBit, bool singleTinyintBool, ESingleTinyintEnum singleTinyintEnum, string singleText)
        {
            try
            {
                J.Entities.Single sg = db.Singles.Where(a => a.ID == id).FirstOrDefault();
                sg.SingleIntNumber = singleIntNumber;
                sg.SingleIntEnum = singleIntEnum;
                sg.SingleMoney = singleMoney;
                sg.SingleDatetime = singleDatetime;
                sg.SingleVarchar = singleVarchar;
                sg.SingleLongVarchar = singleLongVarchar;
                sg.SingleBit = singleBit;
                sg.SingleTinyintBool = singleTinyintBool;
                sg.SingleTinyintEnum =singleTinyintEnum;
                sg.SingleText = singleText;
                if (db.SaveChanges() > 0)
                {
                    return Content("Y");
                }
                else {
                    return null;
                }
            }
            catch(Exception e)
            {
                return Content(e.Message.ToString());
            }
        }

        //
        // POST: /Single/Edit/5

        [HttpPost]
        public ActionResult Edit(J.Entities.Single single)
        {
            if (ModelState.IsValid)
            {
                db.Entry(single).State = EntityState.Modified;
                db.Singles.Attach(single);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(single);
        }

        //
        // GET: /Single/Delete/5

        public ActionResult Delete(int id = 0)
        {
            J.Entities.Single single = db.Singles.Find(id);
            if (single == null)
            {
                return HttpNotFound();
            }
            return View(single);
        }

        //
        // POST: /Single/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            J.Entities.Single single = db.Singles.Find(id);
            db.Singles.Remove(single);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}