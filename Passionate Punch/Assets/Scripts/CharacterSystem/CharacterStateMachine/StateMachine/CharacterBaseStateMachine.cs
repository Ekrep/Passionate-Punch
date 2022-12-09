using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using Items;
using SkillSystem;
using CharacterUtilities;
using InventorySystem;

public class CharacterBaseStateMachine : CharacterStateMachine
{
    [HideInInspector]
    public CharacterIdleState characterIdleState;
    [HideInInspector]
    public CharacterMovingState characterMovingState;
    [HideInInspector]
    public CharacterAttackingState characterAttackingState;
    [HideInInspector]
    public CharacterRecoveryState characterRecoveryState;
    [HideInInspector]
    public CharacterSkillCastState characterSkillCastState;
    [HideInInspector]
    public CharacterDeadState characterDeadState;

    public List<SkillSettings> characterSkills;


    public CharacterSettings characterStats;

    public Animator anim;


    [HideInInspector]
    public float characterMovementSpeed;
//Smoothness of turning action
    public float turnSmoothSpeed;

    [HideInInspector]
    public bool canVisible;

    
    public AutoAim autoAim;


    //Needs refactor
    public List<GameObject> characterWeapons;

    //0 leftHand, 1 rightHand
    public List<ParticleSystem> characterWeaponsParticle;

    public ParticleSystem healthRegenParticle;
    public ParticleSystem levelUpParticle;
    public ParticleSystem manaRegenParticle;
    


    private void Awake()
    {
        characterIdleState = new CharacterIdleState(this);
        characterMovingState = new CharacterMovingState(this);
        characterAttackingState = new CharacterAttackingState(this);
        characterRecoveryState = new CharacterRecoveryState(this);
        characterSkillCastState = new CharacterSkillCastState(this);
        characterDeadState = new CharacterDeadState(this);

       


       
        canVisible = true;
        
    }
    private void OnEnable()
    {
        CharacterHealth.OnPlayerDeath += CharacterHealth_OnPlayerDeath;
        CharacterHealth.OnHealthRecovery += CharacterHealth_OnHealthRecovery;
        CharacterExperience.OnLevelUp += CharacterExperience_OnLevelUp;
        DataManager.OnDataPulled += DataManager_OnDataPulled;
        Respawn.OnRespawn += Respawn_OnRespawn;
        Equipment.OnEquipmentHappened += Equipment_OnEquipmentHappened;
        CharacterMana.OnManaRecoveryEnabled += CharacterMana_OnManaRecoveryEnabled;
        
      
        
    }

    private void CharacterMana_OnManaRecoveryEnabled()
    {
        manaRegenParticle.Play();
    }

    private void Equipment_OnEquipmentHappened()
    {
        anim.SetFloat("AttackSpeed", characterStats.attackSpeed);
    }

    private void Respawn_OnRespawn()
    {
        anim.SetBool("Dead", false);
        StartCoroutine(RaiseCharacter());
       
    }

    private void DataManager_OnDataPulled()
    {
        for(int i = 0; i < characterStats.skillList.Count; i++)
        {
            characterSkills.Add(characterStats.skillList[i]);
        }
        characterMovementSpeed = characterStats.moveSpeed;
    }

   

    private void CharacterExperience_OnLevelUp()
    {
        levelUpParticle.Play();
    }

    private void CharacterHealth_OnHealthRecovery(float obj)
    {
        healthRegenParticle.Play();
    }

    private void CharacterHealth_OnPlayerDeath()
    {
        ChangeState(characterDeadState);
    }

    private void OnDisable()
    {
        CharacterHealth.OnPlayerDeath -= CharacterHealth_OnPlayerDeath;
        CharacterHealth.OnHealthRecovery -= CharacterHealth_OnHealthRecovery;
        CharacterExperience.OnLevelUp -= CharacterExperience_OnLevelUp;
        DataManager.OnDataPulled -= DataManager_OnDataPulled;
        Respawn.OnRespawn -= Respawn_OnRespawn;
        Equipment.OnEquipmentHappened -= Equipment_OnEquipmentHappened;
        CharacterMana.OnManaRecoveryEnabled -= CharacterMana_OnManaRecoveryEnabled;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Chest>(out Chest chest))
        {
            chest.Open();
        }
    }
    

   


    protected override CharacterBaseState GetInitialState()
    {
        return characterIdleState;
    }
    

    //works on animation
    public void CallAttackFunction()
    {
        characterAttackingState.Attack();
        
        
    }
  
    //works on animation
    public void CallAttackStateChangeFunction()
    {
        characterAttackingState.ChangeAttackState();
    }

    //works on animation
    public void PlayLeftHandWeaponParticle()
    {
        if (characterWeaponsParticle[0]!=null)
        {
            characterWeaponsParticle[0].Play();
        }
    }
    //works on animation
    public void PlayRightHandWeaponParticle()
    {
        if (characterWeaponsParticle[1] != null)
        {
            characterWeaponsParticle[1].Play();
        }
    }
    IEnumerator RaiseCharacter()
    {
        yield return new WaitForSeconds(2f);
        ChangeState(characterIdleState);
    }




}


