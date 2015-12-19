using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TetstProject.Models;
using TetstProject.ViewModel;

namespace TetstProject.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Index()
        {
            ListProductViewModel listProductViewModel = new ListProductViewModel();
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            List<Product> products = storeBL.GetProduct();
            List<ProductViewModel> productsViewModel = new List<ProductViewModel>();
            foreach (Product product in products)
            {
                ProductViewModel productVM = new ProductViewModel();
                productVM.Id = product.ProductId.ToString();
                productVM.Code = product.Code.ToString();
                productVM.Name = product.Name;
                productVM.Price = product.Price.ToString("C");
                productVM.StoreName = product.Store.Name;
                productsViewModel.Add(productVM);
            }
            listProductViewModel.Products = productsViewModel;
            return View("Index", listProductViewModel);
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productVM)
        {
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            if (ModelState.IsValid)
            {

                storeBL.CreateProduct(productVM);
                return RedirectToAction("Index", "Product");
            }
            ViewBag.StoresName = storeBL.GetStore().Select(s => s.Name);
            return View("CreateProduct", new ProductViewModel()); ;
        }

        public ActionResult DeleteProduct(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            ProductViewModel productVM = storeBL.FindProductById(id);
            if (productVM == null)
            {
                return HttpNotFound();
            }

            return View(productVM);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult DeleteConfirmed(string id)
        {
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            string str = storeBL.DeleteProduct(id);
            if (str!=null)
            {
                ViewBag.Error = str;
                return View("Error");
            }
            return RedirectToAction("Index", "Product");
           
        }

        public ActionResult AddProduct()
        {
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            ViewBag.StoresName = storeBL.GetStore().Select(s => s.Name);

            return View("CreateProduct", new ProductViewModel());
        }
       
        public ActionResult EditProduct(string id)
        {
            
            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            ViewBag.StoresName = storeBL.GetStore().Select(s => s.Name);
            ProductViewModel productVM = storeBL.FindProductById(id);
            if (productVM!=null)
            {
                return View("EditProduct", productVM);
            } else return HttpNotFound();

        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel productVM)
        {

            StoreBuisnessLayer storeBL = new StoreBuisnessLayer();
            if (ModelState.IsValid)
            {
                storeBL.EditProduct(productVM);
                return RedirectToAction("Index", "Product");
            }
            ViewBag.StoresName = storeBL.GetStore().Select(s => s.Name);
            return View("EditProduct", productVM);

        }
    } 
}