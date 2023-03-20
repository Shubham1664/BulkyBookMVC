﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}
