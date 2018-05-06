using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SprintBar : MonoBehaviour {
    [SerializeField] Slider slider;
    PlayerControlNew playControl;
    private bool canPlayerMove;
    private bool canPlayerRun;
    private float playerSpeedScale;
    [SerializeField] private float speedUpScale;
    [SerializeField] private float speedUpTime;
    [SerializeField] private float recoverTime;
    [SerializeField] private float coolingTime;
    [SerializeField] private float stamina; //just init it as 1

    bool isCooling = false;
    bool runStatus = false;
    bool recoverStatus=false;

    private void Start()
    {
       
        stamina = 1;
        playerSpeedScale = 1;
    }

    private void Update()
    {
        if (slider == null)
            slider = GameObject.Find("SprintBar").GetComponent<Slider>();
        slider.value = stamina;
        Debug.Log(stamina);
    if (!isCooling)
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            recoverStatus = false;
            if (stamina > 0)
                runStatus = true;
            else
                runStatus = false;
        }
        else
        {
            runStatus = false;
            if (stamina < 1)
                recoverStatus = true;
            else
                recoverStatus = false;
        }
    }
    else
    {
        runStatus = false;
        recoverStatus = false;
    }
       

        if(runStatus)
        {
            playerSpeedScale = speedUpScale;
            consumeStamina();
        }
        else if(recoverStatus)
        {
            playerSpeedScale = 1;
            recoverStamina();
        }
        else
        {
            playerSpeedScale = 1;
        }

    }

    void recoverStamina()
    {
        if (stamina >= 1)
            return;
        stamina += Time.deltaTime/recoverTime;
    }

    IEnumerator CoolingTime()
    {
        yield return new WaitForSeconds(coolingTime);
        isCooling = false;
    }

    void consumeStamina()
    {
        if (stamina <= 0)
            return;
        stamina -= Time.deltaTime/speedUpTime;
        if (stamina <= 0)
        {
            isCooling = true;
            StartCoroutine(CoolingTime());
        }
    }

    public float PlayerSpeedScale()
    {
        return playerSpeedScale;
    }
}
