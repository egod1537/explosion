using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GameJamLibrary
{
    //다항 회귀를 위한 행렬 클래스
    public class Matrix
    {

        public int size;

        public double[,] matrix;

        public Matrix(int _size)
        {
            size = _size;
            matrix = new double[size, size];
        }

        public double this[int index1, int index2]
        {
            get { return matrix[index1, index2]; }
            set { matrix[index1, index2] = value; }
        }

        public void setIdentity()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matrix[i, j] = (i == j) ? 1 : 0;
        }

        //Gaussian Elimination으로 역행렬을 만든다.
        public Matrix ReverseMatrix()
        {
            Matrix unit_matrix = new Matrix(size);
            unit_matrix.setIdentity();

            for(int k=0; k < size; k++)
            {
                int t = k - 1;
                while (t + 1 < size && (matrix[++t, k] == 0)) ;

                swapRow(k, t); unit_matrix.swapRow(k, t);

                double temp = matrix[k, k];
                for (int i = 0; i < size; i++)
                {
                    matrix[k, i] /= temp;
                    unit_matrix.matrix[k, i] /= temp;
                }
                  
                for(int i=0; i < size; i++)
                {
                    if (i == k) continue;
                    temp = matrix[i, k];
                    for(int j=0; j < size; j++)
                    {
                        if (j >= k) matrix[i, j] -= matrix[k, j] * temp;
                        unit_matrix.matrix[i, j] -= unit_matrix.matrix[k, j] * temp;
                    }


                }

            }

            return unit_matrix;
        }

        public void print()
        {

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < size; i++)
            {

                for (int j = 0; j < size; j++)
                    sb.Append(matrix[i, j] + " ");

                sb.AppendLine();

            }

            Debug.Log(sb.ToString());

        }

        void swap(double _x, double _y)
        {

            double temp = _x;
            _x = _y;
            _y = temp;

        }

        void swapRow(int x1, int x2)
        {

            for (int i = 0; i < size; i++)
                swap(matrix[i, x1], matrix[i, x2]);

        }

    }

}
