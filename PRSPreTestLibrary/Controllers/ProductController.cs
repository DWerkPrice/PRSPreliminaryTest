using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PRSPreTestLibrary.Models;

namespace PRSPreTestLibrary.Migrations
{
    public class ProductController
    {
        private AppDbContext context = new AppDbContext();


        public IEnumerable<Product> GetAll() {
            return context.Products.ToList();  //do not need to check for null will always get collection back even if empty 
        }
        public Product GetByPK(int id) {// have to include type here because it could be different
            if (id < 1) throw new Exception("Id must be GT zero");
            return context.Products.Find(id);
        }
        public Product Insert(Product product) {//need to pass entire instance of the product}
            if (product == null) throw new Exception("Product cannot be null");
            context.Products.Add(product);
            try {
                context.SaveChanges();// hover over save changes will see exceptions that can occur 
            } catch (DbUpdateException ex) {
                throw new Exception("Product name must be unique" , ex);
            } catch (Exception) {
                throw;
            }
            return product;// the add will generate the id into the passed in product and return back here
        }
        public bool Update(int id , Product product) {// passing in both the product and the id so that you can verify you are updateing the correct record
            if (product == null) throw new Exception("Product cannot be null");
            if (id != product.Id) throw new Exception("Id and Product.Id must match");// can also check id >0 and all the instance data validity like name done under insert
            // update datetime? variable Updated           
            context.Entry(product).State = EntityState.Modified;  // state tells the status of the data in the context[same as if you had read it from database and updated
            try {
                context.SaveChanges();// hover over save changes will see exceptions that can occur 
            } catch (DbUpdateException ex) {
                throw new Exception("Product must be unique" , ex);
            } catch (Exception) {
                throw;
            }
            return true;
        }
        // ***** add error checking ********
        public bool Delete(int id) {
            if (id <= 0) throw new Exception("Id must be GT zero");
            var product = context.Products.Find(id);
            // need to add exception here.   
            return Delete(product);
        }
        // the two deletes are overloading the delete methods by allowing them to pass either the id or the entire product in
        public bool Delete(Product  product) {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Product> entityEntry = context.Products.Remove(product);
            context.SaveChanges();
            return true;
        }
    }
}

