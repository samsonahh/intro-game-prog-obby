using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<Transform> spawnLocations = new List<Transform>();
    public int Level = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void Respawn()
    {
        PlayerManager.Instance.transform.position = spawnLocations[Level].position;
    }
}
