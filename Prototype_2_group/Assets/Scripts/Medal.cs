using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Medal : MonoBehaviour {
    public Text winMsg;
    public Player player;
    public int rankHigh, rankMiddle, rankLow;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            string text = "Now you safe! \n";
            winMsg.enabled = true;
            if (player.another_eye > rankHigh)
                text += "\tExcellent!";
            else if (player.another_eye > rankMiddle)
                text += "\tNot Bad!";
            else
                text += "\tPoor!";
            winMsg.DOText(text, 2f).OnComplete(()=> { SceneManager.LoadScene("Prototype_2_v1"); }).Play();
        }
    }
}
