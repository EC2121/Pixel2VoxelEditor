using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadInstantiator : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject whiteQuad;
    private GameObject greyQuad;
    private GameObject root;
    private List<GameObject> batchingList;
    public List<Mesh> Meshes;
    void Start()
    {
        for (int i = 0; i < Meshes.Count; i++)
        {
            DataContainer.MeshsIndexes[Meshes[i]] = i;
        }

        DataContainer.VoxelIndexes = new Quad[16, 16];
       
        whiteQuad = Resources.Load("QuadWhite") as GameObject;
        greyQuad = Resources.Load("QuadGrey") as GameObject;
        GameObject whiteQuadBG = Resources.Load("QuadWhiteBG") as GameObject;
        GameObject greyQuadBG = Resources.Load("QuadGreyBG") as GameObject;
        root = GameObject.Find("Root");
        batchingList = new List<GameObject>();
        Vector3 pos = new Vector3(-4f, -4f, 0.1f);


        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                GameObject go = Instantiate<GameObject>(((y + x) % 2 == 0) ? whiteQuadBG : greyQuadBG, pos, Quaternion.identity, root.transform);
                pos.x += 0.5f;
            }
            pos.y += 0.5f;
            pos.x = -4f;
        }

        pos = new Vector3(-4f, -4f, 0);

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                GameObject go = Instantiate<GameObject>(((y + x) % 2 == 0) ? whiteQuad : greyQuad, pos, Quaternion.identity, root.transform);
                go.GetComponent<Quad>().MyIndexX = y;
                go.GetComponent<Quad>().MyIndexY = x;
                batchingList.Add(go);
                pos.x += 0.5f;
            }
            pos.y += 0.5f;
            pos.x = -4f;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
