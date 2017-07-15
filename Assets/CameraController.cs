using UnityEngine;

public class CameraController : MonoBehaviour
{

    private bool doMovement = true;

    public float panSpeed = 10.0f;
    public float panBorderThickness = 50.0f;

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    private Vector2 prevTouchPos, nowTouchPos = Vector2.zero;


    private void Update()
    {

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (!doMovement)
            return;


        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }


#else
        if (!doMovement)
            return;
        
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                prevTouchPos = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                
                nowTouchPos = touch.position - touch.deltaPosition;
                
                if (prevTouchPos.y - nowTouchPos.y < 0)
                {
                    transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
                }
                
                if (prevTouchPos.y - nowTouchPos.y > 0)
                {
                    transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
                }

                if (prevTouchPos.x - nowTouchPos.x > 0)
                {
                    transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
                }

                if (prevTouchPos.x - nowTouchPos.x < 0)
                {
                    transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
                }
            }
        }

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (Camera.main.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 30.0f, 60.0f);
            }
        }
#endif

    }

}