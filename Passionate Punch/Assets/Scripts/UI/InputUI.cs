using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI
{

    public class InputUI : MonoBehaviour
    {
        public Joystick joystick;
        public InventoryUI inventory;
        public StatScreen statScreen;
        public Button attackButton;
        public Joystick skill1Button;
        public Joystick skill2Button;


        private bool _isPressedSkillOne;
        private bool _isPressedSkillTwo;
        void OnEnable()
        {
            UIManager.OnTriggeredWithItem += UIManager_OnTriggeredWithItem;
            UIManager.OnTriggerExitWithItem += UIManager_OnTriggerExitWithItem;
        }

        void UIManager_OnTriggeredWithItem()
        {
            //pickUpButton.gameObject.SetActive(true);
        }

        void UIManager_OnTriggerExitWithItem()
        {
            //pickUpButton.gameObject.SetActive(false);
        }

        void OnDisable()
        {
            UIManager.OnTriggeredWithItem -= UIManager_OnTriggeredWithItem;
            UIManager.OnTriggerExitWithItem -= UIManager_OnTriggerExitWithItem;
        }

        private void Update()
        {
            if (_isPressedSkillOne)
            {
                SetRotationOfSkill1();
            }
            if (_isPressedSkillTwo)
            {
                SetRotationOfSkill2();
            }

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

        public void DisplayStatScreen()
        {
            CanvasGroup canvasGroup = statScreen.GetComponent<CanvasGroup>();
            if (canvasGroup.alpha == 0f)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

        }
        // Invisible Skill
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
        // Whirl Skill
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



        public void PressedSkillOne()
        {
            _isPressedSkillOne = true;


        }

        public void PressedSkillTwo()
        {
            _isPressedSkillTwo = true;
        }

        public void ReleasedSkillOne()
        {
            _isPressedSkillOne = false;
        }

        public void ReleasedSkillTwo()
        {
            _isPressedSkillTwo = false;
        }



        public void SetRotationOfSkill1()
        {
            if (GameManager.Instance.character.characterSkills.Count > 0)
            {

                Vector3 joystickPos = new Vector3(skill1Button.Horizontal, skill1Button.Vertical);
                GameManager.Instance.character.characterSkills[0].CreateDecal(joystickPos);


            }

        }
        public void SetRotationOfSkill2()
        {
            if (GameManager.Instance.character.characterSkills.Count > 1)
            {

                Vector3 joystickPos = new Vector3(skill2Button.Horizontal, skill2Button.Vertical);
                GameManager.Instance.character.characterSkills[1].CreateDecal(joystickPos);


            }

        }



        public void ActivatePickUp()
        {
            UIManager.Instance.isPickUpButtonPressed = true;
        }

        public void DisablePickUp()
        {
            UIManager.Instance.isPickUpButtonPressed = false;
        }


    }

}
