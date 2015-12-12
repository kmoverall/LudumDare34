using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

    enum RobotState { Moving, Blocking, Attacking, Breaking, Dashing };

    public KeyCode leftKey, rightKey;
    public float walkSpeed, punchSpeed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    void SetState()
    {

    }
}
