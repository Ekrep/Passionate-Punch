using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;   

public class ExpBar : MonoBehaviour
{
    Slider expBar;
    public CharacterSettings assassin, ranger;
    private void Start()
    {
        expBar = GetComponent<Slider>();
        // Update the experience bar values wheter player is an assassin or a ranger
        // Set the max experience point to reach next level.
        expBar.value = assassin.experience; // Temporary value
    }
}
