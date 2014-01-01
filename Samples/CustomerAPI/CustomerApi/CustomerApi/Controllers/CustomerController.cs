using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerApi.Models.WebApi.Models;
using CustomerApi.Models;
using System.Configuration;
using NotificationsExtensions;
using NotificationsExtensions.ToastContent;
using System.Diagnostics;

namespace CustomerApi.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerContext db = new CustomerContext();

        private void SendNotification(Customer customer)
        {
            var clientId = ConfigurationManager.AppSettings["ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            var tokenProvider = new WnsAccessTokenProvider(clientId, clientSecret);
            var notification = ToastContentFactory.CreateToastText02();

            notification.TextHeading.Text = "New customer added!";
            notification.TextBodyWrap.Text = customer.Name;

            var channels = db.Channels;

            Trace.TraceInformation("Channel Notifications: {0} pending", channels.Count());
            foreach (var channel in channels)
            {
                var result = notification.Send(new Uri(channel.Uri), tokenProvider);
                Trace.TraceInformation(" Notifying: {0}", channel.Id);
            }

        }

        // GET: /Customer/
        public async Task<ActionResult> Index()
        {
            Trace.TraceInformation("Customer Index Executing");
            return View(await db.Customers.ToListAsync());
        }

        // GET: /Customer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="CustomerId,Name,Phone,Address,Company,Title,Email,Image")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                this.SendNotification(customer);
                return RedirectToAction("Index");
            }


            return View(customer);
        }

        // GET: /Customer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: /Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CustomerId,Name,Phone,Address,Company,Title,Email,Image")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: /Customer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
