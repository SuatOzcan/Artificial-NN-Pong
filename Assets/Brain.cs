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
	 * Ball X                     Paddle velocity on y  axis.
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
	List<double> Run(double bx, double by,double bvx,double bvy, double px, double py,double pv,bool train)
    {
		List<double> inputs = new List<double>();
		List<double> outputs = new List<double>();
		inputs.Add(bx);
		inputs.Add(by);
		inputs.Add(bvx);
		inputs.Add(bvy);
		inputs.Add(px);
		inputs.Add(py);
		outputs.Add(pv);
		if (train)
		{
			return (ann.Train(inputs, outputs));
		}
		else
			return (ann.CalcOutput(inputs, outputs));

	}
	// Update is called once per frame
	void Update () {
		float posy = Mathf.Clamp(paddle.transform.position.y + (yvel * Time.deltaTime * paddleMaxSpeed), paddleMinY, paddleMaxY);
		paddle.transform.position = new Vector3(paddle.transform.position.x, posy, paddle.transform.position.z);
		
		List<double> output = new List<double>();
		int layerMask = 1 << 8;
        RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, brb.velocity, 1000, layerMask);

		if (hit.collider != null)
		{
			if (hit.collider.gameObject.tag == "tops")
			{
				Vector3 reflection = Vector3.Reflect(brb.velocity, hit.normal);
				hit = Physics2D.Raycast(hit.point, reflection, 1000, layerMask);
			}

			if (hit.collider != null && hit.collider.gameObject.tag == "backwall")
			{
				float dy = (hit.point.y - paddle.transform.position.y);
				output = Run(ball.transform.position.x, ball.transform.position.y, brb.velocity.x, brb.velocity.y,
							paddle.transform.position.x, paddle.transform.position.y, dy, true);
				yvel = (float)output[0];
			}
		}
		else
		{
			yvel = 0;
		}
	
    }
}
