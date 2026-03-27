# baseline_evaluator.py
# Exact full port of BaselineEvaluator.cs - NO STUBS

import xml.etree.ElementTree as ET
import datetime
from pathlib import Path
from typing import List, Dict
from shared.baseline_defs import BaselineDefs
from shared.effective_state_sources import EffectiveStateSources

# Import all item classes
from shared.adv_audit_audit_option_item import AdvAuditAuditOptionItem
from shared.adv_audit_global_audit_item import AdvAuditGlobalAuditItem
from shared.adv_audit_subcategory_item import AdvAuditSubcategoryItem
from shared.computer_registry_pol_item import ComputerRegistryPolItem
from shared.user_registry_pol_item import UserRegistryPolItem
from shared.sec_template_registry_item import SecTemplateRegistryItem
from shared.sec_template_simple_value_item import SecTemplateSimpleValueItem
from shared.sec_template_privilege_rights_item import SecTemplatePrivilegeRightsItem
from shared.sec_template_service_general_setting_item import SecTemplateServiceGeneralSettingItem
from shared.sec_template_file_security_item import SecTemplateFileSecurityItem
from shared.sec_template_group_membership_item import SecTemplateGroupMembershipItem
from shared.sec_template_registry_keys_item import SecTemplateRegistryKeysItem
from shared.client_side_extension_item import ClientSideExtensionItem

class BaselineEvaluator:
    def __init__(self):
        self.baselineItems: Dict[str, object] = {}
        self.bNeedSecedit = False
        self.bNeedAuditpol = False

    def AddBaselineDoc(self, s_baseline_path: str, out_diagnostics: List[str]) -> bool:
        try:
            tree = ET.parse(s_baseline_path)
            root = tree.getroot()

            for node in root.iter():
                tag = node.tag

                if tag == BaselineDefs.sComputerConfigElem:
                    self._add_item(ComputerRegistryPolItem(node))
                elif tag == BaselineDefs.sUserConfigElem:
                    self._add_item(UserRegistryPolItem(node))
                elif tag == BaselineDefs.sSecurityTemplateElem:
                    self.bNeedSecedit = True
                    section = node.get("Section") or ""
                    if section == BaselineDefs.sRegistryValues:
                        self._add_item(SecTemplateRegistryItem(node))
                    elif section == BaselineDefs.sPrivilegeRights:
                        self._add_item(SecTemplatePrivilegeRightsItem(node))
                    elif section == BaselineDefs.sServiceGeneralSetting:
                        self._add_item(SecTemplateServiceGeneralSettingItem(node))
                    elif section == BaselineDefs.sFileSecurity:
                        self._add_item(SecTemplateFileSecurityItem(node))
                    elif section == BaselineDefs.sRegistryKeys:
                        self._add_item(SecTemplateRegistryKeysItem(node))
                    elif section == BaselineDefs.sGroupMembership:
                        self._add_item(SecTemplateGroupMembershipItem(node))
                elif tag == "AuditOption":
                    self.bNeedAuditpol = True
                    self._add_item(AdvAuditAuditOptionItem(node))
                elif tag == "GlobalAudit":
                    self.bNeedAuditpol = True
                    self._add_item(AdvAuditGlobalAuditItem(node))
                elif tag == "AuditSubcategory":
                    self.bNeedAuditpol = True
                    self._add_item(AdvAuditSubcategoryItem(node))
                elif tag.startswith("CSE-"):
                    self._add_item(ClientSideExtensionItem(node))

        except Exception as ex:
            out_diagnostics.append(f"Error loading baseline {s_baseline_path}: {ex}")
            return False
        return True

    def _add_item(self, item):
        key = item.SortKey()
        if key not in self.baselineItems:
            self.baselineItems[key] = item

    def EvaluateEffectiveState(self, s_output_rules_path: str, out_diagnostics: List[str]) -> bool:
        sources = EffectiveStateSources(b_need_secedit_auditpol=(self.bNeedSecedit or self.bNeedAuditpol))

        root = ET.Element("PolicyRules")
        doc = ET.ElementTree(root)

        for item in self.baselineItems.values():
            try:
                item.Evaluate(sources, doc)
            except Exception as ex:
                out_diagnostics.append(f"Evaluate failed for {type(item).__name__}: {ex}")

        try:
            doc.write(s_output_rules_path, encoding="utf-8", xml_declaration=True)
            return True
        except Exception as ex:
            out_diagnostics.append(f"Failed to save effective state file: {ex}")
            return False