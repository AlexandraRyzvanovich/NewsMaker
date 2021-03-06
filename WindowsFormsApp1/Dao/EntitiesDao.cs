﻿using NewsMaker.ConnectionFactory;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Dao
{
    class EntitiesDao
    {
        private readonly DbConnectionFactory dbConnector = new DbConnectionFactory();
        public void Save(Entity entity)
        {
            var dbFactory = dbConnector.createConnectionFactory();

            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<Entity>();
                db.Insert(entity);

            }
        }
        public List<Entity> SelectAll()
        {
            var dbFactory = dbConnector.createConnectionFactory();

            using (var db = dbFactory.Open())
            {
                var entities = db.Select<Entity>();
                return entities;
            }
        }

        public Entity SelectById(int ids)
        {
            var dbFactory = dbConnector.createConnectionFactory();
            using (var db = dbFactory.Open())
            {
                var entityById = db.Select<Entity>().Find(m => m.Id == ids);
                return entityById;
            }
        }

        public List<Entity> SelectByEntityType(EntitiesType type)
        {
            var dbFactory = dbConnector.createConnectionFactory();
            using (var db = dbFactory.Open())
            {
                var entityById = db.Select<Entity>().Where(m => m.Type == type).ToList();
                return entityById;
            }
        }
        public void SaveAll(List<Entity> entities) 
        {
            var dbFactory = dbConnector.createConnectionFactory();

            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<Entity>();
                db.InsertAll(entities);
            }
        }
        public void UpdateAll(List<Entity> entities)
        {
            var dbFactory = dbConnector.createConnectionFactory();

            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<Entity>();
                db.UpdateAll(entities);
            }
        }
    }
}

