# gplookup/pol_setting_choice_t.py
# Exact full port of PolSettingChoice_t.cs

from .pol_location_t import PolLocation_t

class PolSettingChoice_t:
    def __init__(self, pol_loc: PolLocation_t, s_value: str):
        self.m_PolLoc = pol_loc
        self.m_sSettingChoice = s_value

    @property
    def PolLocation(self):
        return self.m_PolLoc

    @property
    def SettingChoice(self):
        return self.m_sSettingChoice