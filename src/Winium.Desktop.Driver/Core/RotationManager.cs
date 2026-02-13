namespace Winium.Desktop.Driver.Core
{
    #region using

    using System;
    using System.Runtime.InteropServices;

    #endregion

    internal static class RotationManager
    {
        #region Constants

        private const int ENUM_CURRENT_SETTINGS = -1;

        private const int CDS_UPDATEREGISTRY = 0x01;

        private const int DISP_CHANGE_SUCCESSFUL = 0;

        private const int DISP_CHANGE_RESTART = 1;

        private const int DISP_CHANGE_BADMODE = -2;

        private const int DMDO_DEFAULT = 0;

        private const int DMDO_90 = 1;

        private const int DMDO_180 = 2;

        private const int DMDO_270 = 3;

        private const int DM_DISPLAYORIENTATION = 0x00000080;

        private const int DM_PELSWIDTH = 0x00080000;

        private const int DM_PELSHEIGHT = 0x00100000;

        #endregion

        #region Public Methods and Operators

        public static DisplayOrientation GetCurrentOrientation()
        {
            var dm = new DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(dm);

            if (!EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref dm))
            {
                return DisplayOrientation.LANDSCAPE;
            }

            switch (dm.dmDisplayOrientation)
            {
                case DMDO_DEFAULT:
                    return DisplayOrientation.LANDSCAPE;
                case DMDO_90:
                    return DisplayOrientation.PORTRAIT;
                case DMDO_180:
                    return DisplayOrientation.LANDSCAPE_FLIPPED;
                case DMDO_270:
                    return DisplayOrientation.PORTRAIT_FLIPPED;
                default:
                    return DisplayOrientation.LANDSCAPE;
            }
        }

        public static int SetOrientation(DisplayOrientation orientation)
        {
            var dm = new DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(dm);

            if (!EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref dm))
            {
                return -1;
            }

            int newOrientation;
            switch (orientation)
            {
                case DisplayOrientation.LANDSCAPE:
                    newOrientation = DMDO_DEFAULT;
                    break;
                case DisplayOrientation.PORTRAIT:
                    newOrientation = DMDO_90;
                    break;
                case DisplayOrientation.LANDSCAPE_FLIPPED:
                    newOrientation = DMDO_180;
                    break;
                case DisplayOrientation.PORTRAIT_FLIPPED:
                    newOrientation = DMDO_270;
                    break;
                default:
                    return -1;
            }

            if ((dm.dmDisplayOrientation % 2) != (newOrientation % 2))
            {
                var temp = dm.dmPelsWidth;
                dm.dmPelsWidth = dm.dmPelsHeight;
                dm.dmPelsHeight = temp;
            }

            dm.dmDisplayOrientation = newOrientation;
            dm.dmFields = DM_DISPLAYORIENTATION | DM_PELSWIDTH | DM_PELSHEIGHT;

            return ChangeDisplaySettingsEx(null, ref dm, IntPtr.Zero, CDS_UPDATEREGISTRY, IntPtr.Zero);
        }

        #endregion

        #region Native Methods

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        private static extern bool EnumDisplaySettings(
            string deviceName,
            int modeNum,
            ref DEVMODE devMode);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        private static extern int ChangeDisplaySettingsEx(
            string deviceName,
            ref DEVMODE devMode,
            IntPtr hwnd,
            int flags,
            IntPtr lParam);

        #endregion

        #region Nested Types

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        #endregion
    }
}
