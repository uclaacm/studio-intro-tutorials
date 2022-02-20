using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public static int coins = 100;
    // Start is called before the first frame update
    void Start()
    {
        foreach (InventoryItem item in Resources.FindObjectsOfTypeAll<InventoryItem>())
        {
            item.reset();
        }
        coins = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SceneManager.LoadScene("OverworldScene");
    }
}
