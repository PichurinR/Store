using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TetstProject.Models;
using TetstProject.ViewModel;

namespace TetstProject.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult ShowStores()
        {
            ListStoreViewModel listStoreViewModel = new ListStoreViewModel();
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            List<Store> stores = storeBL.GetStore();
            List<StoreViewModel> storesViewModel = new List<StoreViewModel>();
            foreach (Store store in stores)
            {
                StoreViewModel storeVM = new StoreViewModel()
                {
                    Id = store.StoreId.ToString(),
                    Name = store.Name
                };
                storesViewModel.Add(storeVM);
            }
            listStoreViewModel.Stores = storesViewModel;
            return View("ShowStores", listStoreViewModel);
        }

        [HttpPost]
        public ActionResult CreateStore(StoreViewModel storeVM)
        {
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            if (ModelState.IsValid)
            {
                storeBL.CreateStore(storeVM);
                return RedirectToAction("ShowStores", "Store");
            }
            return View("CreateStore", new ProductViewModel ());
        }

        public ActionResult DeleteStore(string id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            StoreViewModel storeVM = storeBL.FindStoreById(id);
            if (storeVM == null)
            {
                    return  HttpNotFound();
            }

            return View(storeVM);
        }

        [HttpPost, ActionName("DeleteStore")]
        public ActionResult DeleteConfirmed(string id)
        {
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            string str = storeBL.DeleteStore(id);
            if (str!=null)
            {
                ViewBag.Error = str;
                return View("Error");
            }
            return RedirectToAction("ShowStores","Store");
        }

        public ActionResult AddStore()
        {
           
            return View("CreateStore", new StoreViewModel());
        }

        public ActionResult EditStore(string id)
        {

            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            StoreViewModel storeVM = storeBL.FindStoreById(id);
            if (storeVM != null)
            {
                return View("EditStore", storeVM);
            }
            else return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditStore(StoreViewModel storeVM)
        {

            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            if (ModelState.IsValid)
            {
                storeBL.EditStore(storeVM);
                return RedirectToAction("ShowStores","Store");
            }
            return View("EditProduct", storeVM);

        }

    }
}