using UnityEngine;
using UnityEngine.UI;

namespace UI
{

    public class InputUI : MonoBehaviour
    {
        public FixedJoystick joystick;
        public InventoryUI inventory;

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

        public void DisplayInventory()
        {
            inventory.gameObject.SetActive(true);
        }
    }
}
