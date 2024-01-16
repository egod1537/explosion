using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
struct SerializationVector3
{

    public float x, y;

    public SerializationVector3(Vector3 _pos) {x = _pos.x; y = _pos.y;}

}

namespace GameJamLibrary
{

    public class GraphFunction
    {

        public double[] port;

        public GraphFunction(double[] _port) =>  port = _port; 

        public double this[double x]{get { return getFunc(x); }}

        public void print()
        {

            int length = port.Length;

            StringBuilder sb = new StringBuilder();

            sb.Append("y=");

            for (int i = length - 1; 0 <= i; i--)
            {

                sb.Append(port[i] + "x^" + i);
                if (i > 0) sb.Append(" + ");

            }
                
            Debug.Log(sb.ToString());

        }

        //x에 대해 추정한 f(x)의 값 구하기
        public double getFunc(double x)
        {

            double result = 0;
            int length = port.Length;

            for (int i = 1; i < length; i++)
                result += port[i] * MathLibrary.my_pow(x, i);

            result += port[0];

            return result;

        }

    }

    ///@breif 플레이어가 죽은 위치를 회귀 분석하여 
    ///다음 플레이어 위치를 예상한다.
    ///최고 th_model차항의 함수까지 분석하여
    ///(Hypothesis(x) - Instanc[y])^2 를 통해 제일 적은 cost를 가진
    ///th_model차항의 함수를 사용한다.
    public static class BossRegression
    {

        public static List<Vector3> InstanceList = new List<Vector3>(); ///< 플레이어가 죽은 위치들

        public static GraphFunction Func;

        public static int th_model = 3; //< Hypothesis의 고려하는 최고차항
        public static int max_instance = 1000;

        public static void Init()
        {

            loadInstance();

            Func = getSuitableModel();

        }

        public static void saveInstance()
        {

            List<SerializationVector3> temp = new List<SerializationVector3>();

            for (int i = 0; i < InstanceList.Count; i++)
            {

                SerializationVector3 sv3
                    = new SerializationVector3(InstanceList[i]);

                temp.Add(sv3);

            }

            IOData.SaveData("BossRegression", "Instance", temp);

        }

        public static void loadInstance()
        {

            List<SerializationVector3> sv3 =
                IOData.LoadData<List<SerializationVector3>>("BossRegression", "Instance");

            InstanceList = new List<Vector3>();

            if (sv3 == null) return;

            for (int i = 0; i < sv3.Count; i++)
                InstanceList.Add(new Vector3(sv3[i].x, sv3[i].y, 0f));


        }

        public static void addInstance(Vector3 _pos)
        {

            if (InstanceList.Contains(_pos)) return;

            InstanceList.Add(_pos);

            Func = getSuitableModel();

            if (InstanceList.Count >= max_instance) Theorem();

            saveInstance();

        }

        public static void Theorem()
        {

            InstanceList.Sort((d1, d2) => 
            {

                double cost1 = MathLibrary.my_pow(Func[d1.x] - d1.y, 2),
                    cost2 = MathLibrary.my_pow(Func[d2.x] - d2.y, 2);

                if (cost1 > cost2) return 1;
                else if (cost1 < cost2) return -1;
                else return 0;

            });

            InstanceList.RemoveRange(InstanceList.Count / 2, InstanceList.Count / 2);

        }

        //f(x) 추정
        public static GraphFunction getModel(int model)
        {

            int max_port = (model + 1) * 2,
                count = InstanceList.Count;

            #region Degree Cashing

            double[] Degree = new double[max_port];

            for (int i = 0; i < max_port; i++)
                for (int j = 0; j < count; j++)
                    Degree[i] += MathLibrary.my_pow(InstanceList[j].x, i + 1);

            #endregion

            #region SetMatrix1

            Matrix m1 = new Matrix(model + 1);

            m1[0, 0] = count;
            for (int i = 0; i < model + 1; i++)
                for (int j = 0; j < model + 1; j++)
                {

                    if (i + j == 0) continue;

                    m1[i, j] = Degree[i + j - 1];

                }

            m1 = m1.ReverseMatrix();

            #endregion

            #region SetMatrix2

            Matrix m2 = new Matrix(model + 1);

            for (int i = 0; i < model + 1; i++)
            {

                double sum = 0;

                for (int j = 0; j < count; j++)
                {

                    if (i == 0) sum += InstanceList[j].y;
                    else sum += InstanceList[j].y * MathLibrary.my_pow(InstanceList[j].x, i);

                }

                m2[i, 0] = sum;

            }

            #endregion

            double[] result_model = new double[model + 1];

            for (int i = 0; i < model + 1; i++)
                for (int j = 0; j < model + 1; j++)
                    result_model[i] += m1[i, j] * m2[j, 0];

            GraphFunction gf = new GraphFunction(result_model);

            return gf;

        }

        public static GraphFunction getSuitableModel()
        {

            int count = InstanceList.Count;
            GraphFunction func;

            double[] cost = new double[th_model];

            for (int i = 0; i < th_model; i++)
            {

                func = getModel(i + 1);

                for (int j = 0; j < count; j++)
                    cost[i] +=
                        MathLibrary.my_pow(InstanceList[j].y - func[InstanceList[j].x], 2);

            }

            int min = 0;

            for (int i = 0; i < th_model; i++)
                if (cost[min] >= cost[i]) min = i;


            return getModel(min+1);

        }

        public static float getRegressionPos(float x){ return (float)Func[x]; }

    }

}
