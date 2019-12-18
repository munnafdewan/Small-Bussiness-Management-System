using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.DatabaseContext.DatabaseContext;

namespace Error404.Repository.Repository
{
   public  class SupplierRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Supplier supplier)
        {
            _dbContext.Suppliers.Add(supplier);

            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            Supplier asupplier = _dbContext.Suppliers.FirstOrDefault(c => c.Id==id);
            _dbContext.Suppliers.Remove(asupplier);
            return _dbContext.SaveChanges() > 0;
            
        }
        public bool Update(Supplier supplier)
        {
           Supplier asupplier = _dbContext.Suppliers.FirstOrDefault(c => c.Id == supplier.Id);

            if (asupplier != null)
            {
                asupplier.Code = supplier.Code;
                asupplier.Name = supplier.Name;
                asupplier.Address = supplier.Address;
                asupplier.Email = supplier.Email;
                asupplier.Contact = supplier.Contact;
                asupplier.Contactperson = supplier.Contactperson;
            }


            return _dbContext.SaveChanges() > 0;
        }
        public List<Supplier> GetAll()
        {
            return _dbContext.Suppliers.ToList();
        }
        public Supplier GetById(int id)
        {
            return _dbContext.Suppliers.FirstOrDefault(c => c.Id == id);
        }
        public string UniqueTest(Supplier supplier)
        {
            bool isNUnq, isCUnq;
            string errString = "";

            if ((_dbContext.Suppliers.FirstOrDefault(c => c.Name == supplier.Name)) == null)
            {
                isNUnq = true;
                errString += "";
            }
            else
            {
                isNUnq = false;
                errString += "Name is not Unique ";
            }


            if ((_dbContext.Suppliers.FirstOrDefault(c => c.Code == supplier.Code)) == null)
            {
                isCUnq = true;
                errString += "";
            }
            else
            {
                isCUnq = false;
                errString += " Code is not Unique";
            }

            if ((_dbContext.Suppliers.FirstOrDefault(c => c.Email == supplier.Email)) == null)
            {
                isCUnq = true;
                errString += "";
            }
            else
            {
                isCUnq = false;
                errString += " Email is not Unique";
            }

            if ((_dbContext.Suppliers.FirstOrDefault(c => c.Contact == supplier.Contact)) == null)
            {
                isCUnq = true;
                errString += "";
            }
            else
            {
                isCUnq = false;
                errString += " Contact is not Unique";
            }

            return errString;

        }

    }
}
