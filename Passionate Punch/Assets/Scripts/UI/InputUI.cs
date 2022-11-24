using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI
{

    public class InputUI : MonoBehaviour
    {
        public Joystick joystick;
        public InventoryUI inventory;
        public Button attackButton;
        public Button pickUpButton;

        void OnEnable()
        {
            UIManager.OnTriggeredWithItem += UIManager_OnTriggeredWithItem;
        }

        void UIManager_OnTriggeredWithItem()
        {
            if (pickUpButton.gameObject.activeSelf)
            {
                pickUpButton.gameObject.SetActive(false);

            }
            else
            {
                pickUpButton.gameObject.SetActive(true);

            }
        }

        void OnDisable()
        {
            UIManager.OnTriggeredWithItem -= UIManager_OnTriggeredWithItem;
        }

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
            StartCoroutine(UnCastSkillOne());


        }
        IEnumerator UnCastSkillOne()
        {
            yield return new WaitForEndOfFrame();
            UIManager.Instance.isPressedSkillOne = false;
        }
        public void CastSkillTwo()
        {

            UIManager.Instance.isPressedSkillTwo = true;
            StartCoroutine(UnCastSkillTwo());



        }
        IEnumerator UnCastSkillTwo()
        {
            yield return new WaitForEndOfFrame();
            UIManager.Instance.isPressedSkillTwo = false;
        }

        public void ActivatePickUp()
        {
            UIManager.Instance.isPickUpButtonPressed = true;
        }

        IEnumerator DisablePickUpButton()
        {
            yield return new WaitForEndOfFrame();
            UIManager.Instance.isPickUpButtonPressed = false;
        }



    }

}
