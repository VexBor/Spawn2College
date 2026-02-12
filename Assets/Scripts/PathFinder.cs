using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private LineRenderer line;
    [SerializeField] private NavMeshPath path;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        path = new NavMeshPath();
    }

    private void Update()
    {
        if (startPoint == null || endPoint == null) return;

        if (NavMesh.CalculatePath(startPoint.position, endPoint.position, NavMesh.AllAreas, path))
        {
            line.positionCount = path.corners.Length;

            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 point = path.corners[i] + Vector3.up * 0.1f;
                line.SetPosition(i, point);
            }
        }
    }

    public void SetTarget(Transform target){
        endPoint = target;
    }
}