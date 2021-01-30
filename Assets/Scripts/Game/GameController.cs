using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
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

        // Load Scene
        SceneManager.LoadScene("Testing Level", LoadSceneMode.Additive);

        // Gets CHILDREN
        GameObject audioControllerObject = gameObject.transform.Find("AudioController").gameObject;
        GameObject cameraObject = gameObject.transform.Find("Camera").gameObject;
        GameObject canvas = gameObject.transform.Find("Canvas").gameObject;

        // Gets COMPONENT from Children
        audioController = audioControllerObject.GetComponent<AudioController>();
        cameraController = cameraObject.GetComponent<CameraController>();
        hudController = canvas.GetComponent<HUDController>();

        // Instantiating the Player
        Vector3 position = new Vector3(-10f, -5f, 0f);
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        GameObject player = Instantiate(playerPrefab, position, rotation);

        // Setting some variables
        playerController = player.GetComponent<PlayerController>();
        cameraController.Players.Add(player.transform);
        hudController.maxHP = playerController.maxHP;
    }

    void Start()
    {
        audioController.Play("ggj-2");

        hudController.currentHP = playerController.currentHP;
    }

    void Update() 
    {
        hudController.currentHP = playerController.currentHP;

        if(playerController.currentHP <= 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}