using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBaseController : MonoBehaviour {

    public int health;
    private Transform groundTransform;
    public EnemyManager enemyManager;
    public Transform player;
    public Text healthText;
    public Text gameOverText;

    public Text retryText;

    private bool gameFinished = false;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        this.healthText.text = this.health.ToString();
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
        this.audioSource.Play();
        this.groundTransform = GameObject.FindWithTag("Ground").transform;
        this.player = GameObject.FindWithTag("Player").transform;
    }
	
    public void takeDamage(int damage)
    {
        this.health -= damage;
        this.healthText.text = this.health.ToString();
    }


    public void shrinkStage()
    {
        float newX = this.groundTransform.localScale.x * 0.9f;
        float newZ = this.groundTransform.localScale.z * 0.9f;
        Vector3 newSize = new Vector3(newX, this.groundTransform.localScale.y, newZ);
        this.groundTransform.localScale = newSize;
    }

    public void gameOver()
    {
        this.gameFinished = true;
        this.gameOverText.gameObject.SetActive(true);
        this.retryText.gameObject.SetActive(true);
        this.enemyManager.GetComponent<EnemyManager>().gameOver();
        this.player.GetComponent<PlayerController>().gameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!(this.gameFinished))
        {
            Transform collidedObject = collision.gameObject.transform;
            Debug.Log("BASE ENTERED");
            if (collidedObject.tag == "Enemy")
            {
                this.takeDamage(10);
                if (this.health <= 0)
                {
                    this.gameOver();
                }
                Destroy(collidedObject.gameObject);
                this.shrinkStage();
                this.enemyManager.GetComponent<EnemyManager>().resetSpawners();
            }
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
