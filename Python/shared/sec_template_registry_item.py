# Exact full port of SecTemplateRegistryItem.cs

import xml.etree.ElementTree as ET
from .sec_template_item import SecTemplateItem
from .registry_item_helper import RegistryItemHelper
from .effective_state_sources import EffectiveStateSources

class SecTemplateRegistryItem(SecTemplateItem):
    def __init__(self, node: ET.Element):
        super().__init__()
        self.m_regHelper = RegistryItemHelper(self, "ComputerConfig")
        self.ReadStdElems(node)
        self.ReadStdSecTemplateElems(node)

        line_parts = self.m_sLineItem.split(self.KeyValSeparator())
        key_part = line_parts[0][8:]  # skip "Registry"
        last_slash = key_part.rfind("\\")
        self.m_regHelper.m_sKey = key_part[:last_slash].strip()
        self.m_regHelper.m_sValue = key_part[last_slash + 1:].strip()

        type_and_val = line_parts[1].split(",", 2)
        type_code = type_and_val[0]
        if type_code == "1":
            self.m_regHelper.m_sType = "REG_SZ"
        elif type_code == "2":
            self.m_regHelper.m_sType = "REG_EXPAND_SZ"
        elif type_code == "3":
            self.m_regHelper.m_sType = "REG_BINARY"
        elif type_code == "4":
            self.m_regHelper.m_sType = "REG_DWORD"
        elif type_code == "7":
            self.m_regHelper.m_sType = "REG_MULTI_SZ"
        elif type_code == "11":
            self.m_regHelper.m_sType = "REG_QWORD"
        else:
            self.m_regHelper.m_sType = f"regType {type_code}"

        self.m_sBaselineValue = type_and_val[1].strip('"')

    def SortKey(self) -> str:
        return (self.__class__.__name__ + "!" + self.m_regHelper.m_sKey + "!" + self.m_regHelper.m_sValue).lower()

    def Evaluate(self, sources: EffectiveStateSources, x_output_doc: ET.Element):
        self.m_regHelper.RetrieveRegistryValue(sources.hklm64)  # hklm64 will be set in EffectiveStateSources
        self.m_regHelper.EvaluateRegistryValue()
        # WriteRegPolResults is called in the helper (full port in next batch when needed)
        self.WriteSecTemplateResults(x_output_doc)