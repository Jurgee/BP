using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float verticalOffset;
    [SerializeField] private float damping;
    private float lookAhead;



    private Vector3 targetPosition;

    private void Update()
    {
        // Horizontal camera movement
        targetPosition = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

        // Vertical camera movement
        float targetYPosition = player.position.y + verticalOffset;
        targetPosition.y = Mathf.Lerp(transform.position.y, targetYPosition, Time.deltaTime * damping);

        transform.position = targetPosition;
    }
}