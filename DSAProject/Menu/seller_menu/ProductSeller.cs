using System;
using FindingProductClass;
using ProductClass;

namespace ProductSellerClass
{
    public class Node
    {
        public Product Data;
        public Node Next;

        public Node(Product data)
        {
            Data = data;
            Next = null;
        }
    }
    public struct ProductSellerLinkedList
    {
        private Node head;
        private Node tail;
        private int size;

        public ProductSellerLinkedList()
        {
            this.head = null;
            this.tail = null;
            this.size = 0;
        }
        public Node getHead()
        {
            return head;
        }
        public int getSize()
        {
            return size;
        }
        public void AddFirst(Product product)
        {
            Node newNode = new Node(product);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head = newNode;
            }
            size++;
        }
        public void Remove(string productID)
        {
            if (head == null)
            {
                Console.WriteLine("Danh sách sản phẩm rỗng!");
                return;
            }

            Node current = head;
            Node previous = null;

            while (current != null)
            {
                if (current.Data.productID == productID)
                {
                    if (previous == null)
                    {
                        head = current.Next;
                        if (head == null)
                        {
                            tail = null;
                        }
                    }
                    else
                    {
                        previous.Next = current.Next;
                        if (previous.Next == null)
                        {
                            tail = previous;
                        }
                    }

                    size--;
                    Console.WriteLine($"Đã xóa sản phẩm: {current.Data.productName}");
                    return;
                }

                previous = current;
                current = current.Next;
            }

            Console.WriteLine("Sản phẩm không tồn tại trong danh sách!");
        }
        //kiểm tra sản phẩm tồn tại bằng ID
        public bool findProduct(string productID)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.productID == productID)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void Update(string oldID, findingList findinglist, string shopName)
        {
            bool checkPrice = false;
            bool checkQuantity = false;
            Node current = head;
            while (current != null)
            {
                if (current.Data.productID == oldID)
                {
                    findinglist.remove(oldID);
                    Console.WriteLine("ID mới sản phẩm: ");
                    string newproductID = Console.ReadLine();
                    newproductID += "-" + shopName;
                    current.Data.productID = newproductID;
                    Console.WriteLine("Tên mới sản phẩm: ");
                    string newproductName = Console.ReadLine();
                    current.Data.productName = newproductName;
                    while (!checkPrice)
                    {
                        try
                        {
                            Console.WriteLine("Giá mới: ");
                            double newprice = double.Parse(Console.ReadLine());
                            current.Data.price = newprice;
                            checkPrice = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Lỗi: Vui lòng nhập đúng định dạng cho giá");
                        }
                    }
                    while (!checkQuantity)
                    {
                        try
                        {
                            Console.WriteLine("Số lượng: ");
                            int quantity = int.Parse(Console.ReadLine());
                            current.Data.quantity = quantity;
                            checkQuantity = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Lỗi: Vui lòng nhập đúng định dạng cho số lượng");
                        }
                    }
                    Console.WriteLine("mô tả: ");
                    String description = Console.ReadLine();
                    current.Data.description = description;
                    findinglist.add(current.Data);
                }
                current = current.Next;
            }
        }
        //update rate đánh giá
        public void updateRateFromFindingList(findingList listFindingProduct)
        {
            if (head == null)
            {
                return;
            }
            Node current = head;
            List<Product> products = listFindingProduct.searchShopName(current.Data.shopName);
            while (current != null)
            {
                foreach (Product product in products)
                {
                    if (current.Data.productID.Equals(product.productID))
                    {
                        current.Data.rate = product.rate;
                        break;
                    }
                }
                current = current.Next;
            }
        }

        public void ShowProducts()
        {
            Node current = head;
            if (current == null)
            {
                Console.WriteLine("Danh sách sản phẩm rỗng!");
                return;
            }

            Console.WriteLine("Danh sách sản phẩm:");
            while (current != null)
            {
                current.Data.showProduct();
                current = current.Next;
            }
        }
    }
}
