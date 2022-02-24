using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    public bool isFlipped = false;
    public int type = 0;
    public List<Material> images = new List<Material>(12);
    Animator anim;
    MeshRenderer m_renderer;
    public Renderer face;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        m_renderer = GetComponent<MeshRenderer>();
        face = this.transform.GetChild(0).GetComponent<Renderer>();
    }

    public void ReloadImage()
    {
        StartCoroutine("ImageReload");
    }

    IEnumerator ImageReload()
    {
        yield return new WaitForEndOfFrame();
        face.material = images[type];
    }

    void OnMouseOver()
    {
        m_renderer.material.color = Color.red;
    }

    void OnMouseExit()
    {
        m_renderer.material.color = Color.white;
    }

    void OnMouseDown()
    {
        gameObject.SendMessageUpwards("SelectCard", this.gameObject);
    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        anim.SetBool("isFlipped", isFlipped);
    }

    public void Discard()
    {
        Destroy(this);
    }


}
