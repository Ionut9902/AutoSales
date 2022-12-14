using Microsoft.EntityFrameworkCore;
using AutoSales.Data;
using System.Diagnostics.Metrics;
using AutoSales.Models.DBObjects;
using AutoSales.Models;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AutoSales.Models.Repository
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public UserRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private UserModel MapDBObjectToModel(User dbobject)
        {
            var model = new UserModel();
            if (dbobject != null)
            {
                model.IdUser = dbobject.IdUser;
                model.Name = dbobject.Name;
                model.YearOfBirth = dbobject.YearOfBirth;
                model.NumberOfPosts = dbobject.NumberOfPosts;
                model.FirstRegistered = dbobject.FirstRegistered;
                model.EmailAddress = dbobject.EmailAddress;
            }
            return model;
        }

        private User MapModelToDBObject(UserModel model)
        {
            var dbobject = new User();
            if (dbobject != null)
            {
                dbobject.IdUser = model.IdUser;
                dbobject.Name = model.Name;
                dbobject.YearOfBirth = model.YearOfBirth;
                dbobject.NumberOfPosts = model.NumberOfPosts;
                dbobject.FirstRegistered = model.FirstRegistered;
                dbobject.EmailAddress = model.EmailAddress;
            }
            return dbobject;
        }

        public List<UserModel> GetAllUsers()
        {
            var list = new List<UserModel>();
            foreach (var dbobject in _DBContext.MyUsers)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }

        public UserModel GetUserByID(Guid id)
        {
            return MapDBObjectToModel(_DBContext.MyUsers.FirstOrDefault(x => x.IdUser == id));
        }

        public UserModel GetUserByEmail(string email)
        {
            return MapDBObjectToModel(_DBContext.MyUsers.FirstOrDefault(x => x.EmailAddress == email));
        }


        public void InsertUser(UserModel model)
        {
            var list = _DBContext.Users;    
           
            model.IdUser = Guid.NewGuid();
            _DBContext.MyUsers.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateUser(UserModel model)
        {
            var dbobject = _DBContext.MyUsers.FirstOrDefault(x => x.IdUser == model.IdUser);
            if (dbobject != null)
            {
                dbobject.IdUser = model.IdUser;
                dbobject.Name = model.Name;
                dbobject.YearOfBirth = model.YearOfBirth;
                dbobject.NumberOfPosts = model.NumberOfPosts;
                dbobject.FirstRegistered = model.FirstRegistered;
                dbobject.EmailAddress = model.EmailAddress;
                _DBContext.SaveChanges();
            }


        }

        public void DeleteUser(UserModel model)
        {
            var dbobject = _DBContext.MyUsers.FirstOrDefault(x => x.IdUser == model.IdUser);
            if (dbobject != null)
            {
                _DBContext.MyUsers.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }
    }
}