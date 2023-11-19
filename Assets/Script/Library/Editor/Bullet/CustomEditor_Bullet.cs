using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using GameJamLibrary;

using BulletType = GameJamLibrary.Bullet.BulletType;
using AniType = GameJamLibrary.BulletTween.AniType;

[CustomEditor(typeof(Bullet))]
public class CustomEditor_Bullet : Editor
{

    Bullet _this = null;

    private void OnEnable()
    {

        _this = (Bullet)target;

    }

    bool optionFold = false,
         btnAddType = false,
         btnSubType = false;

    int bullletType_size
        = Enum.GetNames(typeof(BulletType)).Length - 1;

    public override void OnInspectorGUI()
    {

        EditorLibrary.DrawEmptySpace(20);

        Button_SubType();
        Button_AddType();

        Label_titleBulletType();
        Label_BulletType();

        Fold_Option();

        EditorLibrary.DrawLine(Color.gray);

        Fold_TypeOption();

    }

    void Fold_Option()
    {

        _this.speed = EditorGUILayout.Slider("속도", _this.speed, 0, 3);

        _this.color = EditorGUILayout.ColorField("색깔", _this.color);

    }

    void Fold_TypeOption()
    {

        switch (_this.bullet_Type)
        {

            #region linear

            case BulletType.linear:

                _this.normal = EditorGUILayout.Vector3Field("방향", _this.normal);

                break;

            #endregion

            #region Tracking

            case BulletType.Tracking:

                _this.target = 
                    (GameObject)EditorGUILayout.ObjectField("타겟", _this.target, typeof(GameObject), true);

                _this.delay = EditorGUILayout.FloatField("오차", _this.delay);

                break;

            #endregion

        }

    }

    void Label_BulletType()
    {

        GUILayout.BeginArea(new Rect(225, 5, 100, 100));

        GUILayout.Label(_this.bullet_Type.ToString());

        GUILayout.EndArea();

    }

    void Label_titleBulletType()
    {

        GUILayout.BeginArea(new Rect(10, 5, 100, 100));

        GUILayout.Label("탄막 종류 : ");

        GUILayout.EndArea();

    }

    void Button_AddType()
    {

        GUILayout.BeginArea(new Rect(325, 5, 100, 100));

        btnAddType = GUILayout.Button("▶", GUILayout.Width(75));

        GUILayout.EndArea();

        if (btnAddType)
        {

            if ((int) _this.bullet_Type < bullletType_size) _this.bullet_Type++;
            else _this.bullet_Type = 0; 

        }

    }

    void Button_SubType()
    {

        GUILayout.BeginArea(new Rect(100, 5, 100, 100));

        btnSubType = GUILayout.Button("◀", GUILayout.Width(75));

        GUILayout.EndArea();

        if (btnSubType)
        {

            if ((int)_this.bullet_Type > 0) _this.bullet_Type--;
            else _this.bullet_Type = (BulletType) (bullletType_size);

        }

    }

}
