using BulkyBook.Data;
using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objfromdb = _db.Products.FirstOrDefault(u => u.ID== obj.ID);
            if(objfromdb != null)
            {
                objfromdb.Title = obj.Title;
                objfromdb.ISBN = obj.ISBN;
                objfromdb.Description = obj.Description;
                objfromdb.Price100= obj.Price100;
                objfromdb.Price50= obj.Price50;
                objfromdb.ListPrice= obj.ListPrice;
                objfromdb.CategoryId= obj.CategoryId;
                objfromdb.Author = obj.Author;
                objfromdb.CoverTypeId= obj.CoverTypeId;
                if(obj.ImageUrl!= null)
                {
                    objfromdb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
