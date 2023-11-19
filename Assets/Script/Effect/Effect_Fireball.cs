using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Effect_Fireball : MonoBehaviour
{

    public float speed;

    Renderer tr_renderer;

    bool isAni = false;

    private void Awake()
    {

        tr_renderer = GetComponent<Renderer>();

        Off();

        FireBall.OnHit.AddListener((Vector3 pos) =>
        {

            if (isAni) return;

            pos += new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);

            pos = new Vector3(pos.x / Screen.width, pos.y / Screen.height, 0f);

            pos = Vector3.one - pos;

            tr_renderer.material.SetVector("_pos", new Vector4(pos.x, pos.y, 0f, 0f));

            isAni = true;

            StartCoroutine(Ani());

        });

    }

    public IEnumerator Ani()
    {

        yield return new WaitForSeconds(0.15f);

        float radius = 0f;

        tr_renderer.material.SetFloat("_radius", 0);

        while ((radius = tr_renderer.material.GetFloat("_radius")) <= 2f)
        {

            tr_renderer.material.SetFloat("_radius", radius + speed);

            yield return new WaitForSeconds(Time.deltaTime);

        }

        Off();

        isAni = false;
        
    }

    public void Off() => tr_renderer.material.SetFloat("_radius", 0f);

}
