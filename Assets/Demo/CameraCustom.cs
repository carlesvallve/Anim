using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public enum CameraMode {
	ONE = 0,
	TWO = 1,
	THREE = 2,
	FOUR = 4,
	FIVE = 5
}

public enum LightMode {
	ONE = 0,
	TWO = 1,
	THREE = 2,
	FOUR = 3
}


public class CameraCustom : MonoBehaviour {

	public Transform target;
	public float distance = 20;
	public float up = 10f;
	public CameraMode cameraMode;
	public LightMode lightMode;

	private Light directionalLight;
	private Light pointLight;
	private GameObject bg;

	private BloomOptimized bloom;

	private GameObject torch;



	void Start () {
		directionalLight = GameObject.Find ("Directional light").GetComponent<Light>();
		directionalLight.gameObject.SetActive (true);

		pointLight = GameObject.Find ("Point light").GetComponent<Light>();
		pointLight.gameObject.SetActive (true);

		bg = GameObject.Find("CubeBg");
		bg.gameObject.SetActive (false);

		bloom = GetComponent<BloomOptimized> ();
		bloom.enabled = true;

		torch = GameObject.Find ("HandTorch");
		torch.SetActive (false);
	}


	void Update () {

		// Camera mode
		if (Input.GetKeyDown ("1")) {
			cameraMode = CameraMode.ONE;
		} else if (Input.GetKeyDown ("2")) {
			cameraMode = CameraMode.TWO;
		} else if (Input.GetKeyDown ("3")) {
			cameraMode = CameraMode.THREE;
		} else if (Input.GetKeyDown ("4")) {
			cameraMode = CameraMode.FOUR;
		} else if (Input.GetKeyDown ("5")) {
			cameraMode = CameraMode.FIVE;
		}

		// Bloom mode
		else if (Input.GetKeyDown (KeyCode.B)) {
			bloom.enabled = !bloom.enabled;
		}

		// Wall mode
		else if (Input.GetKeyDown (KeyCode.W)) {
			bg.SetActive (!bg.activeSelf);
		}

		// Light mode
		else if (Input.GetKeyDown (KeyCode.L)) {
			int i = (int)lightMode;
			i++;
			if (i >= System.Enum.GetValues (typeof(LightMode)).Length) {
				i = 0;
			}
			SetLightMode ((LightMode)i);
		}
	}


	void LateUpdate () {
		switch (cameraMode) {
		case CameraMode.ONE:
			transform.position = Vector3.zero + new Vector3 (0, 3, -60);
			transform.rotation = Quaternion.identity;
			break;
		case CameraMode.TWO:
			transform.position = Vector3.up * up  + new Vector3 (0, 3, -60);
			transform.LookAt (target);
			break;
		case CameraMode.THREE:
			transform.position = -Vector3.forward * distance + Vector3.up * target.position.y + Vector3.up * up;
			transform.LookAt (target);
			break;
		case CameraMode.FOUR:
			transform.position = -Vector3.forward * distance + Vector3.up * up;
			transform.LookAt (target);
			break;
		case CameraMode.FIVE:
			transform.position = target.position -Vector3.forward * distance + Vector3.up * up;
			transform.LookAt (target);
			break;
		}
	}


	private void SetLightMode (LightMode mode) {
		lightMode = mode;

		switch (lightMode) {
		case LightMode.ONE:
			directionalLight.gameObject.SetActive (true);
			directionalLight.intensity = 1f;
			pointLight.gameObject.SetActive (true);
			torch.SetActive (false);
			break;
		case LightMode.TWO:
			directionalLight.gameObject.SetActive (true);
			directionalLight.intensity = 3f;
			pointLight.gameObject.SetActive (false);
			torch.SetActive (false);
			break;
		case LightMode.THREE:
			directionalLight.gameObject.SetActive (false);
			directionalLight.intensity = 0.5f;
			pointLight.gameObject.SetActive (true);
			torch.SetActive (false);
			break;
		case LightMode.FOUR:
			directionalLight.gameObject.SetActive (false);
			directionalLight.intensity = 0.5f;
			pointLight.gameObject.SetActive (false);
			torch.SetActive (true);
			break;
		}
	}
}
