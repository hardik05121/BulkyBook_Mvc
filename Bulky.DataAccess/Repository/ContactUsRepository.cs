using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        private ApplicationDbContext _db;
        public ContactUsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void Update(ContactUs obj)
        {
            _db.ContactUss.Update(obj);
        }
    }
}
