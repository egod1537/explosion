using System.Collections;
using System.Collections.Generic;
using GameJamLibrary;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamLibrary
{

    [RequireComponent(typeof(BulletTween))]
    public class Bullet : Entity
    {

        public enum BulletType
        {

            linear,
            Tracking

        }
        public BulletType bullet_Type; ///< 탄막 종류

        public GameObject target; ///< 따라갈 방향
        public float delay; ///< Tracking 오차율

        public float speed; ///< 속도

        private Vector3 normal_;
        public Vector3 normal
        {
            get { return normal_; }
            set
            {

                normal_ = value;

                angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;

            }
        } ///< linear 방향

        public Color color = Color.white; ///< 색깔

        public float angle = 0f;

        UISprite tr_sprite;

        private void Awake()
        {

            tr_sprite = GetComponent<UISprite>();

            transform.GetComponent<BulletTween>().bullet = this;

        }

        private void Update()
        {

            tr_sprite.color = color;

            EndPooling();

        }

        private void OnTriggerEnter(Collider other)
        {

            if (target == null) return;

            if (other.CompareTag("Bullet")) return;

            if (other.name.Equals(target.name))
            {

                transform.parent = BulletAction.getPool(transform.name);

                gameObject.SetActive(false);

            }

        }

        public void Restore()
        {

            normal = Vector3.zero;

            speed = 0;

            color = Color.white;

            BulletTween bt = GetComponent<BulletTween>();

            bt.ani_Type = BulletTween.AniType.None;

            bt.aniBounce_MinScale = bt.aniCircle_Increase 
                = bt.aniBounce_MaxScale = bt.aniBounce_Increase = 0;

        }

        public Bullet setAnimation(BulletTween.AniType _type)
        {

            BulletTween bt = GetComponent<BulletTween>();

            bt.ani_Type = _type;

            return this;

        }

        public Bullet setAniProperty(
            Color _color,
            float aniCircle_Increase = 0f,
            float aniBounce_Increase = 0f,
            float aniBounce_MinScale = 0f,
            float aniBounce_MaxScale = 0f)
        {

            color = _color;

            BulletTween bt = GetComponent<BulletTween>();

            bt.aniCircle_Increase = aniCircle_Increase;
            bt.aniBounce_Increase = aniBounce_Increase;
            bt.aniBounce_MinScale = aniBounce_MinScale;
            bt.aniBounce_MaxScale = aniBounce_MaxScale;
            
            return this;

        }

        void EndPooling()
        {

            if (Screen.width * 0.5f > Mathf.Abs(transform.localPosition.x)
                && Screen.height * 0.5f > Mathf.Abs(transform.localPosition.y)) return;

            transform.parent = BulletAction.getPool(transform.name);

            gameObject.SetActive(false);

        }

    }

}
