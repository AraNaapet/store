﻿using System.Linq;
using Marina.Store.Web.DataAccess;
using Marina.Store.Web.Models;

namespace Marina.Store.Web.Commands
{
    public class ListProductsByCategoryCommand : Command
    {
        private readonly StoreDbContext _db;

        public ListProductsByCategoryCommand(StoreDbContext db)
        {
            _db = db;
        }

        public Product[] ListProducts(int categoryId, int skip = 0, int top = 25)
        {
            return _db.Products.Where(p=>p.Category.Id == categoryId).OrderBy(p=>p.Id).Skip(skip).Take(top).ToArray();
        }
    }
}