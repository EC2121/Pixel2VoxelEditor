using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum Mode { Draw, Voxel, Shape, Preview };
public class RaycastMGR : MonoBehaviour
{
    private int quadLayer = 1 << 6;
    private int colorLayer = 1 << 7;
    private int meshLayer = 1 << 8;
    RaycastHit info;
    public static UnityEvent OnColorChanged = new UnityEvent();
    private Mode currentMode;


    private void Start()
    {
        CanvasMGR.OnDrawSelected.AddListener(() => currentMode = Mode.Draw);
        CanvasMGR.OnVoxelSelected.AddListener(() => currentMode = Mode.Voxel);
        CanvasMGR.OnShapeSelected.AddListener(() => currentMode = Mode.Shape);
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        switch (currentMode)
        {
            case Mode.Draw:
                if (Input.GetMouseButton(0))
                {
                    if (Physics.Raycast(ray, out info, 100, quadLayer))
                        info.transform.GetComponent<Quad>().ChangeColor();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out info, 100, colorLayer))
                    {
                        Color color = info.transform.GetComponent<MeshRenderer>().material.color;

                        if (color == DataContainer.CurrentSelectedColor)
                            return;

                        DataContainer.CurrentSelectedColor = color;
                        OnColorChanged?.Invoke();
                    }
                }

                if (Input.GetMouseButton(1))
                {
                    if (Physics.Raycast(ray, out info, 100, quadLayer))
                        info.transform.GetComponent<Quad>().ResetColor();
                }
                break;
            case Mode.Voxel:
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out info, 100, quadLayer))
                        info.transform.GetComponent<Quad>().ChangeVoxelNumber();
                }

                break;
            case Mode.Shape:
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out info, 100, meshLayer))
                        DataContainer.CurrentSelectedMesh = info.transform.GetComponent<MeshFilter>().mesh;

                    if (Physics.Raycast(ray, out info, 100, quadLayer))
                        info.transform.GetComponent<Quad>().ChangeMesh();
                }

                break;
            case Mode.Preview:
                break;
            default:
                break;
        }


    }

}