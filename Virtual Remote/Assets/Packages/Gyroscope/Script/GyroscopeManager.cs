using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeManager : MonoBehaviour
{
    Vector3 origins;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    //Callibrer l'origine du téléphone
    public void Callibrate()
    {
        origins.x = Mathf.Round(Input.gyro.attitude.eulerAngles.x);
        origins.y = Mathf.Round(Input.gyro.attitude.eulerAngles.y);
        origins.z = Mathf.Round(Input.gyro.attitude.eulerAngles.z);
    }

    //Obtenir les origines des axes
    public Vector3 GetAxeOrigin()
    {
        return origins;
    }

    //Obtenir les angles
    public Vector3 GetAngles()
    {
        return new Vector3(Input.gyro.attitude.eulerAngles.x, Input.gyro.attitude.eulerAngles.y, Input.gyro.attitude.eulerAngles.z);
    }

    //Obtenir les angles en fonction de leur origin
    public Vector3 GetAnglesByOrigin()
    {
        // Soustraire l'angle d'origine
        Vector3 angle = GetAngles() - GetAxeOrigin();

        // Assurer que l'angle reste dans la plage [0, 360]
        angle.x = (angle.x + 360) % 360;
        angle.y = (angle.y + 360) % 360;
        angle.z = (angle.z + 360) % 360;

        return angle;
    }

    //Obtenir l'angle dans la plage [-180, 180]
    public Vector3 GetAngleNegatifForm(Vector3 angles)
    {
        Vector3 newAngles = angles;
        if (newAngles.x > 180)
        {
            newAngles.x -= 360;
        }
        if (newAngles.y > 180)
        {
            newAngles.y -= 360;
        }
        if (newAngles.z > 180)
        {
            newAngles.z -= 360;
        }
        return newAngles;
    }

    //Normalisé l'angle
    public Vector3 GetNormalizedAngles(float valMax, Vector3 angles)
    {
        return new Vector3(angles.x / valMax, angles.y / valMax, angles.z / valMax);
    }

}
