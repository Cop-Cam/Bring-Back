
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPreset", menuName = "Scriptable Objects/Player/PlayerPreset")]
public class PlayerPreset : ScriptableObject 
{
    [field: SerializeField] public GameObject playerMeshInformation { get; private set; }

    // [SerializeField] private Mesh playerMeshInformation;
    // public Mesh PlayerMeshInformation 
    // {
    //     get { return playerMeshInformation; }
    //     private set { playerMeshInformation = value; }
    // }

}

