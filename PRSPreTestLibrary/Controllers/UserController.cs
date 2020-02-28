using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PRSPreTestLibrary.Models;

namespace PRSPreTestLibrary.Controllers{



    public class UserController {

        private AppDbContext context = new AppDbContext();

        public IEnumerable<User> GetAll() {
            return context.Users.ToList();  //do not need to check for null will always get collection back even if empty 
        }
        public User GetByPK(int id) {// have to include type here because it could be different
            if (id < 1) throw new Exception("Id must be GT zero");
            var user = context.Users.Find(id); //find is used to get one user by id would return null and need to be checked by if statement if null changes what we need to do
            return context.Users.Find(id);
        }
        public User Insert(User user) {//need to pass entire instance of the user}
            if (user == null) throw new Exception("User cannot be null");
            // should check username is not null and maxlength of usernamer not exceeded
            // should also check that id is not anything other than 0 because database is providing key
            //edit checking here
            context.Users.Add(user);
            try {
                context.SaveChanges();// hover over save changes will see exceptions that can occur 
            } catch (DbUpdateException ex) {
                throw new Exception("Username must be unique" , ex);
            } catch (Exception) {
                throw;
            }
            return user;// the add will generate the id into the passed in user and return back here
        }
        public bool Update(int id , User user) {// passing in both the user and the id so that you can verify you are updateing the correct record
            if (user == null) throw new Exception("User cannot be null");
            if (id != user.Id) throw new Exception("Id and User.IC must match");// can also check id >0 and all the instance data validity like name done under insert
            // update datetime? variable Updated           
            context.Entry(user).State = EntityState.Modified;  // state tells the status of the data in the context[same as if you had read it from database and updated
            try {
                context.SaveChanges();// hover over save changes will see exceptions that can occur 
            } catch (DbUpdateException ex) {
                throw new Exception("Username must be unique" , ex);
            } catch (Exception ex) {
                throw ex;
            }
            context.SaveChanges();
            return true;
        }
        // ***** add error checking ********
        public bool Delete(int id) {
            if (id <= 0) throw new Exception("Id must be GT zero");
            var user = context.Users.Find(id);
            // need to add exception here.   
            return Delete(user);
        }
        // the two deletes are overloading the delete methods by allowing them to pass either the id or the entire user in
        public bool Delete(User user) {
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
       }
   }
}
