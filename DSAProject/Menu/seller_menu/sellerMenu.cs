using System;
using UserInfClass;
using SellerClass;
using CheckTrueInput;
using FindingProductClass;

namespace SellerMenuClass
{
    public struct sellerMenuProgram
    {
        #region Menu Login
        public static void loginSeller(ref SellerInf userSeller, findingList myList, ref UserInf userCurrent)
        {
            bool backToMain = false;
            //SellerInf userSeller = new SellerInf();
            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("===== Menu dành cho người bán hàng =====");
                Console.WriteLine("1. Đăng nhập");
                Console.WriteLine("2. Tạo shop");
                Console.WriteLine("3. Quay về Menu chính");
                Console.Write("Lựa chọn của bạn: ");

                string customerChoice = Console.ReadLine();

                switch (customerChoice)
                {
                    case "1":
                        login(ref userSeller, myList, userCurrent);
                        break;
                    case "2":
                        register(ref userSeller);
                        break;

                    case "3":
                        backToMain = true;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để thử lại...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        #endregion
        #region Menu After Login
        public static void sellerMenu(ref NodeSeller userSeller, findingList product, UserInf userCurrent)
        {
            bool backToLogin = false;
            while (!backToLogin)
            {
                Console.Clear();
                Console.WriteLine("===== Menu dành cho người bán hàng =====");
                Console.WriteLine("1. Đăng bán sản phẩm mới");
                Console.WriteLine("2. Xóa sản phẩm đăng bán");
                Console.WriteLine("3. Cập nhật thông tin sản phẩm đăng bán");
                Console.WriteLine("4. Xem đơn hàng đang chờ xác nhận");
                Console.WriteLine("5. Xem đơn hàng đã xác nhận");
                Console.WriteLine("6. Thông tin shop");
                Console.WriteLine("7. Kho hàng");
                Console.WriteLine("8. Đăng xuất");
                Console.Write("Lựa chọn của bạn: ");

                string sellerChoiceAfterLogin = Console.ReadLine();
                switch (sellerChoiceAfterLogin)
                {
                    case "1":
                        Console.WriteLine("===== Đăng sản phẩm mới =====");
                        userSeller.dataSeller.UploadProduct(product);
                        Console.WriteLine("Nhập phím bất kì để tiếp tục");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("===== Xóa một sản phẩm trong kho hàng =====");

                        if (userSeller.dataSeller.productList.getSize() == 0)
                        {
                            Console.WriteLine("Chưa có sản phẩm đăng bán, Không thể xóa!");
                        }
                        else
                        {
                            Console.WriteLine(new string('-', 40));
                            Console.WriteLine("Kho hàng hiện tại ");
                            userSeller.dataSeller.productList.ShowProducts();
                            Console.WriteLine(new string('-', 40));
                            Console.WriteLine("Nhập ID sản phẩm cần xóa: ");
                            String myID = Console.ReadLine();
                            if (userSeller.dataSeller.productList.findProduct(myID))
                            {
                                userSeller.dataSeller.RemoveProduct(myID, product);
                                product.remove(myID);
                            }
                            else
                            {
                                Console.WriteLine("Không tìm thấy ID! Vui lòng thử lại!");
                            }
                        }
                        Console.WriteLine("Nhập phím bất kì để tiếp tục");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.WriteLine("===== Cập nhật một sản phẩm trong kho hàng =====");
                        if (userSeller.dataSeller.productList.getSize() == 0)
                        {
                            Console.WriteLine("Chưa có sản phẩm đăng bán, không thể cập nhật thông tin sản phẩm trong kho");
                        }
                        else
                        {
                            Console.WriteLine(new string('-', 40));
                            Console.WriteLine("Kho hàng hiện tại ");
                            userSeller.dataSeller.productList.ShowProducts();
                            Console.WriteLine(new string('-', 40));
                            Console.WriteLine("Nhập ID sản phẩm cần cập nhật: ");
                            String oldID = Console.ReadLine();
                            if (userSeller.dataSeller.productList.findProduct(oldID))
                            {
                                userSeller.dataSeller.UpdateProduct(oldID, product);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Không tìm thấy ID! Vui lòng thử lại!");
                            }
                        }
                        Console.WriteLine("Nhập phím bất kì để tiếp tục");
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.WriteLine("===== Xem đơn hàng đang chờ xác nhận =====");
                        userSeller.dataSeller.showUnconfirmedOrder();
                        while (true)
                        {
                            if (userSeller.dataSeller.unconfirmedOrder.Count() == 0)
                            {
                                break;
                            }
                            Console.WriteLine($"Xác nhận đơn hàng có ID {userSeller.dataSeller.unconfirmedOrder.Peek()} ? ");
                            String myID = userSeller.dataSeller.unconfirmedOrder.Peek();
                            String myChoice = InputChecker.checkChoiceYesNo();
                            if (myChoice == "yes")
                            {
                                userSeller.dataSeller.confirmOrder(userCurrent, product, myID);

                            }
                            else if (myChoice == "no")
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Nhập phím bất kì để tiếp tục");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.WriteLine("===== Xem đơn hàng đã xác nhận =====");
                        userSeller.dataSeller.showConfirmedOrder();
                        Console.WriteLine("Nhập phím bất kì để quay về");
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.WriteLine($"===== Thông tin shop {userSeller.dataSeller.shopName} =====");
                        userSeller.dataSeller.showInfo();
                        Console.WriteLine("Nhập phím bất kì để quay về");
                        Console.ReadKey();
                        break;
                    case "7":
                        Console.WriteLine($"===== Thông tin kho hàng =====");
                        userSeller.dataSeller.productList.updateRateFromFindingList(product);
                        userSeller.dataSeller.productList.ShowProducts();
                        if (userSeller.dataSeller.productList.getSize() > 0)
                        {
                            Console.WriteLine("Sắp xếp theo lượng mua giảm dần ?");
                            String mychoice = InputChecker.checkChoiceYesNo();
                            if (mychoice == "yes")
                            {
                                userSeller.dataSeller.InterchangeSortByBuyCountDescending();
                                userSeller.dataSeller.productList.ShowProducts();
                            }
                        }
                        Console.WriteLine("Nhập phím bất kì để quay về");
                        Console.ReadKey();
                        break;
                    case "8":
                        backToLogin = true;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để thử lại...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        #endregion
        #region Login Static Function
        static void login(ref SellerInf userSeller, findingList myList, UserInf userCurrent)
        {
            string account;
            string password;
            do
            {
                Console.WriteLine("Tên shop: ");
                account = Console.ReadLine();
                Console.WriteLine("Mật khẩu: ");
                password = Console.ReadLine();
                if (!userSeller.checkAccount(account, password))
                {
                    Console.WriteLine("Tên shop hoặc mật khẩu không đúng. Vui lòng nhập lại.");
                    Console.WriteLine("Bạn có muốn tiếp tục đăng nhập ? ");
                    string choice = InputChecker.checkChoiceYesNo();
                    if (choice == "no")
                    {
                        return;
                    }
                }
            }
            while (!userSeller.checkAccount(account, password));
            NodeSeller accountCurrent = userSeller.sellerFindingByAccount(account, password);
            Console.WriteLine("Đăng nhập thành công, nhấn nút bất kì để đi đến Menu");
            Console.ReadKey();
            sellerMenu(ref accountCurrent, myList, userCurrent);

        }
        #endregion
        #region Register Static Function
        static void register(ref SellerInf userSeller)
        {
            userSeller.register();
            Console.WriteLine("Đăng kí thành công! Nhấn nút bất kì để tiếp tục");
            Console.ReadKey();
        }
        #endregion
    }
}
