﻿using BulkyBook.Data;
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
        }
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}