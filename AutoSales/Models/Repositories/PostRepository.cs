using Microsoft.EntityFrameworkCore;
using AutoSales.Data;
using System.Diagnostics.Metrics;
using AutoSales.Models;
using AutoSales.Models.DBObjects;

namespace ProiectulFinal.Models.Repository
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public PostRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private PostModel MapDBObjectToModel(Post dbobject)
        {
            var model = new PostModel();
            if (dbobject != null)
            {
                model.IdPost = dbobject.IdPost;
                model.Make = dbobject.Make;
                model.Model = dbobject.Model;
                model.VehicleType = dbobject.VehicleType;
                model.Edition = dbobject.Edition;
                model.Price = dbobject.Price;
                model.FirstRegistration = dbobject.FirstRegistration;
                model.Mileage = dbobject.Mileage;
                model.Power = dbobject.Power;
                model.FuelType = dbobject.FuelType;
                model.EngineCapacity = dbobject.EngineCapacity;
                model.NumberOfDoors = dbobject.NumberOfDoors;
                model.Colour = dbobject.Colour;
                model.Description = dbobject.Description;
                model.IdUser = dbobject.IdUser;
                model.Location = dbobject.Location;
                model.Image = dbobject.Image;
            }
            return model;
        }

        private Post MapModelToDBObject(PostModel model)
        {
            var dbobject = new Post();
            if (dbobject != null)
            {
                dbobject.IdPost = model.IdPost;
                dbobject.Make = model.Make;
                dbobject.Model = model.Model;
                dbobject.VehicleType = model.VehicleType;
                dbobject.Edition = model.Edition;
                dbobject.Price = model.Price;
                dbobject.FirstRegistration = model.FirstRegistration;
                dbobject.Mileage = model.Mileage;
                dbobject.Power = model.Power;
                dbobject.FuelType = model.FuelType;
                dbobject.EngineCapacity = model.EngineCapacity;
                dbobject.NumberOfDoors = model.NumberOfDoors;
                dbobject.Colour = model.Colour;
                dbobject.Description = model.Description;
                dbobject.IdUser = model.IdUser;
                dbobject.Location = model.Location;
                dbobject.Image = model.Image;
            }
            return dbobject;
        }

        public List<PostModel> GetAllPosts()
        {
            var list = new List<PostModel>();
            foreach (var dbobject in _DBContext.Posts)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }

        public PostModel GetPostByID(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Posts.FirstOrDefault(x => x.IdPost == id));
        }

        public void InsertPost(PostModel model)
        {
            model.IdPost = Guid.NewGuid();
            _DBContext.Posts.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdatePost(PostModel model)
        {
            var dbobject = _DBContext.Posts.FirstOrDefault(x => x.IdPost == model.IdPost);
            if (dbobject != null)
            {
                dbobject.IdPost = model.IdPost;
                dbobject.Make = model.Make;
                dbobject.Model = model.Model;
                dbobject.VehicleType = model.VehicleType;
                dbobject.Edition = model.Edition;
                dbobject.Price = model.Price;
                dbobject.FirstRegistration = model.FirstRegistration;
                dbobject.Mileage = model.Mileage;
                dbobject.Power = model.Power;
                dbobject.FuelType = model.FuelType;
                dbobject.EngineCapacity = model.EngineCapacity;
                dbobject.NumberOfDoors = model.NumberOfDoors;
                dbobject.Colour = model.Colour;
                dbobject.Description = model.Description;
                dbobject.IdUser = model.IdUser;
                dbobject.Location = model.Location;
                dbobject.Image = model.Image;
                _DBContext.SaveChanges();
            }


        }

        public void DeletePost(PostModel model)
        {
            var dbobject = _DBContext.Posts.FirstOrDefault(x => x.IdPost == model.IdPost);
            if (dbobject != null)
            {
                _DBContext.Posts.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }
    }
}