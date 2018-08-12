using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Button startButton;


    private void Start()
    {
        Button button = this.startButton.GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        Debug.Log("Started Game");
        SceneManager.LoadScene("1");
    }
		
}
