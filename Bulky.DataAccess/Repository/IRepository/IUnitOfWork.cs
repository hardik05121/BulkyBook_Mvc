using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IProductImageRepository ProductImage { get; }
        ICountryRepository Country { get; }
        IStateRepository State { get; }
        ICityRepository City { get; }
        IContactUsRepository ContactUs { get; }
        IHtagRepository Htag { get; }
        IItagRepository Itag { get; }
        IHtmlTagRepository HtmlTag { get; }



        void Save();
    }
}
