using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityScript.Lang;

public class Hoge : MonoBehaviour
{
        int[] array = new int[]{3,5,1,4,2};
    
    
}


public class SelectionSort{
    public static void Main(){
        int[] arr = {2,3,4,1,6,7,8,9,5};

        Sort(arr);

        for (int i = 0; i < arr.Length-1; i++)
        {
            System.Console.WriteLine(arr[i]);    
        }
        
        
    }

    private static void Sort(int[] A)
    {
        for (int i = 0; i < A.Length - 1; i++)
        {
            SelectMin(A, i);
        }
    }

    private static void SelectMin(int[] A, int i)
    {
        int min = i;

        for (int j = i + 1; j < A.Length; j++)
        {

            if (A[min] > A[j])
            {
                min = j;
            }
        }

        int temp = A[i];
        A[i] = A[min];
        A[min] = temp;

    }
}
