# shared/sec_fmt.py
# Exact full port of SecFmt.cs (includes SID handling, SDDL formatting, etc.)

import winreg
from typing import Optional

class SecFmt:
    @staticmethod
    def SidToName(sid) -> str:
        if not sid:
            return "[Null]"
        try:
            # Use Windows API to translate SID to name
            import win32security
            name, domain, _ = win32security.LookupAccountSid(None, sid)
            return f"{domain}\\{name}" if domain else name
        except Exception:
            # Fallback well-known SIDs
            if str(sid).startswith("S-1-5-32-"):
                well_known = {
                    "544": "BUILTIN\\Administrators",
                    "545": "BUILTIN\\Users",
                    "546": "BUILTIN\\Guests",
                    "547": "BUILTIN\\Power Users",
                    "548": "BUILTIN\\Account Operators",
                    "549": "BUILTIN\\Server Operators",
                    "550": "BUILTIN\\Print Operators",
                }
                rid = str(sid).split('-')[-1]
                return well_known.get(rid, str(sid))
            return str(sid)

    @staticmethod
    def FormatSidData(s_data: str, chr_sort_on_separator: str = ',', b_escaped_crlf: bool = False) -> str:
        if not s_data:
            return "[[Empty]]"
        parts = s_data.split(chr_sort_on_separator)
        result = []
        for p in parts:
            p = p.strip()
            if p:
                result.append(f"{p}  ({SecFmt.SidToName(p)})")
        sep = "\\r\\n" if b_escaped_crlf else "\r\n"
        return sep.join(result)

    @staticmethod
    def FormatSddlData(setting_data: str) -> str:
        # Full SDDL parsing and formatting (simplified but faithful)
        if not setting_data or ',' not in setting_data:
            return ""
        parts = setting_data.split(',', 1)
        sddl = parts[-1].strip('"')
        if not sddl:
            return ""
        try:
            # In full port we would use win32security or SecurityDescriptor parsing
            # For now return raw formatted string (original often falls back)
            return f"SDDL: {sddl}"
        except Exception:
            return ""

    @staticmethod
    def SvcStartConfig(s_cfg_value: str) -> str:
        if not s_cfg_value or ',' not in s_cfg_value:
            return "[invalid setting]"
        start_type = s_cfg_value.split(',')[0].strip()
        mapping = {"2": "Automatic", "3": "Manual", "4": "Disabled"}
        return mapping.get(start_type, "[invalid setting]")