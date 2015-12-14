using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Text readyText, p1Text, p2Text, vicText, roundTxt, p1Counter, p2Counter, genText, p1Stats, p2Stats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void UpdateGameText (int round, int p1wins, int p2wins, bool p1ready, bool p2ready, bool ready, int winner, int gen, RobotData p1, RobotData p2) {
        roundTxt.text = "Round " + round + " / 7";
        p1Text.enabled = !p1ready;
        p2Text.enabled = !p2ready;
        readyText.enabled = !ready;
        p1Counter.text = "Wins: " + p1wins;
        p2Counter.text = "Wins: " + p2wins;
        genText.text = "Generation " + gen;

        p1Stats.text = "Walk Speed: " + p1.walkSpeed.ToString("0.00");
        p1Stats.text += "\nDash Speed: " + p1.dashSpeed.ToString("0.00");
        p1Stats.text += "\nHealth: " + p1.health.ToString("0.00");
        p1Stats.text += "\nShield: " + (1/p1.shieldPower).ToString("0.00");
        p1Stats.text += "\nAtk Strength: " + p1.punchStrength.ToString("0.00");
        p1Stats.text += "\nAtk Speed: " + p1.punchSpeed.ToString("0.00");
        p1Stats.text += "\nBreaker Str: " + p1.breakStrength.ToString("0.00");
        p1Stats.text += "\nBreaker Spd: " + p1.breakSpeed.ToString("0.00");
        p1Stats.text += "\nRange: " + p1.reach.ToString("0.00");

        p2Stats.text = "Walk Speed: " + (p2.walkSpeed * -1).ToString("0.00");
        p2Stats.text += "\nDash Speed: " + (p2.dashSpeed * -1).ToString("0.00");
        p2Stats.text += "\nHealth: " + p2.health.ToString("0.00");
        p2Stats.text += "\nShield: " + (1 / p2.shieldPower).ToString("0.00");
        p2Stats.text += "\nAtk Strength: " + p2.punchStrength.ToString("0.00");
        p2Stats.text += "\nAtk Speed: " + p2.punchSpeed.ToString("0.00");
        p2Stats.text += "\nBreaker Str: " + p2.breakStrength.ToString("0.00");
        p2Stats.text += "\nBreaker Spd: " + p2.breakSpeed.ToString("0.00");
        p2Stats.text += "\nRange: " + p2.reach.ToString("0.00");






        if (winner == 0)
        {
            vicText.enabled = false;
        } 
        else if (winner == 1)
        {
            vicText.enabled = true;
            vicText.text = "P2 Destroyed\nPlayer 1 Wins!";
        }
        else if (winner == 2)
        {
            vicText.enabled = true;
            vicText.text = "P2 Out of Power\nPlayer 1 Wins!";
        }
        else if (winner == 3)
        {
            vicText.enabled = true;
            vicText.text = "P1 Destroyed\nPlayer 2 Wins!";
        }
        else if (winner == 4)
        {
            vicText.enabled = true;
            vicText.text = "P1 Out of Power\nPlayer 2 Wins!";
        }

	}
}
