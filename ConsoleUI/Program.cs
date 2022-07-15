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
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var item in productManager.GetAll())
            {
                Console.WriteLine(item.ProductName);
            }

            Console.WriteLine("Kategori ID'e Göre Getir****************");
            foreach (Product product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("Fiyat Aralığına Göre Getir****************");
            foreach (Product product in productManager.GetByUnitPrice(1,10))
            {
                Console.WriteLine(product.ProductName);
            }


            Console.ReadLine();
        }
    }
}
