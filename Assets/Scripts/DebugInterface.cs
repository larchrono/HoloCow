using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInterface : MonoBehaviour {

	public static DebugInterface current;

	void Awake(){
		current = this;
	}

	public Text DebugTestObj;

	public static void DebugMsg(string msg){
		if (current.DebugTestObj == null)
			return;
		if (current.DebugTestObj.text.Length > 200)
			current.DebugTestObj.text = "";
		current.DebugTestObj.text += "\n" + msg;
	}

	public void SliderValueShow(float src){
		if (current.DebugTestObj == null)
			return;
		if (current.DebugTestObj.text.Length > 200)
			current.DebugTestObj.text = "";
		current.DebugTestObj.text += "\n" + "Setting To : " + src.ToString("F2") ;
	}
}
