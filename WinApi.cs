using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace KoWordSearch
{
    /// WinApi.cs :: Win32 API Library Routines
    /// 06/28/13 16.0.8.1  Added to ATMS 16
    /// 03/29/12 11.0.12.0/6.1.0  
    /// 09/13/11 11.0.4.8/5.77.10.3  Updated
    /// 07/21/11 11.0.1.2/5.75.18.2  Windows WM_* Message constants added
    /// 07/08/11 11.0.0.0/5.75.16.7
    /// 01/09/07 5.14.10  Error Bypases for window detection and activation
    /// 05/24/05 4.14.0  ATMS_Lib Implementation

    public class WinApi
    {
        public const string THIS_NAME = "WinApi";

        //// Windows Message Constants
        public const int WM_KEYDOWN = 0x100;

        // Adobe's Application Message = 0x1450 (5200L)
        public const int WM_ACROBAT_1450 = 0x1450;


        [DllImport("user32.dll", EntryPoint = "LockWindowUpdate", CallingConvention = CallingConvention.StdCall)] // EntryPoint:="LockWindowUpdate", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> 
        public static extern int LockWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWind);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWind, int nCmdShow);

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWind);


        public static void SetTopProcess(string sProcessName)
        {
            try
            {
                //Debug_ListProcesses();
                Process proc1 = Process.GetProcessesByName(sProcessName)[0];
                IntPtr hWnd = proc1.MainWindowHandle;

                if (IsIconic((IntPtr)hWnd))
                {
                    ShowWindowAsync(hWnd, SW_RESTORE);
                }
                SetForegroundWindow(hWnd);
            }
            catch (Exception ex)
            {
                //Log.Err(ex, THIS_NAME, "CWinAPI32.SetTopProcess", Log.LogDevices.CON_LOG_DLG_EMAIL_STATUS);
            }
        }


        public static void Debug_ListProcesses()
        {
            Process[] procDebug = Process.GetProcesses();
            for (int ii = 0; ii < procDebug.Length; ii++)
            {
                //Log.Info(THIS_NAME, "Process[" + ii.ToString() + "]: "
                //            + procDebug[ii].Id.ToString()
                //            + " - " + procDebug[ii].ProcessName, Log.LogDevices.LOG);
            }
        }


        public delegate bool ApiCallBack(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32")]
        public static extern int EnumWindows(ApiCallBack x, int y);

        [DllImport("user32")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int cch);

        [DllImport("user32")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32")]
        public static extern int GetDesktopWindow();

        [DllImport("user32")]
        public static extern int GetWindowRect(IntPtr hWnd, out System.Drawing.Rectangle lpRect);

        [DllImport("user32")]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, int bRepaint);

        [DllImport("user32")]
        public static extern int EnumChildWindows(IntPtr hWndParent, ApiCallBack lpEnumFunc, IntPtr lParam);

        [DllImport("user32")]
        public static extern int EnumThreadWindows(IntPtr dwThreadId, ApiCallBack lpfn, IntPtr lParam);

        [DllImport("user32")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);


        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern string MAKEINTATOM(int wInteger);


        [DllImport("user32.dll")]
        public static extern int GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int SetFocusAPI(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,     // handle to destination window 
                                             uint Msg,     // message 
                                             IntPtr wParam,   // first message parameter 
                                             IntPtr lParam);  // second message parameter 

        [DllImport("user32.dll")]
        public static extern int SetActiveWindow(IntPtr hWnd);

        public static IntPtr ActivateWindow(string sClassText, string sWinText)
        {
            IntPtr hWnd = (IntPtr)0;
            try
            {
                // Determine the handle to the Application window.       
                hWnd = FindWindow(sClassText, sWinText);
                if (((int)hWnd > 0) && SetForegroundWindow(hWnd))
                {
                    int iOk = SetActiveWindow(hWnd);
                    iOk = SetFocusAPI(hWnd);
                }
            }
            catch
            {
                // Ignore
            }
            return hWnd;
        }


        //struct _RECT 
        //{
        //  Int32 left;
        //  Int32  top;
        //  Int32  right;
        //  Int32  bottom;
        //} 

        public static void ActivateWindowAtom(string sClassText, string sWinText, int iAtom)
        {
            try
            {
                // Determine the handle to the Application window. 
                IntPtr hWnd = FindWindowEx((IntPtr)0, (IntPtr)0, MAKEINTATOM(iAtom), "");
                bool bOk = SetForegroundWindow(hWnd);
                //int iOk = SetActiveWindow(hWnd);
                //iOk = SetFocusAPI(hWnd);
            }
            catch
            {
                // This is a non-critical error; trap, but ignore
            }
        }


        /// <summary>
        /// Win32 FormatMessage function.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, ref IntPtr Arguments);


        /// <summary>
        /// Formats and returns a .NET string containing the Windows API level error message corresponding to the specified error code.
        /// </summary>
        /// <param name="errorCode">An <see cref="Int32"/> value corresponding to the specified error code.</param>
        /// <returns>A formatted error message corresponding to the specified error code.</returns>
        public static string GetErrorMessage(int errorCode)
        {
            const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
            const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
            const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;

            int dwFlags = FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;
            int messageSize = 255;
            string lpMsgBuf = "";
            IntPtr ptrlpSource = IntPtr.Zero;
            IntPtr prtArguments = IntPtr.Zero;

            if (FormatMessage(dwFlags, ref ptrlpSource, errorCode, 0, ref lpMsgBuf, messageSize, ref prtArguments) == 0)
                throw new InvalidOperationException("Failed to format message for error code " + errorCode);

            return lpMsgBuf;
        }


        // The purpose of this function is to retrieve a device context;
        // essentially a drawing area for the application.
        [DllImport("User32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        // Use this function to release the device context.
        [DllImport("User32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 ReleaseDC(IntPtr hWnd, IntPtr hDC);

        // Device Parameters for GetDeviceCaps() 
        public enum DevCapParm
        {
            // General device capabilities.
            DRIVERVERSION = 0,     // Device driver version                    
            TECHNOLOGY = 2,     // Device classification                    
            HORZSIZE = 4,     // Horizontal size in millimeters           
            VERTSIZE = 6,     // Vertical size in millimeters             
            HORZRES = 8,     // Horizontal width in pixels               
            VERTRES = 10,    // Vertical height in pixels                
            BITSPIXEL = 12,    // Number of bits per pixel                 
            PLANES = 14,    // Number of planes                         
            NUMBRUSHES = 16,    // Number of brushes the device has         
            NUMPENS = 18,    // Number of pens the device has            
            NUMMARKERS = 20,    // Number of markers the device has         
            NUMFONTS = 22,    // Number of fonts the device has           
            NUMCOLORS = 24,    // Number of colors the device supports     
            PDEVICESIZE = 26,    // Size required for device descriptor      
            CURVECAPS = 28,    // Curve capabilities                       
            LINECAPS = 30,    // Line capabilities                        
            POLYGONALCAPS = 32,    // Polygonal capabilities                   
            TEXTCAPS = 34,    // Text capabilities                        
            CLIPCAPS = 36,    // Clipping capabilities                    
            RASTERCAPS = 38,    // Bitblt capabilities                      
            ASPECTX = 40,    // Length of the X leg                      
            ASPECTY = 42,    // Length of the Y leg                      
            ASPECTXY = 44,    // Length of the hypotenuse                 
            LOGPIXELSX = 88,    // Logical pixels/inch in X                 
            LOGPIXELSY = 90,    // Logical pixels/inch in Y                 
            SIZEPALETTE = 104,    // Number of entries in physical palette    
            NUMRESERVED = 106,    // Number of reserved entries in palette    
            COLORRES = 108,    // Actual color resolution                  

            // Printing related DeviceCaps. These replace the appropriate Escapes
            PHYSICALWIDTH = 110, // Physical Width in device units           
            PHYSICALHEIGHT = 111, // Physical Height in device units          
            PHYSICALOFFSETX = 112, // Physical Printable Area x margin         
            PHYSICALOFFSETY = 113, // Physical Printable Area y margin         
            SCALINGFACTORX = 114, // Scaling factor x                         
            SCALINGFACTORY = 115, // Scaling factor y                         

            // Display driver specific
            VREFRESH = 116,  // Current vertical refresh rate of the    
                             // display device (for displays only) in Hz
            DESKTOPVERTRES = 117,  // Horizontal width of entire desktop in   
                                   // pixels                                  
            DESKTOPHORZRES = 118,  // Vertical height of entire desktop in    
                                   // pixels                                  
            BLTALIGNMENT = 119,  // Preferred blt alignment                 

            SHADEBLENDCAPS = 120,  // Shading and blending caps               
            COLORMGMTCAPS = 121,  // Color Management caps                   
        }

        // Device Capability Masks: 

        // Device Technologies 
        public enum DevTech
        {
            DT_PLOTTER = 0,   // Vector plotter                   
            DT_RASDISPLAY = 1,   // Raster display                   
            DT_RASPRINTER = 2,   // Raster printer                   
            DT_RASCAMERA = 3,   // Raster camera                    
            DT_CHARSTREAM = 4,   // Character-stream, PLP            
            DT_METAFILE = 5,   // Metafile, VDM                    
            DT_DISPFILE = 6    // Display-file                     
        }

        // Curve Capabilities 
        public enum CurveCap
        {
            CC_NONE = 0,   // Curves not supported             
            CC_CIRCLES = 1,   // Can do circles                   
            CC_PIE = 2,   // Can do pie wedges                
            CC_CHORD = 4,   // Can do chord arcs                
            CC_ELLIPSES = 8,   // Can do ellipese                  
            CC_WIDE = 16,  // Can do wide lines                
            CC_STYLED = 32,  // Can do styled lines              
            CC_WIDESTYLED = 64,  // Can do wide styled lines         
            CC_INTERIORS = 128, // Can do interiors                 
            CC_ROUNDRECT = 256  //                                  
        }

        // Line Capabilities 
        public enum LineCap
        {
            LC_NONE = 0,   // Lines not supported              
            LC_POLYLINE = 2,   // Can do polylines                 
            LC_MARKER = 4,   // Can do markers                   
            LC_POLYMARKER = 8,   // Can do polymarkers               
            LC_WIDE = 16,  // Can do wide lines                
            LC_STYLED = 32,  // Can do styled lines              
            LC_WIDESTYLED = 64,  // Can do wide styled lines         
            LC_INTERIORS = 128  // Can do interiors                 
        }

        // Polygonal Capabilities 
        public enum PolygonCap
        {
            PC_NONE = 0,   // Polygonals not supported         
            PC_POLYGON = 1,   // Can do polygons                  
            PC_RECTANGLE = 2,   // Can do rectangles                
            PC_WINDPOLYGON = 4,   // Can do winding polygons          
            PC_TRAPEZOID = 4,   // Can do trapezoids                
            PC_SCANLINE = 8,   // Can do scanlines                 
            PC_WIDE = 16,  // Can do wide borders              
            PC_STYLED = 32,  // Can do styled borders            
            PC_WIDESTYLED = 64,  // Can do wide styled borders       
            PC_INTERIORS = 128, // Can do interiors                 
            PC_POLYPOLYGON = 256, // Can do polypolygons              
            PC_PATHS = 512  // Can do paths                     
        }

        // Clipping Capabilities 
        public enum ClippingCap
        {
            CP_NONE = 0,   // No clipping of output            
            CP_RECTANGLE = 1,   // Output clipped to rects          
            CP_REGION = 2    // obsolete                         
        }

        // Text Capabilities 
        public enum TextCap
        {
            TC_OP_CHARACTER = 0x00000001,  // Can do OutputPrecision   CHARACTER      
            TC_OP_STROKE = 0x00000002,  // Can do OutputPrecision   STROKE         
            TC_CP_STROKE = 0x00000004,  // Can do ClipPrecision     STROKE         
            TC_CR_90 = 0x00000008,  // Can do CharRotAbility    90             
            TC_CR_ANY = 0x00000010,  // Can do CharRotAbility    ANY            
            TC_SF_X_YINDEP = 0x00000020,  // Can do ScaleFreedom      X_YINDEPENDENT 
            TC_SA_DOUBLE = 0x00000040,  // Can do ScaleAbility      DOUBLE         
            TC_SA_INTEGER = 0x00000080,  // Can do ScaleAbility      INTEGER        
            TC_SA_CONTIN = 0x00000100,  // Can do ScaleAbility      CONTINUOUS     
            TC_EA_DOUBLE = 0x00000200,  // Can do EmboldenAbility   DOUBLE         
            TC_IA_ABLE = 0x00000400,  // Can do ItalisizeAbility  ABLE           
            TC_UA_ABLE = 0x00000800,  // Can do UnderlineAbility  ABLE           
            TC_SO_ABLE = 0x00001000,  // Can do StrikeOutAbility  ABLE           
            TC_RA_ABLE = 0x00002000,  // Can do RasterFontAble    ABLE           
            TC_VA_ABLE = 0x00004000,  // Can do VectorFontAble    ABLE           
            TC_RESERVED = 0x00008000,
            TC_SCROLLBLT = 0x00010000   // Don't do text scroll with blt           
        }

        // Raster Capabilities 
        public enum RasterCap
        {
            RC_NONE = 0,
            RC_BITBLT = 1,       // Can do standard BLT.             
            RC_BANDING = 2,       // Device requires banding support  
            RC_SCALING = 4,       // Device requires scaling support  
            RC_BITMAP64 = 8,       // Device can support >64K bitmap   
            RC_GDI20_OUTPUT = 0x0010,      // has 2.0 output calls         
            RC_GDI20_STATE = 0x0020,
            RC_SAVEBITMAP = 0x0040,
            RC_DI_BITMAP = 0x0080,      // supports DIB to memory       
            RC_PALETTE = 0x0100,      // supports a palette           
            RC_DIBTODEV = 0x0200,      // supports DIBitsToDevice      
            RC_BIGFONT = 0x0400,      // supports >64K fonts          
            RC_STRETCHBLT = 0x0800,      // supports StretchBlt          
            RC_FLOODFILL = 0x1000,      // supports FloodFill           
            RC_STRETCHDIB = 0x2000,      // supports StretchDIBits       
            RC_OP_DX_OUTPUT = 0x4000,
            RC_DEVBITS = 0x8000
        }

        // Shading and blending caps 
        public enum ShadeAndBlendCap
        {
            SB_NONE = 0x00000000,
            SB_CONST_ALPHA = 0x00000001,
            SB_PIXEL_ALPHA = 0x00000002,
            SB_PREMULT_ALPHA = 0x00000004,
            SB_GRAD_RECT = 0x00000010,
            SB_GRAD_TRI = 0x00000020
        }

        // Color Management caps 
        public enum ColorMngCap
        {
            CM_NONE = 0x00000000,
            CM_DEVICE_ICM = 0x00000001,
            CM_GAMMA_RAMP = 0x00000002,
            CM_CMYK_COLOR = 0x00000004
        }

        // This function returns the device capability value specified
        // by the requested index value.
        [DllImport("GDI32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 GetDeviceCaps(IntPtr hdc, Int32 nIndex);

    }
}


/*

Notes

Try this right after the InitializeComponent() call:

SetStyle(ControlStyles.DoubleBuffer, true)
SetStyle(ControlStyles.AllPaintingInWmPaint, true)
SetStyle(ControlStyles.UserPaint, true)

This enables double-buffering for your application (the process where the
form is drawn in a hidden (background) "buffer" and once complete is then
thrust into view...it's intent is to eliminate flicker from repeated
painting of controls etc).



*/
