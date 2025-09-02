using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace Trap2_0.Creators
{
    public class CreateDummyAndWorkplane
    {
        public static Beam dummyBeam = new Beam();
        public static Beam dummyBeam2 = new Beam();
        public void Create(Model model, Point punt1, Point punt2, Point punt3)
        {
            ChangePoint(punt1, punt2);

            //Beam dummyBeam = new Beam();
            dummyBeam.Profile.ProfileString = "HEA100";
            dummyBeam.Material.MaterialString = "S235JR";
            dummyBeam.StartPoint = punt1;
            dummyBeam.EndPoint = punt2;
            dummyBeam.Position.Rotation = Position.RotationEnum.FRONT;
            dummyBeam.Position.Depth = Position.DepthEnum.MIDDLE;
            dummyBeam.Insert();

            //Beam beam2 = new Beam();
            dummyBeam2.Profile.ProfileString = "HEA100";
            dummyBeam2.Material.MaterialString = "S235JR";
            dummyBeam2.StartPoint = punt2;
            dummyBeam2.EndPoint = punt3;
            dummyBeam2.Position.Rotation = Position.RotationEnum.FRONT;
            dummyBeam2.Position.Depth = Position.DepthEnum.MIDDLE;
            dummyBeam2.Insert();

            SetWorkplane(model, punt1, punt2, punt3);
            CreateWorkplaneBeams();
        }
        void SetWorkplane(Model model, Point punt1, Point punt2, Point punt3)
        {
            WorkPlaneHandler planeHandler = model.GetWorkPlaneHandler();
            Vector vector1 = new Vector(punt1 - punt2);
            Vector vector2 = new Vector(punt3 - punt2);
            TransformationPlane newPlane = new TransformationPlane(new CoordinateSystem(punt2, vector1, vector2));
            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(newPlane);
        }
        void CreateWorkplaneBeams()
        {
            Beam xbeam = new Beam();
            xbeam.Profile.ProfileString = "R10";
            xbeam.Material.MaterialString = "S235JR";
            xbeam.StartPoint = new Point(0, 0, 0);
            xbeam.EndPoint = new Point(1000, 0, 0);
            xbeam.Position.Depth = Position.DepthEnum.MIDDLE;
            xbeam.Insert();

            Beam ybeam = new Beam();
            ybeam.Profile.ProfileString = "R20";
            ybeam.Material.MaterialString = "S235JR";
            ybeam.StartPoint = new Point(0, 0, 0);
            ybeam.EndPoint = new Point(0, 1000, 0);
            ybeam.Position.Depth = Position.DepthEnum.MIDDLE;
            ybeam.Insert();

            Beam zbeam = new Beam();
            zbeam.Profile.ProfileString = "R30";
            zbeam.Material.MaterialString = "S235JR";
            zbeam.StartPoint = new Point(0, 0, 0);
            zbeam.EndPoint = new Point(0, 0, 1000);
            zbeam.Position.Depth = Position.DepthEnum.MIDDLE;
            zbeam.Insert();
        }
        void ChangePoint(Point punt1, Point punt2)
        {
            double maatVoorzijdeTrede = 200;

        }
    }
}
