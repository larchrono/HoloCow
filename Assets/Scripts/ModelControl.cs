using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelControl : MonoBehaviour {

	public GameObject myModel;
	public Text coinValue;

	public AudioSource heartBeat;
	public AudioSource coinSound;

	int nowCoin = 0;

	float modelResumeTime = 0;
	float modelFadeTime = 1f;

	float modelCoinEffectTime = 0;

	float nowTime = 0;
	delegate void StoreAction();
	event StoreAction storeAction;

	// Use this for initialization
	void Start () {
		SerialDelegate.coinAddInEvent += OnCoinIn;
		SerialDelegate.closeSignEvent += OnUserClosetIn;

	}
	
	// Update is called once per frame
	void Update () {

		nowTime = Time.time;

		if (storeAction != null) {
			storeAction ();
			storeAction = null;
		}

		////////

		float nowAlpha = myModel.GetComponent<Renderer> ().material.color.a;

		if (Time.time < modelCoinEffectTime) {
			//Coin in and effect Time

			if (Time.time > modelResumeTime) {
				// Model will be show
				heartBeat.volume = nowAlpha;
				myModel.GetComponent<Renderer> ().material.color = new Color (0, 0, 0, Mathf.Lerp (nowAlpha, 0, modelFadeTime * Time.deltaTime));
				//myModel.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
				//Debug.Log ("keep show : " + myModel.GetComponent<Renderer>().material.color.a);
				//Debug.Log ("keep show");

			} else {
				//Model will be disapear
				heartBeat.volume = nowAlpha;
				myModel.GetComponent<Renderer> ().material.color = new Color (0, 0, 0, Mathf.Lerp (nowAlpha, 1, modelFadeTime * Time.deltaTime));
				//myModel.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
				//Debug.Log ("keep disapear : " + + myModel.GetComponent<Renderer>().material.color.a);
				//Debug.Log ("keep disapear");

			}
		} else {
			// no coin add in , so sound and model disapear
			heartBeat.volume = 0;
			myModel.GetComponent<Renderer> ().material.color = new Color (0, 0, 0, Mathf.Lerp (nowAlpha, 1, modelFadeTime * Time.deltaTime));
			//Debug.Log ("keep disapear");
		}
	}

	public void OnCoinIn(){
		Debug.Log ("Coin Add");
			
		nowCoin++;
		if (nowCoin >= 2) {
			nowCoin = 0;

			modelCoinEffectTime = nowTime + 50;
			modelResumeTime = nowTime + 5;

			storeAction += delegate() {
				myModel.GetComponent<Renderer> ().material.color = new Color (0, 0, 0, -1);
			};

		}

		storeAction += delegate() {
			coinSound.Play ();
			coinValue.text = "" + nowCoin + "/2";
		};

	}

	public void OnUserClosetIn(string data){
		Debug.Log ("Closet Sign :" + data);

		float number = 0;
		bool conversionSuccessful = float.TryParse(data, out number);

		if (conversionSuccessful) {
			modelResumeTime = nowTime + 4;
		}
	}

}
