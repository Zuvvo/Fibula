using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectList 
{


    // Lista ze wszystkimi stworzonymi obiektami w grze
 //   public static List<FibulaObject> ObjList = new List<FibulaObject>();

    //// po stworzeniu np. Spider spider = new Spider() powinno tutaj dodawać do listy referencję do tego obiektu
    //public static void AddObject(FibulaObject toAdd)
    //{
    //    ObjList.Add(toAdd);
    //    foreach (var thing in ObjList)
    //    {
    //        Debug.Log(thing);
    //    }
    //}
    //Funkcja na start w innych obiketach cos takiego:


    // W SpiderTest.cs przetestujemy wczytywanie danych z klasy Spider (chociażby prędkość)

    // po przesunięciu pająka na inne pole powinna zaktualizować się propercja spider.position 
    // a w SingleTile[position.x,position.y] powinno być dodane info, że znajduje się tam pająk i jaki pająk
    // myślę, że to jaki pająk się tam znajduje musi być powiązane jakoś z tą listą
}