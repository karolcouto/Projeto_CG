using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // quem a câmera segue
    public Vector3 offset = new Vector3(0, -5, -9);

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
    
}