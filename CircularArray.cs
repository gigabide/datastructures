    public class CircularArray<T> : IEnumerable
    {
        private T[] array;
        private int front;
        private int rear;
        private int capacity;

        //Constructor
        public CircularArray(int size)
        {
            capacity = size;
            array = new T[size + 1];
            front = 0;
            rear = 0;
        }

        public void addBack(T value) //Enqueue
        {
            if (isFull())
            {
                grow(array.Length * 2);
            }
            else if(isEmpty())
                front++;
            rear = (rear + 1) % array.Length;
            array[rear] = value;
        }
        public T removeFront() //Dequeue
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
                front = (front + 1) % array.Length;
            array[front] = default(T);
            return temp;
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
            Console.Write("Queue:");
            for (int i = 0; i < array.Length; i++)
            {
                var temp = array[i];
                Console.Write(array[i] + " ");
            }
            Console.WriteLine("");
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
            foreach(T item in array)
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
                if(rear >= front)
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