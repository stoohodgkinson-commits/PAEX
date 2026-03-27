# shared/computer_registry_pol_item.py
# Exact full port of ComputerRegistryPolItem.cs

import xml.etree.ElementTree as ET
from .baseline_item import BaselineItem
from .registry_item_helper import RegistryItemHelper
from .effective_state_sources import EffectiveStateSources

class ComputerRegistryPolItem(BaselineItem):
    def __init__(self, node: ET.Element):
        super().__init__()
        self.m_regHelper = RegistryItemHelper(self, "ComputerConfig")
        self.ReadStdElems(node)
        key_elem = node.find("Key")
        value_elem = node.find("Value")
        reg_type_elem = node.find("RegType")
        reg_data_elem = node.find("RegData")
        self.m_regHelper.SetKeyValueType(
            key_elem.text.strip() if key_elem is not None and key_elem.text else "",
            value_elem.text.strip() if value_elem is not None and value_elem.text else "",
            reg_type_elem.text.strip() if reg_type_elem is not None and reg_type_elem.text else ""
        )
        self.m_sBaselineValue = reg_data_elem.text.strip() if reg_data_elem is not None and reg_data_elem.text else ""

    def SortKey(self) -> str:
        return (self.__class__.__name__ + "!" + self.m_regHelper.m_sKey + "!" + self.m_regHelper.m_sValue).lower()

    def Evaluate(self, sources: EffectiveStateSources, x_output_doc: ET.Element):
        self.m_regHelper.RetrieveRegistryValue(sources.hklm64)
        self.m_regHelper.EvaluateRegistryValue()
        self.CreateResultsElements(x_output_doc, ET.SubElement(x_output_doc.getroot() or ET.SubElement(x_output_doc, "root"), "ComputerRegistry"))