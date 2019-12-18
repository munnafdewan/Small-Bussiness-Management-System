using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.DatabaseContext.DatabaseContext;

namespace Error404.Repository.Repository
{
    public class CustomerRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Customer customer)
        {
            _dbContext.Customers.Add(customer);

            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            Customer acustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
            _dbContext.Customers.Remove(acustomer);
            return _dbContext.SaveChanges() > 0;

        }
        public bool Update(Customer customer)
        {
            Customer acustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == customer.Id);

            if (acustomer != null)
            {
                acustomer.Code = customer.Code;
                acustomer.Name = customer.Name;
                acustomer.Address = customer.Address;
                acustomer.Email = customer.Email;
                acustomer.Contact = customer.Contact;
                acustomer.Loyality = customer.Loyality;
            }


            return _dbContext.SaveChanges() > 0;
        }
        public List<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }
        public Customer GetById(int id)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Id == id);
        }
        public string UniqueTest(Customer customer)
        {
            bool isNUnq, isCUnq;
            string errString = "";

            if ((_dbContext.Customers.FirstOrDefault(c => c.Name == customer.Name)) == null)
            {
                isNUnq = true;
                errString += "";
            }
            else
            {
                isNUnq = false;
                errString += "Name is not Unique ";
            }


            if ((_dbContext.Customers.FirstOrDefault(c => c.Code == customer.Code)) == null)
            {
                isCUnq = true;
                errString += "";
            }
            else
            {
                isCUnq = false;
                errString += " Code is not Unique";
            }

            if ((_dbContext.Customers.FirstOrDefault(c => c.Email == customer.Email)) == null)
            {
                isCUnq = true;
                errString += "";
            }
            else
            {
                isCUnq = false;
                errString += " Email is not Unique";
            }

            if ((_dbContext.Customers.FirstOrDefault(c => c.Contact == customer.Contact)) == null)
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
