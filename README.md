# sweep-3d-scanner-unity-viewer
A simple unity project to view scans in first person, including VR support.


## How to use 
### Quickstart
- Download the project
- Open the folder in unity, Waiting for initialization.
> **Note:** My Unity version is 2020.3.13f1c1. As long as it is the 2020 version, it will be OK, just click to complete the version matching automatically.
- Once the project is opened. Go to Assets/Scenes folder and double-click Main.unity scene.
- Click Play button.
- Select a PLY (binary) or CSV pointcloud file, with the [expected format](#compatible-file-format).
> **Note:** binary `.ply` files parse significantly faster than `.csv`
- Wait for the application to parse the file and render the points. 

### Controls
- `WASD`: movement
- `Mouse`: look around
- `Q` & `E`: adjust the vertical position of the point cloud
- `Z` & `C`: adjust the scale of the point cloud (only available in top-down scene)
- `R` & `T`: adjust the yaw rotation of the point cloud (only available in top-down  scene)
- `ESC`: exit application
- `M`: transform from top-down scale to room scale

### Compatible file format
Only files with the expected format will open correctly. Attempting to open unexpected files will terminate the unity application. The application expects either `.csv` or `.ply (binary)` files.

.cvs format
```csv
X,Y,Z,R,G,B
6.7,-124.2,-71.3,255,255,255
6.7,-125.4,-69.2,255,255,255
...
```
 
.ply format
```ply
property float x
property float y
property float z
property uchar red
property uchar green
property uchar blue
...
```