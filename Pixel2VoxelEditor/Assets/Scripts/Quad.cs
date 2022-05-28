using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quad : MonoBehaviour
{
    // Start is called before the first frame update
    public Material MyMat;
    private Material defaultMat;
    private MeshRenderer myRenderer;
    private Transform myTextParent;
    private TextMeshPro myText;
    public int MyVoxelNumber;
    private Mesh myMesh;
    private Mesh defaultMesh;
    private Vector3 rotation = Vector3.forward * 90;
    public int MyMeshIndex;
    public int MyIndexX;
    public int MyIndexY;
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        defaultMat = myRenderer.material;
        MyMat = myRenderer.material;
        /*MyCol = defaultColor*/;
        myTextParent = transform.GetChild(0);
        myText = myTextParent.GetChild(0).GetComponent<TextMeshPro>();
        CanvasMGR.OnVoxelSelected.AddListener(() => myTextParent.GetChild(0).gameObject.SetActive(true));
        CanvasMGR.OnDrawSelected.AddListener(() => myTextParent.GetChild(0).gameObject.SetActive(false));
        CanvasMGR.OnShapeSelected.AddListener(() => myTextParent.GetChild(0).gameObject.SetActive(false));
        MyVoxelNumber = 1;
        myMesh = transform.GetComponent<MeshFilter>().mesh;
        defaultMesh = myMesh;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ChangeVoxelNumber()
    {
        if (MyMat == defaultMat)
            return;
        if (myText == null)
            return;

        int voxelNumber = DataContainer.currentVoxelNumber;
        MyVoxelNumber = voxelNumber;
        myText.text = voxelNumber.ToString();
    }


    public void ChangeMesh()
    {
        if (MyMat == defaultMat || DataContainer.CurrentSelectedMesh == null)
            return;

        Mesh currentMesh = DataContainer.CurrentSelectedMesh;
        if (myMesh == currentMesh)
        {
            Rotate();
            return;
        }

        transform.GetComponent<MeshFilter>().mesh = currentMesh;
        myMesh = currentMesh;
        MyMeshIndex = DataContainer.MeshsIndexes[myMesh];


    }


    private void Rotate()
    {

        transform.Rotate(rotation);  /*Quaternion.Euler(Vector3.forward * myRotationZ);*/
        myTextParent.Rotate(-rotation); /*= Quaternion.Euler(-(Vector3.forward * myRotationZ));*/

    }

    //public void OnDrawSelected()
    //{
    //    myTextTransform.gameObject.SetActive(false);
    //}    

    //public void OnVoxelSelected()
    //{
    //    myTextTransform.gameObject.SetActive(true);
    //}

    public void ResetColor()
    {
        if (MyMat== defaultMat)
            return;
        myRenderer.material= defaultMat;
        MyVoxelNumber = 0;
        myText.text = "";
        DataContainer.VoxelIndexes[MyIndexX, MyIndexY] = null;
        MyMeshIndex = 0;

        if (myMesh != defaultMesh)
        {
            transform.GetComponent<MeshFilter>().mesh = defaultMesh;
            myMesh = defaultMesh;
        }
        MyMat = defaultMat;

    }

    public void ChangeColor()
    {
        Material selectedCol = DataContainer.CurrentSelectedMat;
        if (selectedCol == null)
            return;

        if (MyMat == selectedCol)
            return;

        int voxelNumber = DataContainer.currentVoxelNumber;

        if (MyMat == defaultMat)
        {
            MyVoxelNumber = voxelNumber;
            myText.text = voxelNumber.ToString();
        }
        myRenderer.material = selectedCol;
        MyMat = selectedCol;
        DataContainer.VoxelIndexes[MyIndexX, MyIndexY] = this;
    }
}
