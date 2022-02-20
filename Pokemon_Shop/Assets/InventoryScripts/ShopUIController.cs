
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] GameObject canvasObject; //entire canvas game object
    [SerializeField] KeyCode canvasToggle = KeyCode.C; //canvas toggle
    [SerializeField] bool enableToggle = true;  // Whether or not toggling should be allowed
    Canvas canvasComp; //canvas component
    ShopDisplay uIDisplayComp; //Shop UI Display script

    bool canvasEnabled;

    private void Awake()
    {
        canvasComp = canvasObject.GetComponent<Canvas>();
        uIDisplayComp = canvasObject.GetComponent<ShopDisplay>();
        canvasEnabled = canvasComp.enabled; //set bool to whether it is enabled
    }
    private void Update()
    {
        if (Input.GetKeyDown(canvasToggle) && enableToggle) //toggle canvas and uidisplay components enabled or disabled
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        Debug.Log("attempting toggle");
        canvasEnabled = !canvasEnabled;

        //if (canvasEnabled)
        //    Time.timeScale = 0;
        //else
        //    Time.timeScale = 1;

        canvasComp.enabled = canvasEnabled;
        uIDisplayComp.enabled = canvasEnabled;
    }
}
