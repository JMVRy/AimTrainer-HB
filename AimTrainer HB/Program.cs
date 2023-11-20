using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScreenCapture.NET;

//using Emgu.CV.OCR;
//using Emgu.CV.Structure;

//var ocr = new Tesseract( Tesseract.DefaultTesseractDirectory, "eng", OcrEngineMode.TesseractLstmCombined, "0123456789" );
//ocr.PageSegMode = PageSegMode.SingleWord;

DX11ScreenCaptureService screenCaptureService = new DX11ScreenCaptureService();
IEnumerable<GraphicsCard> graphicsCards = screenCaptureService.GetGraphicsCards();
IEnumerable<Display> displays = screenCaptureService.GetDisplays( graphicsCards.First() );
DX11ScreenCapture screenCapture = screenCaptureService.GetScreenCapture( displays.Last() );

CaptureZone<ColorBGRA> captureZone = screenCapture.RegisterCaptureZone( 470, 225, 1000, 400 );
//ICaptureZone fullscreen = screenCapture.RegisterCaptureZone(0, 0, screenCapture.Display.Width, screenCapture.Display.Height);

// Gets rid of the black screen as the first frame
screenCapture.CaptureScreen();
int iter = 0;

int rowInterval = 8;
int colInterval = 8;
while (true)
{
    screenCapture.CaptureScreen();

    using (captureZone.Lock())
    {
        RefImage<ColorBGRA> image = captureZone.Image;

        Console.WriteLine($"Image row count: {image.Rows.Count}");
        for (int i = 0; i < image.Rows.Count / rowInterval; i++)
        {
            Console.WriteLine($"Row: {i * rowInterval}");
            for (int j = 0; j < image.Columns.Count / rowInterval; j++)
            {
                if (i * rowInterval > 695 - 349 && i * rowInterval < 750 - 349 && j * colInterval > 960 - 470 && j * colInterval < 1110 - 470) return;

                Console.WriteLine( $"Column: {j * colInterval}, ({(j * colInterval) + 1450},{(i * rowInterval) + 349})" );
                if (image[j * colInterval, i * rowInterval].R == 149 && image[j * colInterval, i * rowInterval].G == 195 && image[j * colInterval, i * rowInterval].B == 232)
                {
                    Console.WriteLine("Hit!");

                    MouseOperations.SetCursorPosition((j * colInterval) - 1450, 349 + (i * rowInterval));
                    Thread.Sleep( 50 );
                    MouseOperations.MouseEvent( MouseOperations.MouseEventFlags.LeftDown );
                    MouseOperations.MouseEvent( MouseOperations.MouseEventFlags.LeftUp );

                    goto thingy;
                }
            }
        }
    }
thingy:
    {
        if (iter++ > 60) return;
    }
}

//Console.ReadKey(true);
//}

