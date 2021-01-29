using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //VARIABLES
    private static GameController instance;
    private AudioController audioController;
    [SerializeField] private GameObject[] players;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        // Gets CHILDREN
        GameObject audioControllerObject = gameObject.transform.Find("AudioController").gameObject;

        // Gets COMPONENT from Children
        audioController = audioControllerObject.GetComponent<AudioController>();
    }

    void Start()
    {
        audioController.Play("Song-01");
    }

    public void changeToScene2()
    {
        SceneManager.LoadScene("");
    }
}