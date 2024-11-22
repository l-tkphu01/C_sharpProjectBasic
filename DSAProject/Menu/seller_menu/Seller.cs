using System;
using ProductSellerClass;
using ProductClass;
using OrderManagementClass;
using UserInfClass;
using FindingProductClass;
using OrderClass;

namespace SellerClass
{
    public struct Seller
    {
        public string password;
        public String shopName;
        public ProductSellerLinkedList productList;
        double revenue;
        int salesAmount;
        OrderQueue confirmedOrder;
        public OrderQueue unconfirmedOrder;

        public Seller()
        {
            this.revenue = 0;
            this.salesAmount = 0;
            this.productList = new ProductSellerLinkedList();
            this.confirmedOrder = new OrderQueue();
            this.unconfirmedOrder = new OrderQueue();
        }

        //trả về tổng doanh thu
        public void printTotalRevenue()
        {
            Console.WriteLine(new string('-', 40));
            printProductRevenue();
            Console.WriteLine("Tổng doanh thu cửa hàng: " + this.revenue);
        }
        //in doanh thu từng sản phẩm
        public void printProductRevenue()
        {
            ProductSellerClass.Node current = productList.getHead();

            if (current == null)
            {
                Console.WriteLine("Danh sách sản phẩm đăng bán rỗng, Không có thống kê doanh thu từng sản phẩm.");
                return;
            }
            this.revenue = 0;

            int length = 66;
            Console.WriteLine(new string('-', length));
            Console.WriteLine("| {0,-15} | {1,-25} | {2,-15:F2} |", "ID sản phẩm", "Sản phẩm", "Doanh thu đã bán");
            Console.WriteLine(new string('-', length));

            while (current != null)
            {
                Product product = current.Data;
                double productRevenue = product.buyCount * product.price;
                Console.WriteLine("| {0,-15} | {1,-25} | {2,-16:F2} |", product.productID, product.productName, productRevenue);
                Console.WriteLine(new string('-', length));
                this.revenue += productRevenue;
                current = current.Next;
            }
            Console.WriteLine(new string('-', length));
        }

        //tính tổng số lượng sản phẩm đã bán
        public void setSalesAmount()
        {
            Console.WriteLine(new string('-', 40));

            int totalSalesAmount = 0;
            ProductSellerClass.Node current = productList.getHead();

            if (current == null)
            {
                Console.WriteLine("Danh sách sản phẩm rỗng, Không có thống kê tổng số lượng sản phẩm đã bán.");
                return;
            }
            Console.WriteLine("Thông tin số lượng sản phẩm:");
            int length = 84;
            Console.WriteLine(new string('-', length));
            Console.WriteLine("| {0,-15} | {1,-25} | {2,-15} | {3,-15} |", "ID sản phẩm", "Sản phẩm", "Số lượng bán", "Số lượng còn lại");
            Console.WriteLine(new string('-', length));

            while (current != null)
            {
                Product product = current.Data;
                int remainingQuantity = product.quantity;

                totalSalesAmount += product.buyCount;
                Console.WriteLine("| {0,-15} | {1,-25} | {2,-15} | {3,-16} |", product.productID, product.productName, product.buyCount, remainingQuantity);
                Console.WriteLine(new string('-', length));
                current = current.Next;
            }

            Console.WriteLine(new string('-', length));
            this.salesAmount = totalSalesAmount;
            Console.WriteLine("Tổng sản phẩm đã bán: " + this.salesAmount);
        }

        public void UploadProduct(findingList listFindingProduct)
        {
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("Điền thông tin sản phẩm đăng bán: ");
            Console.WriteLine("ID sản phẩm: ");
            string productID = Console.ReadLine();
            productID += "-" + this.shopName;
            Console.WriteLine("Tên sản phẩm: ");
            string productName = Console.ReadLine();
            double price = 0;
            int quantity = 0;
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Giá: ");
                try
                {
                    price = double.Parse(Console.ReadLine());
                    if (price > 0)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Giá phải lớn hơn 0. Vui lòng nhập lại.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Vui lòng nhập giá thành hợp lệ.");
                }
            }
            validInput = false;

            // Kiểm tra input số lượng
            while (!validInput)
            {
                Console.WriteLine("Số lượng: ");
                try
                {
                    quantity = int.Parse(Console.ReadLine());

                    // Kiểm tra nếu số lượng > 0
                    if (quantity > 0)
                    {
                        validInput = true;  // Nếu hợp lệ, thoát khỏi vòng lặp
                    }
                    else
                    {
                        Console.WriteLine("Số lượng phải lớn hơn 0. Vui lòng nhập lại.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Vui lòng nhập số lượng hợp lệ.");
                }
            }
            Console.WriteLine("Mô tả: ");
            String description = Console.ReadLine();
            Product newProduct = new Product(productID, productName, price, quantity, description, this.shopName);
            productList.AddFirst(newProduct);
            listFindingProduct.add(newProduct);
            Console.WriteLine("Đã thêm sản phẩm mới thành công!");
        }

        //xóa sản phẩm
        public void RemoveProduct(string IDfind, findingList listFindingProduct)
        {
            if (productList.findProduct(IDfind) == true)
            {
                productList.Remove(IDfind);
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm");
            }
        }

