using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using static Tekla.Structures.Plugins.PluginBase;
using Point = Tekla.Structures.Geometry3d.Point;
using Trap2_0.DataModels;
using Trap2_0.Models;
using System.Windows.Interop;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;
using Trap2_0.Creators;
using Tekla.Structures.Datatype;

namespace Trap2_0
{
    [Plugin("Trap2_0")]
    [PluginUserInterface("Trap2_0.MainWindow")]
    //[SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ZERO)]
    //[AutoDirectionType(AutoDirectionTypeEnum.AUTODIR_BASIC)]
    [PositionType(PositionTypeEnum.COLLISION_PLANE)]
    public class Trap2_0 : PluginBase
    {
        #region Fields
        private Model _model;
        private StructuresData _data;

        public ArrayList ListMainPart = new ArrayList { };

        //
        // Define variables for the field values.
        //
        //private string _PartName = string.Empty;
        //private string _Profile = string.Empty;
        //private string _Material = string.Empty;
        //private double _Offset = 0.0;
        //private int _LengthFactor = 0;
        #endregion

        #region Properties
        private Model Model
        {
            get { return this._model; }
            set { this._model = value; }
        }

        private StructuresData Data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        TransformationPlane originalPlane = new TransformationPlane();
        #endregion

        #region Constructor
        public Trap2_0(StructuresData data)
        {
            Model = new Model();
            Data = data;
        }
        #endregion

        #region Overrides

        public override List<InputDefinition> DefineInput()
        {
            List<InputDefinition> inputList = new List<InputDefinition>();
            Picker Picker = new Picker();

            ArrayList Punt1 = Picker.PickPoints(Picker.PickPointEnum.PICK_ONE_POINT, "Kies Punt1");
            ArrayList Punt2 = Picker.PickPoints(Picker.PickPointEnum.PICK_ONE_POINT, "kies Punt2");
            ArrayList Punt3 = Picker.PickPoints(Picker.PickPointEnum.PICK_ONE_POINT, "geef richting aan");

            inputList.Add(new InputDefinition(Punt1));
            inputList.Add(new InputDefinition(Punt2));
            inputList.Add(new InputDefinition(Punt3));

            return inputList;
        }

        public override bool Run(List<InputDefinition> Input)
        {
            try
            {
                Data.ValidateData();
                //GetValuesFromDialog();

                TSG.Point punt1 = Input[0].GetInput() as TSG.Point;
                TSG.Point punt2 = Input[1].GetInput() as TSG.Point;
                TSG.Point punt3 = Input[2].GetInput() as TSG.Point;

                //SplitProfileStringBomen.SplitString(Data);

                var CreatedummyAndworkplane = new Creators.CreateDummyAndWorkplane();
                CreatedummyAndworkplane.Create(Model, punt1, punt2, punt3);
                
                CreateDummyTrede createDummyTrede = new CreateDummyTrede();
                createDummyTrede.Create();


                ClearStaticMembers.ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return true;
        }


        #endregion


        void SetMainPart(object MainPart)
        {
            try
            {
                ContourPlate part = _model.SelectModelObject(MainPart as Identifier) as ContourPlate;
                if (part != null) { ListMainPart.Add(part); }
            }
            catch { }
            try
            {
                Beam part = _model.SelectModelObject(MainPart as Identifier) as Beam;
                if (part != null) { ListMainPart.Add(part); }
            }
            catch { }
        }
    }
}
