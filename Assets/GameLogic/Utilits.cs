using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Utilits
{
    public static class GameUtilits
    {
        public static Vector3 GetRandomDir()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.RandomRange(-1f, 1f)).normalized;
        }

    }
}
