using System;
using CheckTrueInput;
using UserInfClass;
using ProductClass;
using FindingProductClass;
using PaymentMethodClass;
using SellerClass;
using VoucherManageClass;
namespace CustomerMenuClass
{
    public struct CustomerMenuProgram
    {
        #region Menu Login
        public static void loginCustomer(ref UserInf userCustomer, findingList myList, ref SellerInf userSeller, ref VoucherLinkedlist listVoucher)
        {
            bool backToMain = false;
            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("===== Menu dành cho người mua hàng =====");
                Console.WriteLine("1. Đăng nhập");
                Console.WriteLine("2. Đăng kí");
                Console.WriteLine("3. Quay về Menu chính");
                Console.Write("Lựa chọn của bạn: ");

                string customerChoiceMenuLogin = Console.ReadLine();

                switch (customerChoiceMenuLogin)
                {
                    case "1":
                        login(ref userCustomer, myList, ref userSeller, ref listVoucher);
                        break;
                    case "2":
                        register(ref userCustomer);
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
        #region  Menu After Login
        public static void customerMenu(ref nodeAccount accountCurrent, findingList listFindingProduct, ref SellerInf userSeller, ref VoucherLinkedlist listVoucher)
        {
            bool backToLogin = false;
            while (!backToLogin)
            {
                Console.Clear();
                Console.WriteLine("===== Menu dành cho người mua hàng =====");
                Console.WriteLine("1. Tìm kiếm sản phẩm theo từ khóa");
                Console.WriteLine("2. Giỏ hàng của bạn");
                Console.WriteLine("3. Xem đơn hàng chưa xác nhận");
                Console.WriteLine("4. Đơn hàng đang chờ giao");
                Console.WriteLine("5. Đánh giá sản phẩm");
                Console.WriteLine("6. Thông tin người dùng");
                Console.WriteLine("7. Đăng xuất");
                Console.Write("Lựa chọn của bạn: ");

                string customerChoiceAfterLogin = Console.ReadLine();

                switch (customerChoiceAfterLogin)
                {
                    case "1":
                        Console.Write("Nhập từ khóa tìm kiếm sản phẩm: ");
                        string nameKey = Console.ReadLine();
                        List<Product> tempList = listFindingProduct.searchShopNameOrName(nameKey);
                        if (tempList.Count == 0)
                        {
                            Console.WriteLine("Hiện chưa có sản phẩm đăng bán !");
                            Console.WriteLine("Nhập phím bất kì để tiếp tục");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            foreach (Product myproduct in tempList)
                            {
                                myproduct.showProduct();
                            }
                        }
                        Console.WriteLine("Bạn có muốn mua sản phẩm nào không?");
                        string choiceToBuy = InputChecker.checkChoiceYesNo();
                        if (choiceToBuy == "yes")
                        {
                            placeOrder(tempList, ref accountCurrent, listFindingProduct);
                        }
                        Console.WriteLine("Nhập phím bất kỳ để quay lại !");
                        Console.ReadKey();
                        break;
                    case "2":
                        bool backToPreviousMenu = false;
                        while (!backToPreviousMenu)
                        {
                            Console.Clear();
                            Console.WriteLine("===== Menu xem giỏ hàng =====");
                            Console.WriteLine("1. Xem giỏ hàng đã thêm");
                            Console.WriteLine("2. Thanh toán giỏ hàng");
                            Console.WriteLine("3. Cập nhật số lượng sản phẩm trong giỏ hàng");
                            Console.WriteLine("4. Xóa sản phẩm trong giỏ hàng");
                            Console.WriteLine("5. Quay lại trang chủ");
                            Console.Write("Lựa chọn của bạn: ");

                            string customerChoiceAfterMenu = Console.ReadLine();

                            switch (customerChoiceAfterMenu)
                            {
                                case "1":
                                    accountCurrent.dataUser.showShoppingCart();
                                    Console.WriteLine($"{"Tổng giá trị giỏ hàng chưa thanh toán: ".PadRight(30)} {accountCurrent.dataUser.currentCart.totalPriceCart()} VND");
                                    Console.WriteLine("Nhập phím bất kì để tiếp tục");
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    if (accountCurrent.dataUser.currentCart.checkEmptyCart() == true)
                                    {
                                        Console.WriteLine("Giỏ hàng hiện tại chưa có sản phẩm nào được thêm !");
                                        break;
                                    }
                                    else
                                    {
                                        string method = InputChecker.checkMethodPayment();
                                        accountCurrent.dataUser.payment = new Payment(accountCurrent.dataUser.currentCart, method);
                                        Console.WriteLine(new string('=', 40));
                                        Console.WriteLine("Giỏ hàng thanh toán bao gồm các sản phẩm sau:");
                                        accountCurrent.dataUser.payment.printPayment(ref listVoucher);
                                        Console.WriteLine("Bạn có xác nhận thanh toán đơn hàng này ?");
                                        string mychoice = InputChecker.checkChoiceYesNo();
                                        if (mychoice == "yes")
                                        {
                                            Console.WriteLine("Đã xác nhận thanh toán! vui lòng chờ xác nhận");
                                            accountCurrent.dataUser.linkToSeller(userSeller, accountCurrent.dataUser);
                                            accountCurrent.dataUser.currentCart.clear();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Hủy thanh toán.");
                                        }
                                    }
                                    Console.WriteLine("Nhập phím bất kì để tiếp tục");
                                    Console.ReadKey();
                                    break;
                                case "3":
                                    if (accountCurrent.dataUser.currentCart.checkEmptyCart() == true)
                                    {
                                        Console.WriteLine("Giỏ hàng hiện tại chưa có sản phẩm nào được thêm !");
                                    }
                                    else
                                    {
                                        Console.Write("Nhập ID sản phẩm cần thay đổi số lượng ");
                                        string idFindingToUpdateQuantity = Console.ReadLine();
                                        if (accountCurrent.dataUser.currentCart.checkProductID(idFindingToUpdateQuantity))
                                        {
                                            string inputQuantity;
                                            int quantityUpdate;
                                            do
                                            {
                                                Console.Write($"Nhập số lượng sản phẩm cần cập nhật cho sản phẩm ID: {idFindingToUpdateQuantity}: ");
                                                inputQuantity = Console.ReadLine();

                                                // Kiểm tra đầu vào có phải số nguyên và lớn hơn 0
                                                if (!int.TryParse(inputQuantity, out quantityUpdate) || quantityUpdate <= 0)
                                                {
                                                    Console.WriteLine("Số lượng sản phẩm không hợp lệ! Vui lòng nhập lại số lượng sản phẩm.");
                                                }
                                            } while (!int.TryParse(inputQuantity, out quantityUpdate) || quantityUpdate <= 0);
                                            //accountCurrent.dataUser.currentCart.updateQuantityInCart(idFindingTemp, quantityUpdate);
                                            accountCurrent.dataUser.currentCart.removeFromCart(idFindingToUpdateQuantity);
                                            accountCurrent.dataUser.currentCart.addToCart(listFindingProduct.finding, idFindingToUpdateQuantity, quantityUpdate);
                                            Console.WriteLine("Đã cập nhật sản phẩm thành công!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Không tìm thấy sản phẩm. Vui lòng thử lại.");
                                        }
                                    }
                                    Console.WriteLine("Nhập phím bất kì để tiếp tục");
                                    Console.ReadKey();
                                    break;
                                case "4":
                                    if (accountCurrent.dataUser.currentCart.checkEmptyCart() == true)
                                    {
                                        Console.WriteLine("Giỏ hàng hiện tại chưa có sản phẩm nào được thêm !");
                                    }
                                    else
                                    {
                                        Console.Write("Nhập ID sản phẩm cần xóa: ");
                                        string idFinding = Console.ReadLine();
                                        if (accountCurrent.dataUser.currentCart.checkProductID(idFinding))
                                        {
                                            accountCurrent.dataUser.currentCart.removeFromCart(idFinding);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Không tìm thấy sản phẩm. Vui lòng thử lại.");
                                        }
                                    }
                                    Console.WriteLine("Nhập phím bất kì để tiếp tục");
                                    Console.ReadKey();
                                    break;
                                case "5":
                                    backToPreviousMenu = true;
                                    break;
                                default:
                                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để thử lại...");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        break;
                    case "3":
                        bool backToPreviousMenuCase3 = false;
                        while (!backToPreviousMenuCase3)
                        {
                            Console.Clear();
                            Console.WriteLine("===== Menu xem đơn hàng chờ xác nhận =====");
                            Console.WriteLine("1. Xem đơn hàng đang chờ xác nhận");
                            Console.WriteLine("2. Quay lại trang chủ");
                            Console.Write("Lựa chọn của bạn: ");

                            string customerChoiceAfterMenu = Console.ReadLine();

                            switch (customerChoiceAfterMenu)
                            {
                                case "1":
                                    if (accountCurrent.dataUser.payment.shoppingcart.size == 0)
                                    {
                                        Console.WriteLine("Chưa thanh toán đơn hàng nào !");
                                    }
                                    else
                                    {
                                        accountCurrent.dataUser.payment.printUnconfirmedOrder();
                                    }
                                    Console.WriteLine("Nhập phím bất kì để tiếp tục");
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    backToPreviousMenuCase3 = true;
                                    break;
                                default:
                                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để thử lại...");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        break;
                    case "4":
                        bool backToPreviousMenuCase4 = false;
                        while (!backToPreviousMenuCase4)
                        {
                            Console.Clear();
                            Console.WriteLine("===== Menu xem đơn hàng đang giao =====");
                            Console.WriteLine("1. Xem đơn hàng đang chờ giao hàng");
                            Console.WriteLine("2. Quay lại trang chủ");
                            Console.Write("Lựa chọn của bạn: ");

                            string customerChoiceAfterMenu = Console.ReadLine();

                            switch (customerChoiceAfterMenu)
                            {
                                case "1":
                                    Console.WriteLine("==== Sản phẩm đang giao ====");
                                    accountCurrent.dataUser.cartDelivered.showShoppingCart();
                                    Console.WriteLine("Nhập phím bất kì để tiếp tục");
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    backToPreviousMenuCase4 = true;
                                    break;
                                default:
                                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để thử lại...");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        break;
                    case "5":
                        bool backToPreviousMenuCase5 = false;
                        while (!backToPreviousMenuCase5)
                        {
                            Console.Clear();
                            Console.WriteLine("===== Đánh giá của khách hàng =====");
                            if (accountCurrent.dataUser.cartDelivered.size == 0)
                            {
                                Console.WriteLine("Hiện chưa có sản phẩm để đánh giá !");
                                backToPreviousMenuCase5 = true;
                            }
                            else
                            {
                                accountCurrent.dataUser.cartDelivered.showShoppingCart();
                                Console.WriteLine("Nhập ID sản phẩm đánh giá");
                                string IDString = Console.ReadLine();
                                if (accountCurrent.dataUser.cartDelivered.checkProductID(IDString))
                                {
                                    accountCurrent.dataUser.rateProduct(listFindingProduct, IDString);
                                    accountCurrent.dataUser.cartDelivered.removeFromCart(IDString);
                                }
                                else
                                {
                                    Console.WriteLine("Không tìm thấy ID !");
                                }
                                Console.WriteLine("Bạn có muốn đánh giá sản phẩm tiếp ?");
                                string mychoice = InputChecker.checkChoiceYesNo();
                                if (mychoice == "no")
                                {
                                    backToPreviousMenuCase5 = true;

                                }
                            }
                        }
                        Console.WriteLine("Nhấn phím bất kỳ để thoát...");
                        Console.ReadKey();
                        break;
                    case "6":
                        ManageAccountMenu(ref accountCurrent);
                        break;

                    case "7":
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
        static void login(ref UserInf userCustomer, findingList listFindingProduct, ref SellerInf userSeller, ref VoucherLinkedlist listVoucher)
        {
            string account;
            string password;
            do
            {
                Console.WriteLine("Tài khoản: ");
                account = Console.ReadLine();
                Console.WriteLine("Mật khẩu: ");
                password = Console.ReadLine();
                if (!userCustomer.checkAccount(account, password))
                {
                    Console.WriteLine("Tài khoản hoặc mật khẩu không đúng. Vui lòng nhập lại.");
                    Console.WriteLine("Bạn có muốn tiếp tục đăng nhập ? ");
                    string choice = InputChecker.checkChoiceYesNo();
                    if (choice == "no")
                    {
                        return;
                    }
                }
            }
            while (!userCustomer.checkAccount(account, password));
            nodeAccount accountCurrent = userCustomer.userFindingByAccount(account, password);
            Console.WriteLine("Đăng nhập thành công, nhấn nút bất kì để đi đến Menu");
            Console.ReadKey();
            customerMenu(ref accountCurrent, listFindingProduct, ref userSeller, ref listVoucher);
        }
        #endregion
        #region Register Static Function
        static void register(ref UserInf userCustomer)
        {
            userCustomer.register();
            Console.WriteLine("Đăng kí thành công! Nhấn nút bất kì để tiếp tục");
            Console.ReadKey();

        }
        #endregion
        #region placeOrder Static Function 
        static void placeOrder(List<Product> tempList, ref nodeAccount accountCurrent, findingList listFindingProduct)
        {
            Console.Clear();
            Console.WriteLine("=== Giao diện đặt Hàng ===");
            foreach (Product temp in tempList)
            {
                temp.showProduct();
            }
            string idBuying = "";
            bool mycheck = false;
            while (!mycheck)
            {
                Console.Write("Nhập ID sản phẩm cần mua: ");
                idBuying = Console.ReadLine();
                foreach (Product product in listFindingProduct.finding)
                {
                    if (product.productID == idBuying)
                    {
                        mycheck = true;
                    }
                }
                if (mycheck == false)
                {
                    Console.WriteLine("Không tìm thấy ID !, vui lòng nhập lại");
                }
            }

            Product tempProduct = new Product();
            foreach (var productTemp in listFindingProduct.finding)
            {
                if (productTemp.productID == idBuying)
                {
                    tempProduct = productTemp;
                }
            }
            string inputQuantity;
            int quantityBuying;
            do
            {
                Console.Write("Nhập số lượng sản phẩm cần mua: ");
                inputQuantity = Console.ReadLine();

                // Kiểm tra đầu vào có phải số nguyên và lớn hơn 0
                if (!int.TryParse(inputQuantity, out quantityBuying) || quantityBuying < 0 || quantityBuying + accountCurrent.dataUser.currentCart.getQuantityOfProduct(idBuying) > tempProduct.quantity || quantityBuying > tempProduct.quantity)
                {
                    Console.WriteLine("Số lượng sản phẩm không hợp lệ! Vui lòng nhập lại số lượng sản phẩm.");
                }

            } while (!int.TryParse(inputQuantity, out quantityBuying) || quantityBuying < 0 || quantityBuying + accountCurrent.dataUser.currentCart.getQuantityOfProduct(idBuying) > tempProduct.quantity || quantityBuying > tempProduct.quantity);
            if (quantityBuying == 0)
            {
                Console.WriteLine($"Bạn đã hủy thao tác mua sản phẩm ID {idBuying} vì nhập số lượng bằng 0.");
            }
            else if (accountCurrent.dataUser.currentCart.checkProductID(idBuying))
            {
                accountCurrent.dataUser.currentCart.updateQuantityInCart(idBuying, quantityBuying);
            }
            else
            {
                accountCurrent.dataUser.addToCart(listFindingProduct.finding, idBuying, quantityBuying);
                Console.WriteLine($"Sản phẩm {idBuying} đã được thêm vào giỏ hàng.");
            }

            Console.WriteLine("Bạn có muốn mua thêm sản phẩm nào không?");
            string choiceBuying = InputChecker.checkChoiceYesNo();
            if (choiceBuying == "yes")
            {
                placeOrder(tempList, ref accountCurrent, listFindingProduct);
            }

        }
        #endregion
        #region Change Information Account Static Function 
        static void ManageAccountMenu(ref nodeAccount accountCurrent)
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Quản Lý Tài Khoản ===");
                Console.WriteLine("1. Thông tin cá nhân");
                Console.WriteLine("2. Đổi mật khẩu");
                Console.WriteLine("3. Cập nhật thông tin cá nhân (Tên người dùng / Địa chỉ giao hàng)");
                Console.WriteLine("4. Quay lại menu Customer");
                Console.Write("Lựa chọn của bạn: ");

                string accountChoice = Console.ReadLine();

                switch (accountChoice)
                {
                    case "1":
                        accountCurrent.dataUser.showInfo();
                        break;
                    case "2":
                        accountCurrent.dataUser.changePassword();
                        break;
                    case "3":
                        accountCurrent.dataUser.updateName();
                        accountCurrent.dataUser.updateAddress();
                        break;
                    case "4":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }

                if (!back)
                {
                    Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }
        #endregion
    }
}