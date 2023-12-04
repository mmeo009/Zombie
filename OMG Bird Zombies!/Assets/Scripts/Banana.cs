using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public int hp;

    private void Update()
    {
        if(hp <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {

    }
}
