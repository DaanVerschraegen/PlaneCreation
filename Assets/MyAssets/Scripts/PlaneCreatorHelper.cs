using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaneCreatorHelper
{
    public static GameObject CreatePlaneFrom2DArrayVertices(Vector3[,] vertices, Vector3 positionPlane, Transform parentObject,
                                                        bool collider, Material material)
    {
        GameObject gameObject = new GameObject("Plane");
        gameObject.transform.SetParent(parentObject);
        gameObject.transform.position = positionPlane;
        MeshFilter meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh mesh = new Mesh();
        int vertexRowAmount = vertices.GetLength(0);
        int vertexColAmount = vertices.GetLength(1);
        int vertexArrayLength = vertexRowAmount * vertexColAmount;
        Vector3[] arrayVertices = new Vector3[vertexArrayLength];
        for (int row = 0; row < vertexRowAmount; row++)
        {
            for (int col = 0; col < vertexColAmount; col++)
            {
                arrayVertices[row * vertexColAmount + col] = vertices[row, col];
            }
        }
        mesh.vertices = arrayVertices;

        Vector2[] uvs = new Vector2[vertexArrayLength];
        for (int i = 0; i < vertexArrayLength; i++)
        {
            uvs[i] = new Vector2(arrayVertices[i].x, arrayVertices[i].z);
        }
        mesh.uv = uvs;

        int[] triangles = new int[vertexArrayLength * 6];
        int tris = 0;
        // The - 1 is to stop the loop before it hits the last row. To prevent out of range exceptions.
        for (int row = 0; row < vertexRowAmount - 1; row++)
        {
            // The - 1 is to stop the loop before it hits the last col. To prevent out of range exceptions.
            // The + vertexColAmount is to get the vertex that is positioned above the current vertex's position.
            for (int col = 0; col < vertexColAmount - 1; col++)
            {
                int curentVert = row * vertexColAmount + col;
                triangles[tris + 0] = curentVert;
                triangles[tris + 1] = curentVert + vertexColAmount;
                triangles[tris + 2] = curentVert + 1;
                triangles[tris + 3] = curentVert + 1;
                triangles[tris + 4] = curentVert + vertexColAmount;
                triangles[tris + 5] = curentVert + vertexColAmount + 1;

                tris += 6;
            }
        }
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
        meshRenderer.material = material;

        if(collider)
        {
            (gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider).sharedMesh = mesh;
        }

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return gameObject;
    }

    public static GameObject CreatePlaneFrom2DArrayVertices(Vector3[,] vertices, Transform parentObject,
                                                        bool collider, Material material)
    {
        return CreatePlaneFrom2DArrayVertices(vertices, new Vector3(0, 0, 0), parentObject, collider, material);
    }

    /*Vertex positions plane:
        vertex_1 = bottom-left, vertex_2 = bottom-right, vertex_3 = top-right, vertex_4 = top-left
    */
    public static GameObject CreatePlaneFromVectors(Vector3 vertex_1, Vector3 vertex_2, Vector3 vertex_3, Vector3 vertex_4,
                                                    Vector3 positionPlane, Transform parentObject, bool collider, Material material)
    {
        GameObject gameObject = new GameObject("Plane");
        gameObject.transform.SetParent(parentObject);
        gameObject.transform.position = positionPlane;
        MeshFilter meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            vertex_1,
            vertex_2,
            vertex_3,
            vertex_4
        };

        mesh.uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };

        mesh.triangles = new int[]
        {
            2, 1, 0,
            3, 2, 0
        };

        meshFilter.mesh = mesh;
        meshRenderer.material = material;

        if(collider)
        {
            (gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider).sharedMesh = mesh;
        }

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return gameObject;
    }

    public static GameObject CreatePlaneFromVectors(Vector3 vertex_1, Vector3 vertex_2, Vector3 vertex_3, Vector3 vertex_4,
                                                    Transform parentObject, bool collider, Material material)
    {
        return CreatePlaneFromVectors(vertex_1, vertex_2, vertex_3, vertex_4, new Vector3(0, 0, 0), parentObject, collider, material);
    }

    public static GameObject CreatePlane(float width, float depth, Vector3 positionPlane, Transform parentObject,
                                        bool collider, Material material)
    {
        GameObject gameObject = new GameObject("Plane");
        gameObject.transform.SetParent(parentObject);
        gameObject.transform.position = positionPlane;
        MeshFilter meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(width, 0, depth),
            new Vector3(0, 0, depth)
        };

        mesh.uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };

        mesh.triangles = new int[]
        {
            2, 1, 0,
            3, 2, 0
        };

        meshFilter.mesh = mesh;
        meshRenderer.material = material;

        if(collider)
        {
            (gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider).sharedMesh = mesh;
        }

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return gameObject;
    }
}
