# MovePersonInCity Script

This Unity C# script, `movePersonInCity`, is designed to manage player movement, interactions with game objects, and camera following in a city simulation environment. It provides functionality for joystick-based movement, interaction with various objects, and UI updates for information display.

---

## Features

### 1. **Player Movement**
- **Joystick Control**: supports player movement using a virtual joystick.
- **Rotation**: automatically rotates the player towards the movement direction.

### 2. **Object Interactions**
- detects when the player is near specific objects (e.g., buildings, cars, doors) and displays relevant information.
- automatically opens doors when the player approaches.

### 3. **Camera Follow**
- smoothly follows the player with adjustable positioning to maintain a consistent view.

### 4. **UI Updates**
- displays detailed information and location of nearby objects in the city.
- clears information when the player moves away from the objects.

---

## Key Functionalities

### Movement
- controlled by a joystick (`FixedJoystick`) with adjustable movement speed.

### Object Tags and Information
The script interacts with objects based on their tags:
- **Store**: displays "This is a Nail Salon."
- **Car**: cisplays "This is a Car."
- **PetrolStation**: displays "This is a Petrol Station."
- **Building1 & Building2**: shows the number of stories and units.
- **House1 & House2**: dndicates the presence of basements.
- **Door**: automatically opens the door.

### Camera Behavior
- smoothly follows the player with a calculated offset.
- adjusts position and orientation to maintain focus on the player.

### Door Mechanism
- rotates doors to simulate opening, using adjustable speed and angle parameters.

---

## Setup Instructions

1. Attach the `movePersonInCity` script to the player GameObject.
2. Assign the following components in the Unity Editor:
   - **FixedJoystick**: link your UI joystick for player control.
   - **Main Camera**: assign the camera to follow the player.
   - **TextInfo** and **TextLocation**: UI elements for displaying information.
3. Configure object tags in your Unity scene for interaction:
   - example Tags: `Store`, `Car`, `Building1`, `Door`, etc.
4. Customize parameters as needed:
   - `moveSpeed`: adjust player movement speed.
   - `doorOpenAngle` and `doorOpenSpeed`: configure door opening behavior.

---

## Key Classes and Methods

- **Start()**: initializes references and calculates camera offset.
- **Update()**: updates camera behavior.
- **FixedUpdate()**: handles player movement and rotation.
- **OnTriggerEnter(Collider other)**: displays information when near tagged objects.
- **OnTriggerExit(Collider other)**: clears displayed information.
- **FollowPlayer()**: smoothly adjusts the camera position and orientation.
- **OpenDoor(Transform door)**: coroutine for door-opening animations.

---

## Skills Demonstrated
- Unity's Rigidbody and Transform components for physics-based movement.
- Dynamic UI updates using UnityEngine.UI.
- Smooth camera transitions and object interaction mechanics.
- Coroutine implementation for animations (e.g., door opening).

---

## License
This script is open-source and available for educational and non-commercial use. Attribution is appreciated.
