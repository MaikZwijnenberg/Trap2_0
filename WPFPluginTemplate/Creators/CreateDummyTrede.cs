using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace Trap2_0.Creators
{
    public class CreateDummyTrede
    {
        Beam topTrede = new Beam();
        Point startPuntTredeBoven = new Point();
        public void Create()
        {
            double voorzijdeTrede = 200;
            CreateDummyAndWorkplane.dummyBeam.Select();
            startPuntTredeBoven = CreateDummyAndWorkplane.dummyBeam.StartPoint;
                        
            CreateTopTrede(voorzijdeTrede, startPuntTredeBoven);
            bepaalOpEnaanTrede();
            
        }

        void CreateTopTrede(double voorzijdeTrede, Point startPuntTredeBoven)
        {
            //Beam topTrede = new Beam();
            topTrede.Profile.ProfileString = "PL30*300";
            topTrede.Material.MaterialString = "S235JR";
            topTrede.StartPoint = startPuntTredeBoven + new Point(0, voorzijdeTrede, -500);
            topTrede.EndPoint = startPuntTredeBoven + new Point(0, voorzijdeTrede, 500);
            topTrede.Position.Depth = Position.DepthEnum.FRONT;
            topTrede.Position.Rotation = Position.RotationEnum.FRONT;
            topTrede.Position.Plane = Position.PlaneEnum.RIGHT;
            topTrede.Insert();
        }

        void bepaalOpEnaanTrede()
        {
            ///600 < g + 2h < 660

            //Loopvlak Trede (g) moet tussen de 210 en 310mm liggen
            //Optrede (h) moet tussen de 201 en 220mm liggen
            double gMaat = 0;
            double hMaat = 0;
            bool checkForInteger = false;
            for (int h = 201; h < 220; h++)
            {
                for (int g = 210; g < 310; g++)
                {
                    if (g + (2*h) > 600 && g + (2*h) < 660)
                    {
                        gMaat = g;
                        hMaat = h;
                        CreateSecondDummyTrede(gMaat, hMaat);

                        //Checken of er afgeronde aantallen uitkomen.
                        checkForInteger = CheckAantalTreden(hMaat);
                        if(checkForInteger.Equals(true))
                            break;                                                
                    }                    
                } 
                if(checkForInteger.Equals(true))
                    break ;
            }
            ///////////
            ///Hoek van de Trap nog bepalen //Kijken welke hoek eruit komt
            ///
            bepaalHoekTrap();






            var test2 = 0;
        }

        void bepaalHoekTrap()
        {

        }

        void CreateSecondDummyTrede(double gMaat, double hMaat)
        {
            topTrede.Select();

            Beam beam = new Beam();
            beam.Profile.ProfileString = "PL30*300";
            beam.Material.MaterialString = "S235JR";
            beam.StartPoint = topTrede.StartPoint + new Point(-hMaat, gMaat, 0);
            beam.EndPoint = topTrede.EndPoint + new Point(-hMaat, gMaat, 0);
            beam.Position.Depth = Position.DepthEnum.FRONT;
            beam.Position.Rotation = Position.RotationEnum.FRONT;
            beam.Position.Plane = Position.PlaneEnum.RIGHT;
            beam.Insert();
        }
        bool CheckAantalTreden(double hMaat)
        {
            double aantalTreden = 0.0;
            aantalTreden = startPuntTredeBoven.X / hMaat;

            return aantalTreden % 1 == 0;
            
        }
    }
}
