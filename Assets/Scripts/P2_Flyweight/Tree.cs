using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public TreeSeasonColors _treeColors;
    private int _tick;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSeason();
    }
    
    void FixedUpdate()
    {
        UpdateSeason();
    }

    /// <summary>
    /// In the Tree Colors, the Artist assigned very specific values for the seasoning tree.
    /// Each tree needs to access their colors depending on how old they are.
    /// Unfortunately, this solution uses up a lot of Memory :(
    /// </summary>

    void UpdateSeason()
    {
        _treeColors.MoveNext();
        _spriteRenderer.color = _treeColors.CurrentColor;
    }
}
