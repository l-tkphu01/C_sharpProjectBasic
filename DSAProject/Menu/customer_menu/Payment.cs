using System;
using ShoppingCartClass;
using CheckTrueInput;
using VoucherManageClass;
namespace PaymentMethodClass
{
    public struct Payment
    {
        public ShoppingCart shoppingcart;
        public string method;
        public double sumOfPayment;
        public Payment(ShoppingCart shoppingcart, string method)
        {
            this.shoppingcart = shoppingcart;
            this.method = method;
            sumOfPayment = 0;
        }
        public double callTotalPrice()
        {
            return shoppingcart.totalPriceCart();
        }
        //chọn phương thức thanh toán
        public void choosePayment()
        {
            if (shoppingcart.first == null)
            {
                Console.WriteLine("Hiện tại giỏ hàng của bạn chưa có sản phẩm nào!");
                Console.WriteLine("Nhập phím bất kì để tiếp tục");
                Console.ReadKey();
                return;
            }
            while (true)
            {
                if (method.ToLower() == "momo")
                {
                    Console.WriteLine("Phương thức thanh toán qua ví điện tử Momo");
                    Console.WriteLine($"Tổng giá trị thanh toán: {this.sumOfPayment} đ");
                    break;
                }
                else if (method.ToLower() == "atm")
                {
                    Console.WriteLine("Phương thức thanh toán qua Ngân Hàng");
                    Console.WriteLine($"Tổng giá trị thanh toán: {this.sumOfPayment} đ");
                    break;
                }
                else
                {
                    Console.WriteLine("Phương thức thanh toán không phù hợp, vui lòng nhập lại!");
                }
            }
        }
        public void addVoucherToPayment(ref VoucherLinkedlist listVoucher)
        {
            if (shoppingcart.first == null)
            {
                Console.WriteLine("Giỏ hàng của bạn trống, không thể áp dụng voucher.");
                return;
            }
            listVoucher.printVouchers();
            Console.WriteLine("Bạn có muốn sử dụng Voucher hệ thống (Chỉ được chọn một) ? ");
            string voucherChoice = InputChecker.checkChoiceYesNo();

            if (voucherChoice == "yes")
            {
                Console.Write("Nhập mã Voucher: ");
                string voucherCode = Console.ReadLine();
                VoucherNode applyVoucher = listVoucher.findVoucherByCode(voucherCode);

                if (applyVoucher != null)
                {
                    // Tính toán giảm giá 
                    double discountAmount = (applyVoucher.voucherData.discountValue * this.sumOfPayment) / 100;
                    this.sumOfPayment -= discountAmount;
                    Console.WriteLine($"Đã áp dụng voucher {voucherCode} thành công, số tiền giảm giá: {discountAmount} VND ");
                    Console.WriteLine($"Giá trị đơn hàng mới: {this.sumOfPayment} đ");

                    // Xóa voucher sau khi sử dụng
                    listVoucher.removeVoucher(voucherCode);
                }
                else
                {
                    Console.WriteLine($"Không tìm thấy voucher {voucherCode}");
                }
            }
        }
        //thanh toán sản phẩm
        public void printPayment(ref VoucherLinkedlist listVoucher)
        {
            // Cập nhật lại giá trị sumOfPayment
            this.sumOfPayment = callTotalPrice();
            shoppingcart.showShoppingCart();

            Console.WriteLine(new string('-', 40));
            choosePayment();  // Hiển thị phương thức thanh toán
            addVoucherToPayment(ref listVoucher);  // Áp dụng voucher nếu có
            Console.WriteLine(new string('-', 40));

            // In ra giá trị thanh toán cuối cùng
            Console.WriteLine($"Tổng giá trị thanh toán giỏ hàng của bạn: {this.sumOfPayment} VND");
        }
        //in đơn hàng chưa xác nhận
        public void printUnconfirmedOrder()
        {
            // Cập nhật lại giá trị sumOfPayment
            shoppingcart.showShoppingCart();

            Console.WriteLine(new string('-', 40));
            choosePayment();  // Hiển thị phương thức thanh toán
            Console.WriteLine(new string('-', 40));

            // In ra giá trị thanh toán cuối cùng
            Console.WriteLine($"Tổng giá trị thanh toán giỏ hàng của bạn: {this.sumOfPayment} VND");
        }
    }
}
