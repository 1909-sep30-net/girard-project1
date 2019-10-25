using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;

namespace Project1.Controllers
{
    public class StoreController : Controller
    {
        private readonly BlockBusterRepository _repository;

        public StoreController(BlockBusterRepository repository)
        {
            _repository = repository;
        }
        // GET: Store
        public ActionResult Index()
        {
            List<BlockBuster> locations = new List<BlockBuster>();
            _repository.GetStores(locations);

            var blockbusters = locations.Select(s => new StoreModel
            {
                Id = s.LocationId,
                Location = s.Location

            });
            return View(blockbusters);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            var info = _repository.GetStoreOrderHistory(id);
            var sOrders = info.Select(o => new StoreOrderHistoryModel
            {
                OrderID = o.OrderID,
                Name = o.Name,
                Date = o.Date,
                Title = o.Title,
                Price = o.Price,
                InventoryAmount = o.InventoryAmount
            });
            return View(sOrders);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Store/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Store/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}