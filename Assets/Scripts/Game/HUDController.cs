using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // Variables
    public GameObject HPBarFront;
    public GameObject HPBarBack;
    public Image hpBar;
    public float currentHP;
    public float maxHP;

    // Start is called before the first frame update
    void Awake()
    {
        // Gets CHILDREN
        GameObject HPBarFront = gameObject.transform.Find("HealthBar Front").gameObject;
        GameObject HPBarBack = gameObject.transform.Find("HealthBar Front").gameObject;

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
