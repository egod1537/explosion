using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamLibrary
{

    ///@class Bullet
    ///@brief 탄막의 종류 이동에 관한 클래스
    public class BulletTween : MonoBehaviour
    {

        [Flags]
        public enum AniType
        {

            None = 0,
            Tracking = 1 << 0,
            Circle = 1 << 1,
            Bounce = 1 << 2,
            Gradiation = 1 << 3

        }

        public AniType ani_Type; ///< 탄막 애니메이션

        public float aniCircle_Increase; ///< Circle 애니메이션 속도

        public float aniBounce_Increase; ///< Bounce 애니메이션 속도
        public float aniBounce_MinScale; ///< Bounce 애니메이션 최소 크기
        public float aniBounce_MaxScale; ///< Bounce 애니메이션 최대 크기

        public Color[] aniGradiation_Color; ///< Gradiation 애니메이션 색깔 변화

        private Bullet _bullet;
        public Bullet bullet
        {

            get
            {
                if (_bullet == null) _bullet = GetComponent<Bullet>();
                return _bullet;

            }

            set { _bullet = value; }

        }

        float aniBounce_up = 1f;

        Rigidbody tr_rig;
        SphereCollider tr_sphere;
        UISprite tr_sprite;

        Vector3 tr_pos;
        Vector3 tr_localpos;

        private void Awake()
        {

            bullet = GetComponent<Bullet>();
            tr_rig = GetComponent<Rigidbody>();

            tr_sphere = GetComponent<SphereCollider>();
            tr_sprite = GetComponent<UISprite>();

        }

        private void OnEnable()
        {

            transform.localPosition = Vector3.zero;

        }

        private void Update()
        {

            tr_localpos = transform.localPosition;
            tr_pos = transform.position;

        }

        private void FixedUpdate()
        {

            float velocity = bullet.speed * Time.deltaTime;

            #region bullet_Type

            switch (bullet.bullet_Type)
            {

                #region Tracking

                case Bullet.BulletType.Tracking:

                    if (bullet.target != null)
                        bullet.normal = 
                            (bullet.target.transform.position - tr_pos).normalized;

                    break;

                #endregion

            }

            #endregion

            #region ani_Type

            if((ani_Type & AniType.None) != 0)
            {

                transform.localEulerAngles.Set(0f, bullet.angle, 0f);

            }

            if ((ani_Type & AniType.Tracking) != 0)
            {

                bullet.angle = Mathf.Atan2(bullet.normal.y, bullet.normal.x) * Mathf.Rad2Deg;

                transform.localEulerAngles = new Vector3(0f, 0f, bullet.angle);

            }

            if ((ani_Type & AniType.Circle) != 0)
            {

                transform.localEulerAngles +=
                    new Vector3(0f, 0f, aniCircle_Increase) * 10f;

            }

            if ((ani_Type & AniType.Bounce) != 0)
            {

                transform.localScale += Vector3.one * aniBounce_Increase * aniBounce_up * 0.01f;

                if (transform.localScale.x > aniBounce_MaxScale) aniBounce_up = -1f;

                if (transform.localScale.x < aniBounce_MinScale) aniBounce_up = 1f;

                tr_sphere.radius = tr_sprite.width * 0.5f;

            }

            if((ani_Type & AniType.Gradiation) != 0)
            {



            }

            #endregion

            tr_rig.MovePosition(tr_pos + bullet.normal * velocity);

        }

    }

}
