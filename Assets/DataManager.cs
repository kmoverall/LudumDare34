using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    public List<RobotData> player1Data, player2Data;

    public float mutationRate = 0.2f;

    public RobotData player1Parent, player2Parent;

	// Use this for initialization
    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public void InitData()
    {
        player1Data.Clear();
        player2Data.Clear();
        for (int i = 0; i < 7; i++)
        {
            player1Data.Add(new RobotData());
            player2Data.Add(new RobotData());

            player1Data[i].walkSpeed = genNormalRandom(RobotStats.AverageWalkSpeed, RobotStats.WalkSpeedDev);
            player1Data[i].dashSpeed = genNormalRandom(RobotStats.AverageDashSpeed, RobotStats.DashSpeedDev);
            player1Data[i].health = genNormalRandom(100, 10);
            player1Data[i].shieldPower = genNormalRandom(1, 0.25f);
            player1Data[i].punchStrength = genNormalRandom(RobotStats.AveragePunchStrength, RobotStats.PunchStrengthDev);
            player1Data[i].punchSpeed = genNormalRandom(1, 0.15f);
            player1Data[i].breakStrength = genNormalRandom(RobotStats.AverageBreakStrength, RobotStats.BreakStrengthDev);
            player1Data[i].breakSpeed = genNormalRandom(1, 0.15f);
            player1Data[i].reach = genNormalRandom(1, 0.15f);

            player2Data[i].walkSpeed = genNormalRandom(RobotStats.AverageWalkSpeed, RobotStats.WalkSpeedDev) * -1;
            player2Data[i].dashSpeed = genNormalRandom(RobotStats.AverageDashSpeed, RobotStats.DashSpeedDev) * -1;
            player2Data[i].health = genNormalRandom(100, 10);
            player2Data[i].shieldPower = genNormalRandom(1, 0.25f);
            player2Data[i].punchStrength = genNormalRandom(RobotStats.AveragePunchStrength, RobotStats.PunchStrengthDev);
            player2Data[i].punchSpeed = genNormalRandom(1, 0.15f);
            player2Data[i].breakStrength = genNormalRandom(RobotStats.AverageBreakStrength, RobotStats.BreakStrengthDev);
            player2Data[i].breakSpeed = genNormalRandom(1, 0.15f);
            player2Data[i].reach = genNormalRandom(1, 0.15f);
        }
    }

    public void InitNextGen()
    {
        for (int i = 0; i < 7; i++)
        {
            player1Data[i].walkSpeed = randomSelect(player1Parent.walkSpeed, player1Data[i].walkSpeed, genNormalRandom(RobotStats.AverageWalkSpeed, RobotStats.WalkSpeedDev));
            player1Data[i].dashSpeed = randomSelect(player1Parent.dashSpeed, player1Data[i].dashSpeed, genNormalRandom(RobotStats.AverageDashSpeed, RobotStats.DashSpeedDev));
            player1Data[i].health = randomSelect(player1Parent.health, player1Data[i].health, genNormalRandom(100, 20));
            player1Data[i].shieldPower = randomSelect(player1Parent.shieldPower, player1Data[i].shieldPower, genNormalRandom(1, 0.25f));
            player1Data[i].punchStrength = randomSelect(player1Parent.punchStrength, player1Data[i].punchStrength, genNormalRandom(RobotStats.AveragePunchStrength, RobotStats.PunchStrengthDev));
            player1Data[i].punchSpeed = randomSelect(player1Parent.punchSpeed, player1Data[i].punchSpeed, genNormalRandom(1, 0.2f));
            player1Data[i].breakStrength = randomSelect(player1Parent.breakStrength, player1Data[i].breakStrength, genNormalRandom(RobotStats.AverageBreakStrength, RobotStats.BreakStrengthDev));
            player1Data[i].breakSpeed = randomSelect(player1Parent.breakSpeed, player1Data[i].breakSpeed, genNormalRandom(1, 0.2f));
            player1Data[i].reach = randomSelect(player1Parent.reach, player1Data[i].reach, genNormalRandom(1, 0.4f));

            player2Data[i].walkSpeed = randomSelect(player2Parent.walkSpeed, player2Data[i].walkSpeed, genNormalRandom(RobotStats.AverageWalkSpeed, RobotStats.WalkSpeedDev) * -1);
            player2Data[i].dashSpeed = randomSelect(player2Parent.dashSpeed, player2Data[i].dashSpeed, genNormalRandom(RobotStats.AverageDashSpeed, RobotStats.DashSpeedDev) * -1);
            player2Data[i].health = randomSelect(player2Parent.health, player2Data[i].health, genNormalRandom(100, 20));
            player2Data[i].shieldPower = randomSelect(player2Parent.shieldPower, player2Data[i].shieldPower, genNormalRandom(1, 0.25f));
            player2Data[i].punchStrength = randomSelect(player2Parent.punchStrength, player2Data[i].punchStrength, genNormalRandom(RobotStats.AveragePunchStrength, RobotStats.PunchStrengthDev));
            player2Data[i].punchSpeed = randomSelect(player2Parent.punchSpeed, player2Data[i].punchSpeed, genNormalRandom(1, 0.2f));
            player2Data[i].breakStrength = randomSelect(player2Parent.breakStrength, player2Data[i].breakStrength, genNormalRandom(RobotStats.AverageBreakStrength, RobotStats.BreakStrengthDev));
            player2Data[i].breakSpeed = randomSelect(player2Parent.breakSpeed, player2Data[i].breakSpeed, genNormalRandom(1, 0.2f));
            player2Data[i].reach = randomSelect(player2Parent.reach, player2Data[i].reach, genNormalRandom(1, 0.4f));
        }
    }

    public float genNormalRandom(float mean, float stddev)
    {
        float u = Random.value;
        float v = Random.value;
        float r = Mathf.Sqrt(-2 * Mathf.Log(u)) * Mathf.Cos(2 * Mathf.PI * v);
        return r * stddev + mean;
    }

    public float randomSelect(float a, float b, float m)
    {
        float result = 0;
        if (Random.value < 0.5)
        {
            result = a;
        } else
        {
            result = b;
        }
        if (Random.value < mutationRate)
        {
            result = m;
        }

        return result;
    }
}
