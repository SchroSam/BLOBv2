using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class StaticCircleMesh : MonoBehaviour
{
    public int segments = 32;
    public float radius = 1f;

    // void Awake()
    // {
    //     MeshFilter meshFilter = GetComponent<MeshFilter>();
    //     meshFilter.mesh = GenerateCircleMesh(radius, segments);
    // }

    public static Mesh GenerateCircleMesh(float radius, int segments)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[segments + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[segments * 3];

        // Center vertex
        vertices[0] = Vector3.zero;
        uv[0] = new Vector2(0.5f, 0.5f);

        // Outer ring
        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2f;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            vertices[i + 1] = new Vector3(x, y, 0f);
            uv[i + 1] = new Vector2((x / radius + 1f) * 0.5f, (y / radius + 1f) * 0.5f);

            // Triangle indices
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            //triangles[i * 3 + 2] = (i + 2) % (segments + 1);
            triangles[i * 3 + 2] = (i + 2 > segments) ? 1 : i + 2;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class StaticSquareMesh : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = GenerateSquareMesh();
    }

    public static Mesh GenerateSquareMesh()
    {
        Mesh mesh = new Mesh();

        // Define 4 corner vertices (clockwise)
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, 0f), // Bottom Left
            new Vector3( 0.5f, -0.5f, 0f), // Bottom Right
            new Vector3( 0.5f,  0.5f, 0f), // Top Right
            new Vector3(-0.5f,  0.5f, 0f)  // Top Left
        };

        // Define UVs for texture mapping
        Vector2[] uv = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f)
        };

        // Define two triangles (0-1-2 and 2-3-0)
        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 3, 0
        };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}