/*while (true)
{
    MouseOperations.MousePoint mousePos = MouseOperations.GetCursorPosition();
    MouseOperations.Color color = MouseOperations.GetPixelColor( mousePos );

    if (color.R == 75 && color.G == 219 && color.B == 106)
    {
        MouseOperations.MouseEvent( MouseOperations.MouseEventFlags.LeftDown );
        MouseOperations.MouseEvent( MouseOperations.MouseEventFlags.LeftUp );

        break;
    }
}

List<MouseOperations.MousePoint> flashList = new();
DateTime lastFlashTime = DateTime.MinValue;

MouseOperations.MousePoint mousePos = MouseOperations.GetCursorPosition();
MouseOperations.Color color = MouseOperations.GetPixelColor( mousePos );

//Console.WriteLine($"R: {color.R}, G: {color.G}, B: {color.B}, A: {color.A}");
Console.WriteLine( color );

MouseOperations.MousePoint[] squarePoints = new MouseOperations.MousePoint[9];
squarePoints[0] = new MouseOperations.MousePoint( -1096, 460 );
squarePoints[1] = new MouseOperations.MousePoint( -970, 460 );
squarePoints[2] = new MouseOperations.MousePoint( -830, 460 );
squarePoints[3] = new MouseOperations.MousePoint( -1096, 590 );
squarePoints[4] = new MouseOperations.MousePoint( -970, 590 );
squarePoints[5] = new MouseOperations.MousePoint( -830, 590 );
squarePoints[6] = new MouseOperations.MousePoint( -1096, 720 );
squarePoints[7] = new MouseOperations.MousePoint( -970, 720 );
squarePoints[8] = new MouseOperations.MousePoint( -830, 720 );

while (true)
{
    foreach (MouseOperations.MousePoint point in squarePoints)
    {
        if (MouseOperations.GetPixelColor( point ) == new MouseOperations.Color( 255, 255, 255, 0 ))
        {
            // Check if this position hasn't been added recently to avoid duplicate flashes
            if (flashList.Count == 0 || flashList[flashList.Count - 1] != point)
            {
                Console.WriteLine( $"Added: {point}" );
                flashList.Add( point );
                lastFlashTime = DateTime.UtcNow;
            }
        }
    }

    // If 3 seconds has passed since the last flash
    if (DateTime.UtcNow.Ticks - lastFlashTime.Ticks >= TimeSpan.TicksPerMillisecond * 3000)
    {
        foreach (MouseOperations.MousePoint point in flashList)
        {
            Console.WriteLine( $"Clicking: {point}" );
            MouseOperations.SetCursorPosition( point );
            MouseOperations.MouseEvent( MouseOperations.MouseEventFlags.LeftDown );
            MouseOperations.MouseEvent( MouseOperations.MouseEventFlags.LeftUp );

            Thread.Sleep( 50 );
        }

        flashList.Clear();
        lastFlashTime = DateTime.MinValue;
    }
    //else Console.WriteLine(DateTime.UtcNow.Ticks - lastFlashTime.Ticks);

    Thread.Sleep( 100 );
}

/*
 * REFERENCE FROM PYTHON
 * 
 * import pyautogui
 * import time
 * 
 * def compute_positions(top_left, bottom_right):
 *     width = (bottom_right[0] - top_left[0]) // 2
 *     height = (bottom_right[1] - top_left[1] // 2
 *     
 *     positions_ = [
 *         (top_left[0], top_left[1]),
 *         (top_left[0] + width, top_left[1]),
 *         (bottom_right[0], top_left[1]),
 *         (top_left[0], top_left[1] + height),
 *         (top_left[0] + width, top_left[1] + height),
 *         (bottom_right[0], top_left[1] + height),
 *         (top_left[0], bottom_right[1]),
 *         (top_left[0] + width, bottom_right[1]),
 *         (bottom_right[0], bottom_right[1])
 *     ]
 *     
 *     return positions_
 *     
 *     
 * # Set these variables based on your 3x3 grid
 * top_left_ = (1612, 804) # Example: (x1, y1)
 * bottom_right_ = (2283, 1469) # Example: (x3, y3)
 * 
 * positions = compute_positions(top_left_, bottom_right_)
 * 
 * flash_list = []
 * last_flash_time = None
 * 
 * try:
 *     while True:
 *         for idx, pos in enumerate(positions):
 *             # If the position color is white
 *             if pyautogui.pixelMatchesColor(pos[0], pos[1], (255, 255, 255)):
 *                 # Check if this position hasn't been added recently to avoid duplicate flashes
 *                 if len(flash_list) == 0 or flash_list[-1] != idx:
 *                     flash_list.append(idx)
 *                     last_flash_time = time.time()
 *                     
 *         # If 3 seconds has passed since the last flash
 *         if last_flash_time and (time.time() - last_flash_time) >= 3:
 *             for idx in flash_list:
 *                 pyautogui.click(positions[idx][0], positions[idx][1])
 *                 
 *             flash_list.clear()
 *             last_flash_time = None
 *             
 *         time.sleep(0.1)
 * 
 * except KeyboardInterrupt:
 *     print("Script terminated by user.")
 *     
 */

