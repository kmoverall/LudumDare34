using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Text readyText, p1Text, p2Text, vicText, roundTxt, p1Counter, p2Counter, genText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void UpdateGameText (int round, int p1wins, int p2wins, bool p1ready, bool p2ready, bool ready, int winner, int gen) {
        roundTxt.text = "Round " + round + " / 7";
        p1Text.enabled = !p1ready;
        p2Text.enabled = !p2ready;
        readyText.enabled = !ready;
        p1Counter.text = "Wins: " + p1wins;
        p2Counter.text = "Wins: " + p2wins;
        genText.text = "Generation " + gen;

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
