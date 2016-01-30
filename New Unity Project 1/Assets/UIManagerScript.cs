using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class UIManagerScript : MonoBehaviour {

	public void StartGame(int level) {
		SceneManager.LoadScene (level);
	}
}
