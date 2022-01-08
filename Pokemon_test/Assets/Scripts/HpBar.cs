using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    float maxHp;
    // Update is called once per frame
    void setHP(float hp)
    {
        this.transform.localScale = new Vector2(hp / maxHp, 1f);
    }
}
