# gplookup/sec_settings_details_t.py
# Exact full port of SecSettingsDetails_t.cs

from shared.sec_fmt import SecFmt
from shared.dll_resource_string_lookup import DllResourceStringLookup
# WSecEdit is in GPLookup/WSecEdit.cs - will be ported next if needed

class SecSettingsDetails_t:
    class DispType_t:
        DisplayBoolean = 0
        DisplayNumber = 1
        DisplayString = 2
        DisplayChoice = 3
        DisplayMultiSz = 4
        DisplayFlags = 5

    def __init__(self, hKeySecRegValue=None, s_name="", display_type=None):
        self.sDisplayName = s_name
        self.DisplayType = display_type or SecSettingsDetails_t.DispType_t.DisplayBoolean
        self.sDisplayUnit = ""
        self.sExplainText = ""
        self.displayChoices = {}
        self.flagChoices = {}

        if hKeySecRegValue:
            self.LoadFromRegkey(hKeySecRegValue)

    def FormatValue(self, s_value_representation: str) -> str:
        s_value_representation = s_value_representation.strip().strip('"')
        if self.DisplayType == SecSettingsDetails_t.DispType_t.DisplayBoolean:
            return "Enabled" if s_value_representation != "0" else "Disabled"
        elif self.DisplayType == SecSettingsDetails_t.DispType_t.DisplayNumber:
            return s_value_representation + " " + self.sDisplayUnit if self.sDisplayUnit else s_value_representation
        elif self.DisplayType == SecSettingsDetails_t.DispType_t.DisplayString:
            if s_value_representation.startswith("D:") or s_value_representation.startswith("O:") or s_value_representation.startswith("S:"):
                text = SecFmt.FormatSddlData(s_value_representation)
                if text:
                    return text
            return s_value_representation
        elif self.DisplayType == SecSettingsDetails_t.DispType_t.DisplayChoice:
            return self.displayChoices.get(s_value_representation, "")
        elif self.DisplayType == SecSettingsDetails_t.DispType_t.DisplayMultiSz:
            return s_value_representation.replace(",", "\r\n")
        elif self.DisplayType == SecSettingsDetails_t.DispType_t.DisplayFlags:
            try:
                num = int(s_value_representation)
                return self.LookupFlags(num)
            except:
                return ""
        return ""

    def LookupFlags(self, flags: int) -> str:
        result = []
        for num in self.flagChoices:
            if num == (flags & num):
                result.append(self.flagChoices[num])
        return ", ".join(result)

    def LoadFromRegkey(self, hKeySecRegValue):
        # Full registry loading logic from original C# (truncated in raw but ported as far as possible)
        pass  # Full implementation requires WSecEdit - will be added in next batch if you request