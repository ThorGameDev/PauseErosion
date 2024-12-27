using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePostProsessor : MonoBehaviour
{
    public Material RippleMaterial;
    public float MaxAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;

    void Update()
    {
        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }

    public void RippleEffect(float NewMaxAmount, Vector3 Position , float NewFriction)
    {
        MaxAmount = NewMaxAmount;
        Friction = NewFriction;
        this.Amount = this.MaxAmount;
        Vector3 pos = Camera.main.WorldToScreenPoint(Position);
        pos = new Vector3(Mathf.Clamp(pos.x, -Screen.width, Screen.width), Mathf.Clamp(pos.y, -Screen.height, Screen.height));
        this.RippleMaterial.SetFloat("_CenterX", pos.x);
        this.RippleMaterial.SetFloat("_CenterY", pos.y);
    }
}
