using Tekla.Structures.Internal;
using Tekla.Structures.Plugins;

namespace Trap2_0.DataModels
{
    public class StructuresData
    {
        [StructuresField(nameof(conservering))]
        public int conservering;
        [StructuresField(nameof(profiel))]
        public string profiel;
        [StructuresField(nameof(material))]
        public string material;

        [StructuresField(nameof(offsetlinks))]
        public double offsetlinks;
        [StructuresField(nameof(offsetrechts))]
        public double offsetrechts;






        public void ValidateData()
        {
            if (IsDefaultValue(conservering)) conservering = 0;
            if (IsDefaultValue(profiel)) profiel = "";
            if (IsDefaultValue(material)) material = "";

            if (IsDefaultValue(offsetlinks)) offsetlinks = 0;
            if (IsDefaultValue(offsetrechts)) offsetrechts = 0;











        }

        public bool IsDefaultValue(int Value) => Value == StructuresDataStorage.DEFAULT_VALUE;

        public bool IsDefaultValue(double Value) => Value == (double)StructuresDataStorage.DEFAULT_VALUE;

        public bool IsDefaultValue(string Value) => Value == "";

    }    
}
