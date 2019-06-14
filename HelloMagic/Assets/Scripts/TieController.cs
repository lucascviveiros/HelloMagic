using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieController : MonoBehaviour {

    private float Amplitude;
    private float Frequencia;

    Vector3 PosOffset;
    Vector3 TempPos;

    // Use this for initialization
    void Start () {
        PosOffset = transform.position;
        Amplitude = Random.Range(0.30f, 0.90f);
        Frequencia = Random.Range(0.50f, 0.90f);
    }
	
	// Update is called once per frame
	void Update () {
        TempPos = PosOffset;
        TempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * Frequencia) * Amplitude;

        transform.position = TempPos;
	}
}
