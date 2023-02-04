#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Shapes;

#endregion

namespace RAA_Level2
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // put any code needed for the form here

            // open form
            MyForm currentForm = new MyForm()
            {
                Height = 300,
                Width = 400
            };

            currentForm.ShowDialog();

            if (currentForm.DialogResult == false)
            {
                return Result.Cancelled;
            }

            try
            {
            // get form data and do something
            string pathFile = currentForm.GetFileName();

            List<string> levelNames = Utils.GetLevelsNames(pathFile, 0);
            List<double> levelHeightsImperial = Utils.GetLevelsHeight(pathFile, 1);
            List<double> levelHeightsMetric = Utils.GetLevelsHeight(pathFile, 2);

                //Metric Floor Plans and Ceiling Plans
                if (currentForm.GetRadioButtonGroup() == false && currentForm.GetCheckBox1() == true && currentForm.GetCheckBox2() == true)
                {
                    Utils.SetFloorAndCeilingPlansByUnits(doc, levelHeightsMetric, levelNames, "metric");
                }
                //Metric Floor Plans
                else if (currentForm.GetRadioButtonGroup() == false && currentForm.GetCheckBox1() == true)
                {
                    Utils.SetFloorPlansByUnits(doc, levelHeightsMetric, levelNames, "metric");
                }

                //Metric Ceiling Plans
                else if (currentForm.GetRadioButtonGroup() == false && currentForm.GetCheckBox2() == true)
                {
                    Utils.SetCeilingPlansByUnits(doc, levelHeightsMetric, levelNames, "metric");
                }

                //Imperial Floor Plans and Ceiling Plans
                else if (currentForm.GetRadioButtonGroup() == true && currentForm.GetCheckBox1() == true && currentForm.GetCheckBox2() == true)
                {
                    Utils.SetFloorAndCeilingPlansByUnits(doc, levelHeightsImperial, levelNames, "imperial");
                }
                //Imperial Floor Plans
                if (currentForm.GetRadioButtonGroup() == true && currentForm.GetCheckBox1() == true)
                {
                    Utils.SetFloorPlansByUnits(doc, levelHeightsImperial, levelNames, "imperial");
                }
                //Imperial Ceiling Plans
                else if (currentForm.GetRadioButtonGroup() == true && currentForm.GetCheckBox2() == true)
                {
                    Utils.SetCeilingPlansByUnits(doc, levelHeightsImperial, levelNames, "imperial");
                }
            }
            catch (Exception)
            {
                TaskDialog.Show("Error","You must select a CSV File");
            }






            return Result.Succeeded;
        }


        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }

    }
}


