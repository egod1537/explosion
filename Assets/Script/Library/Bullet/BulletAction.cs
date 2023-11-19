using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamLibrary
{

    public class BulletAction : MonoBehaviour
    {

        private static Transform _Bullet_Pool;
        public static Transform Bullet_Pool
        {

            get
            {

                if(_Bullet_Pool == null)
                {

                    GameObject go = new GameObject();

                    go.name = "Bullet_Pool";

                    Transform go_tr = go.transform;
                    go_tr.SetParent(GameObject.Find("BulletLib").transform, false);

                    _Bullet_Pool = go_tr;

                }

                return _Bullet_Pool;

            }

        }

        private static Transform _Bullets;
        public static Transform Bullets
        {

            get
            {

                if (_Bullets == null)
                {

                    GameObject go = new GameObject();

                    go.name = "Bullets";

                    Transform go_tr = go.transform;
                    go_tr.SetParent(GameObject.Find("BulletLib").transform, false);

                    _Bullets = go_tr;

                }

                return _Bullets;

            }

        }

        public static Dictionary<string, GameObject> Resource = new Dictionary<string, GameObject>(); ///< Resource.Load 캐싱
        public static Dictionary<string, Transform> Pool = new Dictionary<string, Transform>(); ///< Object Pool 캐싱
        public static Dictionary<int, Vector3> Trigonometric = new Dictionary<int, Vector3>(); ///< 삼각함수 캐싱

        public static Vector3 getTrigonometric(int _angle)
        {

            if (!Trigonometric.ContainsKey(_angle))
                Trigonometric[_angle] =
                    new Vector3(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));

            return Trigonometric[_angle];

        }

        public static GameObject getResource(string _name)
        {

            if (!Resource.ContainsKey(_name))
                Resource[_name] = Resources.Load<GameObject>("Bullet/" + _name);

            return Resource[_name];

        }

        public static Transform getPool(string _name)
        {

            if (!Pool.ContainsKey(_name))
            {

                GameObject _parent = new GameObject();

                _parent.name = _name;
                _parent.transform.SetParent(Bullet_Pool, false);

                Pool[_name] = _parent.transform;

            }
                
            return Pool[_name];

        }

        public static GameObject CreateBullet(string _resourceName, Vector3 _pos = new Vector3())
        {

            GameObject go;

            if (getPool(_resourceName).childCount > 0) go = getPool(_resourceName).GetChild(0).gameObject;
            else go = Instantiate(getResource(_resourceName));

            go.transform.localPosition = _pos;
            go.name = _resourceName;

            go.transform.SetParent(Bullets, false);

            go.SetActive(true);

            if(go.GetComponent<Bullet>() == null) go.AddComponent<Bullet>();

            return go;

        }

        public static GameObject Shot
            (string _resourceName, Vector3 normal, float _speed = 1f,
            Entity.Team team = Entity.Team.Player, int damage = 0)
        {

            GameObject go = CreateBullet(_resourceName);

            go.transform.localEulerAngles = 
                new Vector3(0f, 0f,
                Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg);

            Bullet go_bullet = go.GetComponent<Bullet>();

            go_bullet.Restore();

            go_bullet.normal = normal;

            go_bullet.speed = _speed;

            go_bullet.Damage = damage;

            go_bullet.bullet_Type = Bullet.BulletType.linear;

            go_bullet.team = team;

            return go;

        }

        public static GameObject Shot
            (string _resourceName, int _angle, float _speed = 1f, 
            Entity.Team team = Entity.Team.Player, int damage = 0)
        {

            return Shot(_resourceName, getTrigonometric(_angle), _speed, team, damage);

        }

        public static GameObject Shot
            (string _resourceName, GameObject _target, float _speed = 1f, float _delay = 1f)
        {

            GameObject go = CreateBullet(_resourceName);

            Bullet go_bullet = go.GetComponent<Bullet>();

            go_bullet.Restore();

            go_bullet.target = _target;

            go_bullet.speed = _speed;

            go_bullet.delay = _delay;

            go_bullet.bullet_Type = Bullet.BulletType.Tracking;

            return go;

        }

        public static void Shot
            (string _resourceName, int angle, int damage = 0)
        {

            Shot(_resourceName,
                _speed : 0.8f,
                _angle: angle,
                team: Entity.Team.Enemy,
                damage: damage)
                .transform.position = Vector3.zero;

            Vector3 getNormal(float addPos)
            {

                return new Vector3(
                Player.Pos.x + addPos,
                BossRegression.getRegressionPos(Player.Pos.x + addPos),
                0f).normalized;

            }

            Shot(_resourceName,
                _speed: 0.8f,
                normal: getNormal(angle + 45f),
                team: Entity.Team.Enemy,
                damage: damage)
                .transform.position = Vector3.zero;

            Shot(_resourceName,
                _speed: 0.8f,
                normal: getNormal(angle - 45f),
                team: Entity.Team.Enemy,
                damage: damage)
                .transform.position = Vector3.zero;

        }

    }

}

