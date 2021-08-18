using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Target targetPrefab;

    [SerializeField]
    private Target onetimeTargetPrefab;

    private int targetCount = 15;

    [SerializeField]
    private float PositionRadius = 3f;

    [SerializeField]
    private float yOffset = 3f;

    [SerializeField]
    private List<Enemy> enemyPrefab;

    [SerializeField]
    private LineRenderer lineRenderer;

    private List<Target> targets = new List<Target>();

    private Player player;

    private Target target;

    private const float PI = 3.14159f;

    public void Init()
    {
        player = FindObjectOfType<Player>();

        targetCount = GameManager.Instance.GetGameStates.LevelItemCount;

        lineRenderer.positionCount = targetCount;

        CreateTargets();

       // CreateOnWaterTarget();
    }

    private void CreateTargets()
    {
        Vector3 targetPosition = Vector3.zero;

        GameObject container = new GameObject("Container");

        container.transform.position = Vector3.zero;

        int random = Random.Range(3, 15);
        var temp = new Target();
        int k = 1;
        int t = 1;

        for (int i = 0; i < targetCount; i++)
        {
            if(target != null)
            {
                temp = target;
            }

            target = Instantiate(targetPrefab);
            targets.Add(target);


            float angle = i * (2 * PI / 20);

            float angleX =  i * (2 * PI / 20);
        
            if (i < 20 * k)
            {
                angle = i * (2 * PI / 10);
            }
            else if (i % 40 == 0)
            {
                k *= 3;
            }

            float x = Mathf.Cos(angle) * PositionRadius;

            float z = Mathf.Sin(angleX) * PositionRadius;

            if(i < 10)
            {
                z = Mathf.Sin(angleX);
                x = 7;
            }

            targetPosition = new Vector3(targetPosition.x + x, targetPosition.y + yOffset, targetPosition.z + z);

            target.transform.position = targetPosition;

            target.transform.SetParent(container.transform);

            lineRenderer.SetPosition(i, target.transform.localPosition);

            if(i == 0)
            {
                target.Init(GameObject.FindGameObjectWithTag("Finish").GetComponent<Target>(), 0);
            }
            else
            {
                target.Init(temp, i);
            }
            
        }

        player.transform.position = new Vector3(targetPosition.x, targetPosition.y + 1f, targetPosition.z);

        CreateEnemy();

        container.AddComponent<ContainerComponent>();

        lineRenderer.transform.parent = container.transform;
        lineRenderer.transform.localPosition = Vector3.zero;
    }

    private void CreateEnemy()
    {
        int k = 0;
        for(int i = targets.Count - 2; i >targets.Count - 2 - 4; i--)
        {
            Enemy enemy = Instantiate(enemyPrefab[k]);

            Vector3 pos = targets[i].transform.position;
            enemy.transform.position = new Vector3(pos.x, pos.y + 1, pos.z);

            k++;
        }
        
    }

    private void CreateOnWaterTarget()
    {

        float xoffset = 0;
        float zOffset = 1;

        for (int i = 0; i < 40; i++)
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


}
