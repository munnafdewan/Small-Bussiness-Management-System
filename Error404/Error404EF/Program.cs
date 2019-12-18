using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.BLL.BLL;
using Error404.Model.Model;

namespace Error404EF
{
    public class Program
    {
        static void Main(string[] args)
        {
            SupplierManager _supplierManager = new SupplierManager();
            Supplier supplier = new Supplier()
            {
                Code="0004",
                Name="Mim",
                Address="Shariatpur",
                Email="Mim@gmail.com",
                Contact="01761781645",
                Contactperson="Mim01761781645"
            };
            //if(_supplierManager.Add(supplier))
            //{
            //    Console.WriteLine("Saved");

            //}
            //else
            //{
            //    Console.WriteLine("Not Saved");
            //}
           
            //if (_supplierManager.Delete(5))
            //{
            //    Console.WriteLine("Deleted");
            //}
            //else
            //{
            //    Console.WriteLine("Not Deleted");
            //}
            

            //Supplier asupplier = new Supplier();
            //asupplier.Id = 3;
            //asupplier.Code = "0003";
            //asupplier.Name = "Mim";
            //asupplier.Address = "KawranBazar";
            //asupplier.Email = "mim@gmail.com";
            //asupplier.Contact = "01761781643";
            //asupplier.Contactperson = "mim01761781643";

            //if (_supplierManager.Update(asupplier))
            //{
            //    Console.WriteLine("Updated");
            //}
            //else
            //{
            //    Console.WriteLine("Not Updated");
            //}
            

            // var suppliers = _supplierManager.GetAll();
            //foreach(Supplier supp in suppliers )
            //{
            //    Console.WriteLine("Id:"+supp.Id+"\nCode:"+supp.Code+"\nName:"+supp.Name+"\nAddress:"+supp.Address+"\nEmail:"+supp.Email+"\nContact:"+supp.Contact+"\nContactPerson:"+supp.Contactperson+"\n");
            //    Console.ReadKey();
            //}
             
          
            var asupplier = _supplierManager.GetById(3);
            Console.WriteLine("Id:" + asupplier.Id + "\nCode:" + asupplier.Code + "\nName:" + asupplier.Name + "\nAddress:" + asupplier.Address + "\nEmail:" + asupplier.Email + "\nContact:" + asupplier.Contact + "\nContactPerson:" + asupplier.Contactperson + "\n");
            Console.ReadKey();


        }
    }
}
