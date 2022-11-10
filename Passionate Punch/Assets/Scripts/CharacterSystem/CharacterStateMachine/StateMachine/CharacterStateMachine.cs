using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

public class CharacterStateMachine : MonoBehaviour
{
    CharacterBaseState currentState;

   
    private void Start()
    {
        currentState = GetInitialState();
        if (currentState!=null)
        {
            currentState.Enter();
            
        }
    }
    private void FixedUpdate()
    {
        if (currentState!=null)
        {
            currentState.FixedUpdate();
        }
    }
    private void Update()
    {
        if (currentState!=null)
        {
            currentState.Update();
        }
    }
    private void LateUpdate()
    {
        if (currentState!=null)
        {
            currentState.LateUpdate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (currentState!=null)
        {
            currentState.OnTriggerEnter(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (currentState!=null)
        {
            currentState.OnTriggerStay(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (currentState!=null)
        {
            currentState.OnTriggerExit(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentState!=null)
        {
            currentState.OnCollisionEnter(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (currentState!=null)
        {
            currentState.OnCollisionStay(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (currentState!=null)
        {
            currentState.OnCollisionExit(collision);
        }
        
    }


    public void ChangeState(CharacterBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

   


    protected virtual CharacterBaseState GetInitialState()
    {
        return null;
    }


    //for debugging
     private void OnGUI()
     {
         GUILayout.BeginArea(new Rect(10f, 10f, 200f, 100f));
         string content = currentState != null ? currentState.name : "(no current state)";
         GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
         GUILayout.EndArea();
     }
    
}
