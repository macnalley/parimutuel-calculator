# Parimutuel Calculator
A console betting program to take bets and calculate odds and payouts in a parimutuel betting system, such as a horse race. It was created to meet the project requirements for the Code Louisville Software Development 1 course.

The program can take manually entered bets to win, place, or show; store them in in-program memory; use those bets to calculate win odds; and display the payouts given race winners.

This program targets .NET 6. The .NET 6 rutime can be obtained from https://dotnet.microsoft.com/en-us/download/dotnet/6.0.

## Functionality
### Take Bet
Bets can be placed on a horse (or hound, as while the program uses the terminology of horse racing, the calculations are the same for any parimutuel pool) to win, place, show. The program operator must enter arguments for the bettor's name, the horse number, the type of bet (win, place, or show), and the amount wagered, in that order. Arguments must be separated by commas or spaces. To enter a first and last name for the bettor, commas must be used to separate arguments. The amount wagered and bet type are optional. If not entered, the amount wagered will default to $2.00. The minimum acceptable bet is $2.00.

### Calculate Odds
Odds displayed are those for horses to win only. Odds for bets to place and show are not displayed. Odds represent the per dollar payment on a win bet. For example, a horse with 2:1 odds (represented by just the number 2) represents $2.00 returned per dollar of bet. A $2.00 bet on a 2:1 horse, would receive $4.00 from the house should that horse win. 

Odds are displayed up to the highest number horse bet on. For example, if a race has 10 horses, but only horses 1-5 have been bet on, only the odds of horses 1-5 will be displayed.

### Calculate Payouts
To calculate payouts, the numbers of the first-, second-, and third-place horses must be entered, in that order. The numbers may be entered separated by commas or spaces. Payouts represent the gross amount to be paid to the winning bettors by the house. This is the amount to be disbursed, and includes the original bet.

For horses with 1:1 odds, where the return would be equal to the bet, $0.10 are added to the return per dollar bet, to ensure bettors make money on winning bets.

### Display Bets
Displays all the current bets. A bet can also be removed from this menu.

### New Race
Starts a new race. Clears all current bets from the program memory. Should only be done after payouts.

## Code Louisville Requirements
### Feature List
#### Implement a “master loop” console application where the user can repeatedly enter commands/perform actions, including choosing to exit the program
The RunMainMenu() method in Program.cs utlizes a while loop to keep the program open until the user presses escape at the main menu. The other menu functions in Program.cs also utilze while and do-while loops to stay in a particular menu until a user enters a valid command or returns to a previous menu.

#### Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
Program.cs begins by instantiating a list of bets. When a bet is placed, it is added to this list. Values of the bets are used to calculate odds and payouts. The list is reinstantiated as any empty list when a new race is selected from the main menu.

#### Use a LINQ query to retrieve information from a data structure (such as a list or array) or file
In order to calculate odds and payouts, the program must know how much money was wagered on win bets, place bets, and show bets, as well as how much is wagered on particular horses to win, place, and show. This information is summed by calling a Linq query on the in-memory list of bets in the CalculateOdds() method in BettingMethods.cs and CalculateBetTotal() and CalculateHorseTotal() methods in Race.cs.