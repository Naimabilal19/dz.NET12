using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleApp11
{
    public class Book : IComparable, ICloneable
    {
        public string name { get; set; }
        public string author { get; set; }
        public Book(string name, string author)
        {
            this.name = name;
            this.author = author;
        }
        public Book() : this("Дом в котором", "Марьям Петросян") { }
        public void Show()
        {
            WriteLine("\n{0}   {1}", name, author);
        }
        public int CompareTo(object obj)
        {
            if (obj is Book)
                return name.CompareTo((obj as Book).name);

            throw new NotImplementedException();
        }
        public class SortByAuthor : IComparer
        {
            int IComparer.Compare(object obj1, object obj2)
            {
                if (obj1 is Book && obj2 is Book)
                    return (obj1 as Book).author.CompareTo((obj2 as Book).author);

                throw new NotImplementedException();
            }
        }
        public object Clone()
        {
            return new Book(name, author);
        }
    }

    public class Library: IEnumerable
    {
            Book[] ar;
            public Library(int len)
            {
                ar = new Book[len];
                for (int i = 0; i < len; i++)
                {
                    ar[i] = new Book();
                }
            }

            public Library() : this(1) { }

            public Library(Book[] clubs)
            {
                ar = new Book[clubs.Length];
                for (int i = 0; i < clubs.Length; i++)
                {
                    ar[i] = new Book(clubs[i].name, clubs[i].author);
                }
            }
            public void ShowClubs()
            {
                for (int i = 0; i < ar.Length; i++)
                    ar[i].Show();
            }

            //Итератор представляет метод, в котором используется ключевое слово yield для перебора по коллекции или массиву 
            public IEnumerator GetEnumerator()
            {
                Console.WriteLine("\nВыполняется метод GetEnumerator");
                for (int i = 0; i < ar.Length; i++)
                    yield return ar[i];
                // При обращении к оператору yield return будет сохраняться текущее местоположение.
                // И когда foreach перейдет к следующей итерации для получения нового объекта, 
                // итератор начнет выполнение с этого местоположения.
            }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Book []a = new Book[3];
            a [0]= new Book("Дом в котором", "Марьям Петросян");
            a [1]= new Book("a1984", "Джордж Оруэлл");
            a [2]= new Book("Лолита", "Владимир Набоков");
            WriteLine();
            foreach (Book temp in a)
                temp.Show();
            Array.Sort(a);
            Console.WriteLine("\nУпорядоченный массив по названию:");
            foreach (Book temp in a)
                temp.Show();
            Array.Sort(a, new Book.SortByAuthor());
            Console.WriteLine("\nМассив, упорядоченный по автору:");
            foreach (Book temp in a)
                temp.Show();
            WriteLine();

            Book []a2 = a;
            a2[0]= new Book("Книга","1");
            foreach (Book temp in a)
                temp.Show();
            Book []a3 = a.Clone() as Book[];
            a3[0] = new Book("Книга2", "2");
            foreach (Book temp in a3)
                temp.Show();
            WriteLine();
            Library lb = new Library(a);
            foreach (Book temp in lb)
                temp.Show();
            foreach (Book temp in lb)
                temp.Show();

        }
    }
}
