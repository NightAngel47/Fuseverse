﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalPainter : MonoBehaviour
{
    public GameObject decalBradley;
    public bool canDecal = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && canDecal)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        canDecal = false;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Instantiate(decalBradley, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
        }

        Invoke("ResetCanDecal", 0.1f);
    }

    void ResetCanDecal()
    {
        canDecal = true;
    }
}