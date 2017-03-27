using OBSwitching;
using System;
using System.Threading.Tasks;
using WindowsInput.Native;

class Program
{
    public static int SCREEN_NUMBER = 1;
    public static bool PAUSE = false;

    [STAThread]
    static void Main(string[] args)
    {
        Task task1 = Task.Factory.StartNew(() => InterceptKeyboard.Start());
        Task task2 = Task.Factory.StartNew(() => InterceptMouse.Start());

        Task.WaitAll(task1, task2);
        Console.WriteLine("All threads complete");
    }

    public static async Task ChangeScreen()
    {
        Console.WriteLine("Screen: " + SCREEN_NUMBER);
        var i = new WindowsInput.InputSimulator();

        var key = SCREEN_NUMBER == 1 ? VirtualKeyCode.F2 : VirtualKeyCode.F3;

        i.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
        i.Keyboard.KeyDown(VirtualKeyCode.LWIN);
        i.Keyboard.KeyDown(VirtualKeyCode.LMENU);
        i.Keyboard.KeyDown(key);
        i.Keyboard.Sleep(50);
        i.Keyboard.KeyUp(key);
        i.Keyboard.KeyUp(VirtualKeyCode.LMENU);
        i.Keyboard.KeyUp(VirtualKeyCode.LWIN);
        i.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
    }
}