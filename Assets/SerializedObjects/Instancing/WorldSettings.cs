using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldSettings", menuName = "ScriptableObjects/WorldSettings", order = 1)]
public class WorldSettings : ScriptableObject
{
    
    public float gravity { get { return _gravity; } set { _gravity = value; } }
    [SerializeField]
    private float _gravity = 9.87f;

}
