using UnityEngine;
using System.Collections;

public class BitControl1 : MonoBehaviour {

	public float speed = 1f;
	public float maxSpeed = 1f;	

	private Animator anim;

	void Update()
	{
		Movement ();
	}

	void Movement()
	{
		var bit = GameObject.Find("Bit");
		anim = gameObject.GetComponent<Animator> ();

		if(Input.GetKey (KeyCode.D)) {
			bit.transform.Translate (Vector2.right * speed * (Time.deltaTime));
			anim.Play ("Bit_WalkRight");

		}

		if (Input.GetKey (KeyCode.A)) {
			bit.transform.Translate (-Vector2.right * speed * (Time.deltaTime));
			anim.Play ("Bit_WalkLeft");	
		}

		if (Input.GetKey (KeyCode.W)) {
			bit.transform.Translate (Vector2.up * speed * (Time.deltaTime));
			anim.Play ("Bit_WalkUp");
		}

		if (Input.GetKey (KeyCode.S)) {
			bit.transform.Translate (Vector2.down * speed * (Time.deltaTime));
			anim.Play ("Bit_WalkDown");
		}
	}
}
