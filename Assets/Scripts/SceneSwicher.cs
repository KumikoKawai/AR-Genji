using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwicher : MonoBehaviour {

	public static int st=1;

	public static AsyncOperation async;

	IEnumerator Start(){

		// 非同期でロード開始
		if(async == null){
			async = SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);
			async.allowSceneActivation = false;
			yield return async;
		}
	}

	public void LoadMain(int number) {
			switch (number) {
				case 1:
					st = 1;
				break;
				case 2:
					st = 2;
				break;
				case 3:
					st = 3;
				break;
			}
			async.allowSceneActivation = true;
	}
}
