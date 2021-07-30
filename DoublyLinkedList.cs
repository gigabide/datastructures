    public class Node
    {
        public object data;
        public Node prev;
        public Node next;
    }
    class DLinkedList
    {
        private Node head;
        private Node tail;
        private Node current;

        public void addFirst(object data)
        {
            if (head == null)
            {
                head = new Node();
                head.data = data;
                head.next = null;
                tail = head;
            }
            else
            {
                Node toAdd = new Node();
                toAdd.data = data;
                toAdd.prev = null;
                toAdd.next = head;
                if (head != null)
                    head.prev = toAdd;
                head = toAdd;
            }
        }
        public void addLast(object data)
        {
            
            if (head == null)
            {
                head = new Node();
                head.data = data;
                head.next = null;
            }
            else
            {
                Node toAdd = new Node();
                toAdd.data = data;

                current = tail;
                current.next = toAdd;
                tail = current.next;
            }
        }
        public void deleteLast()
        {
            Node temp = head;

            if (temp == null)
            {
                return;
            }
            while (temp.next.next != null)
            {
                temp = temp.next;
            }
            tail = temp;
            temp.next = null;
        }
        public void printAllNodes()
        {
            Node current = head;
            while (current != null)
            {
                Console.WriteLine(current.data);
                current = current.next;
            }
        }
        public void rotate(int n)
        {
            if (n == 0)
                return; // Don't rotate 0 places.
            Node current = head;

            if (n > 0)
            {
                int size = 1;
                // Get list size
                while (current.next != null)
                {
                    size++;
                    current = current.next;
                }
                // Counter-clockwise rotation of size-n is a clockwise rotation of n.
                n=size-Math.Abs(n);
                //Reset current to head.
                current = head;
            }
            else
            {
                n = Math.Abs(n);
            }

            int count = 1;
            while(count < n && current != null)
            {
                // Loop to the nth node.
                current = current.next;
                count++;
            }

            // If current is null, n is too large.
            if (current == null)
                return;

            Node nthNode = current;
            while (current.next != null)
            {
                // Loop to last node.
                current = current.next;
            }

            //Add original head after last node.
            current.next = head;
            head.prev = current;

            // Position nth node after the head.
            head = nthNode.next;
            head.prev = null;
            nthNode.next = null;
        }

        public void secondSwap()
        {
            //Get size and set tail.
            int size = 0;
            current = head;
            while (current.next != null)
            {
                current = current.next;
                size++;
            }
            if (size <= 3)
                return;
            tail = current;
            current = head;

            Node temp = new Node();
            temp.data = head.next.data;
            head.next.data = tail.prev.data;
            tail.prev.data = temp.data;
        }
    }