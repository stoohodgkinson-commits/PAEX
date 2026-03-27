# Exact full port of AdvAuditAuditOptionItem.cs

import xml.etree.ElementTree as ET
from .baseline_item import BaselineItem
from .effective_state_sources import EffectiveStateSources

class AdvAuditAuditOptionItem(BaselineItem):
    def __init__(self, node: ET.Element):
        super().__init__()
        self.ReadStdElems(node)
        self.m_sOption = node.find("Option").text.strip() if node.find("Option") is not None else ""
        self.m_sBaselineValue = node.find("Setting").text.strip() if node.find("Setting") is not None else ""

        self.m_sObservedValue: str = ""
        self.m_sObservedPolicyName: str = ""

    def SortKey(self) -> str:
        return (self.__class__.__name__ + "!" + self.m_sOption).lower()

    def Evaluate(self, sources: EffectiveStateSources, x_output_doc: ET.Element):
        self._retrieve_and_compare(sources)
        self.ImplementIgnoreAlwaysPass()
        self._write_results(x_output_doc)

    def _get_node(self, sources: EffectiveStateSources):
        if sources.xmlSeceditAndAuditpolState is None:
            return None
        xpath = f"//AuditOption[Option='{self.m_sOption}']"
        return sources.xmlSeceditAndAuditpolState.find(xpath)

    def _retrieve_and_compare(self, sources: EffectiveStateSources):
        self.m_bValueFound = False
        self.m_bEvalPassed = False
        self.m_bInvalidComparison = False

        node = self._get_node(sources)
        if node is None:
            return

        observed = AdvAuditAuditOptionItem(node)
        self.m_bValueFound = True
        self.m_sObservedValue = observed.m_sBaselineValue
        self.m_sObservedPolicyName = observed.m_sGpoName

        op = self.m_sEvalOperator
        if op == "Equal":
            self.m_bEvalPassed = (self.m_sBaselineValue == self.m_sObservedValue)
        elif op == "NotEqual":
            self.m_bEvalPassed = (self.m_sBaselineValue != self.m_sObservedValue)
        elif op == "NonNull":
            self.m_bEvalPassed = True
        elif op in ("GreaterThanOrEqual", "LessThanOrEqual", "Range"):
            self.m_bInvalidComparison = True
        elif op == "OneOfThese":
            for val in self.m_sEvalValues.split(","):
                if self.m_sObservedValue == val.strip():
                    self.m_bEvalPassed = True
                    return
        else:
            self.m_bInvalidComparison = True

    def _write_results(self, x_output_doc: ET.Element):
        root = x_output_doc.getroot() if x_output_doc.getroot() is not None else ET.SubElement(x_output_doc, "root")
        elem = ET.SubElement(root, "AuditOption")
        ET.SubElement(elem, "Option").text = self.m_sOption
        ET.SubElement(elem, "Setting").text = self.m_sObservedValue
        ET.SubElement(elem, "PolicyName").text = self.m_sObservedPolicyName
        self.CreateResultsElements(x_output_doc, elem)