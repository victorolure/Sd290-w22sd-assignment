
using System.Text;

Dictionary<int, int> MyVendingMachine = new Dictionary<int, int>();

MyVendingMachine.Add(1, 5);
MyVendingMachine.Add(2, 5);
MyVendingMachine.Add(5, 5);
MyVendingMachine.Add(10, 5);
MyVendingMachine.Add(20, 5);


/*
The Vend function takes in a dictionary of a vendingmachine, an integer of the itemPrice and an integer of customerCash(cash a customer is paying).
Inside the vend function, the int change calculates the amount of change the customer is entitled to. The ints is a list that
collects all the keys (denominations) in the vendingMachine dictionary. I applied the Sort() method to sort the keys in the list but since
we needed the denominations to be sorted in descending order, I did a for loop to populate another List- ints2 with the sorted items in
descending order.
If the integer of total availableChange in the vendingMachine returned by the CalculateTotalAvailableChange function is greater than the change
to be given to the customer, the vend operation can continue. The integer "coinsToExtract" is used to keep track of the quantity(value) of a Key(denomination)
to be dispensed as change.

'change -= ChangeCollection[i] * i' at the end of each loop is used to reduce the value of the change by substracting the value of the product of
the key(denomination) by the value (quantity) in the Dictionary of the changeCollection from the current change after each dispensation of a loop.
the continues until the change is zero.

*/


Dictionary<int, int> Vend(Dictionary<int, int> vendingMachine, int itemPrice, int customerCash)
{
    int change = customerCash - itemPrice;
    List<int> ints = new List<int>();
    List<int> ints2 = new List<int>();
    int coinsToExtract;
    int remainder;
    Dictionary<int, int> ChangeCollection = new Dictionary<int, int>();
    foreach (KeyValuePair<int, int> denomination in vendingMachine)
    {
        ints.Add(denomination.Key);
    }
    ints.Sort();
    for (int i = ints.Count - 1; i >= 0; i--)
    {
        ints2.Add(ints[i]);
    }

    if (CalculateTotalAvailableChange(vendingMachine) >= change)
    {
        foreach (int i in ints2)
        {
            if (change >= i)
            {
                coinsToExtract = change / i;
                if (vendingMachine[i] > coinsToExtract)
                {

                    //remainder = change % i;
                    vendingMachine[i] -= coinsToExtract;
                    //change = remainder;
                    ChangeCollection.Add(i, coinsToExtract);
                }
                else
                {
                    ChangeCollection.Add(i, vendingMachine[i]);
                    vendingMachine[i] = 0;
                }
                change -= ChangeCollection[i] * i;


            }

        }

    }




    return ChangeCollection;
}

//TESTS
Dictionary<int, int> test1 = Vend(MyVendingMachine, 2, 25);// $31 change.
Dictionary<int, int> test2 = Vend(MyVendingMachine, 5, 60);// $55 change.
Dictionary<int, int> test3 = Vend(MyVendingMachine, 5, 35);// $30 change.



//For loop to display the Change being returned after a vend.
Console.WriteLine("QUESTION 1\n");
foreach (KeyValuePair<int, int> keyValuePair in test2)
{
    Console.WriteLine($"${keyValuePair.Key} - {keyValuePair.Value} Piece{(keyValuePair.Value > 1 ? 's' : ' ')}");
}

/*
The CalculateTotalAvailable change returns an integer of the total available change that a vending machine dictionary currently has.
It takes in a vendingMachine dictionary as argument and multiply each key(denomination) by their values(quantity). This is used as
an helper function in the Vend function to make sure that the machine has enough change in it for a vend operation to take place.
*/

int CalculateTotalAvailableChange(Dictionary<int, int> vendingMachine)
{
    int availableChange = 0;

    foreach (KeyValuePair<int, int> kvp in vendingMachine)
    {
        availableChange += kvp.Key * kvp.Value;
    }

    return availableChange;
}
Console.WriteLine("\nBelow is to the Calculate the total change available in the vending machine\n");
Console.WriteLine($"Total change in the Vending Machine is ${CalculateTotalAvailableChange(MyVendingMachine)} from the initial total of $190\n");





