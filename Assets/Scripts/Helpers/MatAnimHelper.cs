using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MatAnimHelper : MonoBehaviour {
   
    public Material projectorMat
    {
        get { return mMat; }
        set { mMat = value;
            SetColor();
        }
    }
    public Color mColor;
    public Material mMat;
    public bool triggered = false;
 
   
    void SetColor()
    {
        mColor = projectorMat.color;
        triggered = true;
    }

    void OnRenderObject()
    {
        if (triggered)
        projectorMat.color = mColor;
    }
	 

}
