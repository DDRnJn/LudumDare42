using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Transform playerTransform;
    private Transform baseTransform;
    public float speed;
    public int health;


	// Use this for initialization
	void Start () {
        this.playerTransform = GameObject.FindWithTag("Player").transform;
        this.baseTransform = GameObject.FindWithTag("Base").transform;
	}
	
    private void FollowPlayer()
    {
        this.transform.LookAt(this.playerTransform);
    }

    private void FollowBase()
    {
        this.transform.LookAt(this.baseTransform);
    }

	// Update is called once per frame
	void Update () {
        //FollowPlayer();
        FollowBase();
        this.transform.Translate(0, 0, speed);
    }
}
