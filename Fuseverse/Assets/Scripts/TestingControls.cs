﻿#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingControls : MonoBehaviour
{
    public GameObject selectedGO;
    public GameObject[] terrainModels; // 0 terrainErase, 1 pine, 2 palm, 3 cacti, 4 stump, 5 hill, 6 mountains, 7 ice chunk, 8 rock
    public GameObject[] biomeTextures; // 0 grass, 1 artic, 2 sand, 3 forest, 4 badlands, 5 mountain, 6 plains, 7 water
    public enum tools { none, terrain, biomes };
    public tools toolSelected;
    public bool canPaint = true;
    public GameObject planet;

    private enum terrainTools { none, up, plants, erase }
    private terrainTools terrainToolSelected;

    // Update is called once per frame
    void Update()
    {
        //Arrow key functionality for painting
        if (Input.GetMouseButton(0) && canPaint)
        {
            HandleInput();
        }

        //Arrow key functionality for rotating planet
       if (Input.GetKey(KeyCode.LeftArrow))
       {
           transform.RotateAround(Vector3.zero, Vector3.down, 20 * Time.deltaTime);

       }
       if (Input.GetKey(KeyCode.RightArrow))
       {
           transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
       }
       if (Input.GetKey(KeyCode.UpArrow))
       {
           transform.RotateAround(Vector3.zero, Vector3.left, 20 * Time.deltaTime);
        
       }
       if (Input.GetKey(KeyCode.DownArrow))
       {
           transform.RotateAround(Vector3.zero, Vector3.right, 20 * Time.deltaTime);
       }
    }

    // paints
    void HandleInput()
    {
        canPaint = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // paint raycast
        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Debug.DrawLine(transform.position, hitInfo.transform.position);

            // paint raycast
            if (Physics.Raycast(ray, out hitInfo, 100f, 9))
            {
                Debug.DrawLine(transform.position, hitInfo.transform.position);

                // get selected GO for terrain
                if (toolSelected == tools.terrain)
                {
                    ChangeTerrain();
                }

                // paint
                if (toolSelected != tools.none)
                {
                    PaintGO(hitInfo);
                }
            }
        }

        Invoke("ResetCanPaint", 0.01f);
    }

    // spawn gameobjects
    void PaintGO(RaycastHit hitInfo)
    {
        GameObject newGO = Instantiate(selectedGO, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
        newGO.transform.SetParent(planet.transform);
    }

    // slows down painting
    void ResetCanPaint()
    {
        canPaint = true;
    }

    // change tool to touch with
    public void ChangeTool(string tool)
    {
        tools selectedTool = (tools)System.Enum.Parse(typeof(tools), tool);

        toolSelected = selectedTool;
        print(toolSelected);

        // clear selected to not paint when switching
        selectedGO = null;
    }

    // change terrain paint mode
    public void TerrainOption(string tool)
    {
        terrainToolSelected = (terrainTools)System.Enum.Parse(typeof(terrainTools), tool);
        print(terrainToolSelected);

        selectedGO = null;
    }

    // change terrain model object to place
    private void ChangeTerrain()
    {
        // terrain objects have a script that will update themselves based on biome so spawns default grass version

        if (terrainToolSelected == terrainTools.erase) // erase
        {
            selectedGO = terrainModels[0]; // terrain ereaser
        }
        else // default grass
        {
            if (terrainToolSelected == terrainTools.up)
            {
                selectedGO = terrainModels[5]; // hill
            }
            else if (terrainToolSelected == terrainTools.plants)
            {
                selectedGO = terrainModels[1]; // pine
            }
        }

        print("ChangeTerrain: " + selectedGO);
    }

    // change biome texture object to place
    public void ChangeBiomes(int selectedBiome)
    {
        selectedGO = biomeTextures[selectedBiome];
    }
}

#endif
