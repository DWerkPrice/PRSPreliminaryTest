using System;
using System.Linq;
using PRSPreTestLibrary;
using PRSPreTestLibrary.Controllers;
using PRSPreTestLibrary.Models;

namespace PRSPreliminaryTest
{
    class Program
    {
        static void Main(string[] args) {
            //  var context = new AppDbContext();
            var userCtrl = new UserController();
            userCtrl.GetAll().ToList().ForEach(u => Console.WriteLine(u));

            var id = 12;
            if (userCtrl.GetByPK(id) != null) {
                Console.WriteLine(userCtrl.GetByPK(id));
                var user = new {userCtrl.Find(id) };
            } else {
                Console.WriteLine($"User Id={id} not found");
            }
            

            //           AddUser(context);
            // Add user Data
  /*          var user = new User {
                Id = 12 ,
                Username = "BlaiseTheDog" ,
                Password = "Lightning 451" ,
                Firstname = "Blaise" ,
                Lastname = "Dogson" ,
                Phone = "513-888-3663" ,
                Email = "bDogboy@mail.com" ,
                IsAdmin = false,
                IsReviewer= false
            };*/
            //             userCtrl.Insert(user);
           
            userCtrl.Update(12 , user);

            //                               var rowsAffected = context.SaveChanges();
            //                              if (rowsAffected == 0) throw new Exception("Add failed!");
            //                              return;
            //              */
            /*
                            static void AddVendors(AppDbContext context) {

                                var Vendor1 = new Vendor { Id = 0 , Code = "100" , Name = "Amazon" , Address = "P.O. Box 81226" , City = "Seattle" , State = "WA" , Zip = "98108" , Phone = "888-260-3381" , Email = "cusomerservice@amazon.com" };
                                var Vendor2 = new Vendor { Id = 0 , Code = "200" , Name = "Microsoft" , Address = "P.O. Box 4666" , City = "Seattle" , State = "WA" , Zip = "98108" , Phone = "888-666-3381" , Email = "cusomerservice@microcenter.com" };
                                var Vendor3 = new Vendor { Id = 0 , Code = "300" , Name = "5/3 BankCorp" , Address = "53 Walnut St." , City = "Cincinnti" , State = "OH" , Zip = "45202" , Phone = "888-879-3381" , Email = "cusomerservice@cincinnatibell.com" };
                                context.AddRange(Vendor1 , Vendor2 , Vendor3);

                                var rowsAffected = context.SaveChanges();
                                if (rowsAffected != 3) throw new Exception("Three order did not add!");
                                Console.WriteLine("added 3 orders");
                            }
                        static void AddProducts(AppDbContext context) {

                            var Product1 = new Vendor { Id = 0 , Code = "100" , Name = "Amazon" , Address = "P.O. Box 81226" , City = "Seattle" , State = "WA" , Zip = "98108" , Phone = "888-260-3381" , Email = "cusomerservice@amazon.com" };
                            var Product2 = new Vendor { Id = 0 , Code = "200" , Name = "Microsoft" , Address = "P.O. Box 4666" , City = "Seattle" , State = "WA" , Zip = "98108" , Phone = "888-666-3381" , Email = "cusomerservice@microcenter.com" };
                            var Product3 = new Vendor { Id = 0 , Code = "300" , Name = "5/3 BankCorp" , Address = "53 Walnut St." , City = "Cincinnti" , State = "OH" , Zip = "45202" , Phone = "888-879-3381" , Email = "cusomerservice@cincinnatibell.com" };
                            context.AddRange(Product1 , Product2 , Product3);

                            var rowsAffected = context.SaveChanges();
                            if (rowsAffected != 3) throw new Exception("Three order did not add!");
                            Console.WriteLine("added 3 orders");
                        }

                    }

                    private static void AddUser(object context) {
                        throw new NotImplementedException();
                    }

                    private static void AddNewUser(object context) {
                        throw new NotImplementedException();

                    }
            */
        }
    }
}
