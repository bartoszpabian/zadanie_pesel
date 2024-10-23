using System;

class Program
{
    static void Main(string[] args)
    {
        string pesel;

        // Wczytanie numeru PESEL
        Console.WriteLine("Podaj numer PESEL (11 cyfr), lub naciśnij Enter, aby użyć domyślnego (55030101193):");
        pesel = Console.ReadLine();

        if (string.IsNullOrEmpty(pesel))
        {
            pesel = "55030101193"; // Domyślny PESEL
        }

        // Sprawdzanie płci
        char gender = CheckGender(pesel);
        string genderText = gender == 'K' ? "Kobieta" : "Mężczyzna";
        Console.WriteLine($"Płeć: {genderText}");

        // Sprawdzanie sumy kontrolnej
        bool isValidChecksum = CheckChecksum(pesel);
        if (isValidChecksum)
        {
            Console.WriteLine("Numer PESEL jest poprawny.");
        }
        else
        {
            Console.WriteLine("Numer PESEL jest niepoprawny.");
        }
    }

    static char CheckGender(string pesel)
    {
        if (pesel.Length != 11)
        {
            throw new ArgumentException("Numer PESEL musi mieć 11 cyfr.");
        }

        int genderDigit = int.Parse(pesel[9].ToString());
        return (genderDigit % 2 == 0) ? 'K' : 'M'; // 'K' dla parzystych, 'M' dla nieparzystych
    }

    static bool CheckChecksum(string pesel)
    {
        int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        int sum = 0;

        // Obliczanie sumy kontrolnej
        for (int i = 0; i < 10; i++)
        {
            sum += int.Parse(pesel[i].ToString()) * weights[i];
        }

        int M = sum % 10;
        int R = (M == 0) ? 0 : 10 - M;

        // Sprawdzanie czy suma kontrolna jest zgodna z ostatnią cyfrą PESEL
        return R == int.Parse(pesel[10].ToString());
    }
}
