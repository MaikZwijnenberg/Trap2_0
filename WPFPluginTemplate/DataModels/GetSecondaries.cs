using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using Tekla.Structures.Model;
using Tekla.Structures;
using TSG = Tekla.Structures.Geometry3d;

namespace Trap2_0.DataModels
{
    public class GetSecondaries
    {
        public static ArrayList partsList = new ArrayList();
        public static TSG.Point startPoint = new TSG.Point();
        public static TSG.Point firstRichtingPunt = new TSG.Point();
        public static TSG.Point secondRichtingPunt = new TSG.Point();

        public void Get(dynamic inputList, Model _model)
        {
            var aantal = inputList.Count;
            int nummer = 0;

            foreach (var item in inputList)
            {
                if (nummer != aantal)
                {
                    try
                    {
                        ContourPlate part = _model.SelectModelObject(item.GetInput() as Identifier) as ContourPlate;
                        if (part != null) { partsList.Add(part); }
                    }
                    catch { }
                    try
                    {
                        Beam part = _model.SelectModelObject(item.GetInput() as Identifier) as Beam;
                        if (part != null) { partsList.Add(part); }
                    }
                    catch { }
                }
                nummer++;
            }

            //startPoint = picketPoint;
            startPoint = inputList[aantal - 2].GetInput() as TSG.Point;



            //int aantalkeer = 0;
            //if (inputList[aantal - 1].GetInput() as ArrayList != null)
            //{
            //    foreach (var punt in inputList[aantal - 1].GetInput() as ArrayList)
            //    {
            //        if (aantalkeer == 0)
            //            firstRichtingPunt = punt as TSG.Point;
            //        else if (aantalkeer == 1)
            //            secondRichtingPunt = punt as TSG.Point;
            //        aantalkeer++;
            //    }
            //}
            ArrayList currentArray = inputList[aantal - 1].GetInput() as ArrayList;

            if (currentArray.Count != 0)
            {
                firstRichtingPunt = currentArray[0] as TSG.Point;
                secondRichtingPunt = currentArray[1] as TSG.Point;
            }


            //firstRichtingPunt = inputList[aantal - 2].GetInput() as TSG.Point;
            //secondRichtingPunt = inputList[aantal - 1].GetInput() as TSG.Point;

            //richtingPunten = inputList[aantal - 1].GetInput() as ArrayList;
        }
    }
}