/*
        #region Screenshot Classes

        public class Screenshot
        {
            [DllImport( "user32.dll" )]
            private static extern IntPtr GetDC( IntPtr hWnd );

            [DllImport("user32.dll")]
            private static extern int ReleaseDC( IntPtr hWnd, IntPtr hDC );

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateCompatibleDC( IntPtr hdc );

            [DllImport("gdi32.dll")]
            private static extern int GetDeviceCaps( IntPtr hdc, int index );

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateCompatibleBitmap( IntPtr hdc, int cx, int cy );

            [DllImport("gdi32.dll")]
            private static extern IntPtr SelectObject( IntPtr hdc, IntPtr h );

            [DllImport( "gdi32.dll" )]
            private static extern bool BitBlt( IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, uint rop );

            [DllImport("gdi32.dll")]
            private static extern int GetDIBits( IntPtr hdc, IntPtr hbm, uint start, uint cLines, out IntPtr lpvBits, ref BITMAPINFO lpbmi, uint usage );

            [DllImport("gdi32.dll")]
            private static extern bool DeleteDC( IntPtr hdc );

            public static byte[] TakeScreenshot( IntPtr hWnd, int x, int y, int cx, int cy )
            {
                IntPtr hScreenDC = GetDC( hWnd );
                IntPtr hMemoryDC = CreateCompatibleDC( hScreenDC );

                int width = GetDeviceCaps( hScreenDC,  );
                int height = GetDeviceCaps( hScreenDC,  );

                IntPtr hBitmap = CreateCompatibleBitmap( hScreenDC, width, height );
                IntPtr hOldBitmap = SelectObject( hMemoryDC, hBitmap );

                BitBlt( hMemoryDC, 0, 0, width, height, hScreenDC, 0, 0, );
                hBitmap = SelectObject(hMemoryDC, hOldBitmap );

                DeleteDC( hMemoryDC );
                ReleaseDC( hWnd, hScreenDC );
            }

            //[DllImport( "user32.dll" )]
            //private static extern bool GetWindowRect( IntPtr hWnd, out RECT lpRect );

            //[DllImport("user32.dll")]
            //private static extern bool PrintWindow( IntPtr hWnd, IntPtr hdcBlt, uint nFlags );

            //public static byte[] PrintWindow(IntPtr hWnd)
            //{
            //    RECT rc;
            //    GetWindowRect(hWnd, out rc);

            //    //byte[] bitmap = 


            //}

            [StructLayout( LayoutKind.Sequential )]
            public struct BITMAPINFO
            {
                public BITMAPINFOHEADER bmiHeader;
                public RGBQUAD bmiColors;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RGBQUAD
            {
                public byte rgbBlue;
                public byte rgbGreen;
                public byte rgbRed;
                public byte rgbReserved;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct BITMAPINFOHEADER
            {
                public uint biSize;
                public int biWidth;
                public int biHeight;
                public ushort biPlanes;
                public ushort biBitCount;
                public uint biCompression;
                public uint biSizeImage;
                public int biXPelsPerMeter;
                public int biYPelsPerMeter;
                public uint biClrUsed;
                public uint biClrImportant;
            }

            [StructLayout( LayoutKind.Sequential )]
            public struct RECT
            {
                private int _Left;
                private int _Top;
                private int _Right;
                private int _Bottom;

                public RECT( RECT Rectangle ) : this( Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom )
                {
                }
                public RECT( int Left, int Top, int Right, int Bottom )
                {
                    _Left = Left;
                    _Top = Top;
                    _Right = Right;
                    _Bottom = Bottom;
                }

                public int X
                {
                    get { return _Left; }
                    set { _Left = value; }
                }
                public int Y
                {
                    get { return _Top; }
                    set { _Top = value; }
                }
                public int Left
                {
                    get { return _Left; }
                    set { _Left = value; }
                }
                public int Top
                {
                    get { return _Top; }
                    set { _Top = value; }
                }
                public int Right
                {
                    get { return _Right; }
                    set { _Right = value; }
                }
                public int Bottom
                {
                    get { return _Bottom; }
                    set { _Bottom = value; }
                }
                public int Height
                {
                    get { return _Bottom - _Top; }
                    set { _Bottom = value + _Top; }
                }
                public int Width
                {
                    get { return _Right - _Left; }
                    set { _Right = value + _Left; }
                }
                public System.Drawing.Point Location
                {
                    get { return new System.Drawing.Point( Left, Top ); }
                    set
                    {
                        _Left = value.X;
                        _Top = value.Y;
                    }
                }
                public System.Drawing.Size Size
                {
                    get { return new System.Drawing.Size( Width, Height ); }
                    set
                    {
                        _Right = value.Width + _Left;
                        _Bottom = value.Height + _Top;
                    }
                }

                public static implicit operator System.Drawing.Rectangle( RECT Rectangle )
                {
                    return new System.Drawing.Rectangle( Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height );
                }
                public static implicit operator RECT( System.Drawing.Rectangle Rectangle )
                {
                    return new RECT( Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom );
                }
                public static bool operator ==( RECT Rectangle1, RECT Rectangle2 )
                {
                    return Rectangle1.Equals( Rectangle2 );
                }
                public static bool operator !=( RECT Rectangle1, RECT Rectangle2 )
                {
                    return !Rectangle1.Equals( Rectangle2 );
                }

                public readonly override string ToString()
                {
                    return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
                }

                public readonly override int GetHashCode()
                {
                    return ToString().GetHashCode();
                }

                public readonly bool Equals( RECT Rectangle )
                {
                    return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
                }

                public readonly override bool Equals( object? Object )
                {
                    if (Object == null) return false;

                    if (Object is RECT rect)
                    {
                        return Equals( rect );
                    }
                    else if (Object is System.Drawing.Rectangle rectangle)
                    {
                        return Equals( new RECT( rectangle ) );
                    }

                    return false;
                }
            }
        }

        #endregion
        */

