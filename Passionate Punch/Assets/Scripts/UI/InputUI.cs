using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
            StartCoroutine(MakeInputFalse(UIManager.Instance.isPressedSkillOne));

        }
        public void UnCastSkillOne()
        {
            UIManager.Instance.isPressedSkillOne = false;
        }
        public void CastSkillTwo()
        {
            UIManager.Instance.isPressedSkillTwo = true;
            StartCoroutine(MakeInputFalse(UIManager.Instance.isPressedSkillTwo));
           
        }
        public void UnCastSkillTwo()
        {
            UIManager.Instance.isPressedSkillTwo = false;
        }
        IEnumerator MakeInputFalse(bool value)
        {
            yield return new WaitForEndOfFrame();
            value = false;
        }
        
    }
   
}
