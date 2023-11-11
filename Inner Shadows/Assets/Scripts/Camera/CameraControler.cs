using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosiX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistanece;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosiX, transform.position.y, transform.position.z), ref velocity, speed);

        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistanece * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosiX = _newRoom.position.x;
        
    }
}
