using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CrackMe;

internal class Program
{
	private static void Main(string[] args)
	{
		_8F75FDD7();
	}

	[DllImport("kernel32.dll")]
	public static extern IntPtr ZeroMemory(IntPtr P_0, IntPtr P_1);

	[DllImport("kernel32.dll")]
	public static extern IntPtr VirtualProtect(IntPtr P_0, IntPtr P_1, IntPtr P_2, ref IntPtr P_3);

	private static void _8F75FDD7()
	{
		List<int> list = new List<int> { 8, 12, 16, 20, 24, 28, 36 };
		List<int> list2 = new List<int> { 26, 27 };
		List<int> list3 = new List<int>
		{
			4, 22, 24, 64, 66, 68, 70, 72, 74, 76,
			92, 94
		};
		IntPtr baseAddress = Process.GetCurrentProcess().MainModule.BaseAddress;
		int num = Marshal.ReadInt32((IntPtr)(baseAddress.ToInt32() + 60));
		short num2 = Marshal.ReadInt16((IntPtr)(baseAddress.ToInt32() + num + 6));
		for (int i = 0; i < list3.Count; i++)
		{
			EraseSection((IntPtr)(baseAddress.ToInt32() + num + list3[i]), 1);
		}
		for (int j = 0; j < list2.Count; j++)
		{
			EraseSection((IntPtr)(baseAddress.ToInt32() + num + list2[j]), 2);
		}
		int num3 = 0;
		int num4 = 0;
		while (num3 <= num2)
		{
			if (num4 == 0)
			{
				EraseSection((IntPtr)(baseAddress.ToInt32() + num + 250 + 40 * num3 + 32), 2);
			}
			num4++;
			if (num4 == list.Count)
			{
				num3++;
				num4 = 0;
			}
		}
	}

	public static void EraseSection(IntPtr P_0, int P_1)
	{
		IntPtr intPtr = (IntPtr)P_1;
		IntPtr intPtr2 = default(IntPtr);
		VirtualProtect(P_0, intPtr, (IntPtr)64, ref intPtr2);
		ZeroMemory(P_0, intPtr);
		IntPtr intPtr3 = default(IntPtr);
		VirtualProtect(P_0, intPtr, intPtr2, ref intPtr3);
	}
}
