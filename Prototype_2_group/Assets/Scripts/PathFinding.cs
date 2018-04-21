using UnityEngine;
using System.Collections;

public class PathFinding : MonoBehaviour
{
    [Header("The Path Taken By Monster")]
    public Transform[] path;

    [Header("Monster Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float reachDist;

    private int currentPoint = 0;
    //public float rotateTime;

    // Update is called once per frame
    void Update()
    {
        //Vector3 dir = path[currentPoint].position - transform.position;
        //float rotateAngle = Vector3.SignedAngle(path[currentPoint].position - transform.position, Vector3.up, Vector3.forward);

        //LeanTween.rotateZ(gameObject, -rotateAngle, rotateTime);

        //Calculate the distance between the monster and the current point its going to
        float dist = Vector3.Distance(path[currentPoint].position, transform.position);

        //Lerp/move the monster to the current point its going to
        transform.position = Vector3.Lerp(transform.position, path[currentPoint].position, Time.deltaTime * speed);

        //If the monster is within the reach distance make it go to the next point
        if (dist <= reachDist)
        {
            currentPoint++;
        }

        //Make current point the first point if no more point in the path array
        if(currentPoint >= path.Length)
        {
            currentPoint = 0;
        }
    }

    //Has no effect on the game. Just draw on the scene window to know the path it will be taking
    private void OnDrawGizmos()
    {
        if (path.Length > 0)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                { 
                    Gizmos.DrawSphere(path[i].position, reachDist);
                }
            }

            for (int i = 0; i < path.Length - 1; i++)
            {
                if (path[i] != null && path[i + 1] != null)
                {
                    Gizmos.DrawLine(path[i].position, path[i + 1].position);
                }
            }

            if (path[path.Length - 1] != null)
            {
                Gizmos.DrawLine(path[path.Length - 1].position, path[0].position);
            }

        }
    }
}
