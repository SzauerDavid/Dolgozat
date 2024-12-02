using System;
using System.Collections.Generic;
using System.Linq;

public class Book
{
    public long ISBN { get; private set; }
    public List<Author> Authors { get; private set; }
    public string Cim { get; private set; }
    public int KiadasEve { get; private set; }
    public string Nyelv { get; private set; }
    public int Keszlet { get; set; }
    public int Ar { get; private set; }

    private static Random random = new Random();

    public Book(long isbn, List<Author> authors, string cim, int kiadasEve, string nyelv, int keszlet, int ar)
    {
        if (isbn.ToString().Length != 10) throw new ArgumentException("Az ISBN azonosítónak 10 számjegyűnek kell lennie.");
        if (authors.Count < 1 || authors.Count > 3) throw new ArgumentException("A szerzők számának 1 és 3 között kell lennie.");
        if (string.IsNullOrWhiteSpace(cim) || cim.Length < 3 || cim.Length > 64) throw new ArgumentException("A cím hosszának 3 és 64 karakter között kell lennie.");
        if (kiadasEve < 2007 || kiadasEve > DateTime.Now.Year) throw new ArgumentException("A kiadás éve 2007 és a jelen év között kell legyen.");
        if (nyelv != "angol" && nyelv != "német" && nyelv != "magyar") throw new ArgumentException("Csak az angol, német és magyar nyelv engedélyezett.");
        if (keszlet < 0) throw new ArgumentException("A készlet nem lehet negatív.");
        if (ar < 1000 || ar > 10000 || ar % 100 != 0) throw new ArgumentException("Az ár 1000 és 10000 közötti kerek 100-as szám kell legyen.");

        ISBN = isbn;
        Authors = authors;
        Cim = cim;
        KiadasEve = kiadasEve;
        Nyelv = nyelv;
        Keszlet = keszlet;
        Ar = ar;
    }

    public Book(string cim, string authorName)
    {
        ISBN = UjISBN();
        Authors = new List<Author> { new Author(authorName) };
        Cim = cim;
        KiadasEve = DateTime.Now.Year + 1;
        Nyelv = "magyar";
        Keszlet = 0;
        Ar = 4500;
    }

    private static long UjISBN()
    {
        long isbn;

        do
        {
            isbn = (long)random.Next(1000000000, int.MaxValue);
        } while (false);

        return isbn;
    }

    public override string ToString()
    {
        var mennyiseg = Authors.Count == 1 ? "szerző:" : "szerzők:";
        var jelenlegiKeszlet = Keszlet == 0 ? "beszerzés alatt" : $"{Keszlet} db";
        return $"{mennyiseg} {string.Join(", ", Authors.Select(a => $"{a.Vezeteknev} {a.Keresztnev}"))}, cím: {Cim}, ár: {Ar} Ft, készlet: {jelenlegiKeszlet}";
    }
}