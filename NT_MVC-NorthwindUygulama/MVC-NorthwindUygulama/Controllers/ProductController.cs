using MVC_NorthwindUygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_NorthwindUygulama.Controllers
{
    public class ProductController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Product
        public ActionResult Index()
        {
            //Context içindeki ürünleri listeye çek ve bu ürünler listesini view a gönder. Bunun için en sık kullanılan yöntem MODEL
            //yöntemidir.
            List<Product> products = ctx.Products.ToList();
            List<Category> categories = ctx.Categories.ToList();
            //Tek bir action içerisinden iki farklı listeyi aynı view içerisinde listelemek istersek; MODEL yöntemine ek olarak
            //viewBag yöntemi kullanabiliriz.
            //viewBag yöntemiyle istediğimiz isimde dinamik  bir tip tanımlayabilir ve içini doldurabiliriz.
            ViewBag.categoryList = categories;
            //Aşağıdaki View(); metodunun arasına bir değişken vermek o değişkeni MODEL yöntemiyle view a göndermek demektir.
            //MODEL yöntemiyle view a sadece bir tane liste veri gönderilebilir.
            return View(products);//view içinde parametre varsa MODEL yöntemi
            //Context içindeki ürünler(products) view a gönderilecek bunun için gönderilen action dan gönderilen verinin
            //view tarafından yakalanması gerekir. Bu yüzden bu actionın gösterdiği view(index.cshtml) ın içinde; index()
            //action ı bir veri gönderiyor ve bu verinin tipi List<Product> tır diye belirtmek gerekir.
        }
        public ActionResult AddProduct()
        {
            List<Category> cat = ctx.Categories.ToList();
            List<Supplier> sup = ctx.Suppliers.ToList();

            ViewBag.categoryList = cat;
            ViewBag.supplierList = sup;

            return View();
        }
        // 1. YOL
        //Get ve post actionın ismini aynı veriyoruz
        //[HttpPost] 
        //public ActionResult AddProduct(string productName, decimal unitPrice, short unitInStock, int catID, int supID)
        //{
        //    Product prd = new Product();
        //    prd.ProductName = productName;
        //    prd.UnitPrice = unitPrice;
        //    prd.UnitsInStock = unitInStock;
        //    prd.CategoryID = catID;
        //    prd.SupplierID = supID;

        //    ctx.Products.Add(prd);
        //    ctx.SaveChanges();

        //    //  return View("Index"); view index olarak çağırırsak sadece o actionı çağırır fakat action içerisindeki işlemleri yapmaz.
        //    //RedirectToAction ile hem action çağırılır hem de içinin işlemleri yapılır. Başka controllerdeki çağırmak için index yanına , (virgül) koyulup controller çağırılır
        //    return RedirectToAction("Index");
        //}

            //2. YOL
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            Product prd = new Product();
            prd.ProductName = p.ProductName;
            prd.UnitPrice = p.UnitPrice;
            prd.UnitsInStock = p.UnitsInStock;
            prd.CategoryID = p.CategoryID;
            prd.SupplierID = p.SupplierID;

            ctx.Products.Add(prd);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
