using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;
using UnityEngine.Events;

public partial class FireBall : MonoBehaviour
{

    public class Hit : UnityEvent<Vector3> { }
    public static Hit OnHit = new Hit();

    public GameObject Line;

    public int Count = 0;

    private void Awake()
    {

        Line.SetActive(false);

        Orbit = Instantiate(ResourceManager.Load("Bullet/Orbit"));

        Orbit.GetComponent<OrbitFireBall>().enabled = false;

        Orbit.transform.SetParent(transform, false);

        StartCoroutine(RechargeFireBall());

    }

    bool isSniping = false;
    bool isFireball = false;

    float SnipingAnlge = 0f;
    float SnipingScale = 0f;

    GameObject Orbit;
    public GameObject thisFireball;

    Vector3 Fireball_pos;

    Touch[] touch;

    private void Update()
    {

        if (isFireball)
        {

            if (thisFireball == null)
            {

                OnHit.Invoke(Fireball_pos);
                isFireball = false;

            }else
                Fireball_pos = thisFireball.transform.localPosition;

        }

        if (Input.touchCount > 1)
        {

            touch = new Touch[Input.touchCount];

            for (int i = 0; i < Input.touchCount; i++)
                touch[i] = Input.GetTouch(i);

            Vector3 subPos =
                touch[0].position - touch[1].position;

            SnipingAnlge = Mathf.Atan2(subPos.y, subPos.x) * Mathf.Rad2Deg - 90f;

            Line.transform.localEulerAngles =
                new Vector3(0f, 0f,
                SnipingAnlge);

            SnipingScale = Vector3.Distance(touch[0].position, touch[1].position) / 256f;

            Line.transform.localScale =
                new Vector3(1f, SnipingScale, 1f);

            isSniping = true;

        }
        else if (isSniping)
        {

            Orbit.SetActive(false);
            ShotFireBall();

        }

        if (isSniping)
        {

            Orbit.SetActive(true);

            Orbit.transform.position
                = UICamera.mainCamera.ScreenToWorldPoint(touch[1].position);

        }

        Line.SetActive(isSniping);

    }

    public void ShotFireBall()
    {

        isSniping = false;

        if (Count < 1) return;

        thisFireball =
            BulletAction.Shot
                ("Fireball", (int)SnipingAnlge + 90,
                SnipingScale, team: Entity.Team.Player,
                damage: PlayerManager.FireBall_Damage);

        thisFireball.transform.position = transform.position;

        isFireball = true;

        OnUseFireBall.Invoke(--Count);

    }

    public IEnumerator RechargeFireBall()
    {

        if(Count < PlayerManager.FireBall_MaxCount) OnChargingFireBall.Invoke();

        if (Count < PlayerManager.FireBall_MaxCount)
            OnChargeFireBall.Invoke(++Count);

        yield return 
            new WaitForSeconds(PlayerManager.FireBall_Cooldown * 0.5f);

        StartCoroutine(RechargeFireBall());

    }

}
