using System;
using System.Globalization;
using ProductClass;

namespace FindingProductClass
{
    public struct findingList
    {
        public List<Product> finding;
        public findingList()
        {
            finding = new List<Product>();
        }
        public void add(Product p)
        {
            finding.Add(p);
        }
        public void remove(string ID)
        {
            Product temp = new Product();
            foreach (Product p in finding)
            {
                if (p.productID.Equals(ID))
                {
                    temp = p;
                    break;
                }
            }
            finding.Remove(temp);
        }
        public void show()
        {
            foreach (Product p in finding)
            {
                p.showProduct();
            }
        }
        //tìm kiếm theo tên sản phẩm hoặc tên cửa hàng
        public List<Product> searchShopNameOrName(string search)
        {
            List<Product> tempList = new List<Product>();
            if (finding.Count == 0)
            {
                Console.WriteLine("Hiện danh sách trống");
                return tempList;
            }
            foreach (Product p in finding)
            {
                if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(p.shopName, search, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) >= 0 || CultureInfo.CurrentCulture.CompareInfo.IndexOf(p.productName, search, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) >= 0)
                {
                    tempList.Add(p);
                }
            }
            return tempList;
        }
        //tìm kiếm theo tên cửa hàng
        public List<Product> searchShopName(string search)
        {
            List<Product> tempList = new List<Product>();
            if (finding.Count == 0)
            {
                Console.WriteLine("Hiện danh sách trống");
                return tempList;
            }
            foreach (Product p in finding)
            {
                if (p.shopName.Equals(search))
                {
                    tempList.Add(p);
                }
            }
            return tempList;
        }
    }
}