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
            CanvasGroup canvasGroup = inventory.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        public void CastSkillOne()
        {
            UIManager.Instance.isPressedSkillOne = true;
        }
        public void UnCastSkillOne()
        {
            UIManager.Instance.isPressedSkillOne = false;
        }
        public void CastSkillTwo()
        {
            UIManager.Instance.isPressedSkillTwo = true;
        }
        public void UnCastSkillTwo()
        {
            UIManager.Instance.isPressedSkillTwo = false;
        }
    }
}
