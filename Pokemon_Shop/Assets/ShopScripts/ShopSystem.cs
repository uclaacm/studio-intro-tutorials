using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSystem : MonoBehaviour
{
    public ShopUIController shop;
    // Start is called before the first frame update
    void Start()
    {
        shop.Toggle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("OverworldScene");
        }
    }
}
