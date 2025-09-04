using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSG = Tekla.Structures.Geometry3d;

namespace Trap2_0.Creators
{
    public class CreateDummyTrede
    {
        Beam topTrede = new Beam();
        TSG.Point startPuntTredeBoven = new TSG.Point();
        double VoorlopigaantalTreden = new double();
        int stopForloop = 0;
        SortedDictionary<double, ArrayList> TrapOpties = new SortedDictionary<double, ArrayList>();

        public void Create()
        {
            double voorzijdeTrede = 200;
            CreateDummyAndWorkplane.dummyBeam.Select();
            startPuntTredeBoven = CreateDummyAndWorkplane.dummyBeam.StartPoint;
                        
            CreateTopTrede(voorzijdeTrede, startPuntTredeBoven);
            bepaalOpEnaanTrede();
            
        }

        void CreateTopTrede(double voorzijdeTrede, TSG.Point startPuntTredeBoven)
        {
            //Beam topTrede = new Beam();
            topTrede.Profile.ProfileString = "PL30*300";
            topTrede.Material.MaterialString = "S235JR";
            topTrede.StartPoint = startPuntTredeBoven + new TSG.Point(0, voorzijdeTrede, -500);
            topTrede.EndPoint = startPuntTredeBoven + new TSG.Point(0, voorzijdeTrede, 500);
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
            bool checkVoorlopigAantal = false;
            int stopForloop = 0;
            for (int h = 201; h < 220; h++)
            {
                var breakstop = 0;
                for (int g = 210; g < 310; g++)
                {
                    if (g + (2*h) > 600 && g + (2*h) < 660)
                    {
                        gMaat = g;
                        hMaat = h;

                        //Checken of er afgeronde aantallen uitkomen.
                        checkVoorlopigAantal = CheckVoorlopigAantalTreden(hMaat);
                        double afstandTrede = 0.0;
                        //if(checkForInteger.Equals(true))
                        //{
                        //    CreateSecondDummyTrede(gMaat, hMaat);
                        //    break;
                        //}
                        if (checkVoorlopigAantal.Equals(true))
                        {
                            afstandTrede = startPuntTredeBoven.X / Math.Ceiling(VoorlopigaantalTreden);

                            if (g + (2 * afstandTrede) > 600 && g + (2 * afstandTrede) < 660)
                            {
                                var text = "Trap Voldoet";
                                var HoekTrap = bepaalHoekTrap(g, afstandTrede);//Wanneer halve graden kijken of deze weer te verzetten zijn naar hele graden

                                var hoekTrapMin = Math.Floor(HoekTrap);
                                var hoekTrapPlus = Math.Ceiling(HoekTrap);

                                //double cosHoekLigger = Math.Cos(44 * (Math.PI / 180));
                                //double sinhoekLigger = Math.Sin(44 * (Math.PI / 180));
                                double tanhoekLigger = Math.Tan(hoekTrapMin * (Math.PI / 180));

                                //var overstaandeZijde = afstandTrede / cosHoekLigger;
                                //var overstaandeZijde1 = afstandTrede * cosHoekLigger;
                                //var overstaandeZijde2 = afstandTrede / sinhoekLigger;
                                //var overstaandeZijde3 = afstandTrede * sinhoekLigger;
                                //var overstaandeZijde4 = afstandTrede / tanhoekLigger;
                                var overstaandeZijde5 = afstandTrede * tanhoekLigger;//

                                if (overstaandeZijde5 + (2 * afstandTrede) > 600 && overstaandeZijde5 + (2 * afstandTrede) < 660)
                                {
                                    gMaat = overstaandeZijde5;
                                    hMaat = afstandTrede;

                                    var opsommingGmaat = 0.0;
                                    var opsommingHmaat = 0.0;
                                    for (int i = 0; i < VoorlopigaantalTreden; i++)
                                    {
                                        opsommingHmaat = opsommingHmaat + hMaat;
                                        opsommingGmaat = opsommingGmaat + gMaat;

                                        //CreateDummyTredeRest(opsommingGmaat, opsommingHmaat);
                                    }

                                    if (!TrapOpties.ContainsKey(hoekTrapMin))
                                    {
                                        TrapOpties.Add(hoekTrapMin, new ArrayList());
                                        TrapOpties[hoekTrapMin].Add(gMaat);
                                        TrapOpties[hoekTrapMin].Add(hMaat);
                                    }
                                }
                                //break;
                            }
                            //else
                            //{
                            //    for (int i = 0; i < 15; i++)//Check tot reduceren van 15% van de onderste trede
                            //    {
                            //        var nieuweHoogte = CreateDummyAndWorkplane.dummyBeam.StartPoint.X / afstandTrede - ((afstandTrede / 100) * (100 - i));
                            //        var nieuweAfstandTrede = nieuweHoogte / (VoorlopigaantalTreden - 1);
                            //        if (g + (2 * nieuweAfstandTrede) > 600 && g + (2 * nieuweAfstandTrede) < 660)
                            //        {
                            //            var text = "Nieuwe Hoogte is juist";

                            //            var HoekTrap = bepaalHoekTrap(g, nieuweAfstandTrede);//Wanneer halve graden kijken of deze weer te verzetten zijn naar hele graden

                            //            var hoekTrapMin = Math.Floor(HoekTrap);
                            //            var hoekTrapPlus = Math.Ceiling(HoekTrap);


                            //            if (!TrapOpties.ContainsKey(hoekTrapMin))
                            //            {
                            //                TrapOpties.Add(hoekTrapMin, new List<double>());
                            //                TrapOpties[hoekTrapMin].Add(gMaat);
                            //                TrapOpties[hoekTrapMin].Add(hMaat);
                            //            }
                            //        }
                            //    }
                                
                            //}
                        }
                        else
                        {
                            for (int i = 0; i < 15; i++)//Check tot reduceren van 15% van de onderste trede
                            {
                                CreateDummyAndWorkplane.dummyBeam.Select();
                                var nieuweHoogte = CreateDummyAndWorkplane.dummyBeam.StartPoint.X / VoorlopigaantalTreden - ((VoorlopigaantalTreden / 100) * (100 - i));
                                //var nieuweAfstandTrede = nieuweHoogte / (VoorlopigaantalTreden - 1);
                                if (g + (2 * nieuweHoogte) > 600 && g + (2 * nieuweHoogte) < 660)
                                {
                                    var text = "Nieuwe Hoogte is juist";

                                    var HoekTrap = bepaalHoekTrap(g, nieuweHoogte);//Wanneer halve graden kijken of deze weer te verzetten zijn naar hele graden

                                    var hoekTrapMin = Math.Floor(HoekTrap);
                                    var hoekTrapPlus = Math.Ceiling(HoekTrap);


                                    if (!TrapOpties.ContainsKey(hoekTrapMin))
                                    {
                                        TrapOpties.Add(hoekTrapMin, new ArrayList());
                                        TrapOpties[hoekTrapMin].Add(gMaat);
                                        TrapOpties[hoekTrapMin].Add(nieuweHoogte);
                                        TrapOpties[hoekTrapMin].Add("Reductie laatste Trede =" + " " + i.ToString());
                                    }
                                }
                            }

                        }
                    }                    
                }
                //if (stopForloop == 1)
                //    break;
                //if(checkForInteger.Equals(true))
                //    break ;
            }
            ///////////
            ///Hoek van de Trap nog bepalen //Kijken welke hoek eruit komt
            ///
            //bepaalHoekTrap(gMaat, hMaat);


            var CheckDictionary = TrapOpties;



            var test2 = 0;
        }

