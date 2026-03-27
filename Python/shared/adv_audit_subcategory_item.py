# Exact full port of AdvAuditSubcategoryItem.cs (includes full bitmask logic for GreaterThanOrEqual / LessThanOrEqual)

import xml.etree.ElementTree as ET
from .baseline_item import BaselineItem
from .effective_state_sources import EffectiveStateSources

class AdvAuditSubcategoryItem(BaselineItem):
    def __init__(self, node: ET.Element):
        super().__init__()
        self.ReadStdElems(node)
        guid_elem = node.find("GUID")
        name_elem = node.find("Name")
        setting_elem = node.find("Setting")
        self.m_sGUID = guid_elem.text.strip() if guid_elem is not None and guid_elem.text else ""
        self.m_sName = name_elem.text.strip() if name_elem is not None and name_elem.text else ""
        self.m_sBaselineValue = setting_elem.text.strip() if setting_elem is not None and setting_elem.text else ""

        self.m_sObservedValue: str = ""
        self.m_sObservedPolicyName: str = ""

    def SortKey(self) -> str:
        return (self.__class__.__name__ + "!" + self.m_sGUID).lower()

    def Evaluate(self, sources: EffectiveStateSources, x_output_doc: ET.Element):
        self._retrieve_and_compare(sources)
        self.ImplementIgnoreAlwaysPass()
        self._write_results(x_output_doc)

    def _get_node(self, sources: EffectiveStateSources):
        if sources.xmlSeceditAndAuditpolState is None:
            return None
        guid_lower = self.m_sGUID.lower()
        guid_upper = self.m_sGUID.upper()
        xpath = f"//AuditSubcategory[GUID='{guid_lower}' or GUID='{guid_upper}']"
        return sources.xmlSeceditAndAuditpolState.find(xpath)

    def _retrieve_and_compare(self, sources: EffectiveStateSources):
        self.m_bValueFound = False
        self.m_bEvalPassed = False
        self.m_bInvalidComparison = False

        node = self._get_node(sources)
        if node is None:
            return

        observed = AdvAuditSubcategoryItem(node)
        self.m_bValueFound = True
        self.m_sObservedValue = observed.m_sBaselineValue
        self.m_sObservedPolicyName = observed.m_sGpoName

        try:
            baseline_val = int(self.m_sBaselineValue)
            observed_val = int(self.m_sObservedValue)
        except (ValueError, TypeError):
            self.m_bInvalidComparison = True
            return

        op = self.m_sEvalOperator
        if op == "Equal":
            self.m_bEvalPassed = (baseline_val == observed_val)
        elif op == "NotEqual":
            self.m_bEvalPassed = (baseline_val != observed_val)
        elif op == "NonNull":
            self.m_bEvalPassed = True
        elif op == "GreaterThanOrEqual":
            self.m_bEvalPassed = (baseline_val & observed_val) == baseline_val
        elif op == "LessThanOrEqual":
            self.m_bEvalPassed = (baseline_val | observed_val) == baseline_val
        elif op == "Range":
            self.m_bInvalidComparison = True
        elif op == "OneOfThese":
            for val in self.m_sEvalValues.lower().split(","):
                if self.m_sObservedValue == val.strip():
                    self.m_bEvalPassed = True
                    return
        else:
            self.m_bInvalidComparison = True

    def _write_results(self, x_output_doc: ET.Element):
        root = x_output_doc.getroot()
        if root is None:
            root = ET.SubElement(x_output_doc, "root")
        elem = ET.SubElement(root, "AuditSubcategory")
        ET.SubElement(elem, "GUID").text = self.m_sGUID
        ET.SubElement(elem, "Name").text = self.m_sName
        ET.SubElement(elem, "Setting").text = self.m_sObservedValue
        ET.SubElement(elem, "PolicyName").text = self.m_sObservedPolicyName
        self.CreateResultsElements(x_output_doc, elem)
        root.append(elem)