using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_4.DAL;
using Task_4.DAL.Models;
using WebApplication.Models;
using WebApplication.Models.DTO;

namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {    
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Service service = new Service();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = sortOrder == "Id" ? "Id_desc" : "Id";
            ViewBag.OrderDateSortParm = sortOrder == "OrderDate" ? "OrderDate_desc" : "";
            ViewBag.CustomerNameSortParm = String.IsNullOrEmpty(sortOrder) ? "c_name_desc" : "c_name";
            ViewBag.ShopAssistantNameSortParm = String.IsNullOrEmpty(sortOrder) ? "sa_name_desc" : "sa_name";
            ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ? "p_name_desc" : "p_name";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }

            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var orders = from order in service.GetOrders()
                            select order;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(x => x.Id == Convert.ToInt32(searchString));
            }

            switch (sortOrder)
            {
                case "OrderDate_desc":
                    orders = orders.OrderByDescending(s => s.OrderDate);
                    break;
                case "Id":
                    orders = orders.OrderBy(s => s.Id);
                    break;
                case "Id_desc":
                    orders = orders.OrderByDescending(s => s.Id);
                    break;
                case "c_name":
                    orders = orders.OrderBy(s => s.Customer);
                    break;
                case "c_name_desc":
                    orders = orders.OrderByDescending(s => s.Customer);
                    break;
                case "sa_name":
                    orders = orders.OrderBy(s => s.ShopAssistant);
                    break;
                case "sa_name_desc":
                    orders = orders.OrderByDescending(s => s.ShopAssistant);
                    break;
                case "p_name":
                    orders = orders.OrderBy(s => s.Product);
                    break;
                case "p_name_desc":
                    orders = orders.OrderByDescending(s => s.Product);
                    break;
                case "Amount":
                    orders = orders.OrderBy(s => s.Amount);
                    break;
                case "amount_desc":
                    orders = orders.OrderByDescending(s => s.Amount);
                    break;
                case "Price":
                    orders = orders.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    orders = orders.OrderByDescending(s => s.Price);
                    break;
                default:  // OrderTime ascending 
                    orders = orders.OrderBy(s => s.OrderDate);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Order/Details/5

        public ViewResult Details(int id)
        {
            Service service = new Service();
            var order = service.GetOrder(id);
            return View(order);
        }

        //
        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Order/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDate, Cusomer, ShopAssistant, Amount, Price")]OrderDTO orderDTO)
        {
            IUnitOfWork database = new EFUnitOfWork();

            var product = database.Products.GetIdByName(orderDTO.Product);
            var customer = database.Customers.GetIdByName(orderDTO.Customer);
            var shopAssistant = database.ShopAssistants.GetIdByName(orderDTO.ShopAssistant);

            var order = new OrderDAL
            {
                OrderDate = DateTime.Now,
                Id = orderDTO.Id,
                Amount = orderDTO.Amount,
                Price = orderDTO.Price,
                CustomerId = customer.Value,
                ProductId = product.Value,
                ShopAssistantId = shopAssistant.Value
            };

            try
            {
                if (ModelState.IsValid)
                {
                    database.Orders.Insert(order);
                    database.Orders.Save();
                    return RedirectToAction("Index");
                }
            }

            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return View(orderDTO);
        }

        //
        // GET: /Order/Edit/5

        public ActionResult Edit(int id)
        {
            Service service = new Service();
            OrderDTO order = service.GetOrder(id);
            return View(order);
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDate, Cusomer, ShopAssistant, Amount, Price")]OrderDTO orderDTO)
        {
            IUnitOfWork database = new EFUnitOfWork();

            var product = database.Products.GetIdByName(orderDTO.Product);
            var customer = database.Customers.GetIdByName(orderDTO.Customer);
            var shopAssistant = database.ShopAssistants.GetIdByName(orderDTO.ShopAssistant);

            var order = new OrderDAL
            {
                OrderDate = orderDTO.OrderDate,
                Id = orderDTO.Id,
                Amount = orderDTO.Amount,
                Price = orderDTO.Price,
                CustomerId =  customer.Value,
                ProductId = product.Value,
                ShopAssistantId = shopAssistant.Value
            };

            try
            {
                if (ModelState.IsValid)
                {
                    database.Orders.Update(order);
                    database.Orders.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(orderDTO);
        }

        //
        // GET: /Order/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            Service service = new Service();

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            OrderDTO order = service.GetOrder(id);
            return View(order);
        }

        //
        // POST: /Order/Delete/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Service service = new Service();
            IUnitOfWork database = new EFUnitOfWork();

            OrderDTO orderDTO = service.GetOrder(id);

            var product = database.Products.GetIdByName(orderDTO.Product);
            var customer = database.Customers.GetIdByName(orderDTO.Customer);
            var shopAssistant = database.ShopAssistants.GetIdByName(orderDTO.ShopAssistant);

            var order = new OrderDAL
            {
                OrderDate = orderDTO.OrderDate,
                Id = orderDTO.Id,
                Amount = orderDTO.Amount,
                Price = orderDTO.Price,
                CustomerId = customer.Value,
                ProductId = product.Value,
                ShopAssistantId = shopAssistant.Value
            };
            
            try
            {
                database.Orders.Delete(id);
                database.Orders.Save();
            }

            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            IUnitOfWork database = new EFUnitOfWork();

            database.Dispose();
            base.Dispose(disposing);
        }
    }
}