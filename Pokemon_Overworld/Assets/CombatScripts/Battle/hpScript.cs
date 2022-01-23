using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpScript : MonoBehaviour
{
    public int maxHp;
    public void setHP(int hp)
    {
        this.transform.localScale = new Vector2((float) hp / (float) maxHp, 1f);
    }
}
