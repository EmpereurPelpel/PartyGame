using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static int Mod(int k, int n)
    {
        return ((k %= n) < 0) ? k + n : k;
    }
}
