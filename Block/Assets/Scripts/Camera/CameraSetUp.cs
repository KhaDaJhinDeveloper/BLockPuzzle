using UnityEngine;

public class CameraSetUp : MonoBehaviour
{
    Camera cameraObject;
    void Start()
    {
        this.cameraObject = Camera.main;
        SetUp();
    }

    void SetUp()
    {
        this.cameraObject.transform.position = new Vector3(Board.Size/2, Board.Size*0.35f, -10);
    }
}
