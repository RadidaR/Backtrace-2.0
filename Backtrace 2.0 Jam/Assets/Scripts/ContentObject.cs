using UnityEngine;
using MMC;
[CreateAssetMenu(fileName = "New Content", menuName = "Game/Content")]
public class ContentObject : ScriptableObject
{
    public Type type;
    public bool negative;
    public string title;
    public string description;
    public Sprite graphic;
    public int physical;
    public int mental;
    public int spiritual;
}
