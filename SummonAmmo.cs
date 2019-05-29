using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonAmmo : MonoBehaviour
{
    public Button ShootingButton;
    public static bool YesShoot;

    // Start is called before the first frame update
    void Start()
    {
        ShootingButton.onClick.AddListener(OnShoot);
    }

    public void OnShoot()
    {
        if (YesShoot == false)
        {
            YesShoot = true;
        }
    }
}
