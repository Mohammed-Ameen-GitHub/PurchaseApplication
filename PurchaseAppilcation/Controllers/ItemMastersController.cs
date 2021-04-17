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
    [System.Web.Http.RoutePrefix("Api/ItemMasters")]
    [Authorize]
    public class ItemMastersController : ApiController
    {
        private PurchaseEntities db = new PurchaseEntities();

        [ResponseType(typeof(ItemMaster))]
        [System.Web.Http.Route("PostItemMaster")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostItemMaster(ItemMaster itemMaster)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (db)
            {
                if (itemMaster.Id == 0)
                {
                    db.ItemMasters.Add(itemMaster);
                    db.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }

      
        [ResponseType(typeof(ItemMaster))]
        [System.Web.Http.Route("DeleteItemMaster/{id}")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteItemMaster(int id)
        {

            ItemMaster itemMaster = db.ItemMasters.Find(id);

            if (itemMaster == null)
            {
                return NotFound();
            }

            db.ItemMasters.Remove(itemMaster);
            db.SaveChanges();

            return Ok(itemMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemMasterExists(int id)
        {
            return db.ItemMasters.Count(e => e.Id == id) > 0;
        }
    }
}