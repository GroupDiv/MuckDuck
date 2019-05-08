using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Maintains the speed at which the background scrolls.  Updated by LevelController.
public class ScrollBehavior : MonoBehaviour
{

    //! The speed at which the screen scrolls.  This is what is changed by the Level Controller
    public float scrollOffsetSpeed = 20.0f;

    /*!
    @pre: this is called every frame
    @post: the material covering the background is moved according to the scrollOffsetSpeed
    !*/
    void Update()
    {
        MeshRenderer BGmr = GetComponent<MeshRenderer>();

        Material BGMaterial = BGmr.material;

        Vector2 offset = BGMaterial.mainTextureOffset;

        offset.y += Time.deltaTime / scrollOffsetSpeed;

        BGMaterial.mainTextureOffset = offset;
    }
}
