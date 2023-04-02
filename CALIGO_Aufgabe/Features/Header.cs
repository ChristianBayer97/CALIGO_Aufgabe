using CALIGO_Aufgabe.Features;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALIGO_Aufgabe.Features
{
    /**
     * Header Class inherits from Feature Base
     * dispite not being a feature this will allow the user to change Metadata Values for the output Header
     * 
     */
    internal class Header : FeatureBase
    {
        // expected Time Format
        const string DateFormat = "dd.MM.yyyy HH:mm:ss";

        public Header() 
        {

            this.SourceColumns = new List<string>();
            this.Parameters = new ObservableCollection<Parameter>();

            CreateParameterList();
        }

        public override string GetOutputLine()
        {
            // Convert Parameters to 10 line Header
            return
                $"MAP: {Parameters[1].Value}\r\n" +
                $"MODEL: {Parameters[2].Value}\r\n" +
                $"USER: {Parameters[3].Value} NAME: {Parameters[4].Value} DATUM: {Parameters[5].Value}\r\n" +
                $"SRN: {Parameters[6].Value} DZNR: {Parameters[7].Value}\r\n" +
                "-\r\n" + "-\r\n" + "-\r\n" + "-\r\n" + "-\r\n" + "-\r\n";
        }

        public override void CreateParameterList()
        {
            // Necessary Attributes to fill File Header
            this.Parameters.Add(new Parameter("Type", -1, 0, "", Type.NONE, readOnly: true));
            this.Parameters.Add(new Parameter("Map", -1, 0, "", "Unknown"));
            this.Parameters.Add(new Parameter("Model", -1, 1, ""));
            this.Parameters.Add(new Parameter("User", -1, 2, "", "Test"));
            this.Parameters.Add(new Parameter("Name", -1, 3, "", "Caligo"));
            this.Parameters.Add(new Parameter("Date", -1, 4, "",defaultValue: DateTime.Now.ToString(DateFormat)));
            this.Parameters.Add(new Parameter("SRN", -1, 5, ""));
            this.Parameters.Add(new Parameter("DZNR", -1, 6, ""));


            base.FillParameterList();
        }
    }
}
