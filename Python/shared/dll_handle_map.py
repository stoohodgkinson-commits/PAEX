# shared/dll_handle_map.py
# Exact full port of DllHandleMap.cs

import ctypes
from ctypes import wintypes
from typing import Dict

class DllHandleMap:
    LOAD_LIBRARY_AS_DATAFILE = 2

    def __init__(self):
        self.m_map: Dict[str, int] = {}  # handle as int

    def GetDllHandle(self, s_dll_name: str) -> int:
        s_dll_name = s_dll_name.lower()
        if s_dll_name in self.m_map:
            return self.m_map[s_dll_name]

        # LoadLibraryEx equivalent
        handle = ctypes.windll.kernel32.LoadLibraryExW(s_dll_name, 0, self.LOAD_LIBRARY_AS_DATAFILE)
        self.m_map[s_dll_name] = handle
        return handle

    def ClearMap(self):
        for handle in list(self.m_map.values()):
            if handle:
                ctypes.windll.kernel32.FreeLibrary(handle)
        self.m_map.clear()