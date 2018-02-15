using System.Collections;
using System.Collections.Generic;


    public class MapSystem : Singleton<MapSystem>
    {
        protected MapSystem()
        {
            Initialize();
        }

        public static SingleTile[,] Map1Server = new SingleTile[200, 200];
        public static SingleTile[,] Map1Local = new SingleTile[200, 200];
        public static List<FibulaObject> ObjListSerwer = new List<FibulaObject>();
        public static List<FibulaObject> ObjListLocal = new List<FibulaObject>();

        public void Initialize()
        {
            for (int i = 0; i < Map1Server.GetLength(0); i++)
            {
                for (int j = 0; j < Map1Server.GetLength(1); j++)
                {
                    Map1Server[i, j] = new SingleTile();
                    Map1Local[i, j] = new SingleTile();
                }
            }
        }
    }
