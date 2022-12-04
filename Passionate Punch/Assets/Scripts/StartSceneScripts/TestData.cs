using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using UnityEditor;

public class TestData : MonoBehaviour
{
    public CharacterSettings firstData;
    public CharacterSettings holderData;

    void Start()
    {
        holderData = ScriptableObject.CreateInstance<CharacterSettings>();
        holderData.attackDamage = firstData.attackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
