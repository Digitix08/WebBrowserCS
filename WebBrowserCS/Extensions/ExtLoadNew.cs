using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGExtensionCS;

namespace WebBrowserCS.Extensions
{
    class ExtLoadHost : IHostContext
    {
        public void OpenTab(string title, object content) { }

        public void CloseTab(object tab) { }

        public void Log(string message) { }

        public void AddToHistory(HistoryEntry entry) {
            //add history
        }

        public void RegisterCommand(ExtensionCommand command) { }
    }

    class ExtensionLoaderNew
    {
        ExtLoadHost TheHost;
        List<IExtension> extensions = new List<IExtension>();
        BrowserCS Host;
        string ExtFile = "AvailableExtensions.txt";
        string[] ExtDelimit = new string[] { "_;_" };
        public ExtensionLoaderNew(BrowserCS caller)
        {
            Host = caller;
        }

        public void LoadExtensions()
        {
            TheHost = new ExtLoadHost();
            if (File.Exists(ExtFile))
            {
                StreamReader extens = new StreamReader(ExtFile);
                string line = extens.ReadLine();
                while (line != null)
                {
                    if (line[0] != '#')
                    {
                        //we care about args[1]
                        //string Name = line.Substring(0, line.IndexOf("_;_"));
                        //to identify later
                        // (Host);
                        //

                        line = line.Substring(line.IndexOf("_;_") + 3, line.Length - line.IndexOf("_;_") - 3);
                        string[] args = line.Split(ExtDelimit, StringSplitOptions.None);
                        string path = Directory.GetCurrentDirectory() + "\\" + args[0];
                        Assembly DLL;

                        if (File.Exists(path)) { try
                            {
                                DLL = Assembly.LoadFile(path);
                                foreach (Type type in DLL.GetTypes())
                                {
                                    if (typeof(IExtension).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                                    {
                                        var ext = (IExtension)Activator.CreateInstance(type);
                                        extensions.Add(ext);

                                        //detect features
                                        if (ext is ITab)
                                        {
                                            Host.ProcessTab(ext.Name, ext.Id, args, true); //to be filled
                                        }

                                        if (ext is IWindow winExt)
                                        {
                                            Host.ProcessWindow(ext.Name, ext.Id, args);
                                        }

                                        if (!(ext is ITab) && !(ext is IWindow))
                                        {
                                            ext.Initialize(TheHost);
                                        }

                                        if (ext is ICommands cmdExt)
                                        {
                                            //foreach (var cmd in cmdExt.GetCommands()) ;
                                            //Host.RegisterCommand(cmd);
                                        }

                                        if (ext is ILoadable loadExt)
                                        {
                                            // this extension can load other extensions
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex is System.BadImageFormatException || ex is System.Reflection.TargetInvocationException) { MessageBox.Show("The build you are using is not compatible with this extension." + Environment.NewLine + "Details:" + Environment.NewLine + path, "Invalid extension runtime version. 32 bit extensions need the anycpu build!"); }
                                else MessageBox.Show("Undocumented exception has happened:" + Environment.NewLine + ex.GetType().FullName + ":" + Environment.NewLine + "\"" + ex.Message + "\"" + Environment.NewLine + "Please report this exception to the WBCS team");
                            }
                        }
                        else { MessageBox.Show("The file " + path + " does not exist"); }
                    }
                    line = extens.ReadLine();
                }
                extens.Close();
            }
        }
        public void LaunchExtension(string tag, bool isTab, string vars = "")
        {
            if (extensions.Any(x => x.Id == tag.ToString()))
            {
                IExtension TheExt = extensions.FirstOrDefault(x => x.Id == tag.ToString());
                if (TheExt is ITab && isTab)
                {
                    try
                    {
                        TheExt.Initialize(TheHost);
                        ((ITab)TheExt).OnTabOpened();
                        if (!(TheExt is IExtensionContent)) MessageBox.Show("extension doen not declare content!");
                        IExtensionContent handle = (IExtensionContent)TheExt;
                        IntPtr hwnd = handle.CreateContentHandle();
                        Host.LoadTab(hwnd, TheExt);
                    }
                    catch (Exception ex)
                    {
                        if (ex is System.BadImageFormatException || ex is System.Reflection.TargetInvocationException) { MessageBox.Show("The build you are using is not compatible with this extension." + Environment.NewLine + "Details:" + Environment.NewLine + "IGExtension v2" + Environment.NewLine + "Name: " + TheExt.Name, "Invalid extension runtime version. 32 bit extensions need the anycpu build!"); }
                        if (ex is System.ArgumentNullException) { MessageBox.Show("The extension you are using does not support tabs." + Environment.NewLine + "Details:" + Environment.NewLine + "IGExtension v2" + Environment.NewLine + "Name: " + TheExt.Name, "Invalid extension call mode"); }
                    }
                }
                else if (!(TheExt is ITab) && isTab)
                {
                    MessageBox.Show("The extension you are using does not support tabs." + Environment.NewLine + "Details: " + Environment.NewLine + "IGExtension v2" + Environment.NewLine + "Name: " + TheExt.Name, "Invalid extension call mode");
                }
                else if(TheExt is IWindow)
                {
                    try
                    {
                        TheExt.Initialize(TheHost);
                        ((IWindow)TheExt).OnWindowOpened();
                    }
                    catch (Exception ex)
                    {
                        if (ex is System.BadImageFormatException || ex is System.Reflection.TargetInvocationException) { MessageBox.Show("The build you are using is not compatible with this extension." + Environment.NewLine + "Details:" + Environment.NewLine + "IGExtension v2" + Environment.NewLine + "Name: " + TheExt.Name, "Invalid extension runtime version. 32 bit extensions need the anycpu build!"); }
                        if (ex is System.ArgumentNullException) { MessageBox.Show("The extension you are using does not support tabs." + Environment.NewLine + "Details:" + Environment.NewLine + "IGExtension v2" + Environment.NewLine + "Name: " + TheExt.Name, "Invalid extension call mode"); }
                    }
                }
            }
        }
    }
}
