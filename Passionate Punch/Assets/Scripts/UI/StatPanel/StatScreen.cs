using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using InventorySystem;

public class StatScreen : MonoBehaviour
{
    [SerializeField]
        private CharacterSettings _Character
        {
            get
            {
                return GameManager.Instance.character.characterStats;
            }
        }

    [SerializeField] private List<StatText> statTextList;

    void Start()
    {
        _Character.characterStats.Add(_Character.attackDamage);
        _Character.characterStats.Add(_Character.attackSpeed);
        _Character.characterStats.Add(_Character.maxHealth);
        _Character.characterStats.Add(_Character.maxMana);
        _Character.characterStats.Add(_Character.moveSpeed);
        _Character.characterStats.Add(_Character.defence);
        _Character.characterStats.Add(_Character.range);
        _Character.characterStats.Add(_Character.healthRecoveryTime);
        _Character.characterStats.Add(_Character.healthRecoveryAmount);
        _Character.characterStats.Add(_Character.manaRecoveryTime);
        _Character.characterStats.Add(_Character.manaRecoveryAmount);
        _Character.characterStats.Add(_Character.experience);

        for(int i = 0; i < statTextList.Count; i++){
            Debug.Log("jknad : " + _Character.characterStats[i].ToString());
            statTextList[i].statValueText.text = _Character.characterStats[i].ToString();
            //Debug.Log("deneme stat text" + i + statTextList[i]);
        }

        _Character.characterStats.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable(){
        Equipment.OnEquipmentHappened += UpdateUI;   
    }

    void OnDisable(){
        Equipment.OnEquipmentHappened -= UpdateUI;   
    }

    public void UpdateUI(){
        _Character.characterStats.Add(_Character.attackDamage);
        _Character.characterStats.Add(_Character.attackSpeed);
        _Character.characterStats.Add(_Character.maxHealth);
        _Character.characterStats.Add(_Character.maxMana);
        _Character.characterStats.Add(_Character.moveSpeed);
        _Character.characterStats.Add(_Character.defence);
        _Character.characterStats.Add(_Character.range);
        _Character.characterStats.Add(_Character.healthRecoveryTime);
        _Character.characterStats.Add(_Character.healthRecoveryAmount);
        _Character.characterStats.Add(_Character.manaRecoveryTime);
        _Character.characterStats.Add(_Character.manaRecoveryAmount);
        _Character.characterStats.Add(_Character.experience);

        for(int i = 0; i < statTextList.Count; i++){
            Debug.Log("jknad : " + _Character.characterStats[i].ToString());
            statTextList[i].statValueText.text = _Character.characterStats[i].ToString();
            //Debug.Log("deneme stat text" + i + statTextList[i]);
        }

        _Character.characterStats.Clear();
    }
}
