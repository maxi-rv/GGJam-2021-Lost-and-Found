using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // Variables
    public GameObject HPBarFront;
    public GameObject HPBarBack;
    public GameObject RetryMessage;
    public GameObject JumpMessage;
    public GameObject DoubleMessage;
    public GameObject WallMessage;
    public GameObject FinalMessage;
    private Image hpBar;
    public float currentHP;
    public float maxHP;

    // Start is called before the first frame update
    void Awake()
    {
        // Gets CHILDREN
        HPBarFront = gameObject.transform.Find("HealthBar Front").gameObject;
        HPBarBack = gameObject.transform.Find("HealthBar Back").gameObject;
        RetryMessage = gameObject.transform.Find("RetryMessage").gameObject;
        JumpMessage = gameObject.transform.Find("JumpMessage").gameObject;
        DoubleMessage = gameObject.transform.Find("DoubleMessage").gameObject;
        WallMessage = gameObject.transform.Find("WallMessage").gameObject;
        FinalMessage = gameObject.transform.Find("FinalMessage").gameObject;
        
        // Gets COMPONENT from Children
        hpBar = HPBarFront.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = currentHP / maxHP;
    }

    public void activateBar()
    {
        HPBarFront.SetActive(true);
        HPBarBack.SetActive(true);
    }

    public void deactivateBar()
    {
        HPBarFront.SetActive(false);
        HPBarBack.SetActive(false);
    }
}
