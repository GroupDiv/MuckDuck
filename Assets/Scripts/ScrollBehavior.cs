using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBehavior : MonoBehaviour
{
    public float scrollOffsetSpeed = 20.0f;

    // Update is called once per frame
    void Update()
    {
        MeshRenderer BGmr = GetComponent<MeshRenderer>();

        Material BGMaterial = BGmr.material;

        Vector2 offset = BGMaterial.mainTextureOffset;

        offset.y += Time.deltaTime / scrollOffsetSpeed;

        BGMaterial.mainTextureOffset = offset;
    }
}
