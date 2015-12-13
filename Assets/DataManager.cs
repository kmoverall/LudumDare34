using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    public List<RobotData> player1Data, player2Data;

    public float mutationRate = 0.2f;

    public RobotData player1Parent1, player1Parent2, player2Parent1, player2Parent2;

	// Use this for initialization
	void Start () {
        Object.DontDestroyOnLoad(this);
	}

    public void InitData()
    {
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

            player2Data[i].walkSpeed = genNormalRandom(RobotStats.AverageWalkSpeed, RobotStats.WalkSpeedDev);
            player2Data[i].dashSpeed = genNormalRandom(RobotStats.AverageDashSpeed, RobotStats.DashSpeedDev);
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
            player1Data[i].walkSpeed = randomSelect(player1Parent1.walkSpeed, player1Parent2.walkSpeed, genNormalRandom(RobotStats.AverageWalkSpeed, RobotStats.WalkSpeedDev));
            player1Data[i].dashSpeed = randomSelect(player1Parent1.dashSpeed, player1Parent2.dashSpeed, genNormalRandom(RobotStats.AverageDashSpeed, RobotStats.DashSpeedDev));
            player1Data[i].health = randomSelect(player1Parent1.health, player1Parent2.health, genNormalRandom(100, 10));
            player1Data[i].shieldPower = randomSelect(player1Parent1.shieldPower, player1Parent2.shieldPower, genNormalRandom(1, 0.25f));
            player1Data[i].punchStrength = randomSelect(player1Parent1.punchStrength, player1Parent2.punchStrength, genNormalRandom(RobotStats.AveragePunchStrength, RobotStats.PunchStrengthDev));
            player1Data[i].punchSpeed = randomSelect(player1Parent1.punchSpeed, player1Parent2.punchSpeed, genNormalRandom(1, 0.15f));
            player1Data[i].breakStrength = randomSelect(player1Parent1.breakStrength, player1Parent2.breakStrength, genNormalRandom(RobotStats.AverageBreakStrength, RobotStats.BreakStrengthDev));
            player1Data[i].breakSpeed = randomSelect(player1Parent1.breakSpeed, player1Parent2.breakSpeed, genNormalRandom(1, 0.15f));
            player1Data[i].reach = randomSelect(player1Parent1.reach, player1Parent2.reach, genNormalRandom(1, 0.15f));

            player2Data[i].walkSpeed = randomSelect(player2Parent1.walkSpeed, player2Parent2.walkSpeed, genNormalRandom(RobotStats.AverageWalkSpeed, RobotStats.WalkSpeedDev));
            player2Data[i].dashSpeed = randomSelect(player2Parent1.dashSpeed, player2Parent2.dashSpeed, genNormalRandom(RobotStats.AverageDashSpeed, RobotStats.DashSpeedDev));
            player2Data[i].health = randomSelect(player2Parent1.health, player2Parent2.health, genNormalRandom(100, 10));
            player2Data[i].shieldPower = randomSelect(player2Parent1.shieldPower, player2Parent2.shieldPower, genNormalRandom(1, 0.25f));
            player2Data[i].punchStrength = randomSelect(player2Parent1.punchStrength, player2Parent2.punchStrength, genNormalRandom(RobotStats.AveragePunchStrength, RobotStats.PunchStrengthDev));
            player2Data[i].punchSpeed = randomSelect(player2Parent1.punchSpeed, player2Parent2.punchSpeed, genNormalRandom(1, 0.15f));
            player2Data[i].breakStrength = randomSelect(player2Parent1.breakStrength, player2Parent2.breakStrength, genNormalRandom(RobotStats.AverageBreakStrength, RobotStats.BreakStrengthDev));
            player2Data[i].breakSpeed = randomSelect(player2Parent1.breakSpeed, player2Parent2.breakSpeed, genNormalRandom(1, 0.15f));
            player2Data[i].reach = randomSelect(player2Parent1.reach, player2Parent2.reach, genNormalRandom(1, 0.15f));
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
