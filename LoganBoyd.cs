using System;
using System.Runtime.InteropServices;

namespace Assignment
{
    //makes Strusts for each person and suitcase
    public struct person
    {
        public string firstName;
        public string lastName;
        public string interst;

    }
    public struct Suitcase
    {
        public int Number;
        public double Prize;
        public bool opened;
        public bool picked;
    }


    internal class Program
    {
        
        // sets up global verable   
        private static string locshon = "../../../assets/DealOrNoDeal.txt";
        private static Random rand = new Random();

        private static double[] CaseMoney = new double[26]
        {0.10,1.0,5.0,10.0,15.0,25.0,50,75.0,100.0,150.0,250.0,500.0,750.0,1000.0,2000.0,3000.0,4000.0,5000.0,10000.0,20000.0,25000.0,30000.0,40000.0,50000.0,100000.0,200000.0};

        private static string[] CaseNumber = new string[26]
        {" 1"," 2"," 3"," 4"," 5"," 6"," 7"," 8"," 9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26"};
        
        private static double[] SideMoney = new double[26]
            {0.1,1.0,5.0,10.0,15.0,25.0,50.0,75.0,100.0,150.0,250.0,500.0,750.0,1000.0,2000.0,3000.0,4000.0,5000.0,10000.0,20000.0,25000.0,30000.0,40000.0,50000.0,100000.0,200000.0};
        
        private static Suitcase[] SuitcasesArray = new Suitcase[26];
        private static int PlayerSuitcase;
        private static double playerPrize;



        // the main void 
        static void Main()
        {
            person[] players = new person[19];
            person[] filnalists = new person[10];
            read(players);
            menu(players, filnalists);
        }


        // the menu 
        static void menu(person[] players, person[] filnalists)
        {
            int choice;
            do
            {

                Console.Clear();
                choice = -1;
                while (choice < 0 || choice > 5)
                {
                    Console.WriteLine("welcome to the menu");
                    Console.WriteLine("1 To see all players \n2 for change person Interst  \n3 generate 10 finalists \n4 to view player \n5 to play game (please full screen) \n0 to exit");
                    Console.Write("> ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                }
                switch (choice)
                {

                    case 1:
                        int id = 0;
                        BubbleSort(players);
                        Display(players, filnalists, id);
                        Write(players);
                        choice = 5;
                        break;
                    case 2:
                        Channge(players);
                        Write(players);
                        choice = 5;
                        break;
                    case 3:

                        Finalists(filnalists, players);
                        choice = 5;
                        break;
                    case 4:
                        player(filnalists);
                        choice = 5;
                        break;
                    case 5:
                        GameSetup();
                        GUI();
                        PlayersCase();
                        OpenCases();
                        Exit();
                        break;
                    default:
                        Array.Clear(players);
                        Array.Clear(filnalists);
                        choice = 9;
                        Exit();
                        break;

                }
            } while (choice == 5);
        }

        //reads the DealOrNoDeal file then gives the user the option to change it 
        static void read(person[] players)
        {
            StreamReader sr = new StreamReader($"{locshon}");
            //make an array of structs, cantaning  first and last name and Interst 
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < players.Length; i++)
                {

                    players[i].firstName = sr.ReadLine();
                    players[i].lastName = sr.ReadLine();
                    players[i].interst = sr.ReadLine();

                }
            }
            sr.Close();
        }

        
        
        //sorts the array using bubble sort
        static void BubbleSort(person[] players)
        {
            for (int i = 0; i < players.Length - 1; i++)
            {
                for (int pos = 0; pos < players.Length - 1 - i; pos++)
                {
                    if (players[pos + 1].lastName.CompareTo(players[pos].lastName) < 0)
                    {
                        ThreeStepSwap(players,pos);
                    }
                }
            }
        }
        //three step swap 
        static void ThreeStepSwap(person[] players,int pos)
        {
            // Swap the elements in the array 
            person temp = players[pos + 1];
            players[pos + 1] = players[pos];
            players[pos] = temp;
        }
        
        //Fisher–Yates shuffle Algorithm
        static void RandCase(double[] money, int n)
        {
            Random rand = new Random();
            for (int i = n - 1; i > 0; i--)
            {
                //three step random sort
                int j = rand.Next(0, i + 1);
                double temp = money[i];
                money[i] = money[j];
                money[j] = temp;
            }

        }


