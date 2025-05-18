using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderWork : MonoBehaviour
{
    [Header("Player")]
    public Transform playerTransform; // ���������� ���� ������ ������

    [Header("Grass Material(s)")]
    public Material grassMaterial; // ���������� ���� �������� �����

    // ���� � ��� ����� ������ ���������� �����, ����������� ������:
    // public Material[] grassMaterials;

    // ���������� ID �������� ��� ������������������
    private int playerPositionShaderID;

    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = this.transform; // ���������� ���� ������ ��� ������, ���� �� ������� ������
        }

        // �������� ID �������� ������� ������ ���� ���
        playerPositionShaderID = Shader.PropertyToID("_PlayerPosition");

        if (grassMaterial == null)
        {
            Debug.LogError("Grass Material is not assigned in UpdateGrassShader script.");
        }
        // ���� ����������� ������:
        // if (grassMaterials == null || grassMaterials.Length == 0)
        // {
        //     Debug.LogError("Grass Materials array is empty or not assigned.");
        // }
    }

    void Update()
    {
        if (playerTransform != null && grassMaterial != null)
        {
            // �������� ������� ������ � �������� �������
            grassMaterial.SetVector(playerPositionShaderID, playerTransform.position);
        }
        // ���� ����������� ������:
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
