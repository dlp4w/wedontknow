using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadonclick : MonoBehaviour {

	public void LoadScene(int level) {
		SceneManager.LoadScene (level);
	}
}
