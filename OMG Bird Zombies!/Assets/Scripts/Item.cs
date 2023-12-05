using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemNum;
    PlayerData data;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            data = other.GetComponent<PlayerData>();
            switch(itemNum)
            {
                case 0:
                    int bulletRan = Random.Range(0, 3);
                    if(bulletRan == 0)
                    {
                        data.bulletLevel += 1;
                    }
                    else if(bulletRan == 1)
                    {
                        data.bulletCoolDown -= 0.1f;
                    }
                    else if(bulletRan == 2)
                    {
                        data.bulletSize += 0.05f;
                    }
                    break;
                case 1:
                    int ran = Random.Range(0, 2);
                    if(ran == 0)
                    {
                        data.moveSpeed += 0.5f;
                    }
                    else if(ran == 1)
                    {
                        data.bulletSpeed += 0.5f;
                    }
                    break;
                case 2:
                    data.maxHealth += 1;
                    data.currentHealth += 1;
                    if(data.currentHealth >= data.maxHealth)
                    {
                        data.currentHealth = data.maxHealth;
                    }
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
