using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using Items;
using SkillSystem;

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


    private void Awake()
    {
        characterIdleState = new CharacterIdleState(this);
        characterMovingState = new CharacterMovingState(this);
        characterAttackingState = new CharacterAttackingState(this);
        characterRecoveryState = new CharacterRecoveryState(this);
        characterSkillCastState = new CharacterSkillCastState(this);
        characterDeadState = new CharacterDeadState(this);



        characterMovementSpeed = characterStats.moveSpeed;
        characterSkills = characterStats.skillList;
        canVisible = true;
        
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


    


}
