using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;

    private void Update()
    {
        levelText.text = $"Level {GameManager.Instance.Level + 1}";
    }
}
