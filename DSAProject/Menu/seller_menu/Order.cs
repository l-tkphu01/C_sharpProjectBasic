using ProductClass;
using System;
using UserInfClass;

namespace OrderClass
{
    public struct Order
    {
        public User Customer;
        public Product product;
        public Order(User customer, Product product)
        {
            this.Customer = customer;
            this.product = product;
        }
    }
}
