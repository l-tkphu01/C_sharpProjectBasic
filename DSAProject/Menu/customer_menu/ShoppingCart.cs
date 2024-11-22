using System;
using ProductClass;
namespace ShoppingCartClass
{
    // Khởi tạo node lưu các thông tin của một Product sản phẩm
    public class Node
    {
        public Product product;
        public Node next;

        public Node(Product product)
        {
            this.product = product;
            next = null;
        }
    }
    public struct ShoppingCart
    {
        public Node first;
        public Node last;
        public int size; // số lượng phần tử trong ShoppingCart

        public ShoppingCart()      // Hàm khởi tạo
        {
            first = null;
            last = null;
            size = 0;
        }
        // Hàm duyệt xem trong ShoppingCart có chứa sản phẩm có ID là productID hay không, nếu có trả về true, ngược lại false
        public bool checkProductID(string productID)
        {
            Node current = first;
            while (current != null)
            {
                if (current.product.productID == productID)
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }
        // Hàm duyệt xem trong ShoppingCart có chứa sản phẩm có ID là productID hay không, nếu có trả về số lượng sản phẩm đó, ngược lại trả về 0
        public int getQuantityOfProduct(string productID)
        {
            Node current = first;
            while (current != null)
            {
                if (current.product.productID == productID)
                {
                    return current.product.quantity;
                }
                current = current.next;
            }
            return 0;
        }

        public void addProduct(Product product) // Hàm thêm một Product mới vào ShoppingCart ( hàm phụ được gọi trong hàm addToCart )
        {
            Node newNode = new Node(product); // khởi tạo node mới chứa thông tin của product
            if (first == null)
            {
                first = newNode;
                last = newNode; // nếu Shoppingcart trống thì khởi tạo 1 node đầu first và 1 node cuối là node last 
            }
            else
            {
                last.next = newNode; // nếu Shoppingcart không trống thì add vào node cuối last
                last = newNode;
            }
            size++;
        }
        public void addToCart(List<Product> products, string idProduct, int quantity) // Hàm chính thêm sản phẩm vào giỏ hàng, sử dụng các hàm add khác để dễ dàng quản lí sửa lỗi
        {
            foreach (Product productTemp in products) // Truyền vào list product các sản phẩm có trong hệ thống của toàn bộ shop, kiểm tra sản phẩm cần add vào ShoppingCart có tồn tại hợp lệ không
            {
                if (productTemp.productID == idProduct) // nếu tìm thấy sản phẩm cần add có trong hệ thống toàn bộ sản phẩm sẽ thực hiện lệnh add
                {
                    Product newProduct = productTemp;
                    newProduct.setQuantity(quantity);
                    addProduct(newProduct);
                    return;
                }
            }
        }
        public void removeFromCart(string productID) // Hàm chính tìm sản phẩm trong ShoppingCart bằng id sản phẩm, nếu có sẽ thực hiện xóa ra khỏi ShoppingCart
        {
            if (first == null)
            {
                Console.WriteLine("Giỏ hàng trống.");
                return;
            }
            if (first.product.productID == productID) // xử lí nếu node cần xóa ở vị trí đầu
            {
                Node temp = first;     // vì Trong C#, bộ thu gom rác tự động quản lý và giải phóng bộ nhớ khi không còn tham chiếu đến một đối tượng. Khi chỉ sử dụng first = first.next;, node đầu tiên sẽ không còn được tham chiếu bởi first, vì vậy nó sẽ được bộ thu gom rác giải phóng, có thể gây ra lỗi chương trình
                first = first.next;
                temp = null;          // sử dụng biến temp để lưu con trỏ first, tránh việc bộ nhớ tự động xóa nhầm first khi không có địa chỉ trong ô nhớ lúc gán first = first.next
                size--;
                return;
            }

            Node current = first;
            while (current.next != null)
            {
                if (current.next.product.productID == productID)
                {
                    Node temp = current.next;
                    current.next = current.next.next;
                    temp = null;
                    size--;
                    Console.WriteLine($"Sản phẩm {productID} đã được xóa khỏi danh sách.");
                    return;
                }
                current = current.next;
            }

            Console.WriteLine($"Không tìm thấy sản phẩm {productID}.");
        }
        //cập nhật số lượng sản phẩm trong giỏ hàng
        public void updateQuantityInCart(string idFinding, int moreQuantity)
        {
            Node current = first;

            while (current != null)
            {
                if (current.product.productID == idFinding)
                {
                    current.product.quantity += moreQuantity;
                    return;
                }
                current = current.next;
            }
        }
        //tổng giá trị giỏ hàng
        public double totalPriceCart()
        {
            double total = 0;
            Node current = first;

            while (current != null)
            {
                total += current.product.price * current.product.quantity;
                current = current.next;
            }

            return total;
        }
        public bool checkEmptyCart()
        {
            if (first == null)
            {
                return true;
            }
            return false;
        }
        public void showShoppingCart()
        {
            if (checkEmptyCart())
            {
                Console.WriteLine("Giỏ hàng trống.");
                return;
            }

            Node current = first;
            while (current != null)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"{"ID sản phẩm:".PadRight(15)} {current.product.productID}");
                Console.WriteLine($"{"Tên sản phẩm:".PadRight(15)} {current.product.productName}");
                Console.WriteLine($"{"Giá:".PadRight(15)} {current.product.price} VND");
                Console.WriteLine($"{"Shop:".PadRight(15)} {current.product.shopName}");
                Console.WriteLine($"{"Số lượng:".PadRight(15)} {current.product.quantity}");
                Console.WriteLine(new string('-', 40));
                current = current.next;
            }
        }
        public void clear()
        {
            if (first == null)
            {
                return;
            }
            Node current = first;
            Node next;
            while (current != null)
            {
                next = current.next;
                current = null;
                current = next;
            }
            first = null;
            last = null;
            size = 0;
        }
    }
}