#region Input Operation Classes
public class MouseOperations
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        Move = 0x0001,
        Absolute = 0x8000,
        RightDown = 0x0008,
        RightUp = 0x0010
    }

    [DllImport( "user32.dll", EntryPoint = "SetCursorPos" )]
    [return: MarshalAs( UnmanagedType.Bool )]
    private static extern bool SetCursorPos( int x, int y );

    [DllImport( "user32.dll" )]
    [return: MarshalAs( UnmanagedType.Bool )]
    private static extern bool GetCursorPos( out MousePoint lpMousePoint );

    [DllImport( "user32.dll" )]
    private static extern void mouse_event( int dwFlags, int dx, int dy, int dwData, int dwExtraInfo );

    [DllImport( "user32.dll" )]
    private static extern IntPtr GetDC( IntPtr hWnd );

    [DllImport( "user32.dll" )]
    private static extern int ReleaseDC( IntPtr hWnd, IntPtr hdc );

    [DllImport( "gdi32.dll", EntryPoint = "GetPixel" )]
    private static extern uint GetPixel( IntPtr hdc, int x, int y );

    [DllImport( "user32.dll", EntryPoint = "FindWindow", SetLastError = true )]
    private static extern IntPtr FindWindowByCaption( IntPtr ZeroOnly, string lpWindowName );

    public static IntPtr FindWindowByCaption( string caption ) => FindWindowByCaption( IntPtr.Zero, caption );

    public static Color GetPixelColor( MousePoint mousePoint ) => GetPixelColor( IntPtr.Zero, mousePoint );
    public static Color GetPixelColor( IntPtr hWnd, MousePoint mousePoint )
    {
        IntPtr hdc = GetDC( hWnd );
        uint pixel = GetPixel( hdc, mousePoint.X, mousePoint.Y );
        ReleaseDC( hWnd, hdc );

        Color color = new Color(
            ( byte )((pixel & 0x000000FF) >> 0),
            ( byte )((pixel & 0x0000FF00) >> 8),
            ( byte )((pixel & 0x00FF0000) >> 16),
            ( byte )((pixel & 0xFF000000) >> 24)
        );
        return color;
    }

    public static Color GetPixelColor( int x, int y ) => GetPixelColor( IntPtr.Zero, x, y );
    public static Color GetPixelColor( IntPtr hWnd, int x, int y )
    {
        IntPtr hdc = GetDC( hWnd );
        uint pixel = GetPixel( hdc, x, y );
        ReleaseDC( hWnd, hdc );

        Color color = new Color(
            ( byte )((pixel & 0x000000FF) >> 0),
            ( byte )((pixel & 0x0000FF00) >> 8),
            ( byte )((pixel & 0x00FF0000) >> 16),
            ( byte )((pixel & 0xFF000000) >> 24)
        );
        return color;
    }

    public static void SetCursorPosition( int x, int y )
    {
        SetCursorPos( x, y );
    }

    public static void SetCursorPosition( MousePoint point )
    {
        SetCursorPos( point.X, point.Y );
    }

    public static MousePoint GetCursorPosition()
    {
        MousePoint currentMousePoint;

        var gotPoint = GetCursorPos( out currentMousePoint );
        if (!gotPoint)
            currentMousePoint = new MousePoint( 0, 0 );

        return currentMousePoint;
    }

    public static void MouseEvent( MouseEventFlags value )
    {
        MousePoint position = GetCursorPosition();

        mouse_event( ( int )value, position.X, position.Y, 0, 0 );
    }

    [StructLayout( LayoutKind.Sequential )]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint( int x, int y )
        {
            X = x;
            Y = y;
        }

        public static bool operator !=( MousePoint left, MousePoint right ) => !(left == right);
        public static bool operator ==( MousePoint left, MousePoint right )
        {
            if (left.X == right.X && left.Y == right.Y) return true;
            return false;
        }

        public override readonly bool Equals( object? obj )
        {
            if (obj == null) return false;
            return this == ( MousePoint )obj;
        }

        public override string ToString()
        {
            return (X, Y).ToString();
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }

    public struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public Color( byte r, byte g, byte b, byte a )
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color() : this( 0, 0, 0, 0 ) { }

        public static bool operator !=( Color left, Color right ) => !(left == right);
        public static bool operator ==( Color left, Color right )
        {
            if (left.R == right.R && left.G == right.G && left.B == right.B && left.A == right.A) return true;
            return false;
        }

        public override readonly bool Equals( object? obj )
        {
            if (obj == null) return false;
            return this == ( Color )obj;
        }

        public override string ToString()
        {
            return (R, G, B, A).ToString();
        }

        public override int GetHashCode()
        {
            return (R, G, B, A).GetHashCode();
        }
    }
}

public class InputOperations
{
    [DllImport( "user32.dll", SetLastError = true )]
    private static extern uint SendInput( uint cInputs, INPUT[] pInputs, int cbSize );

    [DllImport( "user32.dll", SetLastError = true )]
    private static extern IntPtr GetMessageExtraInfo();

    public static uint InputEvent( INPUT[] inputs )
    {
        // Extra processing
        // ...

        // Make sure all extra info is included
        for (int i = 0; i < inputs.Length; i++)
        {
            //Console.WriteLine($"[InputEvent] Got Input: { inputs[i].U.ki.wVk }");
            inputs[i].U.mi.dwExtraInfo = ( UIntPtr )GetMessageExtraInfo();
            inputs[i].U.ki.dwExtraInfo = ( UIntPtr )GetMessageExtraInfo();
        }

        //Console.WriteLine($"[InputEvent] Sending {inputs.Length} inputs, INPUT.Size = {INPUT.Size}, sizeof(INPUT) = {sizeof(INPUT)}");
        return SendInput( ( uint )inputs.Length, inputs, INPUT.Size );
    }

    public static uint SendKeypress( VirtualKeyShort vk, uint millisecondDelay = 0 ) => SendKeypress( new VirtualKeyShort[] { vk }, millisecondDelay );
    public static uint SendKeypress( VirtualKeyShort[] virtualKeys, uint millisecondDelay = 0 )
    {
        //Console.WriteLine($"[SendKeypress] Input length: {virtualKeys.Length}");
        INPUT[] inputs = new INPUT[virtualKeys.Length];

        for (int i = 0; i < inputs.Length; i++)
        {
            //Console.WriteLine($"[SendKeypress] Input[{i}]{{wVk = {virtualKeys[i]}}}.KeyDown");
            inputs[i].type = InputType.INPUT_KEYBOARD;
            inputs[i].U.ki.wScan = 0;
            inputs[i].U.ki.time = 0;
            inputs[i].U.ki.dwFlags = 0;
            inputs[i].U.ki.wVk = virtualKeys[i];
        }

        //Console.WriteLine("[SendKeypress] Sending normal inputs");
        InputEvent( inputs );

        //Console.WriteLine("[SendKeypress] Creating delay task");
        Task delay = Task.CompletedTask;
        if (millisecondDelay > 0)
            delay = Task.Delay( ( int )millisecondDelay );

        for (int i = 0; i < inputs.Length; i++)
        {
            //Console.WriteLine($"[SendKeypress] Input[{i}].KeyUp");
            inputs[i].U.ki.dwFlags = KEYEVENTF.KEYUP;
        }

        //Console.WriteLine($"[SendKeypress] Waiting for delay, ended too early = {delay.IsCompleted}, status = {delay.Status}");
        delay.Wait();

        //Console.WriteLine("[SendKeypress] Sending KeyUp inputs");
        return InputEvent( inputs );
    }

