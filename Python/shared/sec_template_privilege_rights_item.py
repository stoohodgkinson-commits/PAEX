# shared/sec_template_privilege_rights_item.py
# Exact full port of SecTemplatePrivilegeRightsItem.cs

import xml.etree.ElementTree as ET
from .sec_template_item import SecTemplateItem
from .effective_state_sources import EffectiveStateSources

class SecTemplatePrivilegeRightsItem(SecTemplateItem):
    def __init__(self, node: ET.Element):
        super().__init__()
        self.ReadStdElems(node)
        self.ReadStdSecTemplateElems(node)

        parts = self.m_sLineItem.split(self.KeyValSeparator())
        self.m_sPrivName = parts[0].strip() if len(parts) > 0 else ""
        self.m_sBaselineValue = parts[1].strip() if len(parts) > 1 else ""

    def KeyValSeparator(self) -> str:
        return "="

    def MultiValueSeparator(self) -> str:
        return ","

    def SortKey(self) -> str:
        return (self.__class__.__name__ + "!" + self.m_sPrivName).lower()

    def Evaluate(self, sources: EffectiveStateSources, x_output_doc: ET.Element):
        self._retrieve_and_compare(sources)
        self.ImplementIgnoreAlwaysPass()
        self.WriteSecTemplateResults(x_output_doc)

    def _retrieve_and_compare(self, sources: EffectiveStateSources):
        self.m_bValueFound = False
        self.m_bEvalPassed = False
        self.m_bInvalidComparison = False

        node = self.GetNodeFromSeceditAndAuditpolState(sources)
        if node is None:
            return

        observed = SecTemplatePrivilegeRightsItem(node)
        self.m_bValueFound = True
        self.m_sObservedLineItem = observed.m_sLineItem
        self.m_sObservedPolicyName = observed.m_sGpoName

        baseline_lower = self.m_sBaselineValue.lower().strip()
        observed_lower = observed.m_sBaselineValue.lower().strip()

        op = self.m_sEvalOperator
        if op == "Equal":
            self.m_bEvalPassed = (baseline_lower == observed_lower)
        elif op == "NotEqual":
            self.m_bEvalPassed = (baseline_lower != observed_lower)
        elif op == "NonNull":
            self.m_bEvalPassed = True
        else:
            self.m_bInvalidComparison = True