using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtensions
{
    public static void ForEach<T>(this T[,] arr, THandler<T> whatToDo)
    {
        for (int rowIndex = 0; rowIndex < arr.GetLength(1); rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < arr.GetLength(0); collumnIndex++)
            {
                whatToDo?.Invoke(arr[collumnIndex, rowIndex]);
            }
        }
    }
    
    public static bool Any<T>(this T[,] arr, BoolOutTHandler<T> whatToCheck)
    {
        for (int rowIndex = 0; rowIndex < arr.GetLength(1); rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < arr.GetLength(0); collumnIndex++)
            {
                if (whatToCheck(arr[collumnIndex, rowIndex]))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
