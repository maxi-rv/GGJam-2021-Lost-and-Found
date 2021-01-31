using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //CHILDREN
    private GameObject intro;
    private GameObject final;
    
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
    private bool playerDestroyed;
    private bool jumpShowed;
    private bool doubleShowed;
    private bool wallShowed;

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
        final = gameObject.transform.Find("Final").gameObject;

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
                if (!playerDestroyed)
                {
                    hudController.RetryMessage.SetActive(true);
                    destroyPlayer();
                }

                if(Input.GetKey(KeyCode.R))
                {
                    reloadScene(currentScene);
                    hudController.RetryMessage.SetActive(false);
                    audioController.Stop("ggj-main");
                    audioController.Play("ggj-main");
                }
                
            }
        }

        if(onPlayingLevel && playerController.collectibles>=13)
        {
            unloadScene(currentScene);
            onPlayingLevel = false;
            hudController.deactivateBar();
            hudController.FinalMessage.SetActive(true);
            final.gameObject.SetActive(true);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(!jumpShowed && onPlayingLevel && playerController.collectibles == 1)
        {
            hudController.JumpMessage.SetActive(true);
            Invoke("deactivateJump", 3f);
            jumpShowed = true;
        }

        if(!wallShowed && onPlayingLevel && playerController.collectibles == 3)
        {
            hudController.WallMessage.SetActive(true);
            Invoke("deactivateWall", 3f);
            wallShowed = true;
        }

        if(!doubleShowed && onPlayingLevel && playerController.collectibles == 8)
        {
            hudController.DoubleMessage.SetActive(true);
            Invoke("deactivateDouble", 3f);
            doubleShowed = true;
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
        playerDestroyed = false;
    }

    private void destroyPlayer()
    {
        cameraController.Players.Remove(player.transform);
        GameObject.Destroy(playerController.gameObject);
        playerDestroyed = true;
    }

    private void loadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        currentScene = scene;
        jumpShowed = false;
        doubleShowed = false;
        wallShowed = false;
    }

    private void reloadScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        instantiatePlayer();
    }

    private void unloadScene(string scene)
    {
        destroyPlayer();
        SceneManager.UnloadSceneAsync(scene);
    }

    private void deactivateJump() 
    {
        hudController.JumpMessage.SetActive(false);
    }

    private void deactivateDouble() 
    {
        hudController.DoubleMessage.SetActive(false);
    }

    private void deactivateWall() 
    {
        hudController.WallMessage.SetActive(false);
    }
    
}