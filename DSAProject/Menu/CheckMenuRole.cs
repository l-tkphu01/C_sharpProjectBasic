using System;

namespace CheckTrueInput
{
    public struct InputChecker
    {
        public static string checkChoiceYesNo()
        {
            string choose;
            while (true)
            {
                Console.Write("Lựa chọn của bạn (YES/NO): ");
                choose = Console.ReadLine();
                if (choose.ToLower() == "yes" || choose.ToLower() == "no")
                {
                    return choose.ToLower();
                }
                else
                {
                    Console.WriteLine("Phương thức nhập không hợp lệ, vui lòng nhập lại !");
                }
            }
        }
        public static string checkMethodPayment()
        {
            string choose;
            while (true)
            {
                Console.Write("Bạn muốn thanh toán qua phương thức nào (Momo/ATM): ");
                choose = Console.ReadLine();
                if (choose.ToLower() == "momo" || choose.ToLower() == "atm")
                {
                    return choose.ToLower();
                }
                else
                {
                    Console.WriteLine("Phương thức nhập không hợp lệ, vui lòng nhập lại !");
                }
            }
        }
    }
}
