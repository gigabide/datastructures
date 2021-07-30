    public class GenericArray<T>
    {
        // The array
        private T[] array;
        private int size;

        // Constructor
        public GenericArray(int arrsize)
        {
            size = arrsize;
            array = new T[size + 1];
        }
        // Array methods
        public T getItem(int index)
        {
            return array[index];
        }
        public int getCount()
        {
            int count = 0;

            for(int i = 0; i < array.Length; ++i)
            {
                if(array[i] != null)
                    count++;
            }
            size = count;
            return count;
        }
        public void setItem(int index, T value)
        {
            if (index >= array.Length)
                Grow(array.Length * 2);
            array[index] = value;
            size++;
        }
        public void Swap(int pos1, int pos2)
        {
            T temp = array[pos1];
            setItem(pos1, array[pos2]);
            setItem(pos2, temp);
        }
        public void addFront(T value)
        {
            //Grow(array.Length * 2);
            size++;
            for (int i = array.Length-1; i > 0; --i)
            {
                array[i] = array[i - 1];
            }
            array[0] = value;
        }
        public void addLast(T value)
        {
            setItem(this.getCount(), value);
        }
        public void deleteFirst()
        {
            array[0] = default(T);
            for (int i = 0; i < getCount()-1; ++i)
            {
                array[i] = array[i + 1];
            }
            array[getCount()-1] = default(T);
        }
        public void deleteLast()
        {
            int i = this.getCount()-1;
            array[i] = default(T);
            size--;
        }
        public void deleteAll()
        {
            for(int i=0; i<getCount(); ++i)
            {
                array[i] = default(T);
            }
        }
        public void insertBefore(T value, int position)
        {
            size++;
            for (int i = array.Length - 1; i > position; --i)
            {
                array[i] = array[i - 1];
            }
            setItem(position, value);
        }
        public void rotateRight()
        {
            T temp = array[getCount() - 1];
            for (int i = array.Length-1; i > 0; --i)
            {
                array[i] = array[i - 1];
            }
            array[0] = temp;
        }
        public void rotateLeft()
        {
            T temp = array[0];
            for(int i = 0; i < getCount()-1; ++i)
            {
                array[i] = array[i + 1];
            }
            array[size - 1] = temp;
        }
        public string stringPrintForward()
        {
            string value = "";

            if (array[0] == null)
            {
                value = "There are no elements in this array.";
            }
            else
            {
                for (int i = 0; i < getCount(); ++i)
                {
                    value += array[i].ToString();
                    value += "\n";
                }                
            }
            return value;
        }
        public string stringPrintBackward()
        {
            string value = "";
            for (int i = getCount()-1; i > -1; --i)
            {
                value += array[i].ToString();
                value += "\n";
            }
            return value;
        }
        public void inPlaceSort()
        {
            Array.Sort(array);
        }
        public GenericArray<T> Merge(GenericArray<T> other)
        {
            GenericArray<T> result = new GenericArray<T>(getCount() + other.getCount());
            for (int i = 0; i < getCount(); ++i)
            {
                result.setItem(i, array[i]);
            }
            for(int i=0; i<other.size; ++i)
            {
                result.setItem(i+size, other.array[i]);
            }
            return result;
        }
        public void Grow(int newsize)
        {
            Array.Resize(ref array, newsize);
        }
    }