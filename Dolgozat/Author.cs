using System;

public class Author
{
    public string Vezeteknev { get; private set; }
    public string Keresztnev { get; private set; }
    public Guid Id { get; private set; }

    public Author(string fullName)
    {
        var names = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (names.Length != 2 || names[0].Length < 3 || names[0].Length > 32 || names[1].Length < 3 || names[1].Length > 32)
        {
            throw new ArgumentException("A név formátuma helytelen. Kereszt- és vezeték név minimum 3, maximum 32 karakter hosszú.");
        }

        Vezeteknev = names[0];
        Keresztnev = names[1];
        Id = Guid.NewGuid();
    }
}