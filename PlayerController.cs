[SerializeField] private GameObject arrowPrefab;
[SerializeField] private ArrowData currentArrow;

public void Shoot(Vector2 direction, float force)
{
    GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

    ArrowBehaviour arrowScript = arrow.GetComponent<ArrowBehaviour>();
    arrowScript.data = currentArrow;
    arrowScript.Initialize(direction, force);
}
