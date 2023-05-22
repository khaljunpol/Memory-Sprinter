using UnityEngine;
using System.Collections.Generic;

public class SphereObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int tileCount = 8;
    [SerializeField] private bool fitObjectsToSurface = true;
    [SerializeField] private float minObjectScale = 0.5f;
    [SerializeField] private float maxObjectScale = 1.5f;
    [SerializeField] private float minObjectSpace = 1f;
    [SerializeField] private int randomSeed = 123;

    private HashSet<Vector3> placedPositions = new HashSet<Vector3>();

    private void Start()
    {
        PlaceObjectsOnSphere();
    }

    private void PlaceObjectsOnSphere()
    {
        if (objectPrefab == null)
        {
            Debug.LogError("Object Prefab is not assigned!");
            return;
        }

        float angleIncrement = 360f / tileCount;
        float verticalAngle = angleIncrement / 2f;
        float horizontalAngle = 0f;

        float parentScale = transform.localScale.x / 2;
        float objectSpace = minObjectSpace * parentScale;

        Random.State originalRandomState = Random.state;
        Random.InitState(randomSeed);

        int placedObjects = 0;

        while (placedObjects < tileCount)
        {
            Vector3 position = GetPositionOnSphere(verticalAngle, horizontalAngle);
            float objectScale = Random.Range(minObjectScale, maxObjectScale);
            bool canPlace = CheckObjectOverlap(position, objectScale, objectSpace);

            if (canPlace)
            {
                GameObject obj = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(transform);

                if (fitObjectsToSurface)
                {
                    FitObjectToSurface(obj.transform, position);
                }

                ScaleObject(obj.transform, objectScale);

                obj.transform.localPosition = transform.InverseTransformPoint(position * parentScale);
                placedPositions.Add(position);
                placedObjects++;
            }

            horizontalAngle += angleIncrement;

            if (horizontalAngle >= 360f)
            {
                horizontalAngle -= 360f;
                verticalAngle += angleIncrement;
            }
        }

        Random.state = originalRandomState;
    }

    private Vector3 GetPositionOnSphere(float verticalAngle, float horizontalAngle)
    {
        float radVertical = verticalAngle * Mathf.Deg2Rad;
        float radHorizontal = horizontalAngle * Mathf.Deg2Rad;

        float x = Mathf.Sin(radVertical) * Mathf.Cos(radHorizontal);
        float y = Mathf.Cos(radVertical);
        float z = Mathf.Sin(radVertical) * Mathf.Sin(radHorizontal);

        return new Vector3(x, y, z);
    }

    private bool CheckObjectOverlap(Vector3 position, float objectScale, float objectSpace)
    {
        foreach (var placedPosition in placedPositions)
        {
            if (Vector3.Distance(position, placedPosition) < objectSpace * objectScale)
            {
                return false;
            }
        }

        return true;
    }

    private void FitObjectToSurface(Transform objTransform, Vector3 position)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, position);
        objTransform.rotation = rotation;
    }

    private void ScaleObject(Transform objTransform, float objectScale)
    {
        objTransform.localScale = Vector3.one * objectScale;
    }
}
