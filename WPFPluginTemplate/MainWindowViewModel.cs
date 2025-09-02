using System.ComponentModel;
using TD = Tekla.Structures.Datatype;
using Tekla.Structures.Dialog;


namespace Trap2_0
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        private int conservering;
        private string profiel = string.Empty;
        private string material = string.Empty;

        private double offsetlinks;
        private double offsetrechts;




        #endregion


        #region Properties

        [StructuresDialog("conservering", typeof(TD.Integer))]
        public int Conservering
        {
            get { return conservering; }
            set { conservering = value; OnPropertyChanged("Conservering"); }
        }
        [StructuresDialog("profiel", typeof(TD.String))]
        public string Profiel
        {
            get { return profiel; }
            set { profiel = value; OnPropertyChanged("Profiel"); }
        }
        [StructuresDialog("material", typeof(TD.String))]
        public string Material
        {
            get { return material; }
            set { material = value; OnPropertyChanged("Material"); }
        }
        [StructuresDialog("offsetlinks", typeof(TD.Double))]
        public double OffsetLinks
        {
            get { return offsetlinks; }
            set { offsetlinks = value; OnPropertyChanged("OffsetLinks"); }
        }
        [StructuresDialog("offsetrechts", typeof(TD.Double))]
        public double OffsetRechts
        {
            get { return offsetrechts; }
            set { offsetrechts = value; OnPropertyChanged("OffsetRechts"); }
        }









        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
