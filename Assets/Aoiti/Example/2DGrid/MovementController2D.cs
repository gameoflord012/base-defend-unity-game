using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding; //import the pathfinding library 
using System.Linq;


//public class NavigationTest: MonoBehaviour
//{
//    Pathfinder<Vector3> pathfinder;
//    List<Vector3> path = new List<Vector3>();

//    private void Start()
//    {
//        pathfinder = new Pathfinder<Vector3>(GetDistance, GetNeighbourNodes);
//    }
//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0)) //check for a new target
//        {
//            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            var _path = new Vector3[0];
//            if (pathfinder.GenerateAstarPath(transform.position, target, out _path)) //if there is a path from current position to target position reassign path.
//                path = new List<Vector3>(_path); 
//        }

//        transform.position = path[0]; //go to next node
//        path.RemoveAt(0); //remove the node from path

//    }

//    float GetDistance(Vector3 A, Vector3 B)
//    {
//        return (A - B).sqrMagnitude; 
//    }
//    Dictionary<Vector3, float> GetNeighbourNodes(Vector3 pos)
//    {
//        Dictionary<Vector3, float> neighbours = new Dictionary<Vector3, float>();
//        for (int i = -1; i < 2; i++)
//        {
//            for (int j = -1; j < 2; j++)
//            {
//                for (int k=-1;k<2;k++)
//                {

//                    if (i == 0 && j == 0 && k==0) continue;

//                    Vector3 dir = new Vector3(i, j,k);
//                    if (!Physics2D.Linecast(pos, pos + dir))
//                    {
//                        neighbours.Add(pos + dir, dir.magnitude);
//                    }
//                }
//            }

//        }
//        return neighbours;
//    }

//}

public class MovementController2D : MonoBehaviour
{
    [Header("Navigator options")]
    [SerializeField] float gridSize = 0.5f; //increase patience or gridSize for larger maps
    [SerializeField] float speed = 0.05f; //increase for faster movement
    
    Pathfinder<Vector2> pathfinder; //the pathfinder object that stores the methods and patience
    [Tooltip("The layers that the navigator can not pass through.")]
    [SerializeField] LayerMask obstacles;
    [Tooltip("Deactivate to make the navigator move along the grid only, except at the end when it reaches to the target point. This shortens the path but costs extra Physics2D.LineCast")] 
    [SerializeField] bool searchShortcut =false; 
    [Tooltip("Deactivate to make the navigator to stop at the nearest point on the grid.")]
    [SerializeField] bool snapToGrid =false;

    [SerializeField] float maxDistanceBetween = 100;

    List <Vector2> path;

    [SerializeField] Rigidbody2D rd2d;
    [SerializeField] CircleCollider2D agentCollider;

    public Vector2 GetAgentPosition()
    {
        return agentCollider.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes,1000); //increase patience or gridSize for larger maps
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) //check for a new target
        //{
        //    GetMoveCommand(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //}

        //if (pathLeftToGo.Count > 0) //if the target is not yet reached
        //{
        //    Vector2 jumpTarget = pathLeftToGo[0] + ((Vector2)rd2d.transform.position - GetAgentPosition());

        //    Vector2 dir = (jumpTarget - (Vector2)rd2d.transform.position);
        //    if(dir.magnitude < waypointOffsetThreshold)
        //    {
        //        rd2d.velocity = Vector2.zero;
        //        pathLeftToGo.RemoveAt(0);
        //    }
        //    else
        //    {
        //        rd2d.velocity = dir.normalized * speed;
        //    }
        //}
    }

    public List<Vector2> GeneratePath(Vector2 source, Vector2 target)
    {
        Vector2 closestNode = GetClosestNode(source);
        List<Vector2> pathLeftToGo = new List<Vector2> { source };

        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            pathLeftToGo.AddRange(path);
            if (!snapToGrid) pathLeftToGo.Add(target);

            if (searchShortcut && path.Count > 0)
                pathLeftToGo = SplitPath(ShortenPath(path), maxDistanceBetween);
        }

        pathLeftToGo.RemoveAt(0);

        return pathLeftToGo;
    }


    Vector2 GetClosestNode(Vector2 target) 
    {
        return new Vector2(Mathf.Round(target.x/gridSize)*gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    float GetDistance(Vector2 A, Vector2 B) 
    {
        return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }

    Dictionary<Vector2,float> GetNeighbourNodes(Vector2 pos) 
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i=-1;i<2;i++)
        {
            for (int j=-1;j<2;j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j)*gridSize;

                Vector2 neighbour = GetClosestNode(pos + dir);

                if (Passable(pos, neighbour))
                {
                    neighbours.Add(neighbour, dir.magnitude);
                }
            }

        }
        return neighbours;
    }

    
    List<Vector2> ShortenPath(List<Vector2> path)
    {
        List<Vector2> newPath = new List<Vector2>();

        for (int i=0;i<path.Count;i++)
        {
            newPath.Add(path[i]);
            for (int j=path.Count-1;j>i;j-- )
            {
                if(Passable(path[i],path[j]))
                {
                    i = j;
                    break;
                }
            }
            newPath.Add(path[i]);
        }
        newPath.Add(path[path.Count - 1]);
        return newPath;
    }

    static public List<Vector2> SplitPath(List<Vector2> path, float splitDistance)
    {
        List<Vector2> result = new();
        for (int i = 0; i < path.Count - 1; i++)
        {
            result.Add(path[i]);
            Vector2 point = path[i];
            Vector2 dir = path[i + 1] - point;

            int numSplit = (int)(dir.magnitude / splitDistance);

            for (float dis = splitDistance; dis < dir.magnitude; dis += splitDistance)
            {
                result.Add(point + dir.normalized * dis);
            }
        }
        result.Add(path.Last());

        return result;
    }

    bool Passable(Vector2 source, Vector2 dest)
    {
        Vector2 trueSource = source + agentCollider.offset * agentCollider.transform.lossyScale;
        Vector2 trueDest = dest + agentCollider.offset * agentCollider.transform.lossyScale;

        return !Physics2D.CircleCast(
            trueSource, agentCollider.radius * agentCollider.transform.lossyScale.y, trueDest - trueSource, 
            (trueDest - trueSource).magnitude, obstacles);
    }
}