    #region Input Data
    [StructLayout( LayoutKind.Sequential )]
    public struct INPUT
    {
        public InputType type;
        public InputUnion U;
        public static int Size
        {
            get { return Marshal.SizeOf( typeof( INPUT ) ); }
        }
    }

    public enum InputType : uint
    {
        INPUT_MOUSE,
        INPUT_KEYBOARD,
        INPUT_HARDWARE
    }

    [StructLayout( LayoutKind.Explicit )]
    public struct InputUnion
    {
        [FieldOffset( 0 )]
        public MOUSEINPUT mi;
        [FieldOffset( 0 )]
        public KEYBDINPUT ki;
        [FieldOffset( 0 )]
        public HARDWAREINPUT hi;
    }

    #region Hardware
    [StructLayout( LayoutKind.Sequential )]
    public struct HARDWAREINPUT
    {
        internal int uMsg;
        internal short wParamL;
        internal short wParamR;
    }
    #endregion

    #region Keyboard
    [StructLayout( LayoutKind.Sequential )]
    public struct KEYBDINPUT
    {
        internal VirtualKeyShort wVk;
        internal ScanCodeShort wScan;
        internal KEYEVENTF dwFlags;
        internal int time;
        internal UIntPtr dwExtraInfo;
    }

    [Flags]
    public enum KEYEVENTF : uint
    {
        EXTENDEDKEY = 0x1,
        KEYUP = 0x2,
        UNICODE = 0x4,
        SCANCODE = 0x8,
    }

    public enum ScanCodeShort : short
    {
        VK_LBUTTON = 0,
        VK_RBUTTON = 0,
        VK_CANCEL = 70,
        VK_MBUTTON = 0,
        VK_XBUTTON1 = 0,
        VK_XBUTTON2 = 0,
        VK_BACK = 14,
        VK_TAB = 15,
        VK_CLEAR = 76,
        VK_RETURN = 28,
        VK_SHIFT = 42,
        VK_CONTROL = 29,
        VK_MENU = 56,
        VK_PAUSE = 0,
        VK_CAPITAL = 58,
        VK_KANA = 0,
        VK_HANGUL = 0,
        VK_JUNJA = 0,
        VK_FINAL = 0,
        VK_HANJA = 0,
        VK_KANJI = 0,
        VK_ESCAPE = 1,
        VK_CONVERT = 0,
        VK_NONCONVERT = 0,
        VK_ACCEPT = 0,
        VK_MODECHANGE = 0,
        VK_SPACE = 57,
        VK_PRIOR = 73,
        VK_NEXT = 81,
        VK_END = 79,
        VK_HOME = 71,
        VK_LEFT = 75,
        VK_UP = 72,
        VK_RIGHT = 77,
        VK_DOWN = 80,
        VK_SELECT = 0,
        VK_PRINT = 0,
        VK_EXECUTE = 0,
        VK_SNAPSHOT = 84,
        VK_INSERT = 82,
        VK_DELETE = 83,
        VK_HELP = 99,
        VK_KEY_0 = 11,
        VK_KEY_1 = 2,
        VK_KEY_2 = 3,
        VK_KEY_3 = 4,
        VK_KEY_4 = 5,
        VK_KEY_5 = 6,
        VK_KEY_6 = 7,
        VK_KEY_7 = 8,
        VK_KEY_8 = 9,
        VK_KEY_9 = 10,
        VK_KEY_A = 30,
        VK_KEY_B = 48,
        VK_KEY_C = 46,
        VK_KEY_D = 32,
        VK_KEY_E = 18,
        VK_KEY_F = 33,
        VK_KEY_G = 34,
        VK_KEY_H = 35,
        VK_KEY_I = 23,
        VK_KEY_J = 36,
        VK_KEY_K = 37,
        VK_KEY_L = 38,
        VK_KEY_M = 50,
        VK_KEY_N = 49,
        VK_KEY_O = 24,
        VK_KEY_P = 25,
        VK_KEY_Q = 16,
        VK_KEY_R = 19,
        VK_KEY_S = 31,
        VK_KEY_T = 20,
        VK_KEY_U = 22,
        VK_KEY_V = 47,
        VK_KEY_W = 17,
        VK_KEY_X = 45,
        VK_KEY_Y = 21,
        VK_KEY_Z = 44,
        VK_LWIN = 91,
        VK_RWIN = 92,
        VK_APPS = 93,
        VK_SLEEP = 95,
        VK_NUMPAD0 = 82,
        VK_NUMPAD1 = 79,
        VK_NUMPAD2 = 80,
        VK_NUMPAD3 = 81,
        VK_NUMPAD4 = 75,
        VK_NUMPAD5 = 76,
        VK_NUMPAD6 = 77,
        VK_NUMPAD7 = 71,
        VK_NUMPAD8 = 72,
        VK_NUMPAD9 = 73,
        VK_MULTIPLY = 55,
        VK_ADD = 78,
        VK_SEPARATOR = 0,
        VK_SUBTRACT = 74,
        VK_DECIMAL = 83,
        VK_DIVIDE = 53,
        VK_F1 = 59,
        VK_F2 = 60,
        VK_F3 = 61,
        VK_F4 = 62,
        VK_F5 = 63,
        VK_F6 = 64,
        VK_F7 = 65,
        VK_F8 = 66,
        VK_F9 = 67,
        VK_F10 = 68,
        VK_F11 = 87,
        VK_F12 = 88,
        VK_F13 = 100,
        VK_F14 = 101,
        VK_F15 = 102,
        VK_F16 = 103,
        VK_F17 = 104,
        VK_F18 = 105,
        VK_F19 = 106,
        VK_F20 = 107,
        VK_F21 = 108,
        VK_F22 = 109,
        VK_F23 = 110,
        VK_F24 = 118,
        VK_NUMLOCK = 69,
        VK_SCROLL = 70,
        VK_LSHIFT = 42,
        VK_RSHIFT = 54,
        VK_LCONTROL = 29,
        VK_RCONTROL = 29,
        VK_LMENU = 56,
        VK_RMENU = 56,
        VK_BROWSER_BACK = 106,
        VK_BROWSER_FORWARD = 105,
        VK_BROWSER_REFRESH = 103,
        VK_BROWSER_STOP = 104,
        VK_BROWSER_SEARCH = 101,
        VK_BROWSER_FAVORITES = 102,
        VK_BROWSER_HOME = 50,
        VK_VOLUME_MUTE = 32,
        VK_VOLUME_DOWN = 46,
        VK_VOLUME_UP = 48,
        VK_MEDIA_NEXT_TRACK = 25,
        VK_MEDIA_PREV_TRACK = 16,
        VK_MEDIA_STOP = 36,
        VK_MEDIA_PLAY_PAUSE = 34,
        VK_LAUNCH_MAIL = 108,
        VK_LAUNCH_MEDIA_SELECT = 109,
        VK_LAUNCH_APP1 = 107,
        VK_LAUNCH_APP2 = 33,
        VK_OEM_1 = 39,
        VK_OEM_PLUS = 13,
        VK_OEM_COMMA = 51,
        VK_OEM_MINUS = 12,
        VK_OEM_PERIOD = 52,
        VK_OEM_2 = 53,
        VK_OEM_3 = 41,
        VK_OEM_4 = 26,
        VK_OEM_5 = 43,
        VK_OEM_6 = 27,
        VK_OEM_7 = 40,
        VK_OEM_8 = 0,
        VK_OEM_102 = 86,
        VK_PROCESSKEY = 0,
        VK_PACKET = 0,
        VK_ATTN = 0,
        VK_CRSEL = 0,
        VK_EXSEL = 0,
        VK_EREOF = 93,
        VK_PLAY = 0,
        VK_ZOOM = 98,
        VK_NONAME = 0,
        VK_PA1 = 0,
        VK_OEM_CLEAR = 0,
    }

