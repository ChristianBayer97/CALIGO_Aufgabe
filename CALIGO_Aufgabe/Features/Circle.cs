using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALIGO_Aufgabe.Features
{
    internal class Circle : FeatureBase
    {
        public override void CreateParameterList()
        {
            // Create necessary Parameters
            this.Parameters.Add(new Parameter("Type", 0, 0, "Type of Feature", Type.CIR, readOnly:true));
            this.Parameters.Add(new Parameter("Name", -1, 1, "Name of Feature", "Circle"));
            this.Parameters.Add(new Parameter("X", 1, 2, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("Y", 2, 3, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("Z", 3, 4, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("i", 4, 5, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("j", 5, 6, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("k", 6, 7, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("Var1", 7, 9, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("Orient", -1, 14, "", "Inner"));
            this.Parameters.Add(new Parameter("TOLname", -1, 15, ""));
            this.Parameters.Add(new Parameter("Layer", -1, 16, ""));
            this.Parameters.Add(new Parameter("Thick", -1, 17, "", 0.0, targetType: typeof(float)));
            this.Parameters.Add(new Parameter("ZGS", -1, 18, ""));
            this.Parameters.Add(new Parameter("TXTname", -1, 20, ""));
            this.Parameters.Add(new Parameter("MSTname", -1, 21, ""));
            this.Parameters.Add(new Parameter("Flange Radius", -1, 22, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("Flange Height", -1, 23, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("i2 Height", -1, 24, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("j2 Height", -1, 25, "", targetType: typeof(float)));
            this.Parameters.Add(new Parameter("k2 Height", -1, 26, "", targetType: typeof(float)));

            // Fill remaining Parameters
            base.FillParameterList();
        }

        public Circle()
        {
            this.SourceColumns = new List<string>();
            this.Parameters = new ObservableCollection<Parameter>();

            CreateParameterList();
        }

        public override object? ManipulateParameter(int column, string originalValue)
        {
            // return Circle Type for Column 1
            // convert Radius to diameter
            switch (column)
            {
                case 0:
                    return Type.CIR;
                case 7:
                    return float.Parse(originalValue) * 2f;
                default:
                    return originalValue;
            }
        }
    }
}
