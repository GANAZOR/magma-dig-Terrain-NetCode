using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorScript : MonoBehaviour
{
    [SerializeField] TerrainDeformationRealtime _tDR;
    [SerializeField] SphereTerrainModifier _sTM;

    public GameObject BOLA;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
                swapCode();
        }
    }
    public void swapCode()
    {
        if(_sTM.enabled == true && _tDR.enabled == false)
        {
            _tDR.enabled = true;
            _sTM.enabled = false;
            BOLA.SetActive(false);
        }
        else
        {
            _sTM.enabled = true;
            _tDR.enabled = false;
            BOLA.SetActive(true);
        }
    }
}
