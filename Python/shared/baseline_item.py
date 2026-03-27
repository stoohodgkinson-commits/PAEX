# Exact full port of BaselineItem.cs (abstract base)

import xml.etree.ElementTree as ET

class BaselineItem:
    def __init__(self):
        self.m_sSourceFile: str = ""
        self.m_sGpoName: str = ""
        self.m_sEvalOperator: str = ""
        self.m_sEvalValues: str = ""
        self.m_sBaselineValue: str = ""
        self.m_bValueFound: bool = True
        self.m_bEvalPassed: bool = False
        self.m_bInvalidComparison: bool = False

    def SortKey(self) -> str:
        raise NotImplementedError("Subclasses must implement SortKey")

    def ReadStdElems(self, node: ET.Element):
        self.m_sSourceFile = self._optional_inner_text(node, "SourceFile")
        self.m_sGpoName = self._optional_inner_text(node, "PolicyName")
        self.m_sEvalOperator = self._optional_inner_text(node, "EvalOperator")
        if not self.m_sEvalOperator:
            self.m_sEvalOperator = "Equal"
        self.m_sEvalValues = self._optional_inner_text(node, "EvalValues")

        flag = self._optional_boolean(node, "ValueNotFound")
        self.m_bValueFound = not flag if flag is not None else True

        flag = self._optional_boolean(node, "EvalResult")
        if flag is not None:
            self.m_bEvalPassed = flag

        flag = self._optional_boolean(node, "InvalidComparison")
        if flag is not None:
            self.m_bInvalidComparison = flag

    def _optional_boolean(self, node: ET.Element, child_name: str):
        elem = node.find(child_name)
        if elem is not None and elem.text:
            return elem.text.strip().lower() == "true"
        return None

    def _optional_inner_text(self, node: ET.Element, child_name: str) -> str:
        elem = node.find(child_name)
        return elem.text.strip() if elem is not None and elem.text else ""

    def CreateResultsElements(self, x_output_doc: ET.Element, e_base_element: ET.Element):
        # EvalResult
        elem = ET.SubElement(e_base_element, "EvalResult")
        elem.text = str(self.m_bEvalPassed).lower()
        # BaselineValue
        elem = ET.SubElement(e_base_element, "BaselineValue")
        elem.text = self.m_sBaselineValue
        # EvalOperator
        elem = ET.SubElement(e_base_element, "EvalOperator")
        elem.text = self.m_sEvalOperator
        if self.m_sEvalValues:
            elem = ET.SubElement(e_base_element, "EvalValues")
            elem.text = self.m_sEvalValues
        # ValueNotFound
        elem = ET.SubElement(e_base_element, "ValueNotFound")
        elem.text = str(not self.m_bValueFound).lower()
        # InvalidComparison
        elem = ET.SubElement(e_base_element, "InvalidComparison")
        elem.text = str(self.m_bInvalidComparison).lower()

    def Evaluate(self, sources, x_output_doc: ET.Element):
        raise NotImplementedError("Subclasses must implement Evaluate")

    def IgnoreAlwaysPass(self) -> bool:
        return self.m_sEvalOperator == "Ignore-AlwaysPass"

    def ImplementIgnoreAlwaysPass(self) -> bool:
        if self.IgnoreAlwaysPass():
            self.m_bEvalPassed = True
            self.m_bInvalidComparison = False
            return True
        return False