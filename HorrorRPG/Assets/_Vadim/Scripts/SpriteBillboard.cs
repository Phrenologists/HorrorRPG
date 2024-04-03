using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpriteBillboard : ICameraTransformDependant
{
    [SerializeField] private bool freezeXZ = true;

    public override void Work(Transform cameraTransform)
    {
        DoBillboard(transform, cameraTransform, freezeXZ);
    }
    private void DoBillboard(Transform sprite, Transform camera, bool freezeXZ)
    {
        EulerAnglesClamper.Instance.ClampObjectAngle(sprite);

        if (freezeXZ)
        {
            sprite.rotation = Quaternion.Euler(sprite.eulerAngles.x,
                camera.transform.eulerAngles.y, sprite.eulerAngles.z);
        }
        else
        {
            sprite.rotation = camera.transform.rotation;
        }
    }

  
}
