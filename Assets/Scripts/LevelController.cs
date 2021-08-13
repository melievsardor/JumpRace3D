using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPrefab;

    [SerializeField]
    private int targetCount = 15;

    [SerializeField]
    private float PositionRadius = 3f;

    [SerializeField]
    private float yOffset = 3f;

    private const float PI = 3.14159f;

    private void Awake()
    {
        CreateTargets();
    }

    private void CreateTargets()
    {
        Vector3 targetPosition = Vector3.zero;

        GameObject container = new GameObject("Container");

        container.transform.position = Vector3.zero;

        int random = Random.Range(3, 15);

        for (int i = 0; i < targetCount; i++)
        {
            GameObject instance = Instantiate(targetPrefab);

            float angle = i * (2 * PI / 10);

            float x = Mathf.Cos(angle) * PositionRadius;

            float z = Mathf.Sin(angle) * PositionRadius;

           
            if (i != 0 && i % random != 0)
                z = 5f;

            targetPosition = new Vector3(targetPosition.x + x, targetPosition.y + yOffset, targetPosition.z + z);

            instance.transform.position = targetPosition;

            instance.transform.SetParent(container.transform);
        }

        container.AddComponent<ContainerComponent>();
    }

}
