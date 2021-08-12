using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Naively parses a ply file of an expected structure into Vector3 position array and Vector3 color array.
/// Expected structure is vertices with following attributes ONLY: float x, float y, float z, u_char R, u_char G,u_char B.
/// Vector3 positions represents the coordinates of a point, Vector3 colors represents the color of a point.
/// Handles the conversion from centimeters to meters.
/// Handles the conversion from right handed coordinates where z is up, to unity's left handed coordinates where y is up.
/// </summary>



public class PLYReader:MonoBehaviour
{
    public static void ReadPoints(string file, out List<Vector3> positions, out List<Vector3> colors)
    {
        // Initial the output
        positions = new List<Vector3>();
        colors = new List<Vector3>();

        // Check file exists
        if (!File.Exists(file))
        {
            Application.Quit();
        }


        try
        {
            // Interpret File
            using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                // pointcloud attributes
                int vertexCount = 0;
                int uselessProperties = 0;
                bool hasUselessProperties = false;
                bool hasColor = false;

                // help to read file
                int charSize = sizeof(char);
                int fileLength = (int)reader.BaseStream.Length;
                int numRead = 0;
                char nextChar;
                string buildingLine = "";


                // process the header
                while ((numRead += charSize) < fileLength)
                {
                    nextChar = reader.ReadChar();
                    if (nextChar == '\n')
                    {
                        if (buildingLine.Contains("end_header"))
                        {
                            break;
                        }
                        else if (buildingLine.Contains("element vertex"))
                        {
                            string[] array = Regex.Split(buildingLine, @"\s+");
                            if (array.Length - 2 > 0)
                            {
                                int target = Array.IndexOf(array, "vertex") + 1;
                                vertexCount = Convert.ToInt32(array[target]);
                                buildingLine = "";
                            }
                            else
                            {
                                Application.Quit();
                            }
                        }
                        else if (buildingLine.Contains("red") || buildingLine.Contains("green") || buildingLine.Contains("blue"))
                        {
                            hasColor = true;
                            buildingLine = "";
                        }
                        else if (buildingLine.Contains("property float")&& !buildingLine.Contains("property float x")&& !buildingLine.Contains("property float y")&& !buildingLine.Contains("property float z"))
                        {
                            hasUselessProperties = true;
                            uselessProperties++;
                        }
                    }
                    else
                    {
                        buildingLine += nextChar;
                    }
                }

                // Read the vertices
                float x, y, z, R, G, B;
                // Find the center
                float x_sum, z_sum;
                x_sum = z_sum = 0;
                // Find the ground
                Dictionary<double, int> yCount = new Dictionary<double, int>();
                double y_round;

                for (int i = 0; i < vertexCount; i++)
                {
                    // Read position
                    // Flip z and y (RHS to LHS)
                    // Meters Unit
                    x = 1.0f * reader.ReadSingle();
                    z = 1.0f * reader.ReadSingle();
                    y = 1.0f * reader.ReadSingle();

                    // If the pointcloud has color property
                    if(hasColor)
                    {
                        // Read color
                        R = 1.0f * reader.ReadByte();
                        G = 1.0f * reader.ReadByte();
                        B = 1.0f * reader.ReadByte();

                    }
                    else
                    {
                        R = G = B = 100.0f;
                    }

                    // If the pointcloud has useless property
                    if (hasUselessProperties)
                    {
                        for(int p = 0;p<uselessProperties;p++)
                        {
                            reader.ReadSingle();
                        }
                    }

                    // Find the center
                    x_sum += x;
                    z_sum += z;
                    // Find the ground
                    y_round = Math.Round(y, 5);
                    if (yCount.ContainsKey(y_round))
                    {
                        yCount[y_round]++;
                    }
                    else
                    {
                        yCount.Add(y_round, 1);
                    }


                    // Return the result
                    positions.Add(new Vector3(x, y, z));
                    colors.Add(new Vector3(R, G, B));

                }




                // Find the ground
                var suspectPoints = new Dictionary<double, int>();
                foreach (KeyValuePair<double, int> kvp in yCount)
                {
                    if (kvp.Value >= 100)
                    {
                        suspectPoints.Add(kvp.Key,kvp.Value);
                    }
                }
                
                float yLowest = float.MaxValue;
                if (suspectPoints.Count == 0) yLowest = 0;
                else
                {
                    foreach (var key in suspectPoints.Keys)
                    {
                        if (key < yLowest) yLowest = (float)key;
                    }
                }


                // Correct offset of the coordinates
                Vector3 offset = new Vector3();
                for (int i = 0; i < vertexCount; i++)
                {
                    offset = new Vector3(x_sum / vertexCount, yLowest, z_sum / vertexCount);
                    positions[i] = positions[i] - offset;
                }
                Debug.Log(offset);

            }
        }
        catch (Exception e)
        {
            Application.Quit();
        }





    }

}
