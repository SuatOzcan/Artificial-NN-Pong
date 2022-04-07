using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

	public GameObject paddle;
	public GameObject ball;
	Rigidbody2D brb;
	float yvel; //y velocity.
	float paddleMinY = 8.8f;
	float paddleMaxY = 17.4f;
	float paddleMaxSpeed = 15;
    public float numSaved = 0;
	public float numMissed = 0;

	/* Inputs                     Outputs
	 * Ball X                     Paddle velocity y 
	 * Ball y
	 * Ball velocity X
	 * ball velocity y
	 * paddle X
	 * paddle y
	 */

	ANN ann;
     	 

    // Use this for initialization
    void Start () {
		ann = new ANN(6, 1, 1, 4, 0.11);
		brb = ball.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
