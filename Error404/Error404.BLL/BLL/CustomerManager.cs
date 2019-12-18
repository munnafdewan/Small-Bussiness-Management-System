using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.Repository.Repository;

namespace Error404.BLL.BLL
{
    public class CustomerManager
    {
        CustomerRepository _customerRepository = new CustomerRepository();
        public bool Add(Customer customer)
        {
            return _customerRepository.Add(customer);
        }
        public bool Delete(int id)
        {
            return _customerRepository.Delete(id);
        }
        public bool Update(Customer customer)
        {
            return _customerRepository.Update(customer);
        }
        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }
        public Customer GetById(int id)
        {
            return _customerRepository.GetById(id);
        }
        public string UniqueTest(Customer customer)
        {
            return _customerRepository.UniqueTest(customer);
        }
    }
}
