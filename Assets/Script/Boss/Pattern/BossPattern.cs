using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{

    public int PatterDelay;

    public virtual void Pattern() { }

    private void Start()
    {

        StartCoroutine(Attack());

    }

    IEnumerator Attack()
    {

        Pattern();

        yield return new WaitForSeconds(PatterDelay);

        StartCoroutine(Attack());

    }

}
