/*
 * Purpose: In this assignment you will design and write a modularized menu-driven program that allows the user to select one of two games (Craps and Pig) to play or to quit the program.
 * Input: Selection of game, bet or point total
 * Output: Roll result, game results
 * Author: Mihiri Kamiss
 * Instructor: Allan Anderson
 * Section: A02
 * Last Modified: November 10, 2022
 * */

namespace CPSC1012Casino
{
    internal class Program
    {
        static void Main()
        {
            //declare variables
            int gameOption;
            bool closeGame = false;

            //display menu
            do
            {
                //reset valid options
                bool validOption = false;

                do
                {
                    gameOption = DisplayMenu();
                    Console.WriteLine(gameOption);

                    //switch case for menu options

                    switch (gameOption)
                    {
                        case 1:
                            PlayGameOfCraps();
                            validOption = true;
                            break;

                        case 2:
                            PlayGameOfPig();
                            validOption = true;
                            break;

                        case 3:
                            Console.WriteLine("Goodbye and thanks for coming to the CPSC1012 Casino");
                            validOption = true;
                            closeGame = true;
                            break;

                        default:
                            Console.WriteLine("ERROR: Please choose a valid option...");
                            break;
                    }

                } while (validOption == false);

            } while (closeGame == false);

            Console.ReadLine();

        }//end of Main

        static int DisplayMenu()
        {
            //create a constant for reoccuring menu lines
            const string MenuBorder = "|-----------------|";

            //declare variable for option
            int option;

            //display menu
            Console.WriteLine(MenuBorder);
            Console.WriteLine("| CPSC1012 Casino |");
            Console.WriteLine(MenuBorder);
            Console.WriteLine("| 1. Play Craps   |");
            Console.WriteLine("| 2. Play Pig     |");
            Console.WriteLine("| 3. Exit Program |");
            Console.WriteLine(MenuBorder);

            //prompt for menu option
            option = GetSafeInt("Enter your menu number choice: ");

            return option;
        }//end of DisplayMeny

        static int GetSafeInt(string prompt)
        {
            int number;
            Console.Write(prompt);

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("ERROR: Enter a valid number: ");

            }//end of while

            return number;
        }// end of GetSafeInt

        static double GetSafeDouble(string prompt)
        {
            int number;
            Console.Write(prompt);

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("ERROR: Enter a valid number: ");

            }//end of while

