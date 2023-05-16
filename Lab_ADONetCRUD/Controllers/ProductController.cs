using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Lab_ADONetCRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;

        ProductRepository _productRepository;
        CategoryRepository _categoryRepository;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _productRepository = new ProductRepository(_configuration.GetConnectionString("DbConnection"));
            _categoryRepository = new CategoryRepository(_configuration.GetConnectionString("DbConnection"));


        }
        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetProducts();
            return View(products);
        }
        public IActionResult Details(int id)
        {
            Product product = _productRepository.GetProduct(id);
            return View(product);
        }
        public ViewResult Create()
        {
            List<Category> categories = _categoryRepository.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {

            if (ModelState.IsValid)
            {
                bool result = _productRepository.AddProduct(product);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            List<Category> categories = _categoryRepository.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        public ViewResult Edit(int id)
        {
            List<Category> categories = _categoryRepository.GetCategories();
            ViewBag.Categories = categories;
            var product=_productRepository.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
               bool result =  _productRepository.UpdateProduct(product);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            List<Category> categories = _categoryRepository.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Delete(int id) 
        { 
             _productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
