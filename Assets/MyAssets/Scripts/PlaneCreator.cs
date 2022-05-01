using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCreator : MonoBehaviour
{
    /*[SerializeField] private Vector3 positionPlane;
    [SerializeField] private Transform parentObject;

    [Header("CreatePlane Settings")]
    [SerializeField] private Material planeMaterial;
    [SerializeField] private float width;
    [SerializeField] private float depth;

    [Header("CreatePlaneFromVectors Settings")]
    [SerializeField] private Material planeMaterial2;
    [SerializeField] private Vector3 vertex_1, vertex_2, vertex_3, vertex_4;

    private void Start()
    {
        PlaneCreatorHelper.CreatePlane(width, depth, positionPlane, parentObject, true, planeMaterial);
        PlaneCreatorHelper.CreatePlaneFromVectors(vertex_1, vertex_2, vertex_3, vertex_4, positionPlane, parentObject, true, planeMaterial2);
    }*/
    [Header("Fighting Zone Settings")]
    [SerializeField] private Transform parentObject;
    [SerializeField] private Material planeMaterial;
    [SerializeField] private float zoneWidth = 1000f;
    [SerializeField] private float zoneDepth = 1000f;
    [SerializeField] private float vertexInterval = 50f;
    [SerializeField] private float maxVertexHeightDisplacement = 5f;
    [SerializeField] private float minVertexHeightDisplacement = -5f;

    private int rowAmount;
    private int colAmount;
    private Vector3[,] vertices;
    private Vector2Int vertex2Offset = new Vector2Int(0,1);
    private Vector2Int vertex3Offset = new Vector2Int(1, 1);
    private Vector2Int vertex4Offset = new Vector2Int(1, 0);

    private void Start()
    {
        Instantiate();
        CalculateAllVertices();
        //GeneratePlanesFromVertices();
        GeneratePlaneFromArrayVertices();
    }

    private void Instantiate()
    {
        // The + 1 is to include the first vertex in the array aswell.
        rowAmount = Mathf.CeilToInt(zoneDepth / vertexInterval) + 1;
        colAmount = Mathf.CeilToInt(zoneWidth / vertexInterval) + 1;

        vertices = new Vector3[rowAmount, colAmount];
    }

    private void CalculateAllVertices()
    {
        float randomHeightDisplacement;
        float width, height, depth;
        float minHeight, maxHeight;

        for (int row = 0; row < rowAmount; row++)
        {
            depth = row * vertexInterval;
            height = 0f;

            for (int col = 0; col < colAmount; col++)
            {
                width = col * vertexInterval;

                randomHeightDisplacement = Random.Range(minVertexHeightDisplacement, maxVertexHeightDisplacement);
                height += randomHeightDisplacement;

                if(row != 0)
                {
                    minHeight = vertices[row - 1, col].y + minVertexHeightDisplacement;
                    maxHeight = vertices[row - 1, col].y + maxVertexHeightDisplacement;
                    height =  Mathf.Clamp(height, minHeight, maxHeight);
                }

                vertices[row, col] = new Vector3(width, height, depth);
            }
        }
    }

    private void GeneratePlaneFromArrayVertices()
    {
        PlaneCreatorHelper.CreatePlaneFrom2DArrayVertices(vertices, parentObject, true, planeMaterial);
    }

    private void GeneratePlanesFromVertices()
    {
        // The - 1 is to stop the loop before it hits the last row. To prevent out of range exceptions.
        for (int row = 0; row < (rowAmount - 1); row++)
        {
            // The - 1 is to stop the loop before it hits the last col. To prevent out of range exceptions.
            for (int col = 0; col < (colAmount - 1); col++)
            {
                GeneratePlaneFromVertices(row, col);
            }
        }
    }

    private void GeneratePlaneFromVertices(int row, int col)
    {
        Vector3 vertex_1 = vertices[row, col];
        Vector3 vertex_2 = vertices[row + vertex2Offset.x, col + vertex2Offset.y];
        Vector3 vertex_3 = vertices[row + vertex3Offset.x, col + vertex3Offset.y];
        Vector3 vertex_4 = vertices[row + vertex4Offset.x, col + vertex4Offset.y];

        PlaneCreatorHelper.CreatePlaneFromVectors(vertex_1, vertex_2, vertex_3, vertex_4,
                                                parentObject, true, planeMaterial);
    }
}
