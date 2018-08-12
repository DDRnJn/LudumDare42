using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour {

    public float speed;
    private Transform playerTransform;

    // Use this for initialization
    void Start () {
        this.playerTransform = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(0, 0, speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform collidedObject = collision.gameObject.transform;
        if (collidedObject.tag == "Enemy")
        {
            Destroy(collidedObject.gameObject);
            this.playerTransform.GetComponent<PlayerController>().incrementScore(10);
        }
        Destroy(this.transform.gameObject);
    }

}
