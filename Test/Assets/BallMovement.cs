using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BallMovement : MonoBehaviour {

	GameObject[] walls;
	GameObject Ball;
	GameObject Wall_Left;
	List<Rectangle> wallboundingboxes;
	List<Rectangle> itemboundingboxes;
	GameObject[] _items;
	string game_msg;

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
	
		game_msg = "";
		Ball = GameObject.Find("s_t");
		Wall_Left = GameObject.Find ("wall_left");
		//walls = new GameObject[9];
		var walls = GameObject.FindGameObjectsWithTag("wall");
		var items = GameObject.FindGameObjectsWithTag ("item");
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
		wallboundingboxes = new List<Rectangle>();
		itemboundingboxes = new List<Rectangle> ();	

		int counter = 0;

		// gather walls
		while(counter < walls.Length) {

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

			wallboundingboxes.Add (r);
			//Debug.Log (counter);

			counter++;

		}

		counter = 0;

		// gather items
		while (counter < items.Length) {

			Rectangle r = new Rectangle(0f, 0f, 0f, 0f);

			r.x = items [counter].transform.position.x;
			r.y = items [counter].transform.position.y;
			r.width = items [counter].transform.lossyScale.x;
			r.height = items [counter].transform.lossyScale.y;

			if(r.width < 0)
				r.width *= -1;
			else
				r.width *= 1;


			if (r.height < 0)
				r.height *= -1;
			else
				r.height *= 1;

			itemboundingboxes.Add (r);
			//Debug.Log (counter);

			counter++;

		}

		/*foreach(Rectangle r in wallboundingboxes)
			Debug.Log ("X-Coord: " + r.x + Environment.NewLine +
				       "Y-Coord: " + r.y + Environment.NewLine +
					   "Width: " + r.width + Environment.NewLine + 
					   "Height: " + r.height);*/

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.E)) {

			if(CheckCollision(Ball, 0) == 2 || CheckCollision(Ball, 1) == 2 || 
				CheckCollision(Ball, 2) == 2 || CheckCollision(Ball, 3) == 2)
			{

				var items = GameObject.FindGameObjectsWithTag ("item");

				// check which item is closer and pick up that one
				int del = 0;
				int counter = 0;
				float distance = -1;
				foreach (GameObject i in items) {

					if (distance == -1) {
						del = counter;
						distance = (float)Math.Sqrt ((i.transform.position.x - Ball.transform.position.x)*(i.transform.position.x - Ball.transform.position.x) +
							(i.transform.position.y - Ball.transform.position.y)*(i.transform.position.y - Ball.transform.position.y));
					} else {

						float tmp_dist = (float)Math.Sqrt ((i.transform.position.x - Ball.transform.position.x)*(i.transform.position.x - Ball.transform.position.x) +
							(i.transform.position.y - Ball.transform.position.y)*(i.transform.position.y - Ball.transform.position.y));

						if (tmp_dist < distance) {
							distance = tmp_dist;
							del = counter;
						}

					}


					counter++;

				}

				//var item = GameObject.Find("Door_Key");
				//var name = item.name;
				//items[del].
				game_msg = "Item Grabbed: " + items[del].name;
				//Debug.Log ("Item Grabbed: " + name);
				//var items = new List<GameObject>(GameObject.FindGameObjectsWithTag ("item"));
				//int index = items.FindIndex (obj => obj.name == "Door_Key");
				itemboundingboxes.RemoveAt (del/*index*/);
				Destroy(items[del], 0);


			}

		}

		if (Input.GetKey (KeyCode.W)) {

			if (CheckCollision (Ball, 0) == 1) {
				//Debug.Log ("Wall is Hit!");
			} else if (CheckCollision (Ball, 0) == 2) {

				Debug.Log ("Item Found!");
				Ball.transform.Translate (0, .1f, 0);

			} /*else if (CheckCollision (Ball, 0) == 3) {



			}*/
			else
				Ball.transform.Translate (0, .1f, 0);

		}

		if (Input.GetKey (KeyCode.A)) {

			if (CheckCollision (Ball, 2) == 1) {
				//Debug.Log ("Wall is Hit!");
			} else if (CheckCollision (Ball, 2) == 2) {

				Debug.Log ("Item Found!");
				Ball.transform.Translate (-.1f, 0, 0);

			}
			else
				Ball.transform.Translate (-.1f, 0, 0);

		}

		if (Input.GetKey (KeyCode.S)) {

			if (CheckCollision (Ball, 1) == 1) {
				//Debug.Log ("Wall is Hit!");
			}
			else if (CheckCollision (Ball, 1) == 2) {

				Debug.Log ("Item Found!");
				Ball.transform.Translate (0, -.1f, 0);

			}
			else
				Ball.transform.Translate (0, -.1f, 0);

		}

		if (Input.GetKey (KeyCode.D)) {

			if (CheckCollision (Ball, 3) == 1) {
				//Debug.Log ("Wall is Hit!");
			}
			else if (CheckCollision (Ball, 3) == 2) {

				Debug.Log ("Item Found!");
				Ball.transform.Translate (.1f, 0, 0);

			}
			else
				Ball.transform.Translate (.1f, 0, 0);

		}

	}

	// 0 = no collision; 1 = wall collision; 2 = item collision; 3 = wall and item collision
	public int CheckCollision(GameObject player, int choice)
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

		foreach (Rectangle r in wallboundingboxes) {

			if ( (current.x >= r.x - r.width/2 && current.x <= r.x + r.width/2) && (current.y >= r.y - r.height/2 && current.y <= r.y + r.height/2) ) {
				wall_collision = true;
				break;
			}
				
		}

		foreach (Rectangle r in itemboundingboxes) {

			if ( (current.x >= (r.x - r.width/2)-.5 && current.x <= (r.x + r.width/2)+.5) && (current.y >= (r.y - r.height/2)-.5 && current.y <= (r.y + r.height/2)+.5) ) {
				item_collision = true;
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

	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.name == "wall_top")  // or if(gameObject.CompareTag("YourWallTag"))
		{
			Debug.Log ("Hit a wall!");
		}
	}

	void OnGUI()
	{

		GUI.Label( new Rect(0, 0,200f,100f) , game_msg);

	}

}
