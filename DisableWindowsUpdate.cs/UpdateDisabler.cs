using System;
using System.Diagnostics;

public class WUP
{
    public Logging l = new Logging();
    public Util util = new Util();
    public string RegExe = Environment.ExpandEnvironmentVariables("%systemroot%") + "\\system32\\reg.exe";
    public string CmdExe = Environment.ExpandEnvironmentVariables("%systemroot%") + "\\system32\\cmd.exe";
    /// <summary>
    /// Checks if RegExe file exists
    /// </summary>
    /// <returns>If RegExe exists</returns>
    public bool CheckReg()
    {
        try
        {
            if (!File.Exists(RegExe))
            {
                throw new FileNotFoundException(RegExe + "was not found.");
            }
            return true;
        } catch(Exception ex)
        {
            Console.WriteLine("" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Disables Windows update.
    /// </summary>
	public void Disable()
    {
        int ErrorCode = 0;
        Console.WriteLine("Tweaking the registry...\n");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} add HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} add HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU /v AUOptions /t REG_DWORD /d 2 /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} add HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU /v UseWUServer /t REG_DWORD /d 1 /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} add HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate /v DoNotConnectToWindowsUpdateInternetLocations /t REG_DWORD /d 1 /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} add HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate /v WUStatusServer /t REG_SZ /d localserver.localdomain.wsus /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} add HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate /v WUServer /t REG_SZ /d localserver.localdomain.wsus /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} add HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate /v UpdateServiceUrlAlternate /t REG_SZ /d wsus.localdomain.localserver /f");
        if (ErrorCode == 0)
        {
            Console.WriteLine("Windows Update has been disabled.");
        }
        else
        {
            Console.WriteLine("Failed to disable Windows Update. (Is it already disabled?)");
            Console.WriteLine($"Error code {ErrorCode}");
        }
    }
    /// <summary>
    /// Enables Windows update.
    /// </summary>
    public void Enable()
    {
        int ErrorCode = 0;
        Console.WriteLine("Tweaking the registry...");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} delete HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} delete HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate /v DoNotConnectToWindowsUpdateInternetLocations /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} delete HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate /v WUStatusServer /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} delete HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate /v WUServer /f");
        ErrorCode = ErrorCode + util.ProcessStart(CmdExe, $"/c {RegExe} delete HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate /v UpdateServiceUrlAlternate /f");
        if(ErrorCode == 0)
        {
            Console.WriteLine("Windows Update has been enabled.");
        } else
        {
            Console.WriteLine("Failed to enable Windows Update. (Is it already enabled?)");
            Console.WriteLine($"Failed to write {ErrorCode} keys");

        }

    }
}

/*
// enable windows update
reg delete HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU /f
reg delete HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU /v AUOptions /f
reg delete HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU /v UseWUServer /f
reg delete HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate /v DoNotConnectToWindowsUpdateInternetLocations /f
reg delete HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate /v WUStatusServer /f
reg delete HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate /v WUServer /f
reg delete HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate /v UpdateServiceUrlAlternate /f
// disable windows update
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU /f
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU /v AUOptions /t REG_DWORD /d 2 /f
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU /v UseWUServer /t REG_DWORD /d 1 /f
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate /v DoNotConnectToWindowsUpdateInternetLocations /t REG_DWORD /d 1 /f
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate /v WUStatusServer /t REG_SZ /d localserver.localdomain.wsus /f
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate /v WUServer /t REG_SZ /d localserver.localdomain.wsus /f
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate /v UpdateServiceUrlAlternate /t REG_SZ /d wsus.localdomain.localserver /f
*/
