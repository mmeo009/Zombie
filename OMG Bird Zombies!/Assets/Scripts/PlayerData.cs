using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float bulletCoolDown = 1.0f;
    public float bulletSpeed = 7.0f;
    public int bulletLevel = 1;
    public float bulletSize = 0.3f;
    public int maxHealth = 30;
    public int currentHealth;
}
