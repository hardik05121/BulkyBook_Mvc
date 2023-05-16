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
    public class HtmlTagRepository : Repository<HtmlTag>, IHtmlTagRepository
    {
        private ApplicationDbContext _db;
        public HtmlTagRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void Update(HtmlTag obj)
        {
            var objFromDb = _db.HtmlTags.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.AboutMe = obj.AboutMe;
                objFromDb.IsPublic = obj.IsPublic;
                objFromDb.Adult = obj.Adult;
                objFromDb.IsActive = obj.IsActive;
                objFromDb.CreateDate = obj.CreateDate;

                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
