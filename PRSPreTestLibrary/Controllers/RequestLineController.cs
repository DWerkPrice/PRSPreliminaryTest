using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PRSPreTestLibrary.Models;

namespace PRSPreTestLibrary.Controllers
{
    public class RequestLineController
    {
        private readonly AppDbContext context = new AppDbContext();

        private void RecalcRequestTotal(int requestId) {//can be made internal and called from change in price
            var request = context.Requests.Find(requestId);
            request.Total = request.RequestLines.Sum(x => x.Quantity * x.Product.Price);
            //var total = context. Requestlines.Where(r1 => r1.RequestId == RequestId)
            //                                  .Sum(r1 => r1quantity * r1.Product.Price);
            //
            // requet.Total = Total;
            context.SaveChanges();
        }

        public IEnumerable<Requestline> GetAll() {
            return context.RequestLines.ToList();  //do not need to check for null will always get collection back even if empty 
        }
        public Requestline GetByPK(int id) {// have to include type here because it could be different
            if (id < 1) throw new Exception("Id must be GT zero");
            var requestline = context.RequestLines.Find(id); //find is used to get one requestline by id would return null and need to be checked by if statement if null changes what we need to do
            return context.RequestLines.Find(id);
        }
        public Requestline Insert(Requestline requestLine) {//need to pass entire instance of the requestline}
            if (requestLine == null) throw new Exception("RequestLine cannot be null");
            // should check requestline name is not null and maxlength of requestline namer not exceeded
            // should also check that id is not anything other than 0 because database is providing key
            //edit checking here
            context.RequestLines.Add(requestLine);
            try {
                context.SaveChanges();
                RecalcRequestTotal(requestLine.RequestId);
                // hover over save changes will see exceptions that can occur 
            } catch (DbUpdateException ex) {
                throw new Exception("Username must be unique" , ex);
            } catch (Exception) {
                throw;
            }
            return requestLine;// the add will generate the id into the passed in requestline and return back here
        }

        public bool Update(int id , Requestline requestLine) {// passing in both the requestline and the id so that you can verify you are updateing the correct record
            if (requestLine == null) throw new Exception("RequestLine cannot be null");
            if (id != requestLine.Id) throw new Exception("Id and Requestline.IC must match");// can also check id >0 and all the instance data validity like name done under insert
                                                                                              // update datetime? variable Updated           
            context.Entry(requestLine).State = EntityState.Modified;  // state tells the status of the data in the context[same as if you had read it from database and updated
                                                                      //          context.requestline.Update(requestline);
            try {
                context.SaveChanges();// hover over save changes will see exceptions that can occur
                RecalcRequestTotal(requestLine.RequestId);

            } catch (DbUpdateException ex) {
                throw new Exception("RequestLinename must be unique" , ex);
            } catch (Exception ex) {
                throw ex;
            }
            context.SaveChanges();
            RecalcRequestTotal(requestLine.RequestId);

            return true;
        }
        // ***** add error checking ********

        public bool Delete(int id) {
            if (id <= 0) throw new Exception("Id must be GT zero");
            var requestLine = context.RequestLines.Find(id);
            // need to add exception here.   
            return Delete(requestLine);
        }
        // the two deletes are overloading the delete methods by allowing them to pass either the id or the entire requestline in


        public bool Delete(Requestline requestLine) { 
            context.RequestLines.Remove(requestLine);
            context.SaveChanges();
            RecalcRequestTotal(requestLine.RequestId);
            return true;
        }
    }
}