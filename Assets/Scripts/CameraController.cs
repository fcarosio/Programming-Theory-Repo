using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject focalPoint;
    [SerializeField] float movementStep;
    [SerializeField] float rotationStep;

    private float[] playerAngle = new float[2] { Board.FORWARD, Board.REVERSE };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);

        float speedUpFactor = shiftPressed ? 2.5f : 1.0f;

        float angle = rotationStep * Time.fixedDeltaTime * hInput * speedUpFactor;
        focalPoint.transform.Rotate(Vector3.up, angle);
    }

    public void SetViewFor(GameManager.PlayerData playerData)
    {
        int colorIndex = (int)playerData.GetColor();
        focalPoint.transform.rotation = Quaternion.Euler(0.0f, playerAngle[colorIndex], 0.0f);
    }
}
