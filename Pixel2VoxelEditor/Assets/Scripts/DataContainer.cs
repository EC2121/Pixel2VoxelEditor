using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataContainer
{
    public static Material CurrentSelectedMat/* = new Color(0,0,0,1)*/;
    public static Mesh CurrentSelectedMesh = null;
    public static Dictionary<Mesh,int> MeshsIndexes = new Dictionary<Mesh, int>();
    public static Quad[,] VoxelIndexes;
    public static int currentVoxelNumber = 1;
}
