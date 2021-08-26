# sweep-3d-scanner-unity-viewer-vrsupport
A VR version point cloud viewer based on Unity and Oculus.

## How to use 
### Quickstart
- Download the project
- Open the folder in unity, Waiting for initialization.
> **Note:** My Unity version is 2020.3.13f1c1. As long as it is the 2020 version, it will be OK, just click to complete the version matching automatically.
- Setup Oculus and connect it to PC.
- Once the project is opened. Go to Assets/Scenes folder and double-click Main.unity scene.
- Click Play button.
- Select a PLY (binary) or CSV pointcloud file, with the [expected format](#compatible-file-format).
> **Note:** binary `.ply` files parse significantly faster than `.csv`.
- Wait for the application to parse the file and render the points. 
- Play.

### Oculus Controller
- `HMD`: look around
- `LeftThumbstick`: movement
- `RightThumbstick`: rotate POV
- `B`: back
- `A`: open function menu
- `IndexTrigger`: selection
- `Y`+`HandTrigger`: adjust the height of the point cloud
- `HandTrigger`: adjust the scale of the point cloud (only available in top-down scene)
- `X`+`HandTrigger`: adjust the yaw rotation of the point cloud (only available in top-down scene, cycle change)


## Features
### point cloud viewer
- loading the .ply and .csv point cloud file
- support RBG color point cloud file with all format 
### Annotation
- open the function menu 
- select "Annotation" function
- use controller's index button to place the position of annotation
- after placement, the virtual keyboard will appear
- input the text
- select "OK" to confirm and "Cancel" to cancel.
### Panorama
- still working on
### Ruler
- open the function menu 
- select "Ruler" function
- use controller's index button to place the first endpoint
- while placing the second endpoint, the length of the current line will appear at the midpoint of the line between two endpoints.
- after place the second endpoint, the text will rotate following user's perspective
- if you need to cancel the measurement, press `X` or `Y`, Otherwise press `ThumbStick` to confirm
### Room-scale view
- allow Annotation function
- allow Ruler function
- allow Panorama function
- allow explore the point cloud 
### Model-scale view
- allow manipulate the point cloud model



