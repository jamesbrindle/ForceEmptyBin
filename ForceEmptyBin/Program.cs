using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace ForceEmptyBin
{
    internal class Program
    {
        static void Main()
        {
            if (!IsRunAsAdmin())
            {
                ProcessStartInfo proc = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Assembly.GetEntryAssembly().CodeBase,
                    Verb = "runas"
                };

                try
                {
                    Process.Start(proc);

                    for (char c = 'A'; c < 'Z'; c++)
                        RunForceEmptyBin(c);

                    UseNirCmd();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("This program must be run as an administrator! \n\n" + ex.ToString());
                }
            }
        }

        private static void RunForceEmptyBin(char driveLetter)
        {
            try
            {
                var process = new Process();
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/C rd /s /q {driveLetter}:\\$Recycle.bin";
                process.Start();
                process.WaitForExit();
            }
            catch { }
        }

        private static void UseNirCmd()
        {
            try
            {
                var process = new Process();
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.Verb = "runas";

                process.StartInfo.FileName = "nircmd.exe";
                process.StartInfo.Arguments = "emptybin";
                process.Start();
                process.WaitForExit();
            }
            catch { }
        }

        private static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
