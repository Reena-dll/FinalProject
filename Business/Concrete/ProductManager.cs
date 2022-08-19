using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        // Secured Operation = Aspect İsmi
        // Claim = Kullanıcının product.add veya admin yetkilerinden birine sahip olması gerekir.
        // product.add ve admin = claim
        // Client = Bir web uygulaması, bir mobil uygulaması her hangi birisi olabilir,
        // Web üzerinden konuşacak olursak bizim her bir tarayacımız client'dır.
        // Jason Web Token (JWT) = Api tarafınnda yollanan JWT'i Client kendi hafızasında tutar. 
        // JWT almış bir client bundan sonra ki isteklerini yaparken istek ile birlikte JWT kodunu da gönderecek.
        // Authorization da bir cross cutting concerns'dır.
        // Kişinin yetkisi var mı, yetkiyi kontrol ederiz.
        // Encryption, Hashing = Bunlar bir datayı karşı taraf okuyamaması için yapılan çalışmalardır.
        // Parolaları biz veri tabanında Hashleriz. 
        // Şifresi 123456 olan bir kullanıcının şifresini biz veri tabanında açık tutmayız. 
        // O veri kaynağına eğer birisi ulaşır ise şifrelere erişmiş olur.
        // Genellikle parolaları hashlediğimiz zaman bir şifreleme algoritması ile (MD5,SHA1 gibi) geri dönüşü olmayacak şekilde hashlenirler.
        // Biz bu parolayı bellekte veya veritabanında 123456 değil, Hashleme algoritmamıza göre onun oluşturduğu şifreyi göndeririz.
        // Kişinin girdiği şifreye ekstra bizim de bir şeyler kattığımız olaya Salting ( Tuzlama ) denir.
        // Kullanıcının girdiği parolayı biz biraz daha güçlendiriyoruz.
        // Kullanıcının girdiği şifre basit bir şifre ise saldırganlar rainbow table adı verilen tablo oluşturuyorlar,
        // Olabilecek olan bütün şifreleri tabloya yerleştirip hash'lerini çıkartıyor.
        // Daha sonrasında bizim kaynağımızdan ele geçirdiği hash'ler ile karşılaştırıp şifreyi çözmeye çalışıyor.
        // Parolamızın gücü ne kadar az ise o kadar kolay bulabilirler.
        // Bu yüzden şifrelerimizde Özel karakter, büyük küçük harf, sayı kullanmalıyız. 
        // Ama bütün kullanıcılar bu kurallara uymuyor, bizde bu yüzden salting uyguluyoruz.
        //Encryption ise geri dönüşü olan bir veridir.
        
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        // İş kuralları AOP
        public IResult Add(Product product)
        {
            // business codes
            // validation

            // İş motoru. Yazdığımız kuralları motora koyuyoruz. 
            IResult result =  BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded());

            if (result!=null)
            {
                return result; 
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

            

        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            // İş kodları
            // Yetkisi var mı?
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();


        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // select count(*) from product where categoryId = categoryId
            var productCount = _productDal.GetAll(p => p.ProductId == categoryId).Count();
            if (productCount >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            var productControl = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (productControl == true)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);

            }
            return new SuccessResult();
        }


        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
