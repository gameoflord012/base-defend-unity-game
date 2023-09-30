using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aoiti.Pathfinding;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] MovementController2D pathFinder;
    [SerializeField] Rigidbody2D rd2d;
    [SerializeField] UnityEvent onFigureStartJumping;

    [SerializeField] float waypointOffsetThreshold = 0.05f;
    [SerializeField] float travelDuration = 1.5f;
    [SerializeField] float restDuration = 1;
    [SerializeField] float rescanDuration = 0.5f;

    List<Vector2> thePath = new List<Vector2>();
    Vector2 agentStartJumpPosition;

    float rescanTimer = 100;
    float restTimer = 100;

    bool isJumping = false;


    private void Start()
    {
        pathFinder = pathFinder ? pathFinder : GetComponentInChildren<MovementController2D>();
    }

    private void Update()
    {
        if (rescanTimer > rescanDuration)
        {
            RescanNewPath();
            rescanTimer = 0;
        }

        if (thePath.Count() > 0)
        {
            Vector2 dirToDest = GetCurrentDest() - pathFinder.GetAgentPosition();

            bool isReachDest = dirToDest.magnitude < waypointOffsetThreshold;
            if (isReachDest)
            {
                rd2d.velocity = Vector2.zero;
                restTimer = 0;
                isJumping = false;

                thePath.RemoveAt(0);
            }
            else if (restTimer > restDuration)
            {
                rd2d.velocity = dirToDest.normalized * ((GetCurrentDest() - agentStartJumpPosition).magnitude / travelDuration);

                if (!isJumping)
                {
                    isJumping = true;
                    onFigureStartJumping.Invoke();
                    agentStartJumpPosition = pathFinder.GetAgentPosition();
                }
            }
        }

        UpdateTimer();
    }

    private Vector2 GetCurrentDest()
    {
        return thePath[0];
    }

    private void UpdateTimer()
    {
        rescanTimer += Time.deltaTime;
        restTimer += Time.deltaTime;
    }

    private void RescanNewPath()
    {
        if (thePath.Count > 0)
        {
            thePath.RemoveRange(1, thePath.Count - 1);
        }
        else
        {
            thePath.Add(pathFinder.GetAgentPosition());
        }

        thePath.AddRange(pathFinder.GeneratePath(GetCurrentDest(), followTarget.transform.position));

        SubtiutePath();
    }
    private void SubtiutePath()
    {
        while (
            thePath.Count > 0 &&
            (pathFinder.GetAgentPosition() - GetCurrentDest()).magnitude < waypointOffsetThreshold)
        {
            thePath.RemoveAt(0);
        }
    }

    private void OnDrawGizmos()
    {

        if ( thePath.Count > 0)
        {
            Gizmos.DrawLine(pathFinder.GetAgentPosition(), GetCurrentDest());
            Gizmos.DrawSphere(GetCurrentDest(), 0.2f);

            for (int i = 0; i < thePath.Count - 1; i++) //visualize your path in the sceneview
            {
                Gizmos.DrawLine(thePath[i], thePath[i + 1]);
                Gizmos.DrawSphere(thePath[i + 1], 0.2f);
            }
        }
    }
}