        double bepaalHoekTrap(double gMaat, double hMaat)
        {
            //Vector secBeamVector = new Vector(secondaryBeam.GetCoordinateSystem().AxisY);
            //Vector testBeamVector = new Vector(testBeam.GetCoordinateSystem().AxisY);

            double hoekTrap = 0.0;

            ControlLine controlLine = new ControlLine();
            controlLine.Line.Point1 = new TSG.Point(0, 0, 0);
            controlLine.Line.Point2 = new TSG.Point(0, gMaat, 0);
            controlLine.Insert();

            ControlLine controlLine2 = new ControlLine();
            controlLine2.Line.Point1 = new TSG.Point(0, gMaat, 0);
            controlLine2.Line.Point2 = new TSG.Point(hMaat, gMaat, 0);
            controlLine2.Insert();

            ControlLine controlLineSchuin = new ControlLine();
            controlLineSchuin.Line.Point1 = controlLine.Line.Point1;
            controlLineSchuin.Line.Point2 = controlLine2.Line.Point2;
            controlLineSchuin.Insert();

            TSG.Vector v1 = new TSG.Vector(controlLine.Line.Point2 - controlLine.Line.Point1);
            TSG.Vector v2 = new TSG.Vector(controlLineSchuin.Line.Point2 - controlLineSchuin.Line.Point1);
            v1.Normalize();
            v2.Normalize();

            double angleBetweenBeamsInRadians = v1.GetAngleBetween(v2);
            double dimAngle = angleBetweenBeamsInRadians * (180 / Math.PI);
            double hoekLigger = 90 - dimAngle;
            var AbsoluteHoekligger = Math.Abs(hoekLigger);

            stopForloop = 1;

            var test = 0.0;

            //double cosHoekLigger = Math.Cos(AbsoluteHoekligger * (Math.PI / 180));
            //double sinhoekLigger = Math.Sin(AbsoluteHoekligger * (Math.PI / 180));
            //double tanhoekLigger = Math.Tan(AbsoluteHoekligger * (Math.PI / 180));

            return AbsoluteHoekligger;
        }

        void CreateDummyTredeRest(double gMaat, double hMaat)
        {
            topTrede.Select();

            Beam beam = new Beam();
            beam.Profile.ProfileString = "PL30*300";
            beam.Material.MaterialString = "S235JR";
            beam.StartPoint = topTrede.StartPoint + new TSG.Point(-hMaat, gMaat, 0);
            beam.EndPoint = topTrede.EndPoint + new TSG.Point(-hMaat, gMaat, 0);
            beam.Position.Depth = Position.DepthEnum.FRONT;
            beam.Position.Rotation = Position.RotationEnum.FRONT;
            beam.Position.Plane = Position.PlaneEnum.RIGHT;
            beam.Insert();
        }
        bool CheckVoorlopigAantalTreden(double hMaat)
        {
            //double VoorlopigaantalTreden = 0.0;
            VoorlopigaantalTreden = startPuntTredeBoven.X / hMaat;

            return VoorlopigaantalTreden % 1 > 0.95;
            
        }
    }
}