        //give the user the ability to change a players information 
        static void Channge(person[] players)
        {
            
            bool found = false;
            string Fname, Lname;

            Console.WriteLine("who are you looking for please enter there last name");
            Console.Write("> ");
            Lname = Console.ReadLine();
            Lname = char.ToUpper(Lname[0]) + Lname.Substring(1); //set the first Character in name to uppercase to match with file format  
            Console.WriteLine("now enter there first name");
            Console.Write("> ");
            Fname = Console.ReadLine();
            Fname = char.ToUpper(Fname[0]) + Fname.Substring(1); //set the first Character in name to uppercase to match with file format 

            //runs throw the players array and check to see in name inputed matches name in file 
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].firstName == Fname && players[i].lastName == Lname)
                {

                    found = true;
                    Console.WriteLine($"You found: {Fname} {Lname}, there Interst is {players[i].interst} \n");
                    Console.WriteLine($"What would you like to change\n1 for First name\n2 for Last Name\n3 for Interst\n4 to cancal");
                    Console.WriteLine("What would you like to change");
                    Console.Write("> ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("what is there Fist name");
                            Console.Write("> ");
                            players[i].firstName = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("what is there Last name");
                            Console.Write("> ");
                            players[i].lastName = Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("what is there Interst");
                            Console.Write("> ");
                            players[i].interst = Console.ReadLine();
                            break;
                        case 4:
                        default:
                            break;


                    }
                }
            }
            if (!found)
            {
                Console.WriteLine("person not found");
            }
        }

        //randomly picks 10 people from the people array and set then in the filnalist array 
        static void Finalists(person[] filnalist, person[] players)
        {
            int id = 1;
            Array.Clear(filnalist);
            for (int i = 0; i < filnalist.Length; i++)
            {
                int num = rand.Next(0, players.Length);

                while (filnalist.Contains(players[num]))
                {
                    num = rand.Next(0, filnalist.Length);
                }


                filnalist[i] = players[num];
            }
            Display(players, filnalist, id);
        }

        // picks a random person from finalist array and show as player 
        static void player(person[] filnalist)
        {

            if (filnalist[1].lastName is not null)
            {
                person winner = filnalist[rand.Next(0, filnalist.Length)];
                Console.WriteLine($"your player is {winner.firstName} {winner.lastName}");
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("you don't have any finalist");
                Console.WriteLine("please generate finalist first\n");
                Console.WriteLine();
                Console.WriteLine(@"Press {ENTER} to continue");
                Console.ReadLine();

            }

        }
        // dose and HTTP request for BitCoin api
        private static async Task<string> GetBitcoinPrice()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Send a GET request to the Bitcoin price API.
                    HttpResponseMessage response = await client.GetAsync("https://api.coindesk.com/v1/bpi/currentprice/BTC.json");

                    // Ensure the response is successful.
                    response.EnsureSuccessStatusCode();

                    // Read the response content as a string.
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the API request.
                    Console.WriteLine($"An error occurred while retrieving the Bitcoin price: {ex.Message}");
                    return null;
                }
            }
        }

        //get the BTC price from API
        private static async Task PrintBitcoinPrice()
        {
            try
            {
                // Retrieve the Bitcoin price from the API.
                string bitcoinPrice = await GetBitcoinPrice();

                if (bitcoinPrice != null)
                {
                    // Extract the price from the API response.
                    string price = bitcoinPrice.Substring(bitcoinPrice.IndexOf("rate") + 7, 10);

                    // Print the Bitcoin price.
                    Console.WriteLine($"The current Bitcoin price is: {price} USD");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the printing process.
                Console.WriteLine($"An error occurred while printing the Bitcoin price: {ex.Message}");
            }
        }
    


    //sets up games
    static void GameSetup()
        {



            // console setup
            Console.Title = "Deal or no Deal";
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.ForegroundColor = ConsoleColor.Green;


            //game setup
            PlayerSuitcase = 99;
            int length = CaseMoney.Length;
            RandCase(CaseMoney, length);

            //set up cases 
            for (int i = 0; i < SuitcasesArray.Length; i++)
            {
                SuitcasesArray[i].Number = i + 1;
                SuitcasesArray[i].Prize = CaseMoney[i];
                SuitcasesArray[i].opened = false;
                SuitcasesArray[i].picked = false;
            }
        }
    
        // the gui 
        public static void GUI()
        {


            // set up verables 
            int caseOnRow = 8, startcase = 0, casenum = 0;
            string[] caseArt = new string[5]
{
        " _________ ",
        "||       ||",
        "||       ||",
        "||_______||",
        "|_________|"
};



            Console.Clear();
            Console.WriteLine("                                                                                               ");
            Console.WriteLine(@"      


                       _____  ______          _             ____  _____        _   _  ____       _____  ______          _             _____          __  __ ______ 
                      |  __ \|  ____|   /\   | |           / __ \|  __ \      | \ | |/ __ \     |  __ \|  ____|   /\   | |           / ____|   /\   |  \/  |  ____|
                      | |  | | |__     /  \  | |          | |  | | |__) |     |  \| | |  | |    | |  | | |__     /  \  | |          | |  __   /  \  | \  / | |__   
                      | |  | |  __|   / /\ \ | |          | |  | |  _  /      | . ` | |  | |    | |  | |  __|   / /\ \ | |          | | |_ | / /\ \ | |\/| |  __|  
                      | |__| | |____ / ____ \| |____      | |__| | | \ \      | |\  | |__| |    | |__| | |____ / ____ \| |____      | |__| |/ ____ \| |  | | |____ 
                      |_____/|______/_/    \_\______|      \____/|_|  \_\     |_| \_|\____/     |_____/|______/_/    \_\______|      \_____/_/    \_\_|  |_|______|
                                                                                                                                                                                                                                                                         
");                                                                                 
            Console.WriteLine("                                                                                               ");
            Console.WriteLine("                                                                                               ");


            //prints GUI 

            // loop for each row of completed cases 
            for (int row = 0; row < 4; row++)
            {
                //goes each row of case art array 
                for (int CaseRow = 0; CaseRow < caseArt.Length; CaseRow++)
                {
                    Console.Write(" ".PadRight(18));
                    //changes amount of cases there is per row from 8 to 2
                    if (row == 3)
                    {
                        Console.Write("          ".PadRight(45));
                        caseOnRow = 2;
                    }

                    //print left size 
                    LeftSide(casenum);

                    //prints one line of cases art for the amount of cases in the row 
                    DisplayCase(caseArt,startcase,caseOnRow,CaseRow);

                    //prints right  side of money 
                    RightSide(casenum);
                    casenum++;
                    Console.WriteLine();
                }
                startcase += 8;

            }

            //check to see if user pick a case
            PlayersCase();

        }

        //prints the money on left 
        static void LeftSide(int casenum)
        {
            if (casenum < 13 && SideMoney[casenum] != 0.0)
            {
                string sidenum;
                sidenum = Convert.ToString(SideMoney[casenum]);
                Console.Write($" (${sidenum}) ".PadRight(10));
            }
            else
            {
                Console.Write("          ");
            }
        }

        //prints one line of cases art for the amount of cases in the row 
        static void DisplayCase(string[] caseArt, int startcase, int caseOnRow, int CaseRow)
        {
            //loops for the amount of cases per row
            for (int i = 0; i < caseOnRow; i++)
            {
                //checks to see if suitcase is pick or opened 
                if (SuitcasesArray[i + startcase].picked == false && SuitcasesArray[i + startcase].opened == false)
                {
                    //if on second line of suitcase art array prints case number
                    if (CaseRow == 2)
                    {
                        Console.Write($"||  {CaseNumber[i + startcase]}   ||".PadRight(15));
                    }
                    else // if not just print that line of suitcase art  array 
                    {
                        Console.Write(caseArt[CaseRow].PadRight(15));
                    }
                }
                else //if opends prints nothing 
                {
                    Console.Write("          ".PadRight(15));
                }
            }
        }
        //prints right side of money 
        static void RightSide(int casenum)
        {
            if (casenum < 13 && SideMoney[casenum + 13] != 0.0)
            {
                string temp, sidenum;
                temp = Convert.ToString(SideMoney[casenum + 13]);
                sidenum = temp.Insert(temp.Length - 3, ",");
                Console.Write($" (${sidenum}) ".PadLeft(14));
            }
            else
            {
                Console.Write("         ");
            }
        }

        //picks and prints player case 
        static void PlayersCase()
        {
            //if players has not pick case gets asked  to pick one 
            if (PlayerSuitcase == 99)
            {
                Console.Write("\n\n\n\n");

                Console.WriteLine("Choose the BitCoin wallet you want to keep for yourself: ");
                PlayerSuitcase = Convert.ToInt32(Console.ReadLine()) - 1;
                SuitcasesArray[PlayerSuitcase].opened = false;
                SuitcasesArray[PlayerSuitcase].picked = true;
                playerPrize = SuitcasesArray[PlayerSuitcase].Prize;
            }
            else // if has prints players cases
            {
                string n = CaseNumber[PlayerSuitcase];
                Console.Write("\n\n\n");
                Console.WriteLine("Player Screen:".PadLeft(10));
                Console.WriteLine(" _________ ".PadLeft(12));
                Console.WriteLine("||       ||".PadLeft(12));
               Console.WriteLine($"||  {n}   ||".PadLeft(12));
                Console.WriteLine("||_______||".PadLeft(12));
                Console.WriteLine("|_________|".PadLeft(12));
            }

        }

        
        //opens all other cases 
        static void OpenCases()
        {
            bool Deal = false;
            int offer = 0, lastCase = 0;
            double BTC = Convert.ToDouble(PrintBitcoinPrice()) ,BTCP;

            do
            {
                GUI();

                string input;
                Console.Write("\n\n What number BitCoin wallet do you want to hack? ");
                input = Console.ReadLine();
                Console.Beep();
                int Case = Convert.ToInt32(input) - 1;
                //gives correct format for 10c
                if (SuitcasesArray[Case].Prize == 0.10)
                {
                    BTCP = SuitcasesArray[Case].Prize * BTC;
                    Console.WriteLine($"\n You hacked a BitCoin wallet worth  {BTCP} BitCoin / ({SuitcasesArray[Case].Prize:C2})  !!");
                }
                else
                {
                    BTCP = SuitcasesArray[Case].Prize * BTC;
                    Console.WriteLine($"\n You hacked a BitCoin wallet worth  {BTCP} BitCoin / ({SuitcasesArray[Case].Prize:C0}) !!");
                }

                SuitcasesArray[Case].opened = true;
                Console.Write("\n Press ENTER to continue ");
                Console.ReadLine();
                
                
                for (int i=0; i < SideMoney.Length; i++)
                {
                    //  if the side money and case are the same set side money to 0.0 so it wont get printed later 
                    if(SideMoney[i] == SuitcasesArray[Case].Prize)
                    {
                        SideMoney[i] = 0.0;
                    }
                            

                }

                GUI();
                offer++;
                lastCase++;
                if (offer == 2)
                {
                    Deal = bank(Deal);
                    offer = 0;
                }
                //if all cases have been pick tell user value of there case and leaves game
                if(lastCase == 26)
                {
                    BTCP = playerPrize * BTC;
                    Console.WriteLine($"\n\n  and your case is worth {BTCP} BitCoin / {playerPrize:C2}");
                    Console.ReadLine();
                    Deal = true;
                }

            }
            while (Deal == false);

        }

        //banks offer
        static bool  bank(bool Deal)
        {
            string choice;
            double total = 0,BTC = 0.000029, BTCP; 
            int CasesLeft=0, casesOpened = 0;

            foreach (Suitcase i in SuitcasesArray)
            {         
                //get the total of unopened cases 
                if (i.opened == false)
                {
                    total += i.Prize;
                    
                }
                else if (i.picked == true) //get the anount of picked cases
                {
                    casesOpened++;

                }

                CasesLeft = SuitcasesArray.Length - casesOpened;
            }
                total = Convert.ToDouble(total / CasesLeft);
                BTCP = total * BTC;
                Console.WriteLine($"\n\nThe BitCoin bros Would like to offer you {BTCP} Bitcoins / ({total:c2}) to stop");
                Console.WriteLine("\n\n Deal or No Deal?");
                choice = Console.ReadLine().ToLower();
                if (choice == "deal" || choice == "d")
                {
                    Deal = true;
                Console.WriteLine($"congratulates on winning {total:c2} ");
                }
                else
                {
                    Deal = false;
                }


                return (Deal);
        }

        //write array to file
        static void Write(person[] players)
        {
            StreamWriter sw = new StreamWriter(($"{locshon}"));

            for (int i = 0; i < players.Length; i++)
            {
                sw.WriteLine(players[i].firstName);
                sw.WriteLine(players[i].lastName);
                sw.WriteLine(players[i].interst);
            }
            sw.Close();
            Console.WriteLine("saved");
        }


        //display the enter people array
        static void Display(person[] players, person[] filnalist, int id)
        {

            if (id == 0)
            {
                Console.WriteLine($"FirstName".PadRight(20) + " LastName".PadRight(30) + "Interst");
                foreach (person p in players)
                {

                    Console.Write($"{p.firstName.PadRight(20)}  {p.lastName.PadRight(20)}  {p.interst}");
                    Console.WriteLine();

                }
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine(@"Press {ENTER} to continue");
                Console.ReadLine();
            }
            else if (id == 1)
            {
                Console.WriteLine($"FirstName".PadRight(20) + " LastName".PadRight(30) + "Interst");
                foreach (person p in filnalist)
                {

                    Console.Write($"{p.firstName.PadRight(20)}  {p.lastName.PadRight(20)}  {p.interst}");
                    Console.WriteLine();

                }
                Console.WriteLine("-----------------------------------------------------------------------------------");
                Console.WriteLine(@"Press {ENTER} to continue");
                Console.ReadLine();
            }

        }

        static void Exit()
        {
            Console.Clear();
            Console.WriteLine($"\n\n\n\n\n\n\n");
            Console.WriteLine(@"




                                                                          _____  ____   ____  _____        ______     ________ 
                                                                         / ____|/ __ \ / __ \|  __ \      |  _ \ \   / /  ____|
                                                                        | |  __| |  | | |  | | |  | |     | |_) \ \_/ /| |__   
                                                                        | | |_ | |  | | |  | | |  | |     |  _ < \   / |  __|  
                                                                        | |__| | |__| | |__| | |__| |     | |_) | | |  | |____ 
                                                                         \_____|\____/ \____/|_____/      |____/  |_|  |______|
                                                                                                                               
                                                    
");

            Thread.Sleep(1000);
        }



    }
}