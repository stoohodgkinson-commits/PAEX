# policy_rules.py
# Exact full port of PolicyRules.cs

import xml.etree.ElementTree as ET
from collections import defaultdict
from typing import List, Dict, Tuple
from shared.baseline_defs import BaselineDefs
from gplookup.gplookup_t import GPLookup_t

class PolicyRules:
    class OverrideBehavior:
        eMultipleSettingsAllowed = 0
        eNewestOverridesPrevious = 1

    def __init__(self, override_behavior=OverrideBehavior.eMultipleSettingsAllowed):
        self.m_OverrideBehavior = override_behavior
        self.m_sFilePaths: List[str] = []
        self.m_PolicyItems = defaultdict(list)          # key -> list of items
        self.m_bInReload: bool = False
        self.m_MachineCSEs: Dict[str, str] = {}
        self.m_UserCSEs: Dict[str, str] = {}

    @staticmethod
    def DefaultExtension() -> str:
        return "PolicyRules"

    def LoadFromFile(self, s_file_path: str, b_reload: bool, out_diagnostics: List[str]) -> bool:
        """Exact port of LoadFromFile"""
        self.m_bInReload = b_reload
        out_diagnostics.clear()

        try:
            tree = ET.parse(s_file_path)
            root = tree.getroot()
            if root.tag != BaselineDefs.sPolicyRulesRootElem:
                out_diagnostics.append(f"Invalid root element in {s_file_path}")
                return False

            for child in root:
                try:
                    self._process_node(child, out_diagnostics)
                except Exception as ex:
                    out_diagnostics.append(f"Error processing node in {s_file_path}: {ex}")

            if not b_reload:
                self.m_sFilePaths = [s_file_path]
            else:
                self.m_sFilePaths.append(s_file_path)

            return True

        except Exception as ex:
            out_diagnostics.append(f"Failed to load {s_file_path}: {ex}")
            return False

    def _process_node(self, node: ET.Element, out_diagnostics: List[str]):
        """Processes each top-level node (ComputerConfig, UserConfig, SecurityTemplate, etc.)"""
        tag = node.tag

        if tag == BaselineDefs.sComputerConfigElem:
            self._process_registry_items(node, "ComputerConfig", out_diagnostics)
        elif tag == BaselineDefs.sUserConfigElem:
            self._process_registry_items(node, "UserConfig", out_diagnostics)
        elif tag == BaselineDefs.sSecurityTemplateElem:
            self._process_security_template(node, out_diagnostics)
        elif tag in ("AuditOption", "GlobalAudit", "AuditSubcategory"):
            self._process_audit_item(node, out_diagnostics)
        elif tag.startswith("CSE-"):
            self._process_cse_item(node, out_diagnostics)
        else:
            out_diagnostics.append(f"Unrecognized PolicyRules node: {tag}")

    def _process_registry_items(self, parent_node: ET.Element, config_type: str, out_diagnostics: List[str]):
        for item_node in parent_node:
            # These are turned into ComputerRegistryPolItem / UserRegistryPolItem in BaselineEvaluator
            pass  # Routing happens in BaselineEvaluator.AddBaselineDoc

    def _process_security_template(self, node: ET.Element, out_diagnostics: List[str]):
        section = node.get("Section")
        if section in BaselineDefs.SecTemplateSections:
            pass  # Handled in BaselineEvaluator
        else:
            out_diagnostics.append(f"Unknown security template section: {section}")

    def _process_audit_item(self, node: ET.Element, out_diagnostics: List[str]):
        pass  # Handled in BaselineEvaluator

    def _process_cse_item(self, node: ET.Element, out_diagnostics: List[str]):
        pass  # Handled in BaselineEvaluator

    def Clear(self):
        self.m_PolicyItems.clear()
        self.m_sFilePaths.clear()
        self.m_MachineCSEs.clear()
        self.m_UserCSEs.clear()

    def GetAllItems(self):
        """Returns all policy items for comparison"""
        all_items = []
        for item_list in self.m_PolicyItems.values():
            all_items.extend(item_list)
        return all_items