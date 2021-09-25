using UnityEngine;
using MMC;
[CreateAssetMenu(fileName = "New Content ", menuName = "Game/Content2")]
public class ContentObjectMMC : ScriptableObject
{
    public Sprite graphic;
    public string title;
    public string description;
    public int physical;
    public int mental;
    public int spiritual;
}
