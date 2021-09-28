using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// DISUSE
/// Naively parses a csv file of an expected structure into Vector3 position array and Vector3 color array.
/// Expected structure is vertices with following attributes ONLY: x,y,z,R,G,B.
/// Vector3 positions represents the coordinates of a point, Vector3 colors represents the color of a point.
/// Handles the conversion from centimeters to meters.
/// Handles the conversion from right handed coordinates where z is up, to unity's left handed coordinates where y is up.
/// </summary>
public class CSVReader
{
    // Parser
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public static void ReadPoints(string file, out List<Vector3> positions, out List<Vector3> colors)
    {
        positions = new List<Vector3>();
        colors = new List<Vector3>();
        try
        {
            string data = System.IO.File.ReadAllText(file);
            string[] lines = Regex.Split(data, LINE_SPLIT_RE);

            int numPoints = lines.Length - 1;

            // check if the .csv contains a column for scan index
            string header = lines[0];
            int offset = header.Contains("SCAN_INDEX") ? 1 : 0;
            bool bContainsSigStrength = header.Contains("SIGNAL_STRENGTH");

            float x, y, z, R, G, B;
            for (int i = 0; i < numPoints; i++)
            {
                string[] row = Regex.Split(lines[i + 1], SPLIT_RE);
                if (row.Length == 0 || row[0] == "") continue;

                // Read the position
                // convert from the centimeters to meters, and flip z and y (RHS to LHS)
                x = 0.01f * float.Parse(row[0 + offset]);
                z = 0.01f * float.Parse(row[1 + offset]);
                y = 0.01f * float.Parse(row[2 + offset]);

                // Read RBG color
                R = float.Parse(row[3 + offset]);
                G = float.Parse(row[4 + offset]);
                B = float.Parse(row[5 + offset]);

                // Return position and color
                positions.Add(new Vector3(x, y, z));
                colors.Add(new Vector3(R, G, B));
            }
        }
        catch (Exception e)
        {
            Application.Quit();
        }
    }
}