using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleJS : MonoBehaviour
{
    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Update()
    {
        if (toggle.isOn)
        {
            InputController.ic.useJoyStick = true;
        }
        else
        {
            InputController.ic.useJoyStick = false;
        }
    }
}