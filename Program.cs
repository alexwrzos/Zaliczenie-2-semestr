using System;
using System.Numerics;

//program zrobiony na inspiracji playlisty na youtube
//program autorstwa Aleksandra Wrzos (zrozumienie i zaimplementowanie klucza oraz drzwi, poprawianie bugów), Aleksandra Franus (Wygląd mapy oraz blokowanie gracza), (Interakcje z graczem) Joanna Riedel

namespace graRogue
{
    class Player
    {
        public int x = 1; //poczatkowa pozycja gracza
        public int y = 4;
        public string nick = "bezimienny";
        public string avatar = "@"; //wygląd gracza
        //inwentarz
        public bool klucz = false; //na początku nie mamy klucza
    }

    class Display
    {
        public static void WriteAt(int columnNumber, int rowNumber, string text)
        {
            Console.SetCursorPosition(columnNumber, rowNumber);
            Console.Write(text);
        }
        public static void WriteAt(int columnNumber, int rowNumber, char sign)
        {
            Console.SetCursorPosition(columnNumber, rowNumber);
            Console.Write(sign);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //poziom na początku gry
            string[] level = {

                "####################",
                "#          #K      #",
                "#          #       #",
                "#          #       #",
                "#      #####       #",
                "#                  D",
                "#          #       #",
                "####################"
            };

            //dodanie drugiego poziomu po otworzeniu drzwi
            string[] level2 =
            {
               "############################################",
                "#          #       ##                      #",
                "#          #       ##                      #",
                "#          #       ##                      #",
                "#      #####       ##                      #",
                "#                                          #",
                "#          #       ##                      #",
                "############################################",
            };

            Player player = new Player();
            Console.Clear();
            
            //////////////////////////////////////////////////
            //zaczynamy dzialanie programu
            //interakcja z graczem
            
            
            Console.WriteLine("Jak się nazywasz podróżniku?");
            player.nick = Console.ReadLine();
            Console.WriteLine("Pracując nad grą na zaliczenie, jakimś cudem wylądowałeś w niej! Teraz musisz wydostać się z niej zanim przyjdzie deadline zaliczeniowy!");
            Console.WriteLine("Czy chcesz kontynuować?(Tak/Nie)");
            string odpowiedz = Console.ReadLine().Trim().ToLower();

            if (odpowiedz == "tak")
            {
                Console.Clear();
            }
            else if (odpowiedz == "nie")
            {
                Console.WriteLine("W takim razie utkniesz tu na zawsze! Chyba ze jednak chcesz kontynuować?");
                odpowiedz = Console.ReadLine().Trim().ToLower();
                while (odpowiedz != "tak")
                {
                    System.Environment.Exit(1); //jesli osoba 
                }
            }
            else
          {
                Console.WriteLine("Nieprawidłowa odpowiedź. Wpisz 'tak' lub 'nie'.");
                return;
          }

            //wyswietlanie mapy
            Console.Clear();
            foreach (string row in level)
            {
                Console.WriteLine(row);
            }

            while (true)
            {
                Display.WriteAt(player.x, player.y, player.avatar);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                string currentRow = level[player.y];
                char currentCell = currentRow[player.x];
                Display.WriteAt(player.x, player.y, currentCell);

                int targetColumn = player.x;
                int targetRow = player.y;

                //poruszanie się po mapie
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    targetColumn = player.x - 1;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    targetColumn = player.x + 1;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    targetRow = player.y - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    targetRow = player.y + 1;
                }
                else
                {
                    break;
                }

                //nie przechodz przez sciane
                if (targetColumn >= 0 && targetColumn < level[player.y].Length && level[player.y][targetColumn] != '#')
                {
                    player.x = targetColumn;
                }

                if (targetRow >= 0 && targetRow < level.Length && level[targetRow][player.x] != '#')
                {
                    player.y = targetRow;
                }

                //co sie dzieje gdy zdobedziemy klucz
                if (level[targetRow][player.x] == 'K')
                {

                    Console.SetCursorPosition(0, 10);
                    Console.Write("                                        "); //musimy jakos wyczyscic tekst na dole
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Zdobywasz klucz!");
                    player.klucz = true;
                    string[] levelK = {
                "####################",
                "#          #K      #",
                "#          #       #",
                "#          #       #",
                "#      #####       #",
                "#                  D",
                "#          #       #",
                "####################"
                    };
                    level = levelK;
                }

                //co sie dzieje gdy otorzymy drzwi
                if (level[targetRow][player.x] == 'D')
                {
                    if (player.klucz)
                    {
                        level = level2;
                        Console.Clear(); //czyszczenie konsoli
                        Console.SetCursorPosition(0, 10);
                        Console.WriteLine("Używasz klucza by otworzyć drzwi!");
                        Console.SetCursorPosition(0,0);
                        //wyswietlenie mapy
                        foreach (string row in level)
                        {
                            Console.WriteLine(row);
                        }
                        player.klucz = false; //klucz został użyty i zniknął
                    }
                    else
                    {

                        Console.SetCursorPosition(0, 10);
                        Console.Write("                                        ");
                        Console.SetCursorPosition(0, 10);
                        Console.WriteLine("Czegoś Ci brakuje by otworzyć drzwi...");
                    }
                    
                }
            }

            Console.SetCursorPosition(0, level.Length);
        }
    }
}