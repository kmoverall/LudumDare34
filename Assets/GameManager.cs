using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    public RobotController player1, player2;
    DataManager data;
    bool p1Ready = false;
    bool p2Ready = false;
    bool ready = false;
    UIManager ui;
    int round = 1;
    int p1Wins = 0;
    int p2Wins = 0;
    int generation = 1;
    int winner = 0;

    bool inFight = true;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        data = GameObject.FindObjectOfType<DataManager>().GetComponent<DataManager>();
        ui = GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>();
        player1 = GameObject.FindWithTag("Player1").GetComponent<RobotController>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<RobotController>();

        if (data.player1Data.Count == 0 || data.player2Data.Count == 0)
        {
            data.InitData();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (data == null)
        {
            data = GameObject.FindObjectOfType<DataManager>().GetComponent<DataManager>();
            if (data.player1Data.Count == 0 || data.player2Data.Count == 0)
            {
                data.InitData();
            }
        }
        if (ui == null)
        {
            ui = GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>();
        }
        if (player1 == null)
        {
            player1 = GameObject.FindWithTag("Player1").GetComponent<RobotController>();
        }
        if (player2 == null)
        {
            player2 = GameObject.FindWithTag("Player2").GetComponent<RobotController>();
        }


        if (inFight)
        {
            player1.enabled = true;
            player2.enabled = true;

            if (!ready)
            {
                player1.anim.enabled = false;
                player2.anim.enabled = false;
                player1.stats = data.player1Data[round - 1];
                player2.stats = data.player2Data[round - 1];
                player1.currHealth = player1.stats.health;
                player2.currHealth = player2.stats.health;
            }
            if (!p1Ready && Input.GetKeyDown(player1.forwardKey) && Input.GetKeyDown(player1.backKey))
            {

                p1Ready = true;
            }
            if (!p2Ready && Input.GetKeyDown(player2.forwardKey) && Input.GetKeyDown(player2.backKey))
            {
                p2Ready = true;
            }
            if (p1Ready && p2Ready)
            {
                ready = true;
                player1.anim.enabled = true;
                player2.anim.enabled = true;
            }

            if (player2.currHealth <= 0)
            {
                winner = 1;
                inFight = false;
                p1Wins += 1;
                StartCoroutine("EndRound");
            }
            if (player2.currPower <= 0)
            {
                winner = 2;
                inFight = false;
                p1Wins += 1;
                StartCoroutine("EndRound");
            }
            if (player1.currHealth <= 0)
            {
                winner = 3;
                inFight = false;
                p2Wins += 1;
                StartCoroutine("EndRound");
            }
            if (player1.currPower <= 0)
            {
                winner = 4;
                inFight = false;
                p2Wins += 1;
                StartCoroutine("EndRound");
            }

            ui.UpdateGameText(round, p1Wins, p2Wins, p1Ready, p2Ready, ready, winner, generation);
        }

        //Generate New Sprite

        //Boot to Evolve Menu

        //Implement Evolve Menu
	}

    IEnumerator EndRound()
    {
        //Time.timeScale = 0.5f;
        for (float t = 0; t < 5; t += Time.deltaTime)
            yield return null;
        p1Ready = false;
        p2Ready = false;
        ready = false;
        inFight = true;
        round += 1;
        winner = 0;

        if (round > 7)
        {
            round = 1;
        }

        //Application.LoadLevel(Application.loadedLevelName);
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}
