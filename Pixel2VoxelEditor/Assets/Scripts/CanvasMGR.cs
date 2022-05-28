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
    public static UnityEvent OnPreviewSelected = new UnityEvent();
    public static UnityEvent OnBackSelected = new UnityEvent();
    Image currSelectedColorImage;

    private void Awake()
    {
        RaycastMGR.OnColorChanged.AddListener(OnColorPicked);
    }
    void Start()
    {
        currSelectedColorImage = transform.GetChild(0).GetComponent<Image>();
        currSelectedColorImage.color = DataContainer.CurrentSelectedColor;
    }

    public void OnPreviewPressed()
    {
        OnPreviewSelected?.Invoke();
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

    public void OnBackPressed()
    {
        OnBackSelected?.Invoke();
    }

    private void OnColorPicked()
    {
        if (currSelectedColorImage != null)
            currSelectedColorImage.color = DataContainer.CurrentSelectedColor;
    }
   
}
