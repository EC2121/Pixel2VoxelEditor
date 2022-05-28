using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quad : MonoBehaviour
{
    // Start is called before the first frame update
    private Color myCol;
    private Color defaultColor;
    private MeshRenderer myRenderer;
    private Transform myTextTransform;
    private TextMeshPro myText;
    private int myVoxelNumber;
    private Mesh myMesh;

    private int myRotationZ = 0;

    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        defaultColor = myRenderer.material.color;
        myCol = defaultColor;
        myTextTransform = transform.GetChild(0);
        myText = myTextTransform.GetComponent<TextMeshPro>();
        CanvasMGR.OnVoxelSelected.AddListener(() => myTextTransform.gameObject.SetActive(true));
        CanvasMGR.OnDrawSelected.AddListener(() => myTextTransform.gameObject.SetActive(false));
        CanvasMGR.OnShapeSelected.AddListener(() => myTextTransform.gameObject.SetActive(false));
        myVoxelNumber = 1;
        myMesh = transform.GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ChangeVoxelNumber()
    {
        if (myCol == defaultColor)
            return;
        if (myText == null)
            return;

        myVoxelNumber++;

        if (myVoxelNumber > 4)
            myVoxelNumber = 1;

        myText.text = myVoxelNumber.ToString();
    }


    public void ChangeMesh()
    {
        if (DataContainer.CurrentSelectedMesh == null)
            return;
        Mesh currentMesh = DataContainer.CurrentSelectedMesh;
        if (myMesh == currentMesh)
        {
            Rotate();
            return;
        }

        transform.GetComponent<MeshFilter>().mesh = currentMesh;
        myMesh = currentMesh;



    }


    private void Rotate()
    {
        myRotationZ += 90;
        transform.rotation = Quaternion.Euler(Vector3.forward * myRotationZ);
        myText.rectTransform.rotation = Quaternion.Euler(Vector3.forward * (-myRotationZ));
        if (myRotationZ >= 360)
            myRotationZ = 0;

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
        if (myCol == defaultColor)
            return;
        myCol = defaultColor;
        myRenderer.material.color = defaultColor;
        myVoxelNumber = 0;
        myText.text = "";

    }

    public void ChangeColor()
    {
        if (myCol == defaultColor)
        {
            myVoxelNumber = 1;
            myText.text = myVoxelNumber.ToString();
        }
        Color selectedCol = DataContainer.CurrentSelectedColor;
        if (myCol == selectedCol)
            return;
        myRenderer.material.color = selectedCol;
        myCol = selectedCol;
    }
}
