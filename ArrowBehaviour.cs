using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ArrowBehaviour : MonoBehaviour
{
    public ArrowData data;

    private Rigidbody2D rb;
    private bool hasHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float force)
    {
        rb.mass = data.mass;
        rb.gravityScale = data.affectedByGravity ? 1f : 0f;

        rb.linearVelocity = direction * force;

        Destroy(gameObject, data.lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHit) return;
        hasHit = true;

        // Check ragdoll
        RagdollCharacter ragdoll = collision.collider.GetComponentInParent<RagdollCharacter>();

        if (ragdoll != null)
        {
            bool isHeadshot = collision.collider.name.ToLower().Contains("head");

            float finalDamage = data.damage;

            if (isHeadshot && data.canHeadshot)
                finalDamage *= 2f;

            ragdoll.TakeDamage(finalDamage, isHeadshot);

            ApplyEffect(collision);
        }

        if (data.destroyOnHit)
            Destroy(gameObject);
    }

    private void ApplyEffect(Collision2D collision)
    {
        switch (data.arrowType)
        {
            case ArrowType.Fire:
                collision.collider.GetComponentInParent<RagdollCharacter>()?.ApplyBurn();
                break;

            case ArrowType.Ice:
                collision.collider.GetComponentInParent<RagdollCharacter>()?.ApplyFreeze();
                break;

            case ArrowType.Explosive:
                Explode();
                break;

            case ArrowType.Knockback:
                ApplyKnockback(collision);
                break;

            case ArrowType.Gravity:
                CreateGravityField();
                break;
        }
    }

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (var hit in hits)
        {
            var ragdoll = hit.GetComponentInParent<RagdollCharacter>();
            if (ragdoll != null)
                ragdoll.TakeDamage(data.damage);
        }
    }

    void ApplyKnockback(Collision2D collision)
    {
        Rigidbody2D targetRb = collision.collider.attachedRigidbody;
        if (targetRb != null)
        {
            targetRb.AddForce(rb.linearVelocity.normalized * 300f);
        }
    }

    void CreateGravityField()
    {
        // placeholder (we'll expand in Anti-Gravity system step)
    }
}
