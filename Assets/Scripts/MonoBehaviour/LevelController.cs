using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Target targetPrefab;

    [SerializeField]
    private Target onetimeTargetPrefab;

    [SerializeField]
    private int targetCount = 15;

    [SerializeField]
    private float PositionRadius = 3f;

    [SerializeField]
    private float yOffset = 3f;

    [SerializeField]
    private Enemy enemyPrefab;

    private List<Target> targets = new List<Target>();

    private Player player;

    private Target target;

    private const float PI = 3.14159f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        CreateTargets();

        CreateOnWaterTarget();
    }

    
    private void CreateOnWaterTarget()
    {

        float xoffset = 0;
        float zOffset = 1;

        for(int i = 0; i < 40; i++)
        {
            Target rightInstance = Instantiate(onetimeTargetPrefab);
            Target leftInstance = Instantiate(onetimeTargetPrefab);

            xoffset++;

            float x = xoffset * 5f;
            float z = zOffset * 5f;

            if (i % 3 == 0)
            {
                zOffset++;
                xoffset = 0;
            }
               

            rightInstance.transform.position = new Vector3(x, 1f, z);
            leftInstance.transform.position = new Vector3(-x + 5f, 1f, z);

            rightInstance.transform.parent = transform;
            leftInstance.transform.parent = transform;
        }
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
            targets.Add(target);

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

        Enemy enemy = Instantiate(enemyPrefab);

        Vector3 pos = targets[targets.Count - 2].transform.position;
        enemy.transform.position = new Vector3(pos.x, pos.y + 1, pos.z);

        container.AddComponent<ContainerComponent>();
    }

}
