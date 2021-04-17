using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Security.Claims;
using PurchaseApplication.Models;

namespace PurchaseApplication.Controllers
{
    [System.Web.Http.RoutePrefix("Api/SalesOrderHeaders")]
    [Authorize]
    public class SalesOrderHeadersController : ApiController
    {
        private PurchaseEntities db = new PurchaseEntities();
        [Route("GetSalesOrderHeaders")]
        [HttpGet]
        public IHttpActionResult GetSalesOrderHeaders()
        {
            IList<SalesOrderHeader> order = null;
            using (db)
            {
                var identity = (ClaimsIdentity)User.Identity;
                order = db.SalesOrderHeaders.ToList();
            }
            if (order.Count == 0)
                return NotFound();
            return Ok(order);
        }

   
        [ResponseType(typeof(SalesOrderHeader))]
        [System.Web.Http.Route("PostSalesOrderHeader")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (db)
            {
                if (salesOrderHeader.Id == 0)
                {
                    db.SalesOrderHeaders.Add(salesOrderHeader);
                    db.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }

     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesOrderHeaderExists(int id)
        {
            return db.SalesOrderHeaders.Count(e => e.Id == id) > 0;
        }
    }
}