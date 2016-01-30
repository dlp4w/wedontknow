using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BallMovement : MonoBehaviour {

	GameObject[] walls;
	GameObject Ball;
	GameObject Wall_Left;
	List<Rectangle> boundingboxes;

	public class Rectangle
	{

		public float x;
		public float y;
		public float width;
		public float height;

		public Rectangle(){}
		public Rectangle(float x1, float y1, float width1, float height1)
		{ 
			x = x1;
			y = y1;
			width = width1;
			height = height1;

		}


	}

	public class Point
	{

		public float x;
		public float y;

		public Point ()
		{
		}

		public Point (float a, float b)
		{
			x = a;
			y = b;
		}

	}

	// Use this for initialization
	void Start () {
	
		Ball = GameObject.Find("s_t");
		Wall_Left = GameObject.Find ("wall_left");
		//walls = new GameObject[9];
		var walls = GameObject.FindGameObjectsWithTag("wall");
		//GameObject t = GameObject.FindGameObjectWithTag("Untagged");
		//Debug.Log (t.transform.position.x);
		Debug.Log (walls.Length);

		/*GameObject[] objects = GameObject.FindObjectOfType (GameObject);
		foreach (GameObject go in objects) {
			if (go.tag=="Untagged") {
				Debug.Log ("Wall Detected!");
			}
		}*/

		//Debug.Log (walls.Length);
		//Wall_Left.collider2D.transform.
		boundingboxes = new List<Rectangle>();
			
		int counter = 0;

		while(counter < 9) {

			Rectangle r = new Rectangle(0f, 0f, 0f, 0f);

			r.x = walls [counter].transform.position.x;
			r.y = walls [counter].transform.position.y;
			r.width = walls [counter].transform.lossyScale.x;
			r.height = walls [counter].transform.lossyScale.y;

			if(r.width < 0)
				r.width *= -1;
			else
				r.width *= 1;


			if (r.height < 0)
				r.height *= -1;
			else
				r.height *= 1;

			boundingboxes.Add (r);
			//Debug.Log (counter);

			counter++;

		}

		foreach(Rectangle r in boundingboxes)
			Debug.Log ("X-Coord: " + r.x + Environment.NewLine +
				       "Y-Coord: " + r.y + Environment.NewLine +
					   "Width: " + r.width + Environment.NewLine + 
					   "Height: " + r.height);

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey (KeyCode.W)) {

			if (CheckWallCollision (Ball, boundingboxes, 0) == 1) {
				//Debug.Log ("Wall is Hit!");
			}
			else
				Ball.transform.Translate (0, .1f, 0);

		}

		if (Input.GetKey (KeyCode.A)) {

			if (CheckWallCollision (Ball, boundingboxes, 2) == 1) {
				//Debug.Log ("Wall is Hit!");
			}
			else
				Ball.transform.Translate (-.1f, 0, 0);

		}

		if (Input.GetKey (KeyCode.S)) {

			if (CheckWallCollision (Ball, boundingboxes, 1) == 1) {
				//Debug.Log ("Wall is Hit!");
			}
			else
				Ball.transform.Translate (0, -.1f, 0);

		}

		if (Input.GetKey (KeyCode.D)) {

			if (CheckWallCollision (Ball, boundingboxes, 3) == 1) {
				//Debug.Log ("Wall is Hit!");
			}
			else
				Ball.transform.Translate (.1f, 0, 0);

		}

	}

	// 0 = no collision; 1 = wall collision; 2 = item collision; 3 = wall and item collision
	public int CheckWallCollision(GameObject player, List<Rectangle> walls, int choice)
	{

		//Debug.Log ("Player Pos: X=" + player.transform.position.x + " | Y=" + player.transform.position.y);

		bool collision = false;

		float x = player.transform.position.x;
		float y = player.transform.position.y;
		float half_width = player.transform.lossyScale.x / 2;
		float half_height = player.transform.lossyScale.y / 2;

		Point top = new Point(0f, 0f);
		Point bottom = new Point(0f, 0f);
		Point left = new Point(0f, 0f);
		Point right = new Point(0f, 0f);

		top.x = x;
		top.y = y + half_height;

		bottom.x = x;
		bottom.y = y - half_height;

		left.x = x - half_width;
		left.y = y;

		right.x = x + half_width;
		right.y = y;

		Point current = new Point (0f, 0f);

		switch (choice) {

			case 0:
				current = top;
				break;
			case 1:
				current = bottom;
				break;
			case 2:
				current = left;
				break;
			case 3:
				current = right;
				break;
			default:
				break;

		}

		bool wall_collision = false;
		bool item_collision = false;

		foreach (Rectangle r in walls) {

			if ( (current.x >= r.x - r.width/2 && current.x <= r.x + r.width/2) && (current.y >= r.y - r.height/2 && current.y <= r.y + r.height/2) ) {
				wall_collision = true;
				break;
			}
				
		}

		if (wall_collision && item_collision)
			return 3;
		else if (item_collision)
			return 2;
		else if (wall_collision)
			return 1;
		else
			return 0;

	}

}