    public enum VirtualKeyShort : short
    {
        [Description( "Left mouse button" )]
        VK_LBUTTON = 0x01,

        [Description( "Right mouse button" )]
        VK_RBUTTON = 0x02,

        [Description( "Control-break processing" )]
        VK_CANCEL = 0x03,

        [Description( "Middle mouse button" )]
        VK_MBUTTON = 0x04,

        [Description( "X1 mouse button" )]
        VK_XBUTTON1 = 0x05,

        [Description( "X2 mouse button" )]
        VK_XBUTTON2 = 0x06,

        [Description( "BACKSPACE key" )]
        VK_BACK = 0x08,

        [Description( "TAB key" )]
        VK_TAB = 0x09,

        [Description( "CLEAR key" )]
        VK_CLEAR = 0x0C,

        [Description( "ENTER key" )]
        VK_RETURN = 0x0D,

        [Description( "SHIFT key" )]
        VK_SHIFT = 0x10,

        [Description( "CTRL key" )]
        VK_CONTROL = 0x11,

        [Description( "ALT key" )]
        VK_MENU = 0x12,

        [Description( "PAUSE key" )]
        VK_PAUSE = 0x13,

        [Description( "CAPS LOCK key" )]
        VK_CAPITAL = 0x14,

        [Description( "IME Kana mode" )]
        VK_KANA = 0x15,

        [Description( "IME Hangul mode" )]
        VK_HANGUL = 0x15,

        [Description( "IME On" )]
        VK_IME_ON = 0x16,

        [Description( "IME Junja mode" )]
        VK_JUNJA = 0x17,

        [Description( "IME final mode" )]
        VK_FINAL = 0x18,

        [Description( "IME Hanja mode" )]
        VK_HANJA = 0x19,

        [Description( "IME Kanji mode" )]
        VK_KANJI = 0x19,

        [Description( "IME Off" )]
        VK_IME_OFF = 0x1A,

        [Description( "ESC key" )]
        VK_ESCAPE = 0x1B,

        [Description( "IME convert" )]
        VK_CONVERT = 0x1C,

        [Description( "IME nonconvert" )]
        VK_NONCONVERT = 0x1D,

