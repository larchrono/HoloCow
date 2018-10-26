using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGetDefault : MonoBehaviour {

	public CameraDynamicSetting camSetting;
	public string valueKey;
	Slider sld;

	// Use this for initialization
	void Start () {
		sld = GetComponent<Slider> ();
		valueKey = camSetting.CamName + valueKey;
		StartCoroutine( GetPlayerValue (sld,valueKey));
	}


	
	IEnumerator GetPlayerValue(Slider sld,string key){
		yield return new WaitForSeconds (0.5f);

		if (!PlayerPrefs.HasKey (key))
			yield break;
		float val = PlayerPrefs.GetFloat (key, 0);
		sld.value = val;
	}
}
