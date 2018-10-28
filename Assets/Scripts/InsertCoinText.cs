using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertCoinText : MonoBehaviour {

	const float max_edge = 1.25f;
	const float min_edge = -0.25f;

	public float slideSpeed;

	public Text mainText;
	public Text subText;

	public Monitor monitor;
	public bool flipSlide;

	public enum Monitor {
		UP,
		DOWN,
		LEFT,
		RIGHT
	}



	RectTransform trans;
	// Use this for initialization
	void Start () {
		trans = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ModelControl.current.inPlaying) {
			mainText.enabled = false;
			subText.enabled = false;
		} else {
			mainText.enabled = true;
			subText.enabled = true;
		}
			

		float now_x = trans.anchorMin.x;
		float now_y = trans.anchorMin.y;

		switch(monitor)
		{
		case Monitor.UP:
			now_x = SlideValue (now_x, false);
			trans.anchorMin = new Vector2 (now_x, 1);
			trans.anchorMax = new Vector2 (now_x, 1);
			break;
		case Monitor.DOWN:
			now_x = SlideValue (now_x, true);
			trans.anchorMin = new Vector2 (now_x, 0);
			trans.anchorMax = new Vector2 (now_x, 0);
			break;
		case Monitor.LEFT:
			now_y = SlideValue (now_y, false);
			trans.anchorMin = new Vector2 (0, now_y);
			trans.anchorMax = new Vector2 (0, now_y);
			break;
		case Monitor.RIGHT:
			now_y = SlideValue (now_y, true);
			trans.anchorMin = new Vector2 (1, now_y);
			trans.anchorMax = new Vector2 (1, now_y);
			break;
		
		}
	}

	float SlideValue(float src, bool isAdd){
		if (isAdd)
			src = (src > max_edge) ? min_edge : src + slideSpeed * Time.deltaTime;
		else
			src = (src < min_edge) ? max_edge : src - slideSpeed * Time.deltaTime;
		
		return src;
	}
}
