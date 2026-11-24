using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public class WebBrowserEx : WebBrowser
    {
        public delegate void OnScriptError(string page, string row, string column, string error, string text);
        public event OnScriptError ScriptError;

        class WebBrowserSiteEx : WebBrowserSite, NativeMethods.IOleCommandTarget
        {
            WebBrowserEx BrowserEx;
            public WebBrowserSiteEx(WebBrowserEx browser) : base(browser)
            {
                BrowserEx = browser;
            }

            public int QueryStatus(IntPtr pguidCmdGroup, uint cCmds, NativeMethods.OLECMD[] prgCmds, ref NativeMethods.OLECMDTEXT CmdText)
            {
                if (pguidCmdGroup == IntPtr.Zero)
                    return NativeMethods.OLECMDERR_E_UNKNOWNGROUP;

                Guid guid = (Guid)Marshal.PtrToStructure(
                    pguidCmdGroup, typeof(Guid));

                if (guid != NativeMethods.CGID_DocHostCommandHandler)
                    return NativeMethods.OLECMDERR_E_UNKNOWNGROUP;

                for (int i = 0; i < cCmds; i++)
                {
                    if (prgCmds[i].cmdID ==
                        NativeMethods.OLECMDID_SHOWSCRIPTERROR)
                    {
                        prgCmds[i].cmdf = 3; // SUPPORTED | ENABLED
                    }
                    else
                    {
                        prgCmds[i].cmdf = 0;
                    }
                }

                return NativeMethods.S_OK;
            }

            public int Exec(IntPtr pguidCmdGroup, uint nCmdId, uint nCmdExecOpt, IntPtr pvaIn, IntPtr pvaOut)
            {
                if (pguidCmdGroup == IntPtr.Zero)
                    return NativeMethods.OLECMDERR_E_UNKNOWNGROUP;

                Guid guid = (Guid)Marshal.PtrToStructure(
                    pguidCmdGroup, typeof(Guid));

                if (guid != NativeMethods.CGID_DocHostCommandHandler)
                    return NativeMethods.OLECMDERR_E_UNKNOWNGROUP;

                if (nCmdId != NativeMethods.OLECMDID_SHOWSCRIPTERROR)
                {
                    return NativeMethods.OLECMDERR_E_NOTSUPPORTED;
                }

                if (pvaIn == IntPtr.Zero || pvaOut == IntPtr.Zero)
                    return NativeMethods.E_POINTER;

                object input = Marshal.GetObjectForNativeVariant(pvaIn);

                // The input variant contains the document.
                var doc = input as mshtml.IHTMLDocument2;

                if (doc != null)
                {
                    mshtml.IHTMLWindow2 window = doc.parentWindow;
                    mshtml.IHTMLEventObj evt = window.@event;

                    if (evt != null)
                    {
                        string[] error = new string[5];
                        string[] properties =
                        {
                            "errorLine",
                            "errorCharacter",
                            "errorCode",
                            "errorMessage",
                            "errorUrl"
                        };

                        for (int i = 0; i < properties.Length; i++)
                        {
                            object value = evt.GetType().InvokeMember(
                                properties[i],
                                System.Reflection.BindingFlags.GetProperty,
                                null,
                                evt,
                                null
                            );

                            if (value != null && !String.IsNullOrEmpty(value.ToString()))
                                error[i] = value.ToString();
                            else error[i] = "";

                            // Inspect value here if needed.
                        }
                        BrowserEx.ScriptError?.Invoke(error[4], error[0], error[1], error[2], error[3]);
                    }
                }

                // Tell MSHTML to continue executing scripts.
                Marshal.GetNativeVariantForObject(true, pvaOut);

                return NativeMethods.S_OK;
            }
        }

        protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
        {
            return new WebBrowserSiteEx(this);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

    static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct OLECMD
        {
            public uint cmdID;
            public uint cmdf;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OLECMDTEXT
        {
            public UInt32 cmdtextf;
            public UInt32 cwActual;
            public UInt32 cwBuf;
            public char rgwz;
        }

        public const int OLECMDERR_E_NOTSUPPORTED = unchecked((int)0x80040100);
        public const int E_POINTER = unchecked((int)0x80004003);
        public const int OLECMDERR_E_UNKNOWNGROUP = unchecked((int)0x80040104);
        public const int OLECMDID_SHOWSCRIPTERROR = 40;
        public static readonly Guid CGID_DocHostCommandHandler = new Guid("f38bc242-b950-11d1-8918-00c04fc2c836");
        public const int S_OK = 0;

        [ComImport, Guid("b722bccb-4e68-101b-a2bc-00aa00404770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleCommandTarget
        {
            [PreserveSig]
            int QueryStatus(
                IntPtr pguidCmdGroup,
                UInt32 cCmds,
                [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] OLECMD[] prgCmds,
                ref OLECMDTEXT CmdText);

            [PreserveSig]
            int Exec(
                IntPtr pguidCmdGroup,
                uint nCmdId,
                uint nCmdExecOpt,
                IntPtr pvaIn,
                IntPtr pvaOut);
        }
    }
}
