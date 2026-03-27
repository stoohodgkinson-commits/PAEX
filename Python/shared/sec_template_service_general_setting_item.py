# shared/sec_template_service_general_setting_item.py
# Exact full port of SecTemplateServiceGeneralSettingItem.cs

import xml.etree.ElementTree as ET
from .sec_template_item import SecTemplateItem
from .effective_state_sources import EffectiveStateSources

class SecTemplateServiceGeneralSettingItem(SecTemplateItem):
    def __init__(self, node: ET.Element):
        super().__init__()
        self.ReadStdElems(node)
        self.ReadStdSecTemplateElems(node)

        parts = self.m_sLineItem.split(self.KeyValSeparator())
        self.m_sServiceName = parts[0].strip().strip('"') if len(parts) > 0 else ""
        self.m_sStartType = parts[1].strip() if len(parts) > 1 else ""
        self.m_sSDDL = parts[2].strip().strip('"') if len(parts) > 2 else ""
        self.m_sBaselineValue = self.m_sLineItem

    def KeyValSeparator(self) -> str:
        return ","

    def SortKey(self) -> str:
        return (self.__class__.__name__ + "!" + self.m_sServiceName).lower()

    def Evaluate(self, sources: EffectiveStateSources, x_output_doc: ET.Element):
        self._retrieve_and_compare(sources)
        self.ImplementIgnoreAlwaysPass()
        self._write_service_results(x_output_doc)

    def _retrieve_and_compare(self, sources: EffectiveStateSources):
        self.m_bValueFound = False
        self.m_bEvalPassed = False
        self.m_bInvalidComparison = False

        try:
            key = sources.hklm64.OpenSubKey(f"SYSTEM\\CurrentControlSet\\Services\\{self.m_sServiceName}", False)
            if key:
                value = key.GetValue("Start")
                if value is not None:
                    self.m_sObservedStartType = str(value)
                    self.m_bValueFound = True
                    try:
                        baseline_num = int(self.m_sStartType)
                        observed_num = int(self.m_sObservedStartType)
                        op = self.m_sEvalOperator
                        if op == "Equal":
                            self.m_bEvalPassed = (baseline_num == observed_num)
                        elif op == "NotEqual":
                            self.m_bEvalPassed = (baseline_num != observed_num)
                        else:
                            self.m_bInvalidComparison = True
                    except ValueError:
                        self.m_bInvalidComparison = True
                key.Close()
        except Exception:
            pass

    def _write_service_results(self, x_output_doc: ET.Element):
        self.WriteSecTemplateResults(x_output_doc)