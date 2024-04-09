using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

public class SpotGame : MiniGame
{
    [SerializeField] Image image;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Skip);
        image = GetComponent<Image>();
    }

   
    public override void ResetGame()
    {
        Color imageColor = image.color;
        imageColor.a = 1;
        image.color = imageColor;
        isSolved = false;
    }

    public override void Skip()
    {
        if (isSolved)
            return;
        Color imageColor = image.color;
        imageColor.a = 0;
        image.color = imageColor;
        isSolved = true;
    }

   
}
