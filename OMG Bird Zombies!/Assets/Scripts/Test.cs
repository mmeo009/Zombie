using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.CreateMonster(1);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.CreateMonster(2);
        }
    }
}
