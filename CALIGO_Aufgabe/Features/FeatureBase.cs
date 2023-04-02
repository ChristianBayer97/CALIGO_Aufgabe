using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace CALIGO_Aufgabe.Features
{
    // supported Types of Features
    public enum Type
    {
        NONE,
        PT,
        BPT,
        LN,
        CIR,
        SLT,
        HEX,
        ELL,
        UDF,
        PLN,
        CYL,
        SPH,
        CON,
        ANG,
        DIST,
        WIN,
        SEC,
        TXT,
        VER,
        OPR,
        MST,
        SET,
        END,
        TOL,
        TG,
        RSY,
        ALG,
        RFT,
        RPT,
        LTT
    }

    /**
     * Parameter Class
     * 
     * hold informatiuon about, and the value of a single Parameter
     * Parameters can be set as readonly to forbit editing of unused parameters (eg. Attr1 for Planes)
     * 
     */
    public class Parameter: INotifyPropertyChanged
    {
        // Name of Parameter
        public string Name {get; set;}

        // index of related column in input file
        public int SourceColumnIndex { get; set; }

        // index of Column in output file
        public int TargetColumnIndex { get; set; }

        // Descripitve text for user guidance
        public string Description { get; set; }

        // value read from source file or new value given by user
        public object? Value { get; set; }

        // true for parametrs that are used to create output data
        public bool enabled = false;
        
        // determaine if user can change value
        public bool readOnly { get; set; }

        // type of Value
        public System.Type TargetType { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        public Parameter(string name, int sourceColumnIndex, int targetColumnIndex, string description, object? defaultValue = null, bool readOnly = false, System.Type targetType = null)
        {
            this.Name = name;
            this.SourceColumnIndex = sourceColumnIndex;
            this.TargetColumnIndex = targetColumnIndex;
            this.Description = description;
            this.Value = defaultValue;
            this.enabled = true;
            this.readOnly = readOnly;
            TargetType = targetType==null?typeof(string):targetType;
        }

        public Parameter(int targetColumnIndex)
        {
            this.Name = "";
            this.SourceColumnIndex = -1;
            this.TargetColumnIndex = targetColumnIndex;
            this.Description = "";
            this.Value = null;
            readOnly = true;
            TargetType = typeof(string);
        }

        public override string ToString()
        {
            return this.Name + ": " + this.Value?.ToString();
        }
    }

    /**
     * Base Class for Features
     * 
     * implements base functionalities
     * some adjustments have to be made in child classes (eg. value manipulation/calculation on Parameter level)
     */
    public abstract class FeatureBase : ViewBase
    {
        // stores source Data
        public List<string> SourceColumns { get; set; }

        // all Parameters wether used or not
        public ObservableCollection<Parameter> Parameters { get; set; }

        // index of Feature in source file
        public int OriginalIndex = 0;

        // return Parameter from Parametr List by Name
        public virtual Parameter getParameter(string name)
        {
            return Parameters.Where(p => p.Name == name).First();
        }

        // Base Constructor
        public FeatureBase()
        {
            // create empty Lists
            this.SourceColumns = new List<string>();
            this.Parameters = new ObservableCollection<Parameter>();

            // fill all unused Spaces with disabled Parameters
            FillParameterList();
        }

        // add relevant Parameters for Feature
        public abstract void CreateParameterList();

        // Parameter-Level manipulation/calculation
        public virtual object? ManipulateParameter(int column, string originalValue)
        {
            return originalValue;
        }

        // fill List of Parameters with disabled Entries
        public void FillParameterList()
        {
            // Sort Parameters by output column index
            var tl = this.Parameters.ToList();
            tl.Sort((p1, p2) => p1.TargetColumnIndex.CompareTo(p2.TargetColumnIndex));

            this.Parameters = new ObservableCollection<Parameter>(tl);
            
            //maximum Parameters as defined by Specification
            int maxParameters = 27;

            // iterate over List
            // add disabled Parameter if target index is not already present
            for(int i = 0; i < maxParameters; i++)
            {
                if (this.Parameters.Count == i || this.Parameters[i].TargetColumnIndex > i)
                {
                    this.Parameters.Insert(i,new Parameter(i));
                }
            }
        }

        // convert csv line to Parameter
        public FeatureBase LoadFromCSV(List<string> columns)
        {
            // copy base Values
            this.SourceColumns = columns;

            //for all Parameters -> store converted Value in Parameter
            foreach(Parameter p in Parameters) 
            {
                if (p.SourceColumnIndex < 0)
                    continue;

                p.Value = ManipulateParameter(p.SourceColumnIndex, columns[p.SourceColumnIndex]);
            }

            return this;
        }

        // Convert Parameter List to CSV Line of Specification
        virtual public string GetOutputLine()
        {
            string result = "";
            for(int i = 0; i < Parameters.Count; i++)
            {
                // use shorter handle
                var val = Parameters[i].Value;

                // null -> ","
                // float -> convert to float and format string with 2 decimals
                // rest -> use as is
                // if last index append new line instead of comma
                result += (val != null?(Parameters[i].TargetType == typeof(float) ? float.Parse(val.ToString()).ToString("0.00") : val) :"") + (i == Parameters.Count-1?"\n":",");
            }

            return result;
        }
    }
}
