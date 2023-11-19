using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using GameJamLibrary;

using BulletType = GameJamLibrary.Bullet.BulletType;
using AniType = GameJamLibrary.BulletTween.AniType;

[CustomEditor(typeof(BulletTween))]
public class CustomEditor_BulletTween : Editor
{

    BulletTween _this = null;
    Bullet bullet = null;

    private void OnEnable()
    {

        _this = (BulletTween) target;
        bullet = _this.GetComponent<Bullet>();

    }

    bool btnAddType = false,
         btnSubType = false;

    int bullletType_size
        = Enum.GetNames(typeof(BulletType)).Length - 1;

    public override void OnInspectorGUI()
    {

        Fold_AniOption();

        EditorLibrary.DrawLine(Color.gray);

    }

    void Fold_AniOption()
    {

        _this.ani_Type = 
            (AniType) EditorGUILayout.EnumFlagsField("애니메이션", _this.ani_Type);

        if ((_this.ani_Type & AniType.Circle) != 0)
        {

            EditorLibrary.DrawLine(Color.gray);

            _this.aniCircle_Increase =
                EditorGUILayout.FloatField("[Circle] 회전 속도", _this.aniCircle_Increase);

        }

        if ((_this.ani_Type & AniType.Bounce) != 0)
        {

            EditorLibrary.DrawLine(Color.gray);

            _this.aniBounce_Increase =
                EditorGUILayout.FloatField("[Bounce] 크기 속도", _this.aniBounce_Increase);

            _this.aniBounce_MinScale =
                EditorGUILayout.FloatField("[Bounce] 최소 크기", _this.aniBounce_MinScale);

            _this.aniBounce_MaxScale =
                EditorGUILayout.FloatField("[Bounce] 최대 크기", _this.aniBounce_MaxScale);

        }

    }

}
