using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Book> books = Books();
        RandomEladas(books);
    }

    static List<Book> Books()
    {
        var cimek = new List<string> { "A téboly otthona", "Doszpot Nyomoz", "A Monarchia szerelmesei", "Micimackó", "Apám szerint", "Vaják VI.", "A kegyelem vonala", "Pillangószoba", "A pokol fiai", "Kékestető" };
        var authors = new List<string> { "Kovács Béla", "Nagy Anna", "Tóth János", "Szabó Gábor", "Kiss Mária" };
        HashSet<long> existingISBNs = new HashSet<long>();

        List<Book> books = new List<Book>();
        Random random = new Random();

        for (int i = 0; i < 15; i++)
        {
            long isbn;
            do
            {
                isbn = (long)random.Next(1000000000, int.MaxValue);
            } while (existingISBNs.Contains(isbn));
            existingISBNs.Add(isbn);

            var nyelv = random.Next(0, 10) < 8 ? "magyar" : "angol";
            var cim = cimek[random.Next(cimek.Count)];
            var numAuthors = random.Next(1, 4);
            var selectedAuthors = authors.OrderBy(a => random.Next()).Take(numAuthors).Select(a => new Author(a)).ToList();

            int keszlet;
            if (random.Next(0, 10) < 3)
            {
                keszlet = 0;
            }
            else
            {
                keszlet = random.Next(5, 11);
            }

            int ar = (random.Next(10, 101) * 100);
            books.Add(new Book(isbn, selectedAuthors, cim, 2024, nyelv, keszlet, ar));
        }

        return books;
    }

    static void RandomEladas(List<Book> books)
    {
        int eladas = 0;
        int fogyott = 0;
        int osszes = books.Sum(b => b.Keszlet);
        Random random = new Random();

        for (int i = 0; i < 100; i++)
        {
            var book = books[random.Next(books.Count)];

            if (book.Keszlet > 0)
            {
                eladas += book.Ar;
                book.Keszlet--;
            }
            else
            {
                if (random.Next(0, 2) == 0)
                {
                    book.Keszlet += random.Next(1, 11);
                }
                else
                {
                    fogyott++;
                    books.Remove(book);
                }
            }
        }

        int vegeredmeny = books.Sum(b => b.Keszlet);
        Console.WriteLine($"Összesített bevétel: {eladas} Ft");
        Console.WriteLine($"Elfogyott a nagykerben: {fogyott} db");
        Console.WriteLine($"Kezdő készlet: {osszes} db, Jelenlegi készlet: {vegeredmeny} db, Különbség: {vegeredmeny - osszes} db");
    }
}