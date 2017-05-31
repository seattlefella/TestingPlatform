using UnityEngine;

namespace Assets.Scripts
{

    // A more efficient realization of the ping-pong function.  
    public class PingPong1 : MonoBehaviour
    {

        [SerializeField]
        private Vector3 minOffset;
        [SerializeField]
        private Vector3 maxOffset;

        [SerializeField]
        private Vector3 moveUnitsPerSecond;
        [SerializeField]
        private Vector3 rotateUnitsPerSecond;

        // All of the critical points that we need to calculate positions.
        private Vector3 maxPos = new Vector3();
        private Vector3 minPos = new Vector3();

        // How far we have moved against how far we have to go.
        private Vector3 distanceToMove = new Vector3();

        // the direction of movement
        private Vector3 direction = new Vector3(1,1,1);


        void Start()
        {
            maxPos = transform.position + maxOffset;
            minPos = transform.position + minOffset;

            direction = Vector3.one;

        }
        // Update is called once per frame
        void Update ()
        {
            // Calculate the incremental motion of this frame
            distanceToMove.x = direction.x * (Time.deltaTime * moveUnitsPerSecond.x);
            distanceToMove.y = direction.y * (Time.deltaTime * moveUnitsPerSecond.y);
            distanceToMove.z = direction.z * (Time.deltaTime * moveUnitsPerSecond.z);

            // Every time we travel the full range we must flip the direction of movement
            calculateDirection();

            // execute the movement
            transform.Translate(distanceToMove, Space.World);
            transform.Rotate(rotateUnitsPerSecond * Time.deltaTime, Space.World);
        }

        private void calculateDirection()
        {

            if (direction.x > 0 && transform.position.x >= maxPos.x)
            {
                direction.x = direction.x * (-1);
            }
            else if (direction.x <= 0 && transform.position.x <= minPos.x)
            {
                direction.x = direction.x * (-1);
            }


            if (direction.y >= 0 && transform.position.y >= maxPos.y)
            {
                direction.y = direction.y * (-1);
            }
            else if (direction.y <= 0 && transform.position.y <= minPos.y)
            {
                direction.y = direction.y * (-1);
            }

            if (direction.z >= 0 && transform.position.z >= maxPos.z)
            {
                direction.z = direction.z * (-1);
            }
            else if (direction.z <= 0 && transform.position.z <= minPos.z)
            {
                direction.z = direction.z * (-1);
            }
        }
    }
}