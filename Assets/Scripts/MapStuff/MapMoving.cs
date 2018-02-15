public class MapMoving {

    public static bool MoveTo(FibulaPosition startPostion, FibulaPosition endPosition, FibulaObject objectToMove)
    {
        if (MapSystem.Map1Server[endPosition.x, endPosition.y].isBlocked)
        {
            return false;
        }
        else
        {
            
            objectToMove.position = endPosition;
            MapSystem.Map1Server[startPostion.x, startPostion.y].isBlocked = false;
            MapSystem.Map1Local[startPostion.x, startPostion.y].isBlocked = false;
            MapSystem.Map1Server[endPosition.x, endPosition.y].isBlocked = true;
            MapSystem.Map1Local[endPosition.x, endPosition.y].isBlocked = true;


            MapSystem.Map1Server[startPostion.x, startPostion.y].EnemyOnTile = null;
            MapSystem.Map1Local[startPostion.x, startPostion.y].EnemyOnTile = null;
            MapSystem.Map1Server[endPosition.x, endPosition.y].EnemyOnTile = objectToMove;
            MapSystem.Map1Local[endPosition.x, endPosition.y].EnemyOnTile = objectToMove;
            objectToMove.position = endPosition;

            if(objectToMove is FibulaPlayer)
            {
                MapSystem.Map1Server[startPostion.x, startPostion.y].isPlayerOnTile = false;
                MapSystem.Map1Local[startPostion.x, startPostion.y].isPlayerOnTile = false;
                MapSystem.Map1Server[endPosition.x, endPosition.y].isPlayerOnTile = true;
                MapSystem.Map1Local[endPosition.x, endPosition.y].isPlayerOnTile = true;
            }

            return true;
        }
    }
}
