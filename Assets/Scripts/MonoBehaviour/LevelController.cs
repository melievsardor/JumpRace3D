using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Target targetPrefab;

    [SerializeField]
    private int targetCount = 15;

    [SerializeField]
    private float PositionRadius = 3f;

    [SerializeField]
    private float yOffset = 3f;

    private Player player;

    private Target target;

    private const float PI = 3.14159f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        CreateTargets();
    }

    private void CreateTargets()
    {
        Vector3 targetPosition = Vector3.zero;

        GameObject container = new GameObject("Container");

        container.transform.position = Vector3.zero;

        int random = Random.Range(3, 15);
        var temp = new Target();
        int k = -1;

        for (int i = 0; i < targetCount; i++)
        {
            if(target != null)
            {
                temp = target;
            }

            target = Instantiate(targetPrefab);

            float angle = i * (2 * PI / 10);

            float x = Mathf.Cos(angle) * PositionRadius;

            float z = Mathf.Sin(angle) * PositionRadius;

           
            if (i != 0 && i % random != 0)
                z = 5f;

            targetPosition = new Vector3(targetPosition.x + x, targetPosition.y + yOffset, targetPosition.z + z);

            target.transform.position = targetPosition;

            target.transform.SetParent(container.transform);

            if(i == 0)
            {
                target.Neighbor = GameObject.FindGameObjectWithTag("Finish").GetComponent<Target>();
            }
            else
            {
                target.Neighbor = temp;
            }
            
        }

        player.transform.position = new Vector3(targetPosition.x, targetPosition.y + 1f, targetPosition.z);
      //  player.transform.eulerAngles = new Vector3(0f, 135f, 0f);

        container.AddComponent<ContainerComponent>();
    }

}
