using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour {

	public float speed = 1.0f;
	public GameObject camera3Pessoa;
	public GameObject camera1Pessoa;
	private bool visao;

	void Start () {
		Time.timeScale = 1.0f;
		visao = true;
		GetComponent<AudioSource> ().Play ();
	}
	
	void Update () {
		transform.Rotate(new Vector3(0, 180, 0));
		transform.position += transform.forward * Time.deltaTime * speed;
		speed -= transform.forward.y * Time.deltaTime * 1.0f;

		/*if (speed < 2.0f)
			//speed = 2.0f;
		if (speed > 10.0f)
			//speed = 10.0f;*/

		if (Input.GetKey ("left shift")) {
			Debug.Log ("shift" + speed);
			speed++;
		}
		if(Input.GetKey("left ctrl")){
			Debug.Log("ctrl"+speed);
			speed--;
		}

		transform.Rotate(-Input.GetAxis("Vertical"), 180.0f, -Input.GetAxis("Horizontal"));
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "SairCenario") {
			//Debug.Log ("Entrou");
			//SceneManager.LoadScene ("CenaJogo");
		}
	}
}
