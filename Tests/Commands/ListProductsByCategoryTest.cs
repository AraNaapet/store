﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Marina.Store.Web.Commands;
using Marina.Store.Web.DataAccess;
using Marina.Store.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marina.Store.Tests.Commands
{
    // TODO: решить, хотим ли мы показывать недоступные в данный момент товары
    [TestClass]
    public class ListProductsByCategoryTest
    {
        [ClassInitialize]
        public static void Init(TestContext ctx)
        {
            using(var db = new StoreDbContext())
            {
                db.Categories.Add( new Category {Id = 1, Name = "Флешки"} );
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Продукты возвращаются
        /// </summary>
        [TestMethod]
        public void Must_list_products()
        {
            GenerateProducts(2);

            using(var db = new StoreDbContext())
            {
                var cmd = new ListProductsByCategoryCommand(db);
                var result = cmd.Execute(1);

                Assert.IsNotNull(result);
                Assert.IsFalse(result.HasErrors);
                Assert.IsTrue(result.Model.Any());
                Assert.AreEqual(2, result.Model.Length);

                Assert.IsTrue(result.Model.First().Params.Any());
            }
        }

        /// <summary>
        /// Возвращаются продукты только для указанной категории
        /// </summary>
        [TestMethod]
        public void Must_list_products_only_by_specified_category()
        {
            Assert.Inconclusive();
        }

        /// <summary>
        /// Для большого кол-ва продуктов в категории
        /// Список продуктов возвращается постранично
        /// </summary>
        [TestMethod]
        public void When_there_are_lots_of_products_Must_paginate()
        {
            Assert.Inconclusive();
        }

        private static void GenerateProducts(int count)
        {
            using (var db = new StoreDbContext())
            {
                db.Products.SqlQuery("delete from products");
                var category = db.Categories.First();

                for (var i = 0; i < count; i++)
                {
                    var product = new Product();
                    product.Params = new Collection<Param>();
                    product.Category = category;
                    product.Params.Add(new Param {Name = "Model", Value = "1"});
                    product.Params.Add(new Param {Name = "Capacity", Value = "32gb"});
                    db.Products.Add(product);
                }
                db.SaveChanges();
            }
        }
    }
}