using System;
namespace ProductClass
{
    // Khai báo kiểu struct cho Product bao gồm các thông tin cần thiết cho một sản phẩm
    public struct Product
    {
        public String productID;
        public String productName;
        public double price;
        public int quantity;
        public String description;
        public String shopName;
        public double rate;
        public int buyCount;
        public int sumRate;
        public int turn;

        // Hàm khởi tạo Product với các thông số giá trị
        public Product(String productID, String productName, double price, int quantity, String description, String shopName)
        {
            this.productID = productID;
            this.productName = productName;
            this.price = price;
            this.quantity = quantity;
            this.description = description;
            this.shopName = shopName;
            this.rate = 0;
            this.buyCount = 0;
            this.sumRate = 0;
            this.turn = 0;
        }

        // Hàm set Id sản phẩm 
        public void setProductID(String productID)
        {
            this.productID = productID;
        }
        // Hàm set tên sản phẩm 
        public void setProductName(String productName)
        {
            this.productName = productName;
        }
        // Hàm set giá tiền của sản phẩm 
        public void setPrice(double price)
        {
            this.price = price;
        }
        // Hàm set số lượng của một sản phẩm 
        public void setQuantity(int quantity)
        {
            this.quantity = quantity;
        }
        // Hàm set mô tả của một sản phẩm 
        public void setDescription(String description)
        {
            this.description = description;
        }
        // Hàm set Shop bán sản phẩm product 
        public void setShopName(String shopName)
        {
            this.shopName = shopName;
        }
        // Hàm tính tổng đánh giá cho sản phẩm ( Sau khi mua mới cho phép gọi, tức là lượt mua buyCount luôn > 0, vì vậy không có ngoại lệ chia cho số 0 )
        public void setRate(int rateValueFromBuyer)
        {
            this.sumRate += rateValueFromBuyer;
            this.rate = this.sumRate / this.turn;
        }
        //Hàm hiển thị thông tin của một sản phẩm
        public void showProduct()
        {
            Console.WriteLine(new string('*', 40));
            Console.WriteLine($"{"ID sản phẩm:".PadRight(15)} {this.productID}");
            Console.WriteLine($"{"Tên sản phẩm:".PadRight(15)} {this.productName}");
            Console.WriteLine($"{"Giá:".PadRight(15)} {this.price}VND");
            Console.WriteLine($"{"Shop:".PadRight(15)} {this.shopName}");
            Console.WriteLine($"{"Số lượng:".PadRight(15)} {this.quantity}");
            Console.WriteLine($"{"Mô tả:".PadRight(15)} {this.description}");
            Console.WriteLine($"{"Đánh giá:".PadRight(15)} {Math.Round(this.rate, 1)}★");
        }
    }
}
