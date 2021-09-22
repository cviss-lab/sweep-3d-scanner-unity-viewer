using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;

/// <summary>
/// Requests the user select .csv and .ply file.
/// Reads the points from the file.
/// Splits the points into multiple children each of which is a PointCloud object with a portion of the points.
/// </summary>
public class PointCloudGenerator : MonoBehaviour
{
    public Material pointCloudMaterial;
    public Material meshMaterial;

    // maintained reference to the generated children (point cloud objects)
    private GameObject[] pointClouds;
    // the point position read from the data file
    private List<Vector3> data_pos;
    // the point color read from the data file
    private List<Vector3> data_col;
    // the number of points read from the data file
    int numPoints = 0;
    // the number of point clouds that will be generated from the collection of points
    int numDivisions = 0;
    // the nubmer of points in each generated point cloud
    int numPointsPerCloud = 0;
    // The maximum number of vertices unity will allow per single mesh
    const int MAX_NUMBER_OF_POINTS_PER_MESH = 65000;

    void Awake()
    {
        // Read the points from the file
        SFB.ExtensionFilter[] extensions = new[] { new ExtensionFilter("Point Cloud Files", "csv", "ply") };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
        if (paths.Length < 1)
        {
            print("Error: Please specify a properly formatted CSV or binary PLY file.");
            Application.Quit();
            return;
        }

        string filePath = paths[0];
        string[] parts = filePath.Split('.');
        string extension = parts[parts.Length - 1];
        Vector3 pos_offset = new Vector3();

        switch (extension)
        {
            case "csv":
            case "CSV":
                CSVReader.ReadPoints(filePath,out data_pos,out data_col);
                break;
            case "ply":
            case "PLY":
                pos_offset = PLYReader.ReadPoints(filePath, out data_pos, out data_col);
                break;
            default:
                print("Error: Please specify a properly formatted CSV or binary PLY file.");
                Application.Quit();
                return;
        }





        // Number of PointCloud
        numPoints = data_pos.Count;
        if (numPoints <= 0)
        {
            print("Error: failed to read points from " + paths[0]);
            Application.Quit();
            return;
        }

        // Calculate the appropriate division of points
        numDivisions = Mathf.CeilToInt(1.0f * numPoints / MAX_NUMBER_OF_POINTS_PER_MESH);

        // For simplicity, only use a number of points that splits evenly among numDivisions pointclouds
        numPoints -= numPoints % numDivisions;
        numPointsPerCloud = numPoints / numDivisions;

        print("" + numPoints + " points, split into " + numDivisions + " clouds of " + numPointsPerCloud + " points each.");

        pointClouds = new GameObject[numDivisions];

        // generate point cloud objects, each with the same number of points
        for (int i = 0; i < numDivisions; i++)
        {
            int offset = i * numPointsPerCloud;
            // generate a subset of data for this point cloud
            Vector3[] positions = new Vector3[numPointsPerCloud];
            Vector3[] colors = new Vector3[numPointsPerCloud];
            for (int j = 0; j < numPointsPerCloud; j++)
            {

                // position stored in the first 3 elements of the vector (conversion handled by implicit cast)
                positions[j] = data_pos[offset + j];
                colors[j] = data_col[offset + j];
            }

            // Create the point cloud using the subset of data
            GameObject obj = new GameObject("PointCloud Division "+i);
            obj.transform.SetParent(transform, false);
            obj.AddComponent<PointCloud>().CreateMesh(positions, colors);
            obj.GetComponent<MeshRenderer>().material = pointCloudMaterial;
            pointClouds[i] = obj;
        }


        //----------------------------------------------------------------------------------------------------
        // Read simplified mesh from the file
        SFB.ExtensionFilter[] extensions2 = new[] { new ExtensionFilter("Point Cloud Files", "stl") };
        string[] paths_obj = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions2, false);
        if (paths_obj.Length < 1)
        {
            print("Error: Please specify a properly formatted obj file.");
            Application.Quit();
            return;
        }

        // generate mesh object
        Mesh[] holderMesh;
        //FastObjImporter newMesh = new FastObjImporter();
        //holderMesh = newMesh.ImportFile(paths_obj[0], pos_offset);
        holderMesh = STLReader.Import(paths_obj[0], pos_offset);

        GameObject meshObj = new GameObject("meshObj");
        MeshRenderer renderer = meshObj.AddComponent<MeshRenderer>();
        MeshFilter filter = meshObj.AddComponent<MeshFilter>();

        for(int i=0;i<holderMesh.Length;i++)
        {
            GameObject subMeshObj = new GameObject("subMeshObj " + i.ToString());
            MeshRenderer subRenderer = subMeshObj.AddComponent<MeshRenderer>();
            subRenderer.material = meshMaterial;
            MeshFilter subFilter = subMeshObj.AddComponent<MeshFilter>();
            subFilter.mesh = holderMesh[i];
            subMeshObj.transform.SetParent(meshObj.transform);
        }

        //meshObj.transform.position += pos_offset;




    }
}