/*
The CompressString function compresses a string. It takes in a string as argument. Inside the function the string input is first converted
into an array of characters. An integer count variable is made to keep track of the occurence of a particular character if it appears 
consecutively. If the occurence is greater than or equal to 3, the count gets appended to just 1 occurence of the character.
The characterbuilder in the function can be termed as a collector that collects the first instance of the the character of the character-array
in the current loop. If the count of the character is less than 3, it s collected in the characterbuilder and then appended to the Finalbuilder
which is the stringbuilder that stores the characters being appended. The Finalbuilder.ToString() is the final string that the CompressString
function returns.
Take note that the characterBuilder is emptied after every iteration and reset to empty so as not to catch the wrong character.
*/


string CompressString (string input)
{
    char[] inputChars = input.ToCharArray();
    int count = 1;
    StringBuilder characterBuilder = new StringBuilder ();
    StringBuilder finalBuilder = new StringBuilder();

    for (int i = 0; i < inputChars.Length; i++)
    {
        characterBuilder.Append (inputChars[i]);
        if(i + 1 != inputChars.Length && inputChars[i] == inputChars[i + 1])
        {
            count++;
        }
        else
        {
            if(count >= 3)
            {
                finalBuilder.Append(inputChars[i]);
                finalBuilder.Append(count);
            }
            else
            {
                finalBuilder.Append(characterBuilder.ToString());
            }
            characterBuilder = new StringBuilder();
            count = 1;
        }
        

    }

    return finalBuilder.ToString ();
}


string testString = CompressString("RTFFFFYYUPPPEEEUU");

Console.WriteLine("\nQUESTION 2a\n");
Console.WriteLine(testString);

/*
The Decompress function does the opposite of the Compress function. It takes in a string as argument and expands the string with the integer
appended to each character that has an integer right next to it. 
*/


string Decompress(string input)
{
    char[] inputChars = input.ToCharArray();
    StringBuilder finalBuilder = new StringBuilder();
    

    for (int i = 0; i < inputChars.Length; i++)
    {
        
        if (IsNumber(inputChars[i].ToString())== false)
        {
            finalBuilder.Append(inputChars[i]);
        }

        else
        {
            finalBuilder.Append(Duplicate(input[i - 1].ToString(), input[i]));
        }
        
        
        
    }

    return finalBuilder.ToString();
}

/*
This Duplicate function returns a string. It takes in a string and a character as arguments and returns a string of duplicates 
of the string based. This character is expected to be a number that is a character in the string argument that the Decompress function
takes.  The duplicate function converts the number character to an integer with the Int32.Parse() method. This integer is then used
to limit the iteration in the for loop to append duplicates of the string input to a string builder. This stringbuilder is converted to a
string and returned in the function.
*/

string Duplicate(string input, char numberChar)
{
    
    StringBuilder sb = new StringBuilder();
    sb.Append(input.ToString());
    for(int i = 0; i < Int32.Parse(numberChar.ToString())-2; i++)
    {
        sb.Append(input);
    }
    return sb.ToString();
}

/*
This IsNumber function returns a boolean. Its an helper function used in the Decompress function to confirm if the current character
being looped is an integer when converted to a a string. if it returns true, the Duplicate function goes ahead to return the string of 
duplicates of the previous character in the character array and appends it to the stringbuilder.
Alternatively, i could have used the "char.IsDigit() method to confirm if a character is an integer but i found it after i already made
the function.
*/

bool IsNumber(string charString)
{
    
    bool isNumber = false;
    int integer;

    if (int.TryParse(charString, out integer) == true)
    {
       return isNumber = true;
    }
    return isNumber;
}



string testString2 = Decompress("RTF4YYUP3E3UU");
Console.WriteLine("\nQUESTION 2b\n");

Console.WriteLine(testString2);


