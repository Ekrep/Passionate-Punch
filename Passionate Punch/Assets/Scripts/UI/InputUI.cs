using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUI : MonoBehaviour
{
    public FixedJoystick joystick;

   
  public void SendJoyStickInput()
    {
        UIManager.Instance.joystickHorizontalInput = joystick.Horizontal;
        UIManager.Instance.joystickVerticalInput = joystick.Vertical;
        
          
    }
}
