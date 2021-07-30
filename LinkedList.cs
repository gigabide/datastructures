    public class Node
    {
        public object data;
        public Node next;
        public Node prev;

        //public Node(object data)
        //{
        //    prev = null;
        //    next = null;
        //    this.data = data;
        //}
        public void Print()
        {
            Console.WriteLine("|{0}|->", data);
        }
    }

    public class LinkedList
    {
        private Node head;
        private Node tail;
        private Node current;

        public void printAllNodes()
        {
            Node current = head;
            while (current != null)
            {
                Console.WriteLine(current.data);
                current = current.next;
            }//end while current is not null
        }
        public void addFirst(object data)
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
                toAdd.next = head;
                toAdd.prev = null;
                head = toAdd;
                head.prev = toAdd;
            }
        }
        public int getCount()
        {
            int count = 1;
            current = head;
            while (current.next != null)
            {
                current = current.next;
                count++;
            }
            return count;
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

                current = head;
                while(current.next != null)
                {
                    current = current.next;
                }
                current.next = toAdd;
            }
        }
        public void insertRandom(object data)
        {
            Random r = new Random();
            Node toAdd = new Node();
            toAdd.data = data;
            
            int pos = r.Next(0, getCount());
            int count = 0;

            current = head;
            while (current.next != null)
            {
                current = current.next;
                count++;
                if (count == pos)
                {
                    toAdd.next = current.next; //links the new node to the next one
                    current.next = toAdd;
                }
            }
        }
        public void deleteFirst()
        {
            Node temp = head;
            if (temp != null)
            {
                head = temp.next;
                head.prev = null;
                return;
            }
            while(temp != null)
            {
                temp.prev = temp;
                temp = temp.next;
            }
            if (temp == null)
            {
                return;
            }
            if(temp.next != null)
            {
                temp.next.prev = temp.prev;
            }
            if (temp.prev != null)
            {
                temp.prev.next = temp.next;
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
            temp.next = null;
        }
        public void rotateLeft()
        {
            Node toAdd = head;
            current = head;
            deleteFirst();
            while (current.next != null)
            {
                current = current.next;
            }
            toAdd.next = current.next;
            current.next = toAdd;
        }
        public void rotateRight()
        {
            Node toAdd = head;
            current = head;
            while (current.next != null)
            {
                current = current.next;
            }
            toAdd = current;
            deleteLast();
            toAdd.next = head;
            head = toAdd;
        }
        //public void deleteAll()
        //{
        //    current = head;
        //    while (current.next != null)
        //    {
        //        deleteLast();
        //    }
        //    deleteFirst();
        //}
        public void Merge(LinkedList list)
        {
            list.current = list.head;
            while(list.current.next != null)
            {
                addLast(list.current.data);
                list.current = list.current.next;
            }
            addLast(list.current.data);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GenericArray<Animal> array1 = new GenericArray<Animal>(19);
            GenerateAnimals(array1, 20);
            LinkedList list = new LinkedList();
            fillList(array1, list);
            list.rotateRight();
            list.printAllNodes();

            //Console.WriteLine("There are {0} nodes in this LinkedList.", list.getCount());
            Console.ReadLine();
        }
        public static void fillList(GenericArray<Animal> array, LinkedList list)
        {
            for (int i = 0; i < array.getCount(); ++i)
            {
                list.addLast(array.getItem(i));
            }
        }

        }
        static string NameGen(int line, object animal)
        {
            string[] names;
            char[] charsToTrim = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ' ' };

            if (animal is Cat)
            {
                names = File.ReadAllLines("catnames.txt");

                for (int i = 0; i < names.Length; ++i)
                {
                    names[i] = names[i].Trim(charsToTrim);
                }
            }
            else
            {
                names = File.ReadAllLines("snakenames.txt");
            }
            return names[line];
        }
    }