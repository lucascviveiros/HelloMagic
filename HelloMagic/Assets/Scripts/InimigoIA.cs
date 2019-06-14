using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoIA : MonoBehaviour {
	
	public Transform myTransform;
	public GameObject player;
	float f_MoveSpeed = 8.0f;

	public GameObject bulletPrefab;
	public GameObject bulletSpawn;

	void Start () {
		player = GameObject.Find ("PlayerShip");
	}
	
	void FixedUpdate () {
		transform.LookAt(player.transform);
		transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
	}

	void Fire()
	{
		GameObject bullet = Instantiate(Resources.Load("Tiro", typeof(GameObject))) as GameObject;
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.GetComponent<AudioSource>().Play(); ;

		bullet.transform.position = bulletSpawn.transform.position;
		rb.velocity = -bulletSpawn.transform.position * 500.0f;
		//rb.AddForce(bulletSpawn.transform.forward * 500.0f);

		Destroy(bullet, 2.0f);
	}
}
