using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Level2
{
    internal static class Utils
    {
        internal static RibbonPanel CreateRibbonPanel(UIControlledApplication app, string tabName, string panelName)
        {
            RibbonPanel currentPanel = GetRibbonPanelByName(app, tabName, panelName);

            if (currentPanel == null)
                currentPanel = app.CreateRibbonPanel(tabName, panelName);

            return currentPanel;
        }

        internal static RibbonPanel GetRibbonPanelByName(UIControlledApplication app, string tabName, string panelName)
        {
            foreach (RibbonPanel tmpPanel in app.GetRibbonPanels(tabName))
            {
                if (tmpPanel.Name == panelName)
                    return tmpPanel;
            }

            return null;
        }

        public static List<string> GetLevelsNames(string pathFile, int index)
        {
            List<string> list = new List<string>();

            using (StreamReader sReader = new StreamReader(pathFile))
            {
                string line;

                while ((line = sReader.ReadLine()) != null)
                {
                    list.Add(line.Split(',')[index].ToString());
                }
            }

            list.RemoveAt(0);
            return list;
        }

        public static List<double> GetLevelsHeight(string pathFile, int index)
        {
            List<double> list = new List<double>();

            using (StreamReader sReader = new StreamReader(pathFile))
            {
                string line;

                while ((line = sReader.ReadLine()) != null)
                {
                    bool IsDouble = double.TryParse(line.Split(',')[index].ToString(), out double result);
                    if (IsDouble == true)
                    {
                        list.Add(result);
                    }
                }
            }

            return list;
        }

        public static void SetCeilingPlansByUnits(Document doc, List<double> levelHeights, List<string> levelNames, string unit)
        {
            ViewFamilyType viewFamilyType = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>().First(x => x.ViewFamily == ViewFamily.CeilingPlan);

            int counter = 0;

            using (Transaction trans = new Transaction(doc))
            {
                trans.Start("Create CeilingPlans");
                foreach (double height in levelHeights)
                {
                    double heightMetric = UnitUtils.ConvertToInternalUnits(height, UnitTypeId.Meters);
                    Level newLevel = null;

                    if (unit == "metric")
                    {
                        newLevel = Level.Create(doc, heightMetric);

                        try
                        {
                            newLevel.Name = levelNames[counter];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    else if (unit == "imperial")
                    {
                        newLevel = Level.Create(doc, height);

                        try
                        {
                            newLevel.Name = levelNames[counter];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    ViewPlan viewPlan = ViewPlan.Create(doc, viewFamilyType.Id, newLevel.Id);
                    viewPlan.Name = levelNames[counter];

                    counter++;
                }

                trans.Commit();
            }
        }

        public static void SetFloorPlansByUnits(Document doc, List<double> levelHeights, List<string> levelNames, string unit)
        {
            ViewFamilyType viewFamilyType = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>().First(x => x.ViewFamily == ViewFamily.FloorPlan);

            int counter = 0;

            using (Transaction trans = new Transaction(doc))
            {
                trans.Start("Create FloorPlans");
                foreach (double height in levelHeights)
                {
                    double heightMetric = UnitUtils.ConvertToInternalUnits(height, UnitTypeId.Meters);
                    Level newLevel = null;

                    if (unit == "metric")
                    {
                        newLevel = Level.Create(doc, heightMetric);

                        try
                        {
                            newLevel.Name = levelNames[counter];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    else if (unit == "imperial")
                    {
                        newLevel = Level.Create(doc, height);

                        try
                        {
                            newLevel.Name = levelNames[counter];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    ViewPlan viewPlan = ViewPlan.Create(doc, viewFamilyType.Id, newLevel.Id);
                    viewPlan.Name = levelNames[counter];

                    counter++;
                }

                trans.Commit();
            }
        }

        public static void SetFloorAndCeilingPlansByUnits(Document doc, List<double> levelHeights, List<string> levelNames, string unit)
        {
            ViewFamilyType viewFamilyTypeCeiling = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>().First(x => x.ViewFamily == ViewFamily.CeilingPlan);

            ViewFamilyType viewFamilyTypeFloor = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>().First(x => x.ViewFamily == ViewFamily.FloorPlan);

            int counter = 0;

            using (Transaction trans = new Transaction(doc))
            {
                trans.Start("Create Floor and CeilingPlans");
                foreach (double height in levelHeights)
                {
                    double heightMetric = UnitUtils.ConvertToInternalUnits(height, UnitTypeId.Meters);
                    Level newLevel = null;

                    if (unit == "metric")
                    {
                        newLevel = Level.Create(doc, heightMetric);

                        try
                        {
                            newLevel.Name = levelNames[counter];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    else if (unit == "imperial")
                    {
                        newLevel = Level.Create(doc, height);

                        try
                        {
                            newLevel.Name = levelNames[counter];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    ViewPlan viewPlan = ViewPlan.Create(doc, viewFamilyTypeFloor.Id, newLevel.Id);
                    ViewPlan viewPlan2 = ViewPlan.Create(doc, viewFamilyTypeCeiling.Id, newLevel.Id);
                    viewPlan.Name = levelNames[counter];

                    counter++;
                }

                trans.Commit();
            }
        }
    }
}