        [Description( "IME accept" )]
        VK_ACCEPT = 0x1E,

        [Description( "IME mode change request" )]
        VK_MODECHANGE = 0x1F,

        [Description( "SPACEBAR" )]
        VK_SPACE = 0x20,

        [Description( "PAGE UP key" )]
        VK_PRIOR = 0x21,

        [Description( "PAGE DOWN key" )]
        VK_NEXT = 0x22,

        [Description( "END key" )]
        VK_END = 0x23,

        [Description( "HOME key" )]
        VK_HOME = 0x24,

        [Description( "LEFT ARROW key" )]
        VK_LEFT = 0x25,

        [Description( "UP ARROW key" )]
        VK_UP = 0x26,

        [Description( "RIGHT ARROW key" )]
        VK_RIGHT = 0x27,

        [Description( "DOWN ARROW key" )]
        VK_DOWN = 0x28,

        [Description( "SELECT key" )]
        VK_SELECT = 0x29,

        [Description( "PRINT key" )]
        VK_PRINT = 0x2A,

        [Description( "EXECUTE key" )]
        VK_EXECUTE = 0x2B,

        [Description( "PRINT SCREEN key" )]
        VK_SNAPSHOT = 0x2C,

        [Description( "INS key" )]
        VK_INSERT = 0x2D,

        [Description( "DEL key" )]
        VK_DELETE = 0x2E,

        [Description( "HELP key" )]
        VK_HELP = 0x2F,

        VK_0 = 0x30,
        VK_1 = 0x31,
        VK_2 = 0x32,
        VK_3 = 0x33,
        VK_4 = 0x34,
        VK_5 = 0x35,
        VK_6 = 0x36,
        VK_7 = 0x37,
        VK_8 = 0x38,
        VK_9 = 0x39,
        VK_A = 0x41,    // Alpha-numeric keys, kinda unnecessary to describe
        VK_B = 0x42,
        VK_C = 0x43,
        VK_D = 0x44,
        VK_E = 0x45,
        VK_F = 0x46,
        VK_G = 0x47,
        VK_H = 0x48,
        VK_I = 0x49,
        VK_J = 0x4A,
        VK_K = 0x4B,
        VK_L = 0x4C,
        VK_M = 0x4D,
        VK_N = 0x4E,
        VK_O = 0x4F,
        VK_P = 0x50,
        VK_Q = 0x51,
        VK_R = 0x52,
        VK_S = 0x53,
        VK_T = 0x54,
        VK_U = 0x55,
        VK_V = 0x56,
        VK_W = 0x57,
        VK_X = 0x58,
        VK_Y = 0x59,
        VK_Z = 0x5A,

        [Description( "Left Windows key" )]
        VK_LWIN = 0x5B,

        [Description( "Right Windows key" )]
        VK_RWIN = 0x5C,

        [Description( "Applications key" )]
        VK_APPS = 0x5D,

        [Description( "Computer Sleep key" )]
        VK_SLEEP = 0x5F,

        VK_NUMPAD0 = 0x60,
        VK_NUMPAD1 = 0x61,
        VK_NUMPAD2 = 0x62,
        VK_NUMPAD3 = 0x63,  // Numpad keys, kinda unnecessary to describe
        VK_NUMPAD4 = 0x64,
        VK_NUMPAD5 = 0x65,
        VK_NUMPAD6 = 0x66,
        VK_NUMPAD7 = 0x67,
        VK_NUMPAD8 = 0x68,
        VK_NUMPAD9 = 0x69,

        [Description( "Numpad Multiply key" )]
        VK_MULTIPLY = 0x6A,

        [Description( "Numpad Add key" )]
        VK_ADD = 0x6B,

        [Description( "Numpad Separator key" )]
        VK_SEPARATOR = 0x6C,

        [Description( "Numpad Subtract key" )]
        VK_SUBTRACT = 0x6D,

        [Description( "Numpad Decimal key" )]
        VK_DECIMAL = 0x6E,

        [Description( "Numpad Divide key" )]
        VK_DIVIDE = 0x6F,

        VK_F1 = 0x70,
        VK_F2 = 0x71,
        VK_F3 = 0x72,
        VK_F4 = 0x73,
        VK_F5 = 0x74,
        VK_F6 = 0x75,
        VK_F7 = 0x76,
        VK_F8 = 0x77,
        VK_F9 = 0x78,
        VK_F10 = 0x79,
        VK_F11 = 0x7A,  // Function keys, kinda unnecessary to describe
        VK_F12 = 0x7B,
        VK_F13 = 0x7C,
        VK_F14 = 0x7D,
        VK_F15 = 0x7E,
        VK_F16 = 0x7F,
        VK_F17 = 0x80,
        VK_F18 = 0x81,
        VK_F19 = 0x82,
        VK_F20 = 0x83,
        VK_F21 = 0x84,
        VK_F22 = 0x85,
        VK_F23 = 0x86,
        VK_F24 = 0x87,

        [Description( "NUM LOCK key" )]
        VK_NUMLOCK = 0x90,

        [Description( "SCROLL LOCK key" )]
        VK_SCROLL = 0x91,

        [Description( "Left SHIFT key" )]
        VK_LSHIFT = 0xA0,

        [Description( "Right SHIFT key" )]
        VK_RSHIFT = 0xA1,

