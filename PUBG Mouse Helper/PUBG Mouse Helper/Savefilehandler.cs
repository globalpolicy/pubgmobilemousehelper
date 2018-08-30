using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace PUBG_Mouse_Helper
{
    public static class Savefilehandler
    {
        private const string saveFileName = "PUBGMousePresets.ini";

        public static List<String> GetSavedPresetNamesList()
        {
            List<String> retval = new List<string>();

            if (File.Exists(saveFileName))
            {
                try
                {
                    int lineNum = 0;
                    foreach (var line in File.ReadLines(saveFileName))
                    {
                        lineNum++;
                        if (lineNum % 2 == 1)
                        {
                            //line contains the name of the preset below it
                            retval.Add(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }

            }

            return retval;
        }

        public static List<int> GetPresetValuesFromName(String presetName)
        {
            List<int> retval = new List<int>();

            if (File.Exists(saveFileName))
            {
                try
                {
                    int lineNum = 0;
                    bool foundPreset = false;
                    foreach (var line in File.ReadLines(saveFileName))
                    {
                        lineNum++;
                        if (lineNum % 2 == 1)
                        {
                            if (line.Equals(presetName))
                            {
                                foundPreset = true;
                                continue;
                            }
                        }
                        if (foundPreset)
                        {
                            string[] tokens = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            if (tokens.Length == 5)
                            {
                                retval.Add(Int32.Parse(tokens[0]));
                                retval.Add(Int32.Parse(tokens[1]));
                                retval.Add(Int32.Parse(tokens[2]));
                                retval.Add(Int32.Parse(tokens[3]));

                                if (tokens.Length > 4)
                                    KeyboardHelperClass.inactiveShootKey = Convert.ToString(tokens[4]).ElementAt(0).ToString();
                                else
                                    KeyboardHelperClass.inactiveShootKey = "]";
                            }
                            else
                            {
                                throw new Exception("Wrong number of preset parameters!");
                            }
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }

            }

            return retval;
        }

        public static bool SavePresets(string presetName, int dx, int dy, int waitms, int delayms, char shootKey)
        {
            bool retval = false;

            string oldContent = "";
            if (File.Exists(saveFileName))
            {
                oldContent = File.ReadAllText(saveFileName);
                File.Delete(saveFileName);
            }
            string newContent = oldContent + presetName + "\r\n" + dx.ToString() + ";" + dy.ToString() + ";" + waitms.ToString() + ";" + delayms.ToString() + ";" + shootKey.ToString() + "\r\n";
            File.WriteAllText(saveFileName, newContent);
            retval = true;

            return retval;
        }

        public static bool DeletePreset(string presetName)
        {
            bool retval = false;

            if (File.Exists(saveFileName))
            {
                if (GetSavedPresetNamesList().Contains(presetName))
                {
                    bool foundPreset = false;
                    string newContent = "";
                    foreach (var line in File.ReadAllLines(saveFileName))
                    {
                        if (line.Equals(presetName))
                        {
                            foundPreset = true;
                            continue;
                        }
                        if (foundPreset)
                        {
                            foundPreset = false;
                            continue;
                        }
                        newContent += line + "\r\n";
                    }
                    File.WriteAllText(saveFileName, newContent);
                    retval = true;
                }
                else
                {
                    throw new Exception($"Preset {presetName} not found");
                }

                
            }

            return retval;
        }

    }
}
