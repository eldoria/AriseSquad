using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
  public Transform Joueur_v5;
  public Camera MainCamera;
  public bool RotateWithPlayer = true;

  private void Start()
  {
      SetPosition();
      SetRotation();
  }

  private void LateUpdate()
  {
      if (Joueur_v5 != null)
      {
          SetPosition();
          if (RotateWithPlayer && MainCamera)
          {
              SetRotation();
          }
      }
  }

  private void SetRotation()
  {
      transform.rotation = Quaternion.Euler(90.0f,MainCamera.transform.eulerAngles.y,0.0f);
  }


  private void SetPosition()
  {
      var newPos = Joueur_v5.position;
      newPos.y = transform.position.y;

      transform.position = newPos;
  }
}
