﻿using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
    }
}
