using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float rotationSpeed;
    public float movementSpeed;

    public float runningSpeed;

    public float walkingSpeed;

    private Animator playerAnimator;
    private Rigidbody playerRigidBody;

    //private bool isJumping;

    public int health;

    private int score;
    public Text ScoreText;

    private bool gameFinished = false;

    public Transform attack1;
    public Transform attack2;

    public float fireRate;

    private float timeSinceLastFired;

    private AudioSource fireSound;

    private void Awake()
    {
        this.score = 0;
        this.fireSound = this.gameObject.GetComponent<AudioSource>();
        this.timeSinceLastFired = fireRate;
        this.playerRigidBody = this.transform.GetComponent<Rigidbody>();
        this.playerAnimator = this.transform.GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {

        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * movementSpeed;

        AnimateWalk(x, z);
        //this.Turn3();

        //this.transform.Rotate(0, x, 0);
        this.transform.Translate(0, 0, z);
        this.transform.Rotate(0, x, 0);

        bool isRunning = Input.GetButton("ToggleRun");
        AnimateRun(isRunning, x, z);

        bool isAttack1 = Input.GetButton("Fire1");
        bool isAttack2 = Input.GetButton("Fire2");

        if (this.timeSinceLastFired > this.fireRate)
        {
            spawnAttack(isAttack1, isAttack2);
        }

        bool reload = Input.GetKeyDown(KeyCode.R);
        if (this.gameFinished && reload)
        {
            this.reloadLevel();
        }
	}

    private void FixedUpdate()
    {
        this.timeSinceLastFired += 1;
    }

    void spawnAttack(bool isAttack1, bool isAttack2)
    {
        
        if(isAttack1)
        {
            Vector3 attackPosition = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
            Vector3 attackRotation = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
            Vector3 playerFacingDirection = playerAnimator.transform.forward;
            Instantiate(this.attack1, attackPosition + playerFacingDirection * 4, this.transform.rotation);
            this.timeSinceLastFired = 0;
        }
        this.fireSound.Play();
        //if (isAttack2)
        //{
        //    Vector3 attackRotation = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
        //    Instantiate(this.attack2, this.transform.position, Quaternion.Euler(attackRotation));
        //}
    }

    public void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void AnimateWalk(float x, float z)
    {
        bool isWalking;
        if(x != 0f || z != 0f)
        {
            isWalking = true;
            this.movementSpeed = walkingSpeed;
        }
        else
        {
            isWalking = false;
        }

        this.playerAnimator.SetBool("isWalking", isWalking);
    }

    void AnimateRun(bool isRunning, float x, float z)
    {
        if(isRunning && (x != 0 || z != 0))
        {
            this.movementSpeed = runningSpeed;
        }
        this.playerAnimator.SetBool("isRunning", (isRunning && (x !=0 || z != 0)));
    }

    public void incrementScore(int addedScore)
    {
        this.score += addedScore;
        this.ScoreText.text = this.score.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!(this.gameFinished))
        {
            Transform collidedObject = collision.gameObject.transform;
            Debug.Log("PLAYER ENTERED");
            if (collidedObject.tag == "Enemy")
            {
                Destroy(collidedObject.gameObject);
                incrementScore(10);
            }
        }
    }

    public void gameOver()
    {
        this.gameFinished = true;
    }
}
