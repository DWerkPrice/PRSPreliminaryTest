using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PRSPreTestLibrary.Models;

namespace PRSPreTestLibrary.Controllers
{
    public class RequestController {
        // defineed as const which means constant and thus can not be changed -- always use caps
        public const string StatusNew = "NEW;";
        public const string StatusEdit = "EDIT";
        public const string StatusReview = "REVIEW";
        public const string StatusApproved = "APPROVED";
        public const string StatusRejected = "REJECTED";

        private readonly AppDbContext context = new AppDbContext();

    public IEnumerable<Request> GetRequestsToReviewNotOwn(int userId) {
            return context.Requests
               .Where(x => x.UserId != userId && x.Status == StatusReview)
               .ToList();
     }
    
    public bool SetToReview(Request request) {
        if (request.Total <= 50) {
            request.Status = StatusApproved;
        } else {
            request.Status = StatusReview;
        }
        return Update(request.Id , request);
    }

    public bool SetToApproved(Request request) {
        request.Status = StatusApproved;
        return Update(request.Id , request);
    }

    public bool SetToRejected(Request request) {
        request.Status = StatusRejected;
        return Update(request.Id , request);
    }

    public IEnumerable<Request> GetAll() {
        return context.Requests.ToList();  //do not need to check for null will always get collection back even if empty 
    }
    public Request GetByPK(int id) {// have to include type here because it could be different
        if (id < 1) throw new Exception("Id must be GT zero");
        var Request = context.Requests.Find(id); //find is used to get one request by id would return null and need to be checked by if statement if null changes what we need to do
        return context.Requests.Find(id);
    }

    public Request Insert(Request request) {//need to pass entire instance of the request}
        if (request == null) throw new Exception("request cannot be null");
        // should check requestname is not null and maxlength of requestnamer not exceeded
        // should also check that id is not anything other than 0 because database is providing key
        //edit checking here
        context.Requests.Add(request);
        try {
        context.SaveChanges();// hover over save changes will see exceptions that can occur 
        } catch (DbUpdateException ex) {
            throw new Exception("requestname must be unique" , ex);
        } catch (Exception) {
            throw;
        }
        return request;// the add will generate the id into the passed in request and return back here
    }

    public bool Update(int id , Request request) {// passing in both the request and the id so that you can verify you are updateing the correct record
        if (request == null) throw new Exception("request cannot be null");
        if (id != request.Id) throw new Exception("Id and request.IC must match");// can also check id >0 and all the instance data validity like name done under insert
                                                                                  // update datetime? variable Updated           
        context.Entry(request).State = EntityState.Modified;  // state tells the status of the data in the context[same as if you had read it from database and updated
                                                              //          context.requests.Update(request);
        try {
            context.SaveChanges();// hover over save changes will see exceptions that can occur 
        } catch (DbUpdateException ex) {
            throw new Exception("requestname must be unique" , ex);
        } catch (Exception ex) {
            throw ex;
        }
        context.SaveChanges();
        return true;
    }
    // ***** add error checking ********
    public bool Delete(int id) {
        if (id <= 0) throw new Exception("Id must be GT zero");
        var request = context.Requests.Find(id);
        // need to add exception here.   
        return Delete(request);
    }
    // the two deletes are overloading the delete methods by allowing them to pass either the id or the entire request in   
    public bool Delete(Request request) {
        context.Requests.Remove(request);
        context.SaveChanges();
        return true;
    }
}