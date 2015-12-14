using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RobotController : MonoBehaviour {

    enum Actions {Attack, Break, Block, Dash}

    public KeyCode backKey, forwardKey;
    public RobotData stats;
    float maxPower;
    public bool isInvulnerable, isBlocking;
    //[HideInInspector]
    public float currHealth, currPower;

    public float doubleTapWindow = 0.15f;

    public Slider healthBar;
    public Slider powerBar;

    bool allowDoubleBack = true;
    bool allowDoubleForward = true;
    bool breakerB = false;
    bool breakerF = false;
    Rigidbody2D hitBox;
    public BoxCollider2D punchBox, breakBox;
    public ParticleSystem punchPart, breakPart, dashPart, hitPart, blockPart;
    public Animator anim;

    Vector3 velocity;

	// Use this for initialization
	void Start () {
        hitBox = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currHealth = stats.health;
        maxPower = 100;
        currPower = maxPower;
        punchBox.transform.localScale = new Vector3(1, stats.reach, 1);
        breakBox.transform.localScale = new Vector3(1, stats.reach, 1);
	}

	// Update is called once per frame
	void Update () {
        velocity = hitBox.velocity;

        healthBar.value = currHealth / stats.health;
        powerBar.value = currPower / maxPower;

        anim.SetBool("BlockPressed", false);
        anim.SetBool("AttackPressed", false);
        anim.SetBool("DoubleForward", false);
        anim.SetBool("DoubleBackward", false);
        if (breakerB && Input.GetKeyDown(forwardKey))
        {
            breakerB = false;
            anim.SetBool("BreakerGrace", true);
        }
        else if (breakerF && Input.GetKeyDown(backKey))
        {
            breakerF = false;
            anim.SetBool("BreakerGrace", true);
        }
        else
        {
            breakerB = false;
            breakerF = false;
            anim.SetBool("BreakerGrace", false);
        }


        if (Input.GetKeyDown(backKey))
        {
            anim.SetBool("BlockHeld", true);
            anim.SetBool("BlockPressed", true);
            breakerB = true;
            if (allowDoubleBack)
            {
                allowDoubleBack = false;
                StartCoroutine("BackDashTimer");
            }

        } else if (Input.GetKeyUp(backKey))
        {
            anim.SetBool("BlockHeld", false);
        }

        if (Input.GetKeyDown(forwardKey))
        {
            anim.SetBool("AttackPressed", true);
            breakerF = true;
            if (allowDoubleForward)
            {
                allowDoubleForward = false;
                StartCoroutine("ForwardDashTimer");
            }
        }

        if (currHealth <= 0 || currPower <= 0)
        {
            anim.SetTrigger("Dead");
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        RobotController opp = other.GetComponent<RobotController>();
        if (!other.isTrigger) {
            if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking 1")) && !opp.isBlocking && !opp.isInvulnerable)
            {
                opp.anim.SetTrigger("Hit");
                opp.currHealth -= stats.punchStrength;
                StartCoroutine("TimeHitch");
            }
            else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Breaking") || anim.GetCurrentAnimatorStateInfo(0).IsName("Breaking 1")) && !opp.isBlocking && !opp.isInvulnerable)
            {
                opp.anim.SetTrigger("Hit");
                opp.currHealth -= stats.breakStrength;
                StartCoroutine("TimeHitch");
            }
            else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking 1")) && opp.isBlocking && !opp.isInvulnerable)
            {
                opp.anim.SetTrigger("Block");
                StartCoroutine("TimeHitch");
            }
            else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Breaking") || anim.GetCurrentAnimatorStateInfo(0).IsName("Breaking 1")) && opp.isBlocking && !opp.isInvulnerable)
            {
                opp.anim.SetTrigger("Hit");
                opp.currHealth -= stats.breakStrength;
                StartCoroutine("TimeHitch");
            }
        }
    }

    IEnumerator TimeHitch()
    {
        Time.timeScale = 0.01f;
        for (int i = 0; i < 5; i++)
        {
            yield return null;
        }
        Time.timeScale = 1.0f;
            
    }

    IEnumerator ForwardDashTimer()
    {
        for (float t = 0; t < doubleTapWindow; t += Time.deltaTime)
        {
            if (Input.GetKeyDown(forwardKey) && t != 0)
            {
                anim.SetBool("DoubleForward", true);
                break;
            }
            if (Input.GetKeyDown(backKey))
            {
                break;
            }
            yield return null;
        }
        allowDoubleForward = true;
    }

    IEnumerator BackDashTimer()
    {
        for (float t = 0; t < doubleTapWindow; t += Time.deltaTime)
        {
            if(Input.GetKeyDown(backKey) && t != 0)
            {
                anim.SetBool("DoubleBackward", true);
                break;
            }
            if(Input.GetKeyDown(forwardKey))
            {
                break;
            }
            yield return null;
        }
        allowDoubleBack = true;
    }

    void Move()
    {
        hitBox.velocity = new Vector3(stats.walkSpeed, 0);
    }

    void Dash()
    {
        hitBox.velocity = new Vector3(stats.dashSpeed, 0);
    }

    void MoveBack()
    {
        hitBox.velocity = new Vector3(-0.5f * stats.walkSpeed, 0);
    }

    void DashBack()
    {
        hitBox.velocity = new Vector3(-1.5f * stats.dashSpeed, 0);
    }

    public void Stop()
    {
        hitBox.velocity = Vector3.zero;
    }

    void KnockBack()
    {
        hitBox.velocity = new Vector3(-0.7f * stats.walkSpeed, 0);
    }

    void ResetSpeed()
    {
        anim.speed = 1;
    }

    void BreakSpeed()
    {
        anim.speed = stats.breakSpeed;
    }
    
    void PunchSpeed()
    {
        anim.speed = stats.punchSpeed;
    }

    void PlayPunchSpark()
    {
        punchPart.Play();
    }

    void PlayBreakSpark()
    {
        breakPart.Play();
    }

    void PlayDashSpark()
    {
        dashPart.Play();
    }

    void PlayHitSpark()
    {
        hitPart.Play();
    }

    void PlayBlockPart()
    {
        blockPart.Play();
    }

    void StopParticles()
    {
        punchPart.Stop();
        breakPart.Stop();
    }

    void DrainPower(Actions act)
    {
        switch (act)
        {
            case Actions.Attack:
                currPower -= Mathf.Sqrt(stats.punchSpeed * stats.punchSpeed + (stats.punchStrength / RobotStats.AveragePunchStrength) * (stats.punchStrength / RobotStats.AveragePunchStrength) + stats.reach * stats.reach) * RobotStats.PunchDrain / Mathf.Sqrt(3);
                break;
            case Actions.Break:
                currPower -= Mathf.Sqrt(stats.breakSpeed * stats.breakSpeed + (stats.breakStrength / RobotStats.AverageBreakStrength) * (stats.breakStrength / RobotStats.AverageBreakStrength) + stats.reach * stats.reach) * RobotStats.BreakDrain / Mathf.Sqrt(3);
                break;
            case Actions.Block:
                currPower -= stats.shieldPower * RobotStats.BlockDrain;
                break;
            case Actions.Dash:
                currPower -= Mathf.Sqrt((stats.dashSpeed / RobotStats.AverageDashSpeed) * (stats.dashSpeed / RobotStats.AverageDashSpeed) + (stats.walkSpeed / RobotStats.AverageWalkSpeed) * (stats.walkSpeed / RobotStats.AverageWalkSpeed)) * RobotStats.DashDrain / Mathf.Sqrt(2);
                break;
        }
    }
}
