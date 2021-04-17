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
using System.Data.Entity.Migrations;
using PurchaseApplication.Models;
using System.Security.Claims;
namespace PurchaseApplication.Controllers
{
    [System.Web.Http.RoutePrefix("Api/CustomerMasters")]
    [Authorize]
    public class CustomerMastersController : ApiController
    {
        private PurchaseEntities db = new PurchaseEntities();

        [Route("GetCustomerMasters")]
        [HttpGet]
        public IHttpActionResult GetCustomerMasters()
        {
            IList<CustomerMaster> customer = null;
            using (db)
            {

                customer = db.CustomerMasters.ToList();
            }
            if (customer.Count == 0)
                return NotFound();
            return Ok(customer);
        }
     
        [ResponseType(typeof(CustomerMaster))]
        [System.Web.Http.Route("PostCustomerMaster")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostCustomerMaster(CustomerMaster customerMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (db)
            {
                if (customerMaster.Id == 0)
                {
                    db.CustomerMasters.Add(customerMaster);
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

        private bool CustomerMasterExists(int id)
        {
            return db.CustomerMasters.Count(e => e.Id == id) > 0;
        }
    }
}