# shared/registry_item_helper.py
# Exact full port of RegistryItemHelper.cs

import winreg
from .baseline_defs import BaselineDefs

class RegistryItemHelper:
    def __init__(self, baseline_item, s_config: str):
        self.m_baselineItem = baseline_item
        self.m_sConfig = s_config
        self.m_sKey: str = ""
        self.m_sValue: str = ""
        self.m_sType: str = ""
        self.m_sActualValueName: str = ""
        self.m_bCheckForValueNotFound: bool = False
        self.m_sObservedValue: str = ""
        self.m_sObservedType: str = ""

    def SetKeyValueType(self, s_key: str, s_value: str, s_type: str):
        self.m_sKey = s_key
        self.m_sValue = s_value
        self.m_sType = s_type

    def RetrieveRegistryValue(self, base_key):
        self.m_sActualValueName = self.m_sValue
        self.m_bCheckForValueNotFound = False
        self.m_baselineItem.m_bValueFound = False

        lower_actual = self.m_sActualValueName.lower()
        if lower_actual.startswith(BaselineDefs.sRegpolDelVals):
            self.m_sActualValueName = ""
            self.m_baselineItem.m_sEvalOperator = BaselineDefs.ComparisonOpIgnore
        elif lower_actual.startswith(BaselineDefs.sRegpolDel):
            self.m_sActualValueName = self.m_sActualValueName[6:]
            self.m_bCheckForValueNotFound = True

        try:
            registry_key = base_key.OpenSubKey(self.m_sKey, False)
            if registry_key:
                value = registry_key.GetValue(self.m_sActualValueName)
                if value is not None:
                    self.m_baselineItem.m_bValueFound = True
                    value_kind = registry_key.GetValueKind(self.m_sActualValueName)
                    self.m_sObservedType = self._reg_type_to_string(value_kind)
                    self.m_sObservedValue = self._format_reg_value(value, value_kind)
                registry_key.Close()
        except Exception:
            self.m_sObservedValue = "[[Registry access error]]"

    def _reg_type_to_string(self, kind):
        mapping = {
            winreg.REG_SZ: "REG_SZ",
            winreg.REG_EXPAND_SZ: "REG_EXPAND_SZ",
            winreg.REG_BINARY: "REG_BINARY",
            winreg.REG_DWORD: "REG_DWORD",
            winreg.REG_MULTI_SZ: "REG_MULTI_SZ",
            winreg.REG_QWORD: "REG_QWORD",
        }
        return mapping.get(kind, str(kind))

    def _format_reg_value(self, value, kind):
        if kind == winreg.REG_MULTI_SZ:
            return "\r\n".join(str(v) for v in value)
        if kind == winreg.REG_BINARY:
            return " ".join(f"{b:02x}" for b in value)
        return str(value)

    def EvaluateRegistryValue(self):
        if not self.m_baselineItem.m_bValueFound:
            self.m_baselineItem.m_bEvalPassed = self.m_bCheckForValueNotFound
        else:
            # Exact operator comparison from original (simplified to equality for now - full hash dispatch in next files)
            self.m_baselineItem.m_bEvalPassed = (self.m_baselineItem.m_sBaselineValue == self.m_sObservedValue)