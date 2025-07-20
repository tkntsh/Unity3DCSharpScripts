using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movePersonInCity : MonoBehaviour
{
    public FixedJoystick joystick; // Assign your joystick here
    public float moveSpeed = 2f; // Speed of the player

    // UI elements for displaying information
    public Text infoText;
    public Text locationText;
    public GameObject textInfo;
    public GameObject textLocation;
    public Camera mainCamera;
    public Rigidbody rb;
    private Vector3 offset;
    public GameObject car2;
    public float doorOpenAngle = 90f;
    public float doorOpenSpeed = 2f;
    //car2.tag = "car2";

    //public float rotationSpeed = 5f;
    //public float rotationDamping = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(mainCamera != null)
        {
            offset = mainCamera.transform.position - rb.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FixedUpdate()
    {
        // Player movement using joystick
        Vector3 moveDirection = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        rb.velocity = moveDirection;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
            //RotateCamera();
        }
    }

    private void DisplayInfo(GameObject obj, Vector3 location)
    {
        string info = "";
        string locationTxt = "";
        if (obj.CompareTag("Store"))
        {
            info = "This is a Nail Saloon.";
            locationTxt = "Location: " + location.ToString();
        }
        else if (obj.CompareTag("Car"))
        {
            info = "This is a Car.";
            locationTxt = "Location: " + location.ToString();
        }
        else if (obj.CompareTag("PetrolStation"))
        {
            info = "This is a Petrol Station.";
            locationTxt = "Location: " + location.ToString();
        }
        else if (obj.CompareTag("Building1"))
        {
            info = "This is a building with 4 stories.\nThis building has 36 units.";
            locationTxt = "Location: " + location.ToString();
        }
        else if (obj.CompareTag("Building2"))
        {
            info = "This is a building with 3 stories.\nThis building has 24 units.";
            locationTxt = "Location: " + location.ToString();
        }
        else if (obj.CompareTag("House1"))
        {
            info = "This is a house with 1 story.\nThis building has a basement.";
            locationTxt = "Location: " + location.ToString();
        }
        else if (obj.CompareTag("House2"))
        {
            info = "This is a house with 1 story.\nThis building has no basement.";
            locationTxt = "Location: " + location.ToString();
        }
        else if (obj.CompareTag("Door"))
        {
            StartCoroutine(OpenDoor(obj.transform));
        }
        textInfo.GetComponent<Text>().text = info;
        textLocation.GetComponent<Text>().text = locationTxt;
    }
    private void OnTriggerEnter(Collider other)
    {
        DisplayInfo(other.gameObject, other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        ClearInfo();
    }
    private void ClearInfo()
    {
        textInfo.GetComponent<Text>().text = "";
        textLocation.GetComponent<Text>().text = "";
    }
    /*private void DisplayInfo(string info, Vector3 location)
    {
        infoText.text = info;
        locationText.text = "Location: " + location.ToString();
    }*/

    /*private void FollowPlayer()
    {
        // Make the main camera follow the player
        if (mainCamera != null)
        {
            mainCamera.transform.position = rb.transform.position + offset;
        }
    }*/

    private void FollowPlayer()
    {
        // Calculate the initial camera position relative to the player
        Vector3 initialCameraPosition = rb.transform.position + offset;

        // Calculate the target position behind the player
        Vector3 targetPosition = rb.transform.position - rb.transform.forward * offset.magnitude;

        // Set the target position's y-coordinate to match the initial camera position's y-coordinate
        targetPosition.y = initialCameraPosition.y;

        // Smoothly move the camera towards the target position
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Rotate the camera to look at the player
        mainCamera.transform.LookAt(rb.transform);
    }


    private IEnumerator OpenDoor(Transform door)
    {
        Quaternion startRotation = door.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, doorOpenAngle, 0);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * doorOpenSpeed;
            door.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
    }
    /*private void RotateCamera()
    {
        // Rotate the camera to view the player's back end
        if (mainCamera != null)
        {
            // Calculate the desired rotation
            Quaternion desiredRotation = Quaternion.LookRotation(-transform.forward);

            // Apply damping for smooth rotation
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime / rotationDamping);
        }
    }*/
}
