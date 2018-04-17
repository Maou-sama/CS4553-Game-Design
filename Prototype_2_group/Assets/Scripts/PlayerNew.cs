using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerNew : MonoBehaviour {
    //This is the infomation of the palyer
    public int hp;
    [SerializeField] private int maxHP;
    [SerializeField] private int battery;
    [SerializeField] private Vector2 savePointPos;
    [SerializeField] private Dictionary<int, bool> keys;
    [SerializeField] private int potion;
    //get info by propertise

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
    
    //set the infomation by all these methods
    public void setSavePointPos(Vector2 pos)
    {
        savePointPos = pos;
    }
    public void setSavePointPos(Vector3 pos)
    {
       savePointPos = new Vector2(pos.x,pos.y);
    }
    public void setSavePointPos(Transform pos)
    {
        setSavePointPos(pos.position);
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
    public void AcquireKey(int door_id)
    {
        if (!keys.ContainsKey(door_id))
        {
            keys.Add(door_id, false);
        }
        keys[door_id] = true;
    }
    public void AcquirePotion(int potion_num)
    {
        potion += potion_num;
    }

    
}
