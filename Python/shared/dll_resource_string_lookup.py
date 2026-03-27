# shared/dll_resource_string_lookup.py
# Exact full port of DllResourceStringLookup.cs

import ctypes
from ctypes import wintypes
from .dll_handle_map import DllHandleMap

class DllResourceStringLookup:
    _handle_map = DllHandleMap()

    @staticmethod
    def LookupResource(s_dll_name: str, u_id: int) -> str:
        handle = DllResourceStringLookup._handle_map.GetDllHandle(s_dll_name)
        if not handle:
            return ""

        buffer = ctypes.create_unicode_buffer(1024)
        length = ctypes.windll.user32.LoadStringW(handle, u_id, buffer, 1024)
        if length > 0:
            return buffer.value[:length]
        return ""

    @staticmethod
    def LookupResourceOrDefault(s_dll_name: str, u_id: int, s_default: str) -> str:
        result = DllResourceStringLookup.LookupResource(s_dll_name, u_id)
        return result if result else s_default

    @staticmethod
    def LookupResourceFromFmt(fmt_string: str) -> str:
        if not fmt_string or ',' not in fmt_string:
            return ""
        parts = fmt_string.split(',')
        if len(parts) != 2:
            return ""

        dll_part = parts[0].strip()
        if dll_part.startswith("@"):
            dll_part = dll_part[1:]
        if dll_part.lower().startswith("(runtime.system32)\\"):
            dll_part = dll_part[18:]

        id_part = parts[1].strip()
        if id_part.startswith("-"):
            id_part = id_part[1:]

        try:
            res_id = int(id_part)
            return DllResourceStringLookup.LookupResource(dll_part, res_id)
        except ValueError:
            return ""

    @staticmethod
    def ResourceIdFromFmtString(fmt_string: str) -> int:
        if not fmt_string or ',' not in fmt_string:
            return 0
        parts = fmt_string.split(',')
        if len(parts) != 2:
            return 0
        id_part = parts[1].strip()
        if id_part.startswith("-"):
            id_part = id_part[1:]
        try:
            return int(id_part)
        except ValueError:
            return 0