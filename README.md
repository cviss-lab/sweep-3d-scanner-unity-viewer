# sweep-3d-scanner-unity-viewer-vrsupport
VR support version.

## How to use 
### Quickstart
- Download the project
- Open the folder in unity, Waiting for initialization.
> **Note:** My Unity version is 2020.3.13f1c1. As long as it is the 2020 version, it will be OK, just click to complete the version matching automatically.
- Setup Oculus and connect it to PC.
- Once the project is opened. Go to Assets/Scenes folder and double-click Main.unity scene.
- Click Play button.
- Select a PLY (binary) or CSV pointcloud file, with the [expected format](#compatible-file-format).
> **Note:** binary `.ply` files parse significantly faster than `.csv`
- Wait for the application to parse the file and render the points. 

### Oculus Controller
- `HMD`: look around
- `LeftJoyStick`: movement
- `RightJoyStick`: rotate POV
- `B`: transform between top-down scale and room scale
- `X` & `Y`: adjust the scale of the point cloud (only available in top-down scene)




