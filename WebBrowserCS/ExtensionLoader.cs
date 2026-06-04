using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    class ExtensionLoader
    {
        BrowserCS Host;
        string ExtFile = "AvailableExtensions.txt";
        string[] ExtDelimit = new string[] { "_;_" };
        public ExtensionLoader(BrowserCS caller)
        {
            Host = caller;
        }

        public void LoadExtensions()
        {
            if (File.Exists(ExtFile))
            {
                StreamReader extens = new StreamReader(ExtFile);
                string line = extens.ReadLine();
                while (line != null)
                {
                    if (line[0] != '#')
                    {
                        string Name = line.Substring(0, line.IndexOf("_;_"));
                        bool isTab = false;

                        line = line.Substring(line.IndexOf("_;_") + 3, line.Length - line.IndexOf("_;_") - 3);
                        string[] args = line.Split(ExtDelimit, StringSplitOptions.None);
                        if (args.Length >= 3) { if (args[2] == "isTab") { isTab = true; line = line.Substring(0, line.IndexOf("_;_isTab")); } }
                        string Tag = line;

                        string path = Directory.GetCurrentDirectory() + "\\" + args[0];

                        if (File.Exists(path))
                        {
                            if (isTab)
                            {
                                Host.ProcessTab(Name, Tag, args);
                            }
                            else
                            {
                                Host.ProcessWindow(Name, Tag, args);
                            }
                        }
                        else MessageBox.Show("The file " + path + " does not exist");
                    }
                    line = extens.ReadLine();
                }
                extens.Close();
            }
        }

        public void LaunchExtension(string tag, string name, bool isTab, string vars = "")
        {
            string[] args = tag.Split(ExtDelimit, StringSplitOptions.None);
            string loc = Directory.GetCurrentDirectory() + "\\" + args[0];
            string exe = args[1];

            Assembly DLL; Type theType; var c = new object(); MethodInfo method;
            if (File.Exists(loc))
            {
                if (isTab) try
                    {
                        DLL = Assembly.LoadFile(loc);
                        theType = DLL.GetType(exe + ".IGTab");
                        c = Activator.CreateInstance(theType);
                        method = theType.GetMethod("init");
                        var tabRaw = method.Invoke(c, new object[] { @vars });
                        if (tabRaw is UserControl)
                        {
                            UserControl tab = (UserControl)tabRaw;
                            Host.LoadTab(tab, name);
                        }
                        else MessageBox.Show("The call did not return the adequate contents for a tab", "Invalid value returned");
                    }
                    catch (Exception ex)
                    {
                        if (ex is System.BadImageFormatException || ex is System.Reflection.TargetInvocationException) { MessageBox.Show("The build you are using is not compatible with this extension." + Environment.NewLine + "Details:" + Environment.NewLine + loc + Environment.NewLine + "Name: " + name, "Invalid extension runtime version"); }
                        if (ex is System.ArgumentNullException) { MessageBox.Show("The extension you are using does not support tabs." + Environment.NewLine + "Details:" + Environment.NewLine + loc + Environment.NewLine + "Name: " + name, "Invalid extension call mode"); }
                    }
                else try
                    {
                        DLL = Assembly.LoadFile(loc);
                        theType = DLL.GetType(exe + ".IGExtension");
                        c = Activator.CreateInstance(theType);
                        method = theType.GetMethod("init");
                        method.Invoke(c, new object[] { @vars });
                    }
                    catch (Exception ex)
                    {
                        if (ex is System.BadImageFormatException || ex is System.Reflection.TargetInvocationException) { MessageBox.Show("The build you are using is not compatible with this extension." + Environment.NewLine + "Details:" + Environment.NewLine + loc + Environment.NewLine + "Name: " + name, "Invalid extension runtime version"); }
                    }
            }
            else MessageBox.Show("The file " + loc + "does not exist");
        }
    }
}