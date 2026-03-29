using UnityEngine;

[CreateAssetMenu(fileName = "ArrowData", menuName = "Game/Arrow Data")]
public class ArrowData : ScriptableObject
{
    public string arrowName;

    [Header("Stats")]
    public float damage = 25f;
    public float speed = 20f;
    public float lifeTime = 5f;

    [Header("Physics")]
    public bool affectedByGravity = true;

    [Header("Effects")]
    public ArrowEffect[] effects;
}
