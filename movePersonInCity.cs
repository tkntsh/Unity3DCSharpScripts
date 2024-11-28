using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movePersonInCity : MonoBehaviour
{
    //joystick to be used on the ui
    public FixedJoystick joystick; 
    //speed implementation of joystick
    public float moveSpeed = 2f;

    //UI elements for displaying information
    public GameObject textInfo;
    public GameObject textLocation;
    //main camera to follow player object
    public Camera mainCamera;
    //rigidbody for player object to interact with gameobjects
    public Rigidbody rb;
    private Vector3 offset;
    //mechanics of the door
    public float doorOpenAngle = 90f;
    public float doorOpenSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //fixing camera on player object with a rigidbody to follow the player
        rb = GetComponent<Rigidbody>();
        if(mainCamera != null)
        {
            offset = mainCamera.transform.position - rb.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //calling follow player method every frame 
        FollowPlayer();
    }
    //update for player movement every fixed frame
    void FixedUpdate()
    {
        //player movement using joystick
        Vector3 moveDirection = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        rb.velocity = moveDirection;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
        }
    }
    //method to display info using text on the ui
    private void DisplayInfo(GameObject obj, Vector3 location)
    {
        //varaibles to store info and location about gameobject near player
        string info = "";
        string locationTxt = "";
        //info to display if player is near a store
        if (obj.CompareTag("Store"))
        {
            info = "This is a Nail Saloon.";
            locationTxt = "Location: " + location.ToString();
        }
        //info to display if player is near a car
        else if (obj.CompareTag("Car"))
        {
            info = "This is a Car.";
            locationTxt = "Location: " + location.ToString();
        }
        //info to display if player is near a petrol station
        else if (obj.CompareTag("PetrolStation"))
        {
            info = "This is a Petrol Station.";
            locationTxt = "Location: " + location.ToString();
        }
        //info to display if player is near the first big building
        else if (obj.CompareTag("Building1"))
        {
            info = "This is a building with 4 stories.\nThis building has 36 units.";
            locationTxt = "Location: " + location.ToString();
        }
        //info to display if player is near the 2nd big building
        else if (obj.CompareTag("Building2"))
        {
            info = "This is a building with 3 stories.\nThis building has 24 units.";
            locationTxt = "Location: " + location.ToString();
        }
        //info to display if player is near the first house
        else if (obj.CompareTag("House1"))
        {
            info = "This is a house with 1 story.\nThis building has a basement.";
            locationTxt = "Location: " + location.ToString();
        }
        //info to display if player is near the 2nd house
        else if (obj.CompareTag("House2"))
        {
            info = "This is a house with 1 story.\nThis building has no basement.";
            locationTxt = "Location: " + location.ToString();
        }
        //when player object is near a door it should open
        else if (obj.CompareTag("Door"))
        {
            StartCoroutine(OpenDoor(obj.transform));
        }
        //info stored to be displayed in the text in unity on the ui
        textInfo.GetComponent<Text>().text = info;
        textLocation.GetComponent<Text>().text = locationTxt;
    }
    //method to trigger objects when near the player (displaying info)
    private void OnTriggerEnter(Collider other)
    {
        DisplayInfo(other.gameObject, other.transform.position);
    }
    //mnethod to untrigger objects when player is further away from them (stop displaying info)
    private void OnTriggerExit(Collider other)
    {
        ClearInfo();
    }
    //method to clear text info 
    private void ClearInfo()
    {
        textInfo.GetComponent<Text>().text = "";
        textLocation.GetComponent<Text>().text = "";
    }
    //method for the camera to follow the player object
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
    //method to open/rotate door
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
}
