using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderWork : MonoBehaviour
{
    [Header("Player")]
    public Transform playerTransform; // Перетащите сюда вашего игрока

    [Header("Grass Material(s)")]
    public Material grassMaterial; // Перетащите сюда материал травы

    // Если у вас много разных материалов травы, используйте массив:
    // public Material[] grassMaterials;

    // Используем ID свойства для производительности
    private int playerPositionShaderID;

    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = this.transform; // Используем этот объект как игрока, если не указано другое
        }

        // Получаем ID свойства шейдера только один раз
        playerPositionShaderID = Shader.PropertyToID("_PlayerPosition");

        if (grassMaterial == null)
        {
            Debug.LogError("Grass Material is not assigned in UpdateGrassShader script.");
        }
        // Если используете массив:
        // if (grassMaterials == null || grassMaterials.Length == 0)
        // {
        //     Debug.LogError("Grass Materials array is empty or not assigned.");
        // }
    }

    void Update()
    {
        if (playerTransform != null && grassMaterial != null)
        {
            // Передаем позицию игрока в материал шейдера
            grassMaterial.SetVector(playerPositionShaderID, playerTransform.position);
        }
        // Если используете массив:
        // if (playerTransform != null && grassMaterials != null)
        // {
        //     Vector3 playerPos = playerTransform.position;
        //     foreach (Material mat in grassMaterials)
        //     {
        //         if (mat != null)
        //         {
        //             mat.SetVector(playerPositionShaderID, playerPos);
        //         }
        //     }
        // }
    }
}
