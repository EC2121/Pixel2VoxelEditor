using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class VoxelCreator : MonoBehaviour
{
    [SerializeField] public List<GameObject> ObjectList;
    private GameObject parent;
    private void Awake()
    {
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
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                Quad voxel = DataContainer.VoxelIndexes[x, y];
                if (voxel != null)
                {
                    pos = voxel.transform.position;
                    pos.z = -(voxel.MyVoxelNumber * 0.25f);
                    for (int i = 1; i < voxel.MyVoxelNumber + 1; i++)
                    {
                        GameObject go = Instantiate<GameObject>(ObjectList[voxel.MyMeshIndex], pos,
                        voxel.transform.rotation, parent.transform);

                        go.GetComponent<MeshRenderer>().sharedMaterial = voxel.MyMat;
                        pos.z += 0.5f;
                    }
                    
                }
            }
        }

       
    }


    public void Save()
    {
        parent.transform.rotation = Quaternion.identity;
        PrefabUtility.SaveAsPrefabAsset(parent.gameObject, "Assets/CreatedObjs/" + parent.gameObject.name + ".prefab");
    }
}