            return number;
        }// end of GetSafeInt

        static char GetSafeChar(string prompt)
        {
            char letter;
            Console.Write(prompt);

            while (!char.TryParse(Console.ReadLine().ToLower(), out letter))
            {
                Console.Write("ERROR: Enter a valid option: ");

            }
            return letter;
        }

        //-------------------PIG GAME---------------------

        static void PlayGameOfPig()
        {

            //declare variables
            int pointCount,
                playerPoints = 0,
                computerPoints = 0;
            bool resultsDisplayed = false;

            //display title
            Console.WriteLine("---------------");
            Console.WriteLine("| Game of Pig |");
            Console.WriteLine("---------------");

            //prompt for point total
            pointCount = GetSafeInt("Enter the point total to play for: ");

            do
            {
                playerPoints = PlayerTurnGameOfPig(pointCount, playerPoints);

                if (playerPoints < pointCount)
                {
                    computerPoints = ComputerTurnGameOfPig(pointCount, computerPoints);
                    Console.WriteLine($"\nYour total points: {playerPoints}");
                    Console.WriteLine($"Computer total points: {computerPoints}");

                }
                else
                {
                    Console.WriteLine("\nYOU WIN");
                    resultsDisplayed = true;
                }

                //total after each round

            } while (pointCount > computerPoints && pointCount > playerPoints);

            //decide and display winner

            if (resultsDisplayed == false)
            {
                if (computerPoints == pointCount)
                {
                    Console.WriteLine("\nTIE!");

                }
                else if (pointCount <= playerPoints || playerPoints > computerPoints)
                {
                    Console.WriteLine("\nYOU WIN");
                }
                else
                {
                    Console.WriteLine("\nYOU LOSE");
                }

            }

            Console.ReadLine();
        }//end of PlayGameOfPig

        static int OneDiceRoll()
        {
            Random random = new Random();
            int roll = random.Next(1, 6);

            return roll;

        }//end of OneDiceRoll

        static int PlayerTurnGameOfPig(int pointCount, int totalPoints)
        {
            int roll = 0,
                turnPoints = 0;
            char option;
            bool isValid = false,
                win = false;

            Console.WriteLine("\nIt's your turn");

            //prompt for option r or h
            do

            {
                do
                {
                    option = GetSafeChar("Enter r to roll or h to hold (r/h): ");
                    switch (option)
                    {
                        case 'r':
                            isValid = true;
                            roll = OneDiceRoll();
                            Console.WriteLine($"You rolled a {roll}");
                            if (roll == 1)
                            {
                                turnPoints = 0;
                                roll = 0;
                            }
                            else
                            {
                                turnPoints += roll;

                                if (turnPoints + totalPoints >= pointCount)
                                {
                                    win = true;
                                }
                            }
                            break;
                        case 'h':
                            isValid = true;
                            Console.WriteLine("You HOLD");
                            break;

                        default:
                            Console.WriteLine("ERROR: Invalid selection...try again");
                            break;
                    }

                } while (isValid == false);

            } while (roll != 0 && win == false && option != 'h');

            Console.WriteLine($"Your turn score is {turnPoints}");

            totalPoints += turnPoints;

            return totalPoints;
        }

        static int ComputerTurnGameOfPig(int pointCount, int totalPoints)

        {
            //declare variables
            Random rand = new Random();
            int rollOption,
                roll = 0,
                turnPoints = 0;
            bool win = false;

            Console.WriteLine("\nComputer's turn");
            do
            {
                rollOption = rand.Next(1, 3);
                if (rollOption == 1)
                {
                    roll = OneDiceRoll();
                    Console.WriteLine($"Computer rolled a {roll}");
                    if (roll == 1)
                    {
                        turnPoints = 0;
                        roll = 0;
                    }
                    else
                    {
                        turnPoints += roll;

                        if (turnPoints + totalPoints >= pointCount)
                        {
                            win = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Computer HOLD");
                }

            } while (rollOption == 1 && roll != 0 && win == false); ;
            Console.WriteLine($"Computer turn score is {turnPoints}");

            totalPoints += turnPoints;

            return totalPoints;
        }

        //-------------------CRAPS GAME-------------------

        static void PlayGameOfCraps()
        {
            //declare variables
            double bet,
                netWinning = 0;
            int point;
            bool playAgain,
                gameWon = false;
            char option;

            //display title
            Console.WriteLine("-----------------");
            Console.WriteLine("| Game of Craps |");
            Console.WriteLine("-----------------");

            do
            {
                playAgain = false;

                //prompt user for bet amount
                bet = GetSafeDouble("\nEnter amount to bet: ");

                //come out roll
                point = ComeOutRoll(bet);

                //if point was established, continue rolling
                if (point != 2 && point != 3 && point != 12 && point != 7 && point != 11)
                {
                    gameWon = TwoDiceRoll(point, bet);
                    if (gameWon == true)
                    {
                        netWinning += bet;
                    }
                }
                else
                {
                    netWinning -= bet;
                }

                //play again?

                option = GetSafeChar("Play again? (y/n): ");
                if (option == 'y')
                {
                    playAgain = true;
                }

            } while (playAgain != false);

            //display net winnings

            Console.WriteLine($"\nYour net winning is {netWinning:c}\n");

        }//end of PlayGameOfCraps

        static int ComeOutRoll(double bet)
        {
            int dice1 = OneDiceRoll(),
                dice2 = OneDiceRoll(),
                rollSum = dice1 + dice2;

            Console.WriteLine($"You rolled {dice1} + {dice2} = {rollSum}");

            if (rollSum == 7 || rollSum == 11)
            {
                Console.WriteLine($"You win {bet:c}");
            }
            else if (rollSum == 2 || rollSum == 3 || rollSum == 12)
            {
                Console.WriteLine($"You lost {bet:c}");
            }
            else
            {
                Console.WriteLine($"Point is {rollSum}");
            }

            return rollSum;
        }

        static bool TwoDiceRoll(int point, double bet)
        {
            const int instantLoss = 7;
            int rollSum,
                dice1,
                dice2;
            bool continueRoll = false,
                win = false;

            do
            {
                dice1 = OneDiceRoll();
                dice2 = OneDiceRoll();

                rollSum = dice1 + dice2;

                Console.WriteLine($"You rolled {dice1} + {dice2} = {rollSum}");

                if (rollSum == 7)
                {
                    Console.WriteLine($"You lost {bet:c}");
                    continueRoll = false;
                }
                else if (rollSum == point)
                {
                    win = true;
                    Console.WriteLine($"You won {bet:c}");
                    continueRoll = false;
                }
                else
                {
                    win = true;
                    continueRoll = true;
                }


            } while (continueRoll != false);

            return win;
        }
    }
}