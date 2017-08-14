using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour 
{

    public static bool open = false;

    public Text consoleLog;
    private static Text _consoleLog;
    public Text consoleInput;
    private string text;

    void Start()
    {
        _consoleLog = consoleLog;    
    }

    public static void Log(string message)
    {
        _consoleLog.text += System.Environment.NewLine + message;
    }

    public void ConsoleInputEdit(string sth)
    {
        text = consoleInput.text;

        //Add Time
        if (text.Contains("AddTime(") && text[text.Length - 1] == ')')
        {
            string amount = "";
            for (int i = 8; i < text.Length - 1; i++)
            {
                amount += text[i];
            }
            DayNightCycle.AddTime(float.Parse(amount) * (3600 / DayNightCycle.REAL_TO_GAME_HOUR));
            Log(amount + " hours added to the game timer.");
        }
        //Help
        else if (text == "help")
        {
            Log("Change Movement Speed:");
            Log("\t set moveSpeed  <value> (DEFAULT: 5)");

            Log("Change Speed Boost:");
            Log("\t set speedBoost <value> (DEFAULT: 3)");

            Log("Change Jump Boost:");
            Log("\t set jumpBoost  <value> (DEFAULT: 35)");

            Log("Set Time:");
            Log("\t SetTime(DAY_START | DAY_PEAK_TIME | NIGHT_START | NIGHT_PEAK_TIME | day | night | <value>)");

            Log("Add Time:");
            Log("\t AddTime(<value>)");

            Log("Change player's mass:");
            Log("\t set playerMass <value> (DEFAULT: 1)");

            Log("Change combat sound:");
            Log("\t set combatSound <value> (RANGE: 0 - 3)");

        }
        //Set time
        else if (text.Contains("SetTime(") && text[text.Length - 1] == ')')
        {
            string amount = "";
            for (int i = 8; i < text.Length - 1; i++)
            {
                amount += text[i];
            }
            if (amount == "PEAK_TIME")
            {
                amount = DayNightCycle.DAY_PEAK_TIME.ToString();
            }
            else if (amount == "DAY_START")
            {
                amount = DayNightCycle.DAY_START.ToString();
            }
            else if (amount == "NIGHT_START")
            {
                amount = DayNightCycle.NIGHT_START.ToString();
            }  
            else if(amount == "day")
            {
                amount = (DayNightCycle.DAY_START + 2).ToString();
            }
            else if (amount == "night")
            {
                amount = (DayNightCycle.NIGHT_START + 2).ToString();
            }
            Log(DayNightCycle.SetTime(float.Parse(amount)));
        }
        //Jump Boost change.
        else if(text.Contains("set jumpBoost "))
        {
            string amount = "";
            for (int i = 14; i < text.Length; i++)
            {
                amount += text[i];
            }
            Player.jumpForce = float.Parse(amount);
            Log("jumpBoost variable set to " + amount);
        }
        //Movement Speed change.
        else if (text.Contains("set moveSpeed "))
        {
            string amount = "";
            for (int i = 14; i < text.Length; i++)
            {
                amount += text[i];
            }
            Player.moveSpeed = float.Parse(amount);
            Log("moveSpeed variable set to " + amount);
        }
        //Speed Boost change.
        else if (text.Contains("set speedBoost "))
        {
            string ammount = "";
            for (int i = 14; i < text.Length; i++)
            {
                ammount += text[i];
            }
            Player.speedBoost = float.Parse(ammount);
            consoleLog.text += System.Environment.NewLine + "speedBoost variable set to " + ammount;
        }
        //Player's mass change
        else if(text.Contains("set playerMass "))
        {
            string ammount = "";
            for(int i = 15; i < text.Length - 1; i++)
            {
                ammount += text[i];
            }
            PlayerMovement.rb.mass = int.Parse(ammount);
            Log("Player's mass set to " + ammount);
        }
        //Change combatSound
        else if(text.Contains("set combatSound "))
        {
            string ammount = "";
            for (int i = 16; i < text.Length; i++)
            {
                ammount += text[i];
            }
            print(ammount);
            AudioManager.selectedIndex = int.Parse(ammount);
            Log("combatSound changed to " + AudioManager._clips[int.Parse(ammount)].name);
        }
        //TO DO: GIVE ITEMS
        //Syntax error
        else
        {
            Log("Syntax error.");
        }

        consoleInput.text = "";
    }
}
