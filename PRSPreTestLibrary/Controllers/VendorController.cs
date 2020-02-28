using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PRSPreTestLibrary.Models;

namespace PRSPreTestLibrary.Controllers
{
   public class VendorController
    {


            private AppDbContext context = new AppDbContext();

            
            public IEnumerable<Vendor> GetAll() {
                return context.Vendors.ToList();  //do not need to check for null will always get collection back even if empty 
            }
            public Vendor GetByPK(int id) {// have to include type here because it could be different
                if (id < 1) throw new Exception("Id must be GT zero");
                var vendor = context.Vendors.Find(id); //find is used to get one vendor by id would return null and need to be checked by if statement if null changes what we need to do
                return context.Vendors.Find(id);
            }
            public Vendor Insert(Vendor vendor) {//need to pass entire instance of the vendor}
                if (vendor == null) throw new Exception("Vendor cannot be null");
                // should check vendorname is not null and maxlength of usernamer not exceeded
                // should also check that id is not anything other than 0 because database is providing key
                //edit checking here
                context.Vendors.Add(vendor);
                try {
                    context.SaveChanges();// hover over save changes will see exceptions that can occur 
                } catch (DbUpdateException ex) {
                    throw new Exception("Username must be unique" , ex);
                } catch (Exception) {
                    throw;
                }
                return vendor;// the add will generate the id into the passed in vendor and return back here
            }
            public bool Update(int id , Vendor vendor) {// passing in both the vendor and the id so that you can verify you are updateing the correct record
                if (vendor == null) throw new Exception("Vendor cannot be null");
                if (id != vendor.Id) throw new Exception("Id and Vendor.IC must match");// can also check id >0 and all the instance data validity like name done under insert
                                                                                    // update datetime? variable Updated           
                context.Entry(vendor).State = EntityState.Modified;  // state tells the status of the data in the context[same as if you had read it from database and updated
                                                                   //          context.vendor.Update(vendor);
                try {
                    context.SaveChanges();// hover over save changes will see exceptions that can occur 
                } catch (DbUpdateException ex) {
                    throw new Exception("Vendorname must be unique" , ex);
                } catch (Exception ex) {
                    throw ex;
                }
                context.SaveChanges();
                return true;
            }
            // ***** add error checking ********
            public bool Delete(int id) {
                if (id <= 0) throw new Exception("Id must be GT zero");
                var vendor = context.Vendors.Find(id);
                // need to add exception here.   
                return Delete(vendor);
            }
            // the two deletes are overloading the delete methods by allowing them to pass either the id or the entire vendor in
            public bool Delete(Vendor vendor) {
                context.Vendors.Remove(vendor);
                context.SaveChanges();
                return true;
            }
        }
    }

}
}
