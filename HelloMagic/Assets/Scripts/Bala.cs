using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bala : MonoBehaviour {


	public GameObject ExplosionPrefab;
	private GameObject enemy;

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Inimigo") {
			enemy = collision.gameObject;
			StartCoroutine("Kill");
		}

		if (collision.gameObject.name == "Monster" || collision.gameObject.name == "human_warship")
		{
			enemy = collision.gameObject;
			StartCoroutine("Explosion");

		}
	}

	IEnumerator Kill()
	{
		Instantiate(ExplosionPrefab, enemy.transform.position, enemy.transform.rotation);
		enemy.SetActive (false);
		yield return new WaitForSeconds(1.0f);

		//SceneManager.LoadScene ("Pre-Final");
	}

	IEnumerator Explosion()
	{

		Instantiate(ExplosionPrefab, enemy.transform.position, enemy.transform.rotation);
		enemy.SetActive(false);
		yield return new WaitForSeconds(1.0f);
	}

}
