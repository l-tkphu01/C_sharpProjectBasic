using System;
using FindingProductClass;
using PaymentMethodClass;
using ShoppingCartClass;
using ProductClass;
using OrderClass;
using SellerClass;

namespace UserInfClass
{
    public struct User
    {
        public string account;
        public string password;
        public string email;
        public string name;
        public string phone;
        public string address;
        public ShoppingCart currentCart;
        public ShoppingCart cartDelivered;
        public Payment payment;
        public User(string account, string password, string email, string name, string phone, string address)
        {
            this.account = account;
            this.password = password;
            this.email = email;
            this.name = name;
            this.phone = phone;
            this.address = address;
            this.currentCart = new ShoppingCart();
            this.cartDelivered = new ShoppingCart();
            this.payment = new Payment();
        }
        //xem giỏ hàng
        public void showShoppingCart()
        {
            currentCart.showShoppingCart();
        }
        public void addToCart(List<Product> products, string idProduct, int quantityBuying)
        {
            currentCart.addToCart(products, idProduct, quantityBuying);
        }
        //xem thanh toán
        public void linkToSeller(SellerInf UserSeller, User currentUser)
        {
            if (currentCart.first == null)
            {
                Console.WriteLine("Chưa có đơn hàng nào");
                return;
            }
            ShoppingCartClass.Node current = currentCart.first;
            NodeSeller sellerCurrent = UserSeller.head;
            while (current != null)
            {
                sellerCurrent = UserSeller.head;
                while (sellerCurrent != null)
                {
                    if (current.product.shopName.Equals(sellerCurrent.dataSeller.shopName))
                    {
                        sellerCurrent.dataSeller.createOrder(new Order(currentUser, current.product));
                    }
                    sellerCurrent = sellerCurrent.next;
                }
                current = current.next;
            }
        }
        public void showPendingOrders()
        {
            cartDelivered.showShoppingCart();
        }
        public void showInfo()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Tên người dùng: " + account.ToUpper());
            Console.WriteLine("Địa chỉ: " + address);
            Console.WriteLine("Số điện thoại: " + phone);
            Console.WriteLine("Email: " + email);
            Console.WriteLine(new string('-', 40));
        }
        //Chức năng đánh giá.
        public void rateProduct(findingList listFindingProduct, string productID)
        {
            Console.Write("Nhập số sao đánh giá: ");
            int rate = int.Parse(Console.ReadLine());
            for (int i = 0; i < listFindingProduct.finding.Count; i++)
            {
                if (listFindingProduct.finding[i].productID.Equals(productID))
                {
                    Product updatedProduct = listFindingProduct.finding[i];
                    updatedProduct.turn++;
                    updatedProduct.setRate(rate);
                    listFindingProduct.finding[i] = updatedProduct;
                    break;
                }
            }
        }
        //thay đổi mật khẩu
        public void changePassword()
        {
            Console.Write("Nhập mật khẩu hiện tại: ");
            string oldPassword = Console.ReadLine();
            Console.Write("Nhập mật khẩu mới: ");
            string newPassword = Console.ReadLine();
            Console.Write("Xác nhận mật khẩu mới: ");
            string checkNewPassword = Console.ReadLine();
            if (oldPassword != this.password)
            {
                Console.WriteLine("Sai mật khẩu hiện tại, vui lòng thử lại !");
                return;
            }
            else if (oldPassword == newPassword)
            {
                Console.WriteLine("Mật khẩu mới không được trùng với mật khẩu hiện tại. Vui lòng thử lại!");
            }
            else if (checkNewPassword != newPassword)
            {
                Console.WriteLine("Xác nhận mật khẩu không đúng!");
            }
            else
            {
                this.password = newPassword;
                Console.WriteLine("Đã cập nhật mật khẩu mới thành công");
            }
        }
        //cập nhật địa chỉ
        public void updateAddress()
        {
            Console.Write("Nhập địa chỉ giao hàng mới: ");
            string newAddress = Console.ReadLine();
            this.address = newAddress;
            Console.WriteLine("Đã cập nhật địa chỉ giao hàng thành công !");
        }
        //cập nhật tên
        public void updateName()
        {
            Console.Write("Nhập họ và tên mới: ");
            string newName = Console.ReadLine();
            this.name = newName;
            Console.WriteLine("Đã cập nhật tên thành công !");
        }
    }
    public class nodeAccount
    {
        public User dataUser;
        public nodeAccount next;
        public nodeAccount(User dataUser)
        {
            this.dataUser = dataUser;
            next = null;
        }
    }
    public struct UserInf
    {
        public nodeAccount head;
        // Chức năng đăng nhập
        public bool checkAccount(string accountCheck, string passwordCheck)
        {
            nodeAccount current = head;
            while (current != null)
            {

                if (current.dataUser.account.Equals(accountCheck, StringComparison.OrdinalIgnoreCase) &&
                    current.dataUser.password.Equals(passwordCheck, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }
        public nodeAccount userFindingByAccount(string account, string password)
        {
            nodeAccount current = head;
            while (current != null)
            {
                if (current.dataUser.account.Equals(account) &&
                    current.dataUser.password.Equals(password))
                {
                    return current;
                }
                current = current.next;
            }
            return null;
        }
        public void logIn()
        {
            string account;
            string password;
            do
            {
                Console.WriteLine("Tài khoản: ");
                account = Console.ReadLine();
                Console.WriteLine("Mật khẩu: ");
                password = Console.ReadLine();
                if (!checkAccount(account, password))
                {
                    Console.WriteLine("Tài khoản hoặc mật khẩu không đúng. Vui lòng nhập lại.");
                }
            }
            while (!checkAccount(account, password));
            Console.WriteLine("Đăng nhập thành công!");
        }
        // Chức năng đăng ký
        public void addUser(User user)
        {
            nodeAccount newNode = new nodeAccount(user);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                nodeAccount current = head;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = newNode;
            };
        }
        //các hàm kiểm tra sự trùng lặp khi tạo tài khoản
        public bool checkAccountExist(string accountCheck)
        {
            nodeAccount current = head;
            while (current != null)
            {
                if (current.dataUser.account.Equals(accountCheck, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }
        public bool checkEmailExist(string emailCheck)
        {
            nodeAccount current = head;
            while (current != null)
            {
                if (current.dataUser.email.Equals(emailCheck, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }
        public bool checkPhoneExist(string phoneCheck)
        {
            nodeAccount current = head;
            while (current != null)
            {
                if (current.dataUser.phone.Equals(phoneCheck, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }
        //tạo mã xác nhận
        public string verificationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] code = new char[6];
            for (int i = 0; i < 6; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }
            return new string(code);
        }
        public void register()
        {
            User newUser = new User();
            Console.WriteLine("Họ và tên: ");
            newUser.name = Console.ReadLine();
            do
            {
                Console.WriteLine("Tài khoản: ");
                newUser.account = Console.ReadLine();
                if (checkAccountExist(newUser.account))
                {
                    Console.WriteLine("Tài khoản đã tồn tại. Vui lòng nhập lại: ");
                }
            }
            while (checkAccountExist(newUser.account));
            Console.WriteLine("Mật khẩu: ");
            newUser.password = Console.ReadLine();
            Console.WriteLine("Xác nhận mật khẩu: ");
            string temp;
            do
            {
                temp = Console.ReadLine();
                if (!temp.Equals(newUser.password))
                {
                    Console.WriteLine("Mật khẩu không đúng. Vui lòng nhập lại: ");
                }
            }
            while (!temp.Equals(newUser.password));
            do
            {
                Console.WriteLine("Số điện thoại: ");
                newUser.phone = Console.ReadLine();

                if (newUser.phone.Length != 10 || !newUser.phone.All(char.IsDigit))
                {
                    Console.WriteLine("Số điện thoại không hợp lệ. Vui lòng nhập lại: ");
                }
                else if (checkPhoneExist(newUser.phone))
                {
                    Console.WriteLine("Số điện thoại đã tồn tại. Vui lòng nhập lại: ");
                }
            }
            while (newUser.phone.Length != 10 || !newUser.phone.All(char.IsDigit) || checkPhoneExist(newUser.phone));
            Console.WriteLine("Địa chỉ thường trú: ");
            newUser.address = Console.ReadLine();
            do
            {
                Console.WriteLine("Email: ");
                newUser.email = Console.ReadLine();
                if (checkAccountExist(newUser.email))
                {
                    Console.WriteLine("Email đã tồn tại. Vui lòng nhập lại: ");
                }
            }
            while (checkEmailExist(newUser.email));
            string code;
            string inputCode;
            do
            {
                code = verificationCode();
                Console.WriteLine("Mã xác thực: " + code);
                Console.Write("Nhập mã xác thực: ");
                inputCode = Console.ReadLine();
                if (!inputCode.Equals(code))
                {
                    Console.WriteLine("Mã xác thực sai. Vui lòng nhập lại.");
                }
            }
            while (!inputCode.Equals(code));
            addUser(newUser);
        }
    }
}