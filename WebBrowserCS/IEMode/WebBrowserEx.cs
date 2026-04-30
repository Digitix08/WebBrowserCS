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
        class WebBrowserSiteEx : WebBrowserSite, NativeMethods.IOleCommandTarget
        {
            public WebBrowserSiteEx(WebBrowser browser) : base(browser)
            {
            }

            public int QueryStatus(IntPtr pguidCmdGroup, uint cCmds, NativeMethods.OLECMD[] prgCmds, ref NativeMethods.OLECMDTEXT CmdText)
            {
                return NativeMethods.OLECMDERR_E_UNKNOWNGROUP;
            }

            public int Exec(IntPtr pguidCmdGroup, uint nCmdId, uint nCmdExecOpt, IntPtr pvaIn, IntPtr pvaOut)
            {
                if (pguidCmdGroup != IntPtr.Zero)
                {
                    Guid guid = (Guid)Marshal.PtrToStructure(pguidCmdGroup, typeof(Guid));
                    if (guid == NativeMethods.CGID_DocHostCommandHandler)
                    {
                        if (nCmdId == NativeMethods.OLECMDID_SHOWSCRIPTERROR)
                        {
                            // for dom: dynamic document = Marshal.GetObjectForNativeVariant(nCmdId);

                            // continue running scripts
                            if (pvaOut != IntPtr.Zero)
                                Marshal.GetNativeVariantForObject(true, pvaOut);

                            //uncomment the line below to disable script error popup
                            //return NativeMethods.S_OK;
                        }
                    }
                }
                return NativeMethods.OLECMDERR_E_UNKNOWNGROUP;
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

        public const int OLECMDERR_E_UNKNOWNGROUP = unchecked((int)0x80040102);
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
