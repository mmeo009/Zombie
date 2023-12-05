using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public GameObject Monster_001;
    public GameObject Monster_002;
    public GameObject[] items;

    public List<MonsterController> monsters = new List<MonsterController>();
    public List<MonsterController> sleepingMonsters = new List<MonsterController>();

    public List<Transform> points = new List<Transform>();

    public GameObject[] deadMonsters;

    private void Awake()
    {
        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        Monster_001 = Resources.Load<GameObject>("Prefabs/Monster_001");
        Monster_002 = Resources.Load<GameObject>("Prefabs/Monster_002");
        items = Resources.LoadAll<GameObject>("Prefabs/Item");
    }
    public void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
    }
    public void CreateMonster(int monsterNum)
    {
        int num = Random.Range(0, points.Count);
        MonsterController monster = sleepingMonsters.Find(mon => mon.monsterId == monsterNum);
        if(monster != null)
        {
            monster.hp = monsterNum * 10;
            monster.speed = monsterNum;
            monster.transform.SetPositionAndRotation(new Vector3(points[num].position.x, 0.64f, points[num].position.z), points[num].rotation);
            monster.gameObject.SetActive(true);
            sleepingMonsters.Remove(monster);
        }
        else
        {
            if(monsterNum == 1)
            {
                MonsterController temp = Instantiate(Monster_001).GetComponent<MonsterController>();
                temp.transform.SetPositionAndRotation(new Vector3(points[num].position.x, 0.64f, points[num].position.z), points[num].rotation);
                temp.speed = 1;
                temp.hp = 10;
                monsters.Add(temp.GetComponent<MonsterController>());
            }
            else if(monsterNum == 2)
            {
                MonsterController temp = Instantiate(Monster_002).GetComponent<MonsterController>();
                temp.transform.SetPositionAndRotation(new Vector3(points[num].position.x, 0.64f, points[num].position.z), points[num].rotation);
                temp.speed = 2;
                temp.hp = 20;
                monsters.Add(temp);
            }
        }
    }
    public void MonsterDie(MonsterController mon)
    {
        int itemNum = Random.Range(0, items.Length);
        GameObject temp = Instantiate(items[itemNum]);
        temp.transform.SetPositionAndRotation(new Vector3(mon.transform.position.x, 0.5f, mon.transform.position.z), mon.transform.rotation);
        mon.gameObject.SetActive(false);
        sleepingMonsters.Add(mon);
    }
}
