﻿
using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private ApplicationDbContext _db;

        public UnitOfWorkRepository(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);   
            Company = new CompanyRepository(_db);
            RegisteredUser= new RegisteredUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            
        }
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepostiory Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IRegisteredUserRepository RegisteredUser { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}