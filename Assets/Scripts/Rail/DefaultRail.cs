using UnityEngine;
using System.Linq;

public class DefaultRail : MonoBehaviour
{
    public int rotation = 0;

    public Hex hexComponent;
    public RailManager railManager;

    private void Start()
    {
        DataManager.Instance.CreateRail(hexComponent.offsetCoordinate);
        CheckMapGenerate();
    }
    private void CheckMapGenerate()
    {
        int lastHexWidth = DataManager.Instance.data.gridData.hexes.Max(hexData => hexData.offsetCoordinate.x);
        if (lastHexWidth < hexComponent.offsetCoordinate.x + 3)
        {
            GridCreator.Instance.GenerateNextLevelGrid();
        }
    }
    public virtual void RotateRail() { }
    public void ChangeRail(Hex nextRail)
    {
        RailManager previousRail = railManager.FindPreviousRail();

        Vector2Int[] neighborOffsets = NeighborOffsets.GetOffsets(nextRail.offsetCoordinate.y, true);
        Vector2Int offset = neighborOffsets[1];

        if (nextRail.offsetCoordinate == hexComponent.offsetCoordinate + offset)
        {
            if (rotation == 60 || rotation == -60)
            {
                railManager.ChangeToTurnedRail();
            }
            return;
        }
        if (previousRail.railComponent is TurnedRail || previousRail.railComponent is SharpRail || (previousRail.railComponent is Rail && this is Rail))
        {
            Vector2Int sharpOffset = new Vector2Int(-1, 0);
            if (previousRail.hexComponent.offsetCoordinate == nextRail.offsetCoordinate + sharpOffset)
            {
                railManager.ChangeToSharpRail();
                return;
            }
        }

        if (rotation == 60 || rotation == -60)
        {
            return;
        }
        railManager.ChangeToTurnedRail();
    }
}
