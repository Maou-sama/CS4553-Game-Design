using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Desicion : ScriptableObject {

    public abstract bool Decide(StateController controller);
}
