using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseState 
{
    public string name;

    protected CharacterStateMachine stateMachine;

    public CharacterBaseState(string name, CharacterStateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void FixedUpdate() { }

    public virtual void Update() { }

    public virtual void LateUpdate() { }

    public virtual void OnTriggerEnter(Collider other) { }

    public virtual void OnTriggerStay(Collider other) { }

    public virtual void OnTriggerExit(Collider other) { }

    public virtual void OnCollisionEnter(Collision collision) { }

    public virtual void OnCollisionStay(Collision collision) { }

    public virtual void OnCollisionExit(Collision collision) { }

    public virtual void Exit() { }

    public virtual void Dead() { }
}
