using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IProductImageRepository ProductImage { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IStateRepository State { get; private set; }
        public ICityRepository City { get; private set; }
        public IContactUsRepository ContactUs { get; private set; }
        public IHtagRepository Htag { get; private set; }
        public IItagRepository Itag { get; private set; }
        public IHtmlTagRepository HtmlTag { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            ProductImage = new ProductImageRepository(_db);
            Country = new CountryRepository(_db);
            State = new StateRepository(_db);
            City = new CityRepository(_db); 
            ContactUs = new ContactUsRepository(_db);
            Htag = new HtagRepository(_db);
            Itag = new ItagRepository(_db);
            HtmlTag = new HtmlTagRepository(_db);



        }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
    
}
      
      
      
