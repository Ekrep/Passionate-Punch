using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using System;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public List<CharacterSettings> characterDatas;
    public CharacterSettings holderData;
    public GameObject holdedCharacter;


   

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //Spawn point (works static can be dynamic)
        Instantiate(holdedCharacter,new Vector3(0,0,-25),Quaternion.identity);
       
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    public static event Action OnDataPulled;


    public void DataPulled()
    {
        if (OnDataPulled!=null)
        {
            OnDataPulled();
        }
    }


    public void SendDataPullRequest(CharacterBaseStateMachine obj)
    {
        CharacterSettings newData = ScriptableObject.CreateInstance<CharacterSettings>();
        SetDataVariables(newData);
        obj.characterStats = newData;
    }

    

 

    private void SetDataVariables(CharacterSettings newData)
    {
        newData.characterName = holderData.characterName;
        newData.characterImage = holderData.characterImage;
        newData.inventorySprite = holderData.inventorySprite;
        newData.attackDamage = holderData.attackDamage;
        newData.attackSpeed = holderData.attackSpeed;
        newData.attackRayThickness = holderData.attackRayThickness;
        newData.maxHealth = holderData.maxHealth;
        newData.mana = holderData.mana;
        newData.maxMana = holderData.maxMana;
        newData.moveSpeed = holderData.moveSpeed;
        newData.defence = holderData.defence;
        newData.range = holderData.range;
        newData.AEORange = holderData.AEORange;
        newData.healthRecoveryTime = holderData.healthRecoveryTime;
        newData.healthRecoveryPeriod = holderData.healthRecoveryPeriod;
        newData.healthRecoveryAmount = holderData.healthRecoveryAmount;
        newData.manaRecoveryTime = holderData.manaRecoveryTime;
        newData.manaRecoveryPeriod = holderData.manaRecoveryPeriod;
        newData.manaRecoveryAmount = holderData.manaRecoveryAmount;
        newData.experience = holderData.experience;
        newData.experienceThreshold = holderData.experienceThreshold;
        newData.level = holderData.level;
        newData.money = holderData.money;
        newData.skillList = holderData.skillList;
        newData.ownedItemList = holderData.ownedItemList;
        newData.equippedItemList = holderData.equippedItemList;
        newData.characterStats = holderData.characterStats;
        newData.characterClass = holderData.characterClass;
    }
}
