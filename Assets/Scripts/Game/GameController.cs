using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //CHILDREN
    private GameObject intro;
    
    //COMPONENTS
    private static GameController instance;
    private AudioController audioController;
    private CameraController cameraController;
    private HUDController hudController;
    private PlayerController playerController;

    //VARIABLES
    public GameObject playerPrefab;
    private GameObject player;  
    private string currentScene;
    private bool onPlayingLevel;

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
        GameObject cameraObject = gameObject.transform.Find("Camera").gameObject;
        GameObject canvas = gameObject.transform.Find("Canvas").gameObject;
        intro = gameObject.transform.Find("Intro").gameObject;

        // Gets COMPONENT from Children
        audioController = audioControllerObject.GetComponent<AudioController>();
        cameraController = cameraObject.GetComponent<CameraController>();
        hudController = canvas.GetComponent<HUDController>();
    }

    void Start() 
    {
        audioController.Play("ggj-intro");
    }

    void Update() 
    {
        if(Input.GetKey(KeyCode.Return) && !onPlayingLevel)
        {
            //Updates HUD
            intro.gameObject.SetActive(false);
            hudController.activateBar();

            //Loads first Scene
            currentScene = "Testing Level";
            loadScene(currentScene);
            onPlayingLevel = true;
            audioController.Stop("ggj-intro");
            audioController.Play("ggj-main");

            //Instantiates player
            instantiatePlayer();
        }

        if(onPlayingLevel)
        {
            hudController.currentHP = playerController.currentHP;

            if(playerController.currentHP <= 0)
            {
                reloadScene(currentScene);
                audioController.Stop("ggj-main");
            audioController.Play("ggj-main");
            }
        }
        
    }

    private void instantiatePlayer()
    {
        // Instantiating the Player
        Vector3 position = new Vector3(-10f, -5f, 0f);
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        player = Instantiate(playerPrefab, position, rotation);

        // Setting some variables
        playerController = player.GetComponent<PlayerController>();
        cameraController.Players.Add(player.transform);
        hudController.maxHP = playerController.maxHP;
        hudController.currentHP = playerController.currentHP;
        hudController.activateBar();
    }

    private void destroyPlayer()
    {
        cameraController.Players.Remove(player.transform);
        GameObject.Destroy(playerController.gameObject);
    }

    private void loadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        currentScene = scene;
    }

    private void reloadScene(string scene)
    {
        destroyPlayer();
        SceneManager.UnloadSceneAsync(scene);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        instantiatePlayer();
    }

    
    
}