using FindingProductClass;
using System;
using System.Collections.Generic;

namespace VoucherManageClass
{
    public struct Voucher
    {
        public string voucherCode;
        public int discountValue;

        public Voucher(string code, int discount)
        {
            voucherCode = code;
            discountValue = discount;
        }
    }

    public class VoucherNode
    {
        public Voucher voucherData;
        public VoucherNode next;

        public VoucherNode(Voucher voucher)
        {
            voucherData = voucher;
            next = null;
        }
    }

    public struct VoucherLinkedlist
    {
        public VoucherNode head;
        // Thêm vào danh sách nút
        public void addToList(Voucher voucher)
        {
            VoucherNode newVoucherNode = new VoucherNode(voucher);
            if (head == null)
            {
                head = newVoucherNode;
            }
            else
            {
                VoucherNode current = head;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = newVoucherNode;
            }
        }

        // Thêm voucher mới vào danh sách

        public void removeVoucher(string code)
        {
            if (head == null)
            {
                Console.WriteLine("Danh sách voucher rỗng.");
                return;
            }

            // Xử lý xóa các voucher ở đầu danh sách có mã trùng khớp
            while (head != null && head.voucherData.voucherCode.Equals(code, StringComparison.OrdinalIgnoreCase))
            {
                head = head.next;
                Console.WriteLine($"Đã xóa voucher {code} khỏi hệ thống sau khi áp dụng ");
            }

            if (head == null)
            {
                return;
            }

            // Duyệt qua danh sách để xóa các voucher còn lại có mã trùng khớp
            VoucherNode current = head;
            while (current.next != null)
            {
                if (current.next.voucherData.voucherCode.Equals(code, StringComparison.OrdinalIgnoreCase))
                {
                    current.next = current.next.next;
                    Console.WriteLine($"Đã xóa voucher: {code}");
                }
                else
                {
                    current = current.next;
                }
            }
        }

        // Tìm kiếm voucher bằng Code

        public VoucherNode findVoucherByCode(string code)
        {
            VoucherNode current = head;
            while (current != null)
            {
                if (current.voucherData.voucherCode.Equals(code, StringComparison.OrdinalIgnoreCase))
                {
                    return current;
                }
                current = current.next;
            }
            return null;
        }


        // In danh sách voucher
        public void printVouchers()
        {
            Console.WriteLine("Danh sách voucher của hệ thống");
            if (head == null)
            {
                Console.WriteLine("Danh sách rỗng.");
                return;
            }

            VoucherNode current = head;
            while (current != null)
            {
                Console.WriteLine($"Voucher Code: {current.voucherData.voucherCode}, Giảm giá: {current.voucherData.discountValue} %");
                current = current.next;
            }
        }
    }
}
