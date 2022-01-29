using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject canvasObject; //entire canvas game object
    [SerializeField] KeyCode canvasToggle = KeyCode.C; //canvas toggle
    Canvas canvasComp; //canvas component
    UIDisplay uIDisplayComp; //UI Display script

    bool canvasEnabled;

    private void Awake()
    {
        canvasComp = canvasObject.GetComponent<Canvas>();
        uIDisplayComp = canvasObject.GetComponent<UIDisplay>();
        canvasEnabled = canvasComp.enabled; //set bool to whether it is enabled
    }
    private void Update()
    {
        if (Input.GetKeyDown(canvasToggle)) //toggle canvas and uidisplay components enabled or disabled
        {
            enabled = !enabled;

            canvasComp.enabled = enabled;
            uIDisplayComp.enabled = enabled;
        }
    }
}
