using System;
using System.IO;
// meg kell csinálni a kivonas osztast hogy csak egyszer legyen 37-41 67-70 79 102-119 141 173-180 ad ik sorok
namespace szamologep
{
    class Program
    {
        private static int selectedMenuIndex = 1;

        public static void Main(string[] args)
        {
            while (true)
            {
                ConsoleKeyInfo key;
                do
                {
                    Console.Clear();
                    menu();

                    key = Console.ReadKey();
                    AdjustSelectedIndex(key);

                } while (key.Key != ConsoleKey.Enter);

                if (!HandleMenuSelection(selectedMenuIndex))
                {
                    break; // Kilépés a programból, ha a felhasználó ki akar lépni
                }
            }
        }

        // menüpontok
        private static void menu()
        {
            Console.WriteLine("Válasszon menüpontot:");
            menuItem("összeadas(a+b)", 1);
            menuItem("kivonas(a-b)", 2);
            menuItem("kivonas(b-a)", 3);
            menuItem("szorzas(a*b)", 4);
            menuItem("osztas(a/b)", 5);
            menuItem("osztas(b/a)", 6);
            menuItem("kilépés", 7);
            menuItem("&#39;kiíratás&#39;", 8);
        }

        private static void menuItem(string text, int menuIndex)
        {
            // menü kezelés és választott menüpont színezése
            if (menuIndex == selectedMenuIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{text} [<--]");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(text);
            }
        }

        private static void AdjustSelectedIndex(ConsoleKeyInfo key)
        {
            // nyilakkal való navigáció
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    selectedMenuIndex = (selectedMenuIndex % 8) + 1;
                    break;
                case ConsoleKey.UpArrow:
                    selectedMenuIndex = (selectedMenuIndex - 2 + 8) % 8 + 1;
                    break;
            }
        }

        private static bool HandleMenuSelection(int selectedMenuIndex)
        {
            double result = 0;

            if (selectedMenuIndex >= 1 && selectedMenuIndex <= 7)
            {
                switch (selectedMenuIndex)
                {
                    case 7:
                        Environment.Exit(0);
                        break;
                }
                Console.Write("Adja meg az első számot: ");
                double num1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Adja meg a második számot: ");
                double num2 = Convert.ToDouble(Console.ReadLine());

                switch (selectedMenuIndex)
                {
                    case 1:
                        result = num1 + num2;
                        break;
                    case 2:
                        result = num1 - num2;
                        break;
                    case 3:
                        result = num2 - num1;
                        break;
                    case 4:
                        result = num2 * num1;
                        break;
                    case 5:
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        else
                        {
                            Console.WriteLine("Nullával való osztás nem lehetséges.");
                            Console.ReadKey();
                            return true;
                        }
                        break;
                    case 6:
                        if (num1 != 0)
                        {
                            result = num2 / num1;
                        }
                        else
                        {
                            Console.WriteLine("Nullával való osztás nem lehetséges.");
                            Console.ReadKey();
                            return true;
                        }
                        break;
                }

                // Az eredmények elmentése egy TXT fájlba
                using (StreamWriter sw = File.AppendText("eredmenyek.txt"))
                {
                    sw.WriteLine($"{num1} {GetOperationSymbol(selectedMenuIndex)} {num2} = {result}");
                }

                Console.WriteLine($"Eredmény: {result}");
            }
            else if (selectedMenuIndex == 8)
            {
                // Kiíratás a TXT fájlból
                Console.WriteLine("Eredmények:");
                using (StreamReader sr = File.OpenText("eredmenyek.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("Érvénytelen választás. Kérem, válasszon újra.");
            }

            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz...");
            Console.ReadKey();
            return true;
        }

        private static char GetOperationSymbol(int selectedMenuIndex)
        {
            switch (selectedMenuIndex)
            {
                case 1:
                    return '+';
                case 2:
                    return '-';
                case 3:
                    return '-';
                case 4:
                    return '*';
                case 5:
                    return '/';
                case 6:
                    return '/';
                default:
                    return ' ';
            }
        }
    }
}
