using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerNew : MonoBehaviour {

    [Header("Player's Properties")]
    [SerializeField] private int maxHP;
    [SerializeField] private int battery;
    [SerializeField] private Vector2 savePointPos;
    [SerializeField] private int potion;

    public int hp { get; set; }

    private Dictionary<int, bool> keys;

    public int MaxHP
    {
        get
        {
            return maxHP;
        }
    }

    public int Battery
    {
        get
        {
            return battery;
        }
    }

    public Vector2 SavePointPos
    {
        get
        {
            return savePointPos;
        }
    }

    public int Potion
    {
        get
        {
            return potion;
        }
    }

    public bool CheckKey(int door_id)
    {
        if(!keys.ContainsKey(door_id))
        {
            keys.Add(door_id, false);
        }
        return keys[door_id];
    }

    public void AcquireKey(int door_id)
    {
        if (!keys.ContainsKey(door_id))
        {
            keys.Add(door_id, false);
        }
        keys[door_id] = true;
    }

    //set the infomation by all these methods
    public void setSavePointPos(Vector2 pos)
    {
        savePointPos = pos;
    }

    public void loseHP(int damage)
    {
        hp -= damage;
    }
    public void loseBatery(int num)
    {
        battery -= num;
    }
    public void losePotion(int num)
    {
        potion -= num;
    }
    public void AcquireBattery(int num)
    {
        battery += num;
    }
    public void AcquirePotion(int potion_num)
    {
        potion += potion_num;
    }
}
