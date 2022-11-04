using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputUI : MonoBehaviour
{
    public FixedJoystick joystick;

    public Button attackButton;


    public void SendJoyStickInput()
    {
        UIManager.Instance.joystickHorizontalInput = joystick.Horizontal;
        UIManager.Instance.joystickVerticalInput = joystick.Vertical;
    }



    public void SetAttackTrue()
    {
        UIManager.Instance.isAttackPress = true;
    }
    public void SetAttackFalse()
    {
        UIManager.Instance.isAttackPress = false;
    }
}
