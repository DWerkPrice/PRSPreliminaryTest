using System;
using System.Linq;
using PRSPreTestLibrary;
using PRSPreTestLibrary.Models;

namespace PRSPreliminaryTest
{
    class Program
    {
        static void Main(string[] args) {
            var context = new AppDbContext();
            AddVendors(context);
        // Add user Data
        /*              var user = new User {
                           Id = 0 ,
                           Username = "ProfessorDoud" ,
                           Password = "Deacon2019" ,
                           Firstname = "Greg" ,
                           Lastname = "Doud" ,
                           Phone = "513-263-3663" ,
                           Email = "gdoud@MaxTrain.com" ,
                           IsAdmin = true
                       };
                       context.Users.Add(user);
                       var rowsAffected = context.SaveChanges();
                       if (rowsAffected == 0) throw new Exception("Add failed!");
                       return;
       */

            static void AddVendors(AppDbContext context) {
    
                var Vendor1 = new Vendor { Id = 0 ,  Code = "100" , Name = "Amazon" , Address = "P.O. Box 81226",City = "Seattle",State = "WA",Zip ="98108",Phone = "888-260-3381",Email="cusomerservice@amazon.com" };
                var Vendor2 = new Vendor { Id = 0 , Code = "200" , Name = "Microsoft" , Address = "P.O. Box 4666" , City = "Seattle" , State = "WA" , Zip = "98108" , Phone = "888-666-3381" , Email = "cusomerservice@microcenter.com" };
                var Vendor3 = new Vendor { Id = 0 , Code = "300" , Name = "5/3 BankCorp" , Address = "53 Walnut St." , City = "Cincinnti" , State = "OH" , Zip = "45202" , Phone = "888-879-3381" , Email = "cusomerservice@cincinnatibell.com" };
                context.AddRange(Vendor1 , Vendor2 , Vendor3 );

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
    }
}
