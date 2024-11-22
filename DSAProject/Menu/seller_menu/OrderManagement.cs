using System;
using ProductClass;
using OrderClass;
using UserInfClass;
using FindingProductClass;
using ProductSellerClass;
namespace OrderManagementClass
{
    public class Node
    {
        public Order data;
        public Node next;
        public Node(Order data)
        {
            this.data = data;
            this.next = null;
        }
    }
    public struct OrderQueue
    {
        Node head;
        Node tail;
        int count;
        public Order deQueueData;
        public OrderQueue()
        {
            this.head = null;
            this.tail = null;
            count = 0;
        }
        public bool IsEmpty()
        {
            return count == 0;
        }
        public int Count()
        {
            return count;
        }
        public String Peek()
        {
            if (IsEmpty())
            {
                return null;
            }
            return head.data.product.productID;
        }
        public void Enqueue(Order value)
        {
            Node newNode = new Node(value);

            if (IsEmpty())
            {
                head = tail = newNode;
            }
            else
            {
                tail.next = newNode;
                tail = newNode;
            }
            count++;
        }
        public void Dequeue()
        {
            if (IsEmpty())
            {
                return;
            }
            deQueueData = head.data;
            head = head.next;

            if (head == null)
            {
                tail = null;
            }
            count--;
        }
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void show()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Danh sách đơn hàng trống !");
                return;
            }
            Node current = head;
            while (current != null)
            {
                Console.WriteLine($"--- Tên khách hàng ---: {current.data.Customer.name}");
                Console.WriteLine($"--- Địa chỉ ----------: {current.data.Customer.address}");
                Console.WriteLine($"--- SDT --------------: {current.data.Customer.phone}");
                current.data.product.showProduct();
                Console.WriteLine("#################################");
                current = current.next;
            }
        }
        // cập nhật thông tin cho người mua sau khi xác nhận
        public void updateForBuyer(UserInf userCurrent, string myID)
        {
            Node currentOrder = head;
            nodeAccount nodeCurrent = userCurrent.head;
            while (currentOrder != null)
            {
                if (currentOrder.data.product.productID.Equals(myID))
                {
                    while (nodeCurrent != null)
                    {
                        if (currentOrder.data.Customer.account.Equals(nodeCurrent.dataUser.account))
                        {
                            nodeCurrent.dataUser.cartDelivered.addProduct(currentOrder.data.product);
                            nodeCurrent.dataUser.payment.shoppingcart.removeFromCart(currentOrder.data.product.productID);
                            break;
                        }
                        nodeCurrent = nodeCurrent.next;
                    }
                    break;
                }
                currentOrder = currentOrder.next;
            }
        }
        //cập nhật thông tin cho danh mục tìm kiếm sau khi xác nhận
        public void updateForFindingList(findingList list, string myID)
        {
            Node currentOrder = head;
            while (currentOrder != null)
            {
                if (currentOrder.data.product.productID.Equals(myID))
                {
                    for (int i = 0; i < list.finding.Count; i++)
                    {
                        if (currentOrder.data.product.productID.Equals(list.finding[i].productID))
                        {
                            Product productTemp = list.finding[i];
                            productTemp.quantity -= currentOrder.data.product.quantity;
                            productTemp.buyCount += currentOrder.data.product.quantity;
                            list.finding[i] = productTemp;
                            break;
                        }
                    }
                    break;
                }
                currentOrder = currentOrder.next;
            }
        }
        //cập nhật thông tin cho danh sách sản phẩm cửa hàng sau khi xác nhận
        public void updateForProductList(ProductSellerLinkedList list, string myID)
        {
            Node currentOrder = head;
            ProductSellerClass.Node listNode = list.getHead();
            while (currentOrder != null)
            {
                if (currentOrder.data.product.productID.Equals(myID))
                {
                    while (listNode != null)
                    {
                        if (currentOrder.data.product.productID.Equals(listNode.Data.productID))
                        {
                            listNode.Data.quantity -= currentOrder.data.product.quantity;
                            listNode.Data.buyCount += currentOrder.data.product.quantity;
                            break;
                        }
                        listNode = listNode.Next;
                    }
                    break;
                }
                currentOrder = currentOrder.next;
            }
        }
    }
}
