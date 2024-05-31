using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDaño{
   void Take_damage(float damage, Vector2 puntoGolpe);
   void Animar_Muerte();
}
