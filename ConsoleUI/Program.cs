using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            //CategoryTest();
            //ProductDetails();
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
            var result = productManager.GetProductDetails();

            if (result.Success)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName+" "+product.CategoryName );
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Console.ReadLine();
        }

        private static void ProductDetails()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));
            foreach (var item in productManager.GetProductDetails().Data)
            {
                Console.WriteLine("{0} => {1} => {2} => {3}"
                    , item.ProductId, item.ProductName,
                    item.CategoryName, item.UnitsInStock);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (Category category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));

            foreach (var item in productManager.GetAll().Data)
            {
                Console.WriteLine(item.ProductName);
            }

            Console.WriteLine("Kategori ID'e Göre Getir****************");
            foreach (Product product in productManager.GetAllByCategoryId(2).Data)
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("Fiyat Aralığına Göre Getir****************");
            foreach (Product product in productManager.GetByUnitPrice(1, 10).Data)
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}
