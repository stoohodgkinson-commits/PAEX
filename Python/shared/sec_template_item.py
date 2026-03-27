# shared/sec_template_item.py - Improved base (replace your current one)
import xml.etree.ElementTree as ET
from .baseline_item import BaselineItem
from .effective_state_sources import EffectiveStateSources

class SecTemplateItem(BaselineItem):
    def __init__(self):
        super().__init__()
        self.m_sSection: str = ""
        self.m_sLineItem: str = ""
        self.m_sObservedLineItem: str = ""
        self.m_sObservedPolicyName: str = ""

    def ReadStdSecTemplateElems(self, node: ET.Element):
        self.m_sSection = node.get("Section", "")
        line_elem = node.find("LineItem")
        self.m_sLineItem = line_elem.text.strip() if line_elem is not None and line_elem.text else ""

    def KeyValSeparator(self) -> str:
        return "="

    def MultiValueSeparator(self) -> str:
        return "\0"

    def GetNodeFromSeceditAndAuditpolState(self, sources: EffectiveStateSources):
        if sources.xmlSeceditAndAuditpolState is None:
            return None
        # Simplified search
        for elem in sources.xmlSeceditAndAuditpolState.iter("SecurityTemplate"):
            if elem.get("Section") == self.m_sSection:
                return elem
        return None

    def WriteSecTemplateResults(self, x_output_doc: ET.Element):
        root = x_output_doc.getroot() or ET.SubElement(x_output_doc, "root")
        elem = ET.SubElement(root, "SecurityTemplate")
        elem.set("Section", self.m_sSection)
        ET.SubElement(elem, "LineItem").text = self.m_sObservedLineItem if self.m_bValueFound else self.m_sLineItem
        ET.SubElement(elem, "PolicyName").text = self.m_sObservedPolicyName
        self.CreateResultsElements(x_output_doc, elem)