        [Description( "Left CONTROL key" )]
        VK_LCONTROL = 0xA2,

        [Description( "Right CONTROL key" )]
        VK_RCONTROL = 0xA3,

        [Description( "Left ALT key" )]
        VK_LMENU = 0xA4,

        [Description( "Right ALT key" )]
        VK_RMENU = 0xA5,

        [Description( "Browser Back key" )]
        VK_BROWSER_BACK = 0xA6,

        [Description( "Browser Forward key" )]
        VK_BROWSER_FORWARD = 0xA7,

        [Description( "Browser Refresh key" )]
        VK_BROWSER_REFRESH = 0xA8,

        [Description( "Browser Stop key" )]
        VK_BROWSER_STOP = 0xA9,

        [Description( "Browser Search key" )]
        VK_BROWSER_SEARCH = 0xAA,

        [Description( "Browser Favorites key" )]
        VK_BROWSER_FAVOTITES = 0xAB,

        [Description( "Browser Start and Home key" )]
        VK_BROWSER_HOME = 0xAC,

        [Description( "Volume Mute key" )]
        VK_VOLUME_MUTE = 0xAD,

        [Description( "Volume Down key" )]
        VK_VOLUME_DOWN = 0xAE,

        [Description( "Volume Up key" )]
        VK_VOLUME_UP = 0xAF,

        [Description( "Next Track key" )]
        VK_MEDIA_NEXT_TRACK = 0xB0,

        [Description( "Previous Track key" )]
        VK_MEDIA_PREV_TRACK = 0xB1,

        [Description( "Stop Media key" )]
        VK_MEDIA_STOP = 0xB2,

        [Description( "Play/Pause Media key" )]
        VK_MEDIA_PLAY_PAUSE = 0xB3,

        [Description( "Start Mail key" )]
        VK_LAUNCH_MAIL = 0xB4,

        [Description( "Start Media key" )]
        VK_LAUNCH_MEDIA_SELECT = 0xB5,

        [Description( "Start Application 1 key" )]
        VK_LAUNCH_APP1 = 0xB6,

        [Description( "Start Application 2 key" )]
        VK_LAUNCH_APP2 = 0xB7,

        [Description( "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ;: key" )]
        VK_OEM_1 = 0xBA,

        [Description( "For any country/region, the + key" )]
        VK_OEM_PLUS = 0xBB,

        [Description( "For any country/region, the , key" )]
        VK_OEM_COMMA = 0xBC,

        [Description( "For any country/region, the - key" )]
        VK_OEM_MINUS = 0xBD,

        [Description( "For any country/region, the . key" )]
        VK_OEM_PERIOD = 0xBE,

        [Description( "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the /? key" )]
        VK_OEM_2 = 0xBF,

        [Description( "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the `~ key" )]
        VK_OEM_3 = 0xC0,

        [Description( "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the [{ key" )]
        VK_OEM_4 = 0xDB,

        [Description( "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the \\| key" )]
        VK_OEM_5 = 0xDC,

        [Description( "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ]} key" )]
        VK_OEM_6 = 0xDD,

        [Description( "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\" key" )]
        VK_OEM_7 = 0xDE,

        [Description( "Used for miscellaneous characters; it can vary by keyboard." )]
        VK_OEM_8 = 0xDF,

        [Description( "The <> keys on the US standard keyboard, or the \\| key on the non-US 102-key keyboard" )]
        VK_OEM_102 = 0xE2,

        [Description( "IME PROCESS key" )]
        VK_PROCESSKEY = 0xE5,

        [Description( "Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP" )]
        VK_PACKET = 0xE7,

        [Description( "Attn key" )]
        VK_ATTN = 0xF6,

        [Description( "CrSel key" )]
        VK_CRSEL = 0xF7,

        [Description( "ExSel key" )]
        VK_EXSEL = 0xF8,

        [Description( "Erase EOF key" )]
        VK_EREOF = 0xF9,

        [Description( "Play key" )]
        VK_PLAY = 0xFA,

        [Description( "Zoom key" )]
        VK_ZOOM = 0xFB,

        [Description( "Reserved" )]
        VK_NONAME = 0xFC,

        [Description( "PA1 key" )]
        VK_PA1 = 0xFD,

        [Description( "Clear key" )]
        VK_OEM_CLEAR = 0xFE
    }
    #endregion

    #region Mouse
    [StructLayout( LayoutKind.Sequential )]
    public struct MOUSEINPUT
    {
        internal int dx;
        internal int dy;
        internal int mouseData;
        internal MOUSEEVENTF dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }

    [Flags]
    public enum MOUSEEVENTF : uint
    {
        ABSOLUTE = 0x8000,
        HWHEEL = 0x01000,
        MOVE = 0x0001,
        MOVE_NOCOALESCE = 0x2000,
        LEFTDOWN = 0x0002,
        LEFTUP = 0x0004,
        RIGHTDOWN = 0x0008,
        RIGHTUP = 0x0010,
        MIDDLEDOWN = 0x0020,
        MIDDLEUP = 0x0040,
        VIRTUALDESK = 0x4000,
        WHEEL = 0x0800,
        XDOWN = 0x0080,
        XUP = 0x0100
    }
    #endregion
    #endregion
}
#endregion