        //sửa đổi sản phẩm
        public void UpdateProduct(string oldID, findingList listFindingProduct)
        {
            productList.Update(oldID, listFindingProduct, this.shopName);
            Console.WriteLine("Đã sửa đổi sản phẩm");
        }
        public void InterchangeSortByBuyCountDescending()
        {
            if (productList.getHead() == null || productList.getHead().Next == null)
            {
                return;
            }

            ProductSellerClass.Node current = productList.getHead();

            while (current != null)
            {
                ProductSellerClass.Node nextNode = current.Next;

                while (nextNode != null)
                {
                    if (current.Data.buyCount < nextNode.Data.buyCount)
                    {
                        Product temp = current.Data;
                        current.Data = nextNode.Data;
                        nextNode.Data = temp;
                    }
                    nextNode = nextNode.Next;
                }
                current = current.Next;
            }

            Console.WriteLine("Danh sách sản phẩm đã được sắp xếp theo ID giảm dần.");
        }

        //Cập nhật lại ProductSellerLinkedList từ List<Product>
        private void UpdateProductList(List<Product> products)
        {
            productList = new ProductSellerLinkedList();
            foreach (Product product in products)
            {
                productList.AddFirst(product);
            }
        }

        public void showInfo()
        {
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("Shop Name: " + this.shopName);
            Console.WriteLine("Số lượng sản phẩm: " + productList.getSize());
            setSalesAmount();
            printTotalRevenue();
            Console.WriteLine(new string('*', 40));
        }

        //auto tạo đơn hàng khi người dùng thanh toán
        public void createOrder(Order newProduct)
        {
            unconfirmedOrder.Enqueue(newProduct);
        }

        //xác nhận đơn hàng
        public void confirmOrder(UserInf userCurrent, findingList list, string myID)
        {
            if (unconfirmedOrder.IsEmpty())
            {
                Console.WriteLine("Không có đơn hàng chờ xác nhận!");
                return;
            }
            unconfirmedOrder.updateForBuyer(userCurrent, myID);
            unconfirmedOrder.updateForFindingList(list, myID);
            unconfirmedOrder.updateForProductList(this.productList, myID);
            unconfirmedOrder.Dequeue();
            confirmedOrder.Enqueue(unconfirmedOrder.deQueueData);
            Console.WriteLine("Đơn hàng đã được xác nhận");
        }
        //show đơn hàng đã xác nhận
        public void showConfirmedOrder()
        {
            confirmedOrder.show();
        }
        //show đơn hàng chưa xác nhận
        public void showUnconfirmedOrder()
        {
            unconfirmedOrder.show();
        }
    }

    public class NodeSeller
    {
        public Seller dataSeller;
        public NodeSeller next;
        public NodeSeller(Seller dataSeller)
        {
            this.dataSeller = dataSeller;
            this.next = null;
        }
    }
    public class SellerInf
    {
        public NodeSeller head;
        // Chức năng đăng nhập
        public bool checkAccount(string shopNameCheck, string passwordCheck)
        {
            NodeSeller current = head;
            while (current != null)
            {
                if (current.dataSeller.shopName.Equals(shopNameCheck, StringComparison.OrdinalIgnoreCase) && current.dataSeller.password.Equals(passwordCheck, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }
        public void login()
        {
            string shopName;
            string password;
            do
            {
                Console.WriteLine("Tên shop: ");
                shopName = Console.ReadLine();
                Console.WriteLine("Mật khẩu: ");
                password = Console.ReadLine();
                if (!checkAccount(shopName, password))
                {
                    Console.WriteLine("Tài khoản hoặc mật khẩu không đúng. Vui lòng nhập lại.");
                }
            }
            while (!checkAccount(shopName, password));
            Console.WriteLine("Đăng nhập thành công!");
        }
        //thêm người dùng vào linkedlist
        public void addSeller(Seller seller)
        {
            NodeSeller newNode = new NodeSeller(seller);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                NodeSeller current = head;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = newNode;
            }
            Console.WriteLine("Đăng ký thành công!");
        }
        public NodeSeller sellerFindingByAccount(string account, string password)
        {
            NodeSeller current = head;
            while (current != null)
            {
                if (current.dataSeller.shopName.Equals(account) &&
                    current.dataSeller.password.Equals(password))
                {
                    return current;
                }
                current = current.next;
            }
            return null;
        }
        public bool checkShopNameExist(string shopNameCheck)
        {
            NodeSeller current = head;
            while (current != null)
            {
                if (current.dataSeller.shopName.Equals(shopNameCheck, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }
        public void register()
        {
            Seller newSeller = new Seller();
            do
            {
                Console.WriteLine("Tên shop: ");
                newSeller.shopName = Console.ReadLine();
                if (checkShopNameExist(newSeller.shopName))
                {
                    Console.WriteLine("Tên shop tồn tại. Vui lòng nhập lại: ");
                }
            }
            while (checkShopNameExist(newSeller.shopName));
            Console.WriteLine("Mật khẩu: ");
            newSeller.password = Console.ReadLine();
            addSeller(newSeller);
        }
    }
}