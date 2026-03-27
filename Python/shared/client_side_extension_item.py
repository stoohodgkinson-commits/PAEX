# Exact full port of ClientSideExtensionItem.cs

import xml.etree.ElementTree as ET
from .baseline_item import BaselineItem
from .effective_state_sources import EffectiveStateSources

class ClientSideExtensionItem(BaselineItem):
    def __init__(self, node: ET.Element):
        super().__init__()
        self.m_sCSEType = node.tag  # LocalName equivalent in ElementTree
        guid_elem = node.find("GUID")
        name_elem = node.find("Name")
        self.m_sGuid = guid_elem.text.strip() if guid_elem is not None and guid_elem.text else ""
        self.m_sName = name_elem.text.strip() if name_elem is not None and name_elem.text else ""

    def SortKey(self) -> str:
        return (self.__class__.__name__ + "!" + self.m_sCSEType + "!" + self.m_sGuid).lower()

    def Evaluate(self, sources: EffectiveStateSources, x_output_doc: ET.Element):
        root = x_output_doc.getroot()
        if root is None:
            root = ET.SubElement(x_output_doc, "root")
        elem = ET.SubElement(root, self.m_sCSEType)
        ET.SubElement(elem, "GUID").text = self.m_sGuid
        ET.SubElement(elem, "Name").text = self.m_sName
        root.append(elem)