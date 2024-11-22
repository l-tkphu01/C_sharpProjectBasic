using System;
using System.Text;
using CustomerMenuClass;
using SellerClass;
using SellerMenuClass;
using UserInfClass;
using FindingProductClass;
using ProductClass;
using VoucherManageClass;
namespace MainProgram
{
    struct Program
    {
        public static void Main(string[] args)
        {
            // Dòng này có tác dụng in ra tiếng Việt ở console không bị lỗi font
            Console.OutputEncoding = Encoding.UTF8;
            //biến toàn cục của dự án
            UserInf userCustomer = new UserInf(); //  Khởi tạo danh sách tài khoản của User
            SellerInf userSeller = new SellerInf();//  Khởi tạo danh sách tài khoản của Seller
            findingList myList = new findingList(); // Khởi tạo package các hàm tìm kiếm sản phẩm
            VoucherLinkedlist listVoucher = new VoucherLinkedlist();
            Voucher newVoucher1 = new Voucher("GIFTVOUCHER10%", 10);
            Voucher newVoucher2 = new Voucher("GIFTVOUCHER20", 20);
            Voucher newVoucher3 = new Voucher("GIFTVOUCHER30", 30);
            Voucher newVoucher4 = new Voucher("GIFTVOUCHER40", 40);
            Voucher newVoucher5 = new Voucher("GIFTVOUCHER50", 50);
            Voucher newVoucher6 = new Voucher("GIFTVOUCHER60", 60);
            Voucher newVoucher7 = new Voucher("GIFTVOUCHER70", 70);
            listVoucher.addToList(newVoucher1);
            listVoucher.addToList(newVoucher2);
            listVoucher.addToList(newVoucher3);
            listVoucher.addToList(newVoucher4);
            listVoucher.addToList(newVoucher5);
            listVoucher.addToList(newVoucher6);
            listVoucher.addToList(newVoucher7);
            bool exitProgram = false;
            while (!exitProgram)
            {
                Console.Clear();
                Console.WriteLine("========== Mô Phỏng Ứng Dụng Bán Hàng Online ==========");
                Console.WriteLine(" -  Chọn vai trò của bạn  - ");
                Console.WriteLine("1. Shop bán hàng");
                Console.WriteLine("2. Người mua hàng");
                Console.WriteLine("3. Thoát chương trình");
                string roleChoice = Console.ReadLine();

                switch (roleChoice)
                {
                    case "1":
                        sellerMenuProgram.loginSeller(ref userSeller, myList, ref userCustomer); // GỌI HÀM ĐĂNG NHẬP CHO SELLER MENU
                        break;
                    case "2":
                        CustomerMenuProgram.loginCustomer(ref userCustomer, myList, ref userSeller, ref listVoucher); // GỌI HÀM ĐĂNG NHẬP CHO CUSTOMER MENU
                        break;
                    case "3":
                        exitProgram = true;
                        Console.WriteLine("Đang thoát chương trình. Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để thử lại...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
