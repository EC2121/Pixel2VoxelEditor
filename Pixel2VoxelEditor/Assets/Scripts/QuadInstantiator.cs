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
    void Start()
    {
        whiteQuad = Resources.Load("QuadWhite") as GameObject;
        greyQuad = Resources.Load("QuadGrey") as GameObject;
        root = GameObject.Find("Root");
        batchingList = new List<GameObject>();

        Vector3 pos = new Vector3(-4, -4f, 0);
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                GameObject go = Instantiate<GameObject>(((y + x) % 2 == 0) ? whiteQuad : greyQuad, pos, Quaternion.identity, root.transform);
                batchingList.Add(go);
                pos.x += 0.5f;
            }
            pos.y += 0.5f;
            pos.x = -4f;
        }
        StaticBatchingUtility.Combine(batchingList.ToArray(),batchingList[0]);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
