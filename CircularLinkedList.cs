using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Assignment3
{
    public class SimpleObject : IComparable<SimpleObject>
    {
        Random rnum = new Random();
        public ulong ID;
        public double[] array = new double[8191];
        public int priority;

        public override string ToString()
        {
            return "ID: " + ID + " Priority: " + priority;
        }

        public int CompareTo(SimpleObject other)
        {
            if (this == null && other == null) { return 0; }
            if (this == null) { return -1; }
            if (other == null) { return 1; }
            return priority.CompareTo(other.priority);
        }
    }
    public class Node
    {
        public SimpleObject data;
        public Node prev;
        public Node next;

        public Node() { }
    }
    class CircularLinkedList
    {
        private Node head;
        private Node tail;
        private Node current;

        public void enqueue(SimpleObject data)
        {
            if (head == null)
            {
                head = new Node();
                head.data = data;
                head.next = null;
                tail = head;
            }
            else if(data.priority > head.data.priority)
            {
                Node toAdd = new Node();
                toAdd.data = data;
                toAdd.prev = null;
                toAdd.next = head;
                if (head != null)
                    head.prev = toAdd;
                head = toAdd;
            }
            else
            {
                current = head;
                while (current.next != null && current.next.data.priority > data.priority)
                {
                    current = current.next;
                }
                if (current == tail)
                    addLast(data);
                else
                {
                    Node toAdd = new Node();
                    toAdd.data = data;
                    toAdd.prev = current.prev;
                    toAdd.next = current.next;
                    current.next = toAdd;
                }
                
            }
        }
        public void addLast(SimpleObject data)
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
        public void dequeue()
        {
            head = head.next;
        }
        public void printAllNodes()
        {
            Node current = head;
            Console.WriteLine("List Queue:");
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
                n = size - Math.Abs(n);
                //Reset current to head.
                current = head;
            }
            else
            {
                n = Math.Abs(n);
            }

            int count = 1;
            while (count < n && current != null)
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
        public void deleteAll()
        {
            current = head;
            while (current != null)
            {
                dequeue();
                current = current.next;
            }
        }
    }
    public class CircularArray<T> : IEnumerable
    {
        public T[] array;
        private int front;
        public int rear;
        private int capacity;

        //Constructor
        public CircularArray()
        {
            capacity = 20;
            array = new T[20];
            front = -1;
            rear = 0;
        }
        public CircularArray(int size)
        {
            capacity = size;
            array = new T[size + 1];
            front = -1;
            rear = 0;
        }

        public void enqueue(T value) //Enqueue
        {
            if (isFull())
            {
                grow(array.Length * 2);
            }
            else if (isEmpty())
                front++;
            rear = (rear + 1) % array.Length;
            array[rear] = value;
            Array.Sort(array);
            Array.Reverse(array);
        }
        public T dequeue() //Dequeue
        {
            if (isEmpty())
            {
                Console.WriteLine("Cannot dequeue. Queue is empty.");
                return default(T);
            }
            T temp = array[front];
            if (front == rear)
                front = rear = -1;
            else
            {
                array[front] = default(T);
                front = (front + 1) % array.Length;
            }
            return temp;
        }
        public void deleteAll()
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i]!=null)
                    dequeue();
            }
        }
        public T peek() // Returns front element.
        {
            if (isEmpty()) //If it is empty
            {
                Console.WriteLine("Cannot peek. Queue is empty.");
                return default(T);
            }
            return array[front];
        }
        public bool isEmpty()
        {
            return front == -1;
        }
        public bool isFull()
        {
            return (rear + 1) % array.Length == front;
        }
        public void Print()
        {
            Console.WriteLine("Array Queue:");
            for (int i = 0; i < rear; i++)
            {
                var temp = array[i];
                Console.Write(array[i] + " \n");
            }
            Console.WriteLine("");
        }
        public int Length()
        {
            return array.Length - 1;
        }
        public void grow(int newsize)
        {
            T[] tempArray = new T[newsize];
            int i = 0;
            int j = front;

            do
            {
                tempArray[i++] = array[j];
                j = (j + 1) % array.Length;
            } while (j != front);

            front = 0;
            rear = array.Length - 1;
            array = tempArray;
        }
        public override string ToString()
        {
            string val = "";
            foreach (T item in array)
            {
                val += item;
                val += " ";
            }
            return val;
        }
        public IEnumerator GetEnumerator()
        {
            if (!isEmpty())
            {
                if (rear >= front)
                {
                    for (int i = front; i <= rear; ++i)
                    {
                        yield return array[i];
                    }
                }
                else
                {
                    for (int i = front; i <= capacity; ++i)
                    {
                        yield return array[i];
                    }

                    for (int i = 0; i <= rear; i++)
                    {
                        yield return array[i];
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CircularArray<SimpleObject> array = new CircularArray<SimpleObject>();
            CircularLinkedList list = new CircularLinkedList();

            Random rnum = new Random(1);
            for(int i = 0; i < 20; ++i)
            {
                SimpleObject obj = new SimpleObject();
                obj.ID = Convert.ToUInt64(rnum.Next(0,1000));
                obj.priority = rnum.Next(0,20);
                list.enqueue(obj);
                array.enqueue(obj);
            }
            list.deleteAll();
            array.Print();
            list.printAllNodes();
            Console.ReadLine();
        }
    }


}
