using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicSystem : MonoBehaviour {

	public static LogicSystem current;

	public GameObject PlayerCamera;
	public float BallMoveSpeed;

	public GameObject currentMaze;

	public GameObject _prefabImageSelect;
	public GameObject nowSelect;

	void Awake(){
		current = this;
	}

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Input.gyro.enabled = true;

		BallMoveSpeed = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SliderModifySpeed(float src){
		BallMoveSpeed = src;
	}

	public void SaveToIP_History(string src){
		PlayerPrefs.SetString ("ToIP",src);
	}

	public void UI_SelectedRect(Button btn){
		if (nowSelect == null)
			nowSelect = Instantiate (_prefabImageSelect);
		nowSelect.transform.SetParent (btn.transform, false);
	}

}
