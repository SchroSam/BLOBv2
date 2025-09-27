using UnityEngine;

public class LimbInJelly : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the parent bounds (assuming the parent has a collider)
        Collider parentCollider = transform.parent.GetComponent<Collider>();
        if (parentCollider != null)
        {
            Bounds bounds = parentCollider.bounds;

            // Clamp the object's position within the parent's bounds
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
            position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
            position.z = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);

            transform.position = position;
        }
    }
}
