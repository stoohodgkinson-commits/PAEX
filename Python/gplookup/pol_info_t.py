# gplookup/pol_info_t.py
# Exact full port of PolInfo_t.cs

from typing import Dict
from .pol_location_t import PolLocation_t

class PolInfo_t:
    class class_t:
        Machine = 0
        User = 1
        SecuritySettings = 2
        AdvAudit = 3

    def __init__(self, cls=None, s_key="", s_value=""):
        self.polClass = cls or PolInfo_t.class_t.Machine
        self.polHive = PolInfo_t.ClassToHive(self.polClass)
        self.polKey = s_key
        self.polValue = s_value
        self.polLocations: Dict[str, PolLocation_t] = {}
        self.secSettingsDetails = None

    @staticmethod
    def ClassToHive(cls):
        return "HKCU" if cls == PolInfo_t.class_t.User else "HKLM"

    def SortKey(self):
        return PolInfo_t.SortKey(self.polHive, self.polKey, self.polValue)

    @staticmethod
    def SortKey(hive, key, value):
        return f"{hive}!{key}!{value}".lower()

    def AddLoc(self, s_pol_name, s_cat, s_disp, s_disp_sub_option, s_explain_text, s_filename):
        loc = PolLocation_t(s_pol_name, s_cat, s_disp, s_disp_sub_option, s_explain_text, s_filename)
        key = loc.SortKey()
        if key not in self.polLocations:
            self.polLocations[key] = loc