using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CSMemory
{
    public class CSMemoryClass
    {
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "OpenProcess")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags access, bool inheritHandle, int procId);
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "ReadProcessMemory")]
        private static unsafe extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte* lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        public static IntPtr GetProcessHandle(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                IntPtr handle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
                return handle;
            }

            return IntPtr.Zero;
        }

        public static unsafe T Read<T>(IntPtr process, IntPtr address)
        {
            int readBytes = 0;
            int size = Marshal.SizeOf<T>();
            byte* buffer = stackalloc byte[size];
            ReadProcessMemory(process, address, buffer, size, out readBytes);
            return (T)Marshal.PtrToStructure((IntPtr)buffer, typeof(T));
        }
    }
}