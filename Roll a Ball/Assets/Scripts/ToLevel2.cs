using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel2 : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        SceneManager.LoadScene("Level 2");
    }
}
