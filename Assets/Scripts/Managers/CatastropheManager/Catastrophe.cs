using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Catastrophe
{
    public abstract string CatastropheName { get; }
}

public class Tsunami : Catastrophe
{
    public override string CatastropheName => "Tsunami";
}
public class Tornado : Catastrophe
{
    public override string CatastropheName => "Tornado";
}
public class Earthquake : Catastrophe
{
    public override string CatastropheName => "Earthquake";
}
public class ForestFire : Catastrophe
{
    public override string CatastropheName => "Forest Fire";
}