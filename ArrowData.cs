using UnityEngine;

[CreateAssetMenu(fileName = "ArrowData", menuName = "Game/Arrow Data")]
public class ArrowData : ScriptableObject
{
    public ArrowType arrowType;

    [Header("Base Stats")]
    public float damage = 20f;
    public float speed = 20f;
    public float lifeTime = 5f;

    [Header("Physics")]
    public bool affectedByGravity = true;
    public float mass = 1f;

    [Header("Special")]
    public bool canHeadshot = true;
    public bool destroyOnHit = true;

    [Header("Effects")]
    public bool useEffect;
}
