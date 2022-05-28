using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasMGR : MonoBehaviour
{
    public static UnityEvent OnVoxelSelected = new UnityEvent();
    public static UnityEvent OnDrawSelected = new UnityEvent();
    public static UnityEvent OnShapeSelected = new UnityEvent();
    Image currSelectedColorImage;
    void Start()
    {
        currSelectedColorImage = transform.GetChild(0).GetComponent<Image>();
        currSelectedColorImage.color = DataContainer.CurrentSelectedColor;
        RaycastMGR.OnColorChanged.AddListener(OnColorPicked);
    }

    public void OnVoxelButtonPressed()
    {
        OnVoxelSelected?.Invoke();
    }

    public void OnDrawButtonPressed()
    {
        OnDrawSelected?.Invoke();
    }


    public void OnShapeButtonPressed()
    {
        OnShapeSelected?.Invoke();
    }


    private void OnColorPicked()
    {
        if (currSelectedColorImage != null)
            currSelectedColorImage.color = DataContainer.CurrentSelectedColor;
    }
   
}
