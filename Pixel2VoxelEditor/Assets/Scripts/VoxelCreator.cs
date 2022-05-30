using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class VoxelCreator : MonoBehaviour
{
    [SerializeField] public List<GameObject> ObjectList;
    private GameObject parent;
    private List<Vector3> verticies;
    private List<int> triangles;
    private List<Material> materials;
    private Mesh mesh;
    private void Awake()
    {
        verticies = new List<Vector3>();
        triangles = new List<int>();
        CanvasMGR.OnBackSelected.AddListener(DestroyAllChildren);
        CanvasMGR.OnPreviewSelected.AddListener(Create);
        CanvasMGR.OnSaveSelected.AddListener(Save);
        parent = GameObject.Find("ObjectParent");


    }

    private void DestroyAllChildren()
    {

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }

    public void Create()
    {
        parent.transform.rotation = Quaternion.identity;
        Vector3 pos;
        int numberOfVerticies = 0;
        materials = new List<Material>();
        List<List<int>> subMeshesTriangles = new List<List<int>>();
        int subMeshes = 0;

        foreach (Quad voxel in DataContainer.VoxelIndexes)
        {
            if (voxel != null)
            {
                pos = voxel.transform.position;
                pos.z = -(voxel.MyVoxelNumber * 0.25f);

                for (int i = 0; i < voxel.MyVoxelNumber; i++)
                {
                    if (!materials.Contains(voxel.MyMat))
                    {
                        materials.Add(voxel.MyMat);
                        subMeshes++;
                        subMeshesTriangles.Add(new List<int>());
                    }
                    int index = materials.IndexOf(voxel.MyMat);

                    GameObject go = Instantiate<GameObject>(ObjectList[voxel.MyMeshIndex], pos,
                    voxel.transform.rotation, parent.transform);
                    foreach (Vector3 item in go.transform.GetComponent<MeshFilter>().mesh.vertices)
                    {
                        Quaternion rotation = Quaternion.Euler(voxel.transform.eulerAngles);
                        Matrix4x4 rotMatrix = Matrix4x4.Rotate(rotation);
                        Vector3 rotatedVert = rotMatrix.MultiplyPoint3x4(item) + pos * 2;
                        verticies.Add(rotatedVert);
                    }

                    foreach (var item in go.transform.GetComponent<MeshFilter>().mesh.triangles)
                    {
                        subMeshesTriangles[index].Add(numberOfVerticies + item);
                    }
                    #region NoSubMesh
                    //foreach (Vector3 item in go.transform.GetComponent<MeshFilter>().mesh.vertices)
                    //{
                    //    verticies.Add(item + (voxel.transform.position * 2));
                    //}

                    //foreach (var item in go.transform.GetComponent<MeshFilter>().mesh.triangles)
                    //{
                    //    triangles.Add(numberOfVerticies + item);
                    //} 
                    #endregion
                    go.GetComponent<MeshRenderer>().sharedMaterial = voxel.MyMat;
                    pos.z += 0.5f;
                    numberOfVerticies = verticies.Count;
                }
            }
        }
        #region NestedFor
        //for (int x = 0; x < 16; x++)
        //{
        //    for (int y = 0; y < 16; y++)
        //    {
        //        Quad voxel = DataContainer.VoxelIndexes[x, y];
        //        if (voxel != null)
        //        {
        //            pos = voxel.transform.position;
        //            pos.z = -(voxel.MyVoxelNumber * 0.25f);

        //            for (int i = 0; i < voxel.MyVoxelNumber; i++)
        //            {
        //                if (!materials.Contains(voxel.MyMat))
        //                {
        //                    materials.Add(voxel.MyMat);
        //                    subMeshes++;
        //                    subMeshesTriangles.Add(new List<int>());
        //                }
        //                int index = materials.IndexOf(voxel.MyMat);

        //                GameObject go = Instantiate<GameObject>(ObjectList[voxel.MyMeshIndex], pos,
        //                voxel.transform.rotation, parent.transform);
        //                foreach (Vector3 item in go.transform.GetComponent<MeshFilter>().mesh.vertices)
        //                {
        //                    Quaternion rotation = Quaternion.Euler(voxel.transform.eulerAngles);
        //                    Matrix4x4 rotMatrix = Matrix4x4.Rotate(rotation);
        //                    Vector3 rotatedVert = rotMatrix.MultiplyPoint3x4(item) + pos * 2;
        //                    verticies.Add(rotatedVert);
        //                }

        //                foreach (var item in go.transform.GetComponent<MeshFilter>().mesh.triangles)
        //                {
        //                    subMeshesTriangles[index].Add(numberOfVerticies + item);
        //                }
        //                //foreach (Vector3 item in go.transform.GetComponent<MeshFilter>().mesh.vertices)
        //                //{
        //                //    verticies.Add(item + (voxel.transform.position * 2));
        //                //}

        //                //foreach (var item in go.transform.GetComponent<MeshFilter>().mesh.triangles)
        //                //{
        //                //    triangles.Add(numberOfVerticies + item);
        //                //}
        //                go.GetComponent<MeshRenderer>().sharedMaterial = voxel.MyMat;
        //                pos.z += 0.5f;
        //                numberOfVerticies = verticies.Count;
        //                Debug.Log(index);
        //            }

        //        }
        //    }
        //} 
        #endregion

        mesh = new Mesh();
        mesh.subMeshCount = subMeshes;
        mesh.SetVertices(verticies.ToArray());
        for (int i = 0; i < subMeshesTriangles.Count ; i++)
        {
            mesh.SetTriangles(subMeshesTriangles[i].ToArray(), i);
        }
        
        //mesh.vertices = verticies.ToArray();
        //mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
        mesh.Optimize();
      
    }


    public void Save()
    {
        GameObject newGo = new GameObject("CustomMesh");
        newGo.AddComponent<MeshFilter>().sharedMesh = mesh;
        newGo.AddComponent<MeshRenderer>().materials = materials.ToArray();
        AssetDatabase.CreateAsset(mesh, "Assets/CreatedObjs/" + "GeneratedMesh" + ".asset");
        parent.transform.rotation = Quaternion.identity;
        PrefabUtility.SaveAsPrefabAsset(newGo, "Assets/CreatedObjs/" + parent.gameObject.name + ".prefab");
        Destroy(newGo);
    }
}
