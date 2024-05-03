/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for camera control
 */
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float verticalOffset;
    [SerializeField] private float damping;
    [SerializeField] private float lookDown;
    [SerializeField] private Camera cam;
    [SerializeField] private FearOfUnknown unknown;

    private FearOfUnknown lostMeter;
    private float lookAhead;
    private Vector3 targetPosition;
    public float unknownMeter;

    private float camStart = 23.50195f;
    private float transitionSpeed = 2f;
    private void Start()
    {
        lostMeter = GameObject.FindObjectOfType<FearOfUnknown>();
    }
    private void Update()
    {
        // Horizontal camera movement
        targetPosition = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

        // Vertical camera movement
        float targetYPosition = player.position.y + verticalOffset;
        unknownMeter = lostMeter.fearMeter.fillAmount;

        
        // fear of lost cam, zooming
        if (!unknown.isFeared)
        {
            unknownMeter = 0f;
        }

        if (unknownMeter <= 0.3f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camStart, Time.deltaTime * transitionSpeed);
            targetYPosition = player.position.y + verticalOffset;

        }
        else if (unknownMeter <= 0.5f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 20f, Time.deltaTime * transitionSpeed);
            targetYPosition -= 2f;

        }
        else if (unknownMeter <= 0.8f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 13f, Time.deltaTime * transitionSpeed);
            targetYPosition -= 6f;
        }
        else if (unknownMeter <= 1f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10f, Time.deltaTime * transitionSpeed);
            targetYPosition -= 10f;
        }
        // Check if the "S" key is being held down
        if (Input.GetKey(KeyCode.S))
        {
            // Move the camera down
            targetYPosition -= lookDown; // Adjust this value as needed for desired downward movement speed
        }
        targetPosition.y = Mathf.Lerp(transform.position.y, targetYPosition, Time.deltaTime * damping);

        transform.position = targetPosition;
    }
}

