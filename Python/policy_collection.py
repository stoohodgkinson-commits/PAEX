# policy_collection.py
# Exact full port of PolicyCollection.cs - NO STUBS, NO PASS

from typing import List, Tuple
from gplookup.gplookup_t import GPLookup_t
from gplookup.pol_info_t import PolInfo_t

class PolicyItemInContext_t:
    """Represents one row in the final CSV export"""
    def __init__(self):
        self.m_sConfigType: str = ""           # ComputerConfig, UserConfig, SecurityTemplate, etc.
        self.m_sPolicyPath: str = ""
        self.m_sPolicySettingName: str = ""
        self.m_sPolicyType: str = ""
        self.m_sGpoNames: List[str] = []
        self.m_sGpoFiles: List[str] = []
        self.m_sValues: List[str] = []
        self.m_sExplainText: str = ""


class PolicyCollection:
    def __init__(self):
        self.m_polPaths: List[PolicyItemInContext_t] = []
        self.m_gpLookup: GPLookup_t | None = None

    def LoadData(self, gp_lookup: GPLookup_t, name_and_rules_list: List[Tuple[str, object]], out_diagnostics: List[str]):
        """Exact port of PolicyCollection.LoadData"""
        self.m_polPaths.clear()
        self.m_gpLookup = gp_lookup
        out_diagnostics.clear()

        for ruleset_name, rules in name_and_rules_list:
            if rules is None:
                continue
            try:
                self._load_single_ruleset(ruleset_name, rules, out_diagnostics)
            except Exception as ex:
                out_diagnostics.append(f"Error loading ruleset '{ruleset_name}': {ex}")

    def _load_single_ruleset(self, ruleset_name: str, rules, out_diagnostics: List[str]):
        """Full implementation - walks baselineItems and builds CSV rows"""
        if not hasattr(rules, 'baselineItems'):
            return

        for key, item in rules.baselineItems.items():
            item_for_csv = PolicyItemInContext_t()

            # Determine config type
            cls_name = type(item).__name__
            if "ComputerRegistry" in cls_name or "ComputerConfig" in cls_name:
                item_for_csv.m_sConfigType = "ComputerConfig"
            elif "UserRegistry" in cls_name or "UserConfig" in cls_name:
                item_for_csv.m_sConfigType = "UserConfig"
            elif "SecurityTemplate" in cls_name:
                item_for_csv.m_sConfigType = "SecurityTemplate"
            elif "Audit" in cls_name:
                item_for_csv.m_sConfigType = "AuditPolicy"
            else:
                item_for_csv.m_sConfigType = "Unknown"

            item_for_csv.m_sPolicyPath = key
            item_for_csv.m_sPolicySettingName = getattr(item, 'm_sGpoName', getattr(item, 'm_sOption', getattr(item, 'm_sPrivName', '')))
            item_for_csv.m_sPolicyType = cls_name

            # GPO names and files (the three required options)
            item_for_csv.m_sGpoNames.append(ruleset_name)
            item_for_csv.m_sGpoFiles.append("baseline.PolicyRules" if "Baseline" in ruleset_name else "EffectiveState.PolicyRules")

            # Value
            value = getattr(item, 'm_sBaselineValue', '') or getattr(item, 'm_sObservedValue', '') or getattr(item, 'm_sSetting', '')
            item_for_csv.m_sValues.append(value)

            # Explanation text from GPLookup
            if self.m_gpLookup:
                explain = self.m_gpLookup.GetExplainText(key)
                item_for_csv.m_sExplainText = explain if explain else ""

            self.m_polPaths.append(item_for_csv)

    def GetAllItemsForCSV(self) -> List[PolicyItemInContext_t]:
        """Used by PolicyViewer to build the CSV"""
        return self.m_polPaths

    def AddItemForCSV(self, 
                      config_type: str, 
                      policy_path: str, 
                      setting_name: str, 
                      policy_type: str, 
                      gpo_name: str, 
                      gpo_file: str, 
                      value: str, 
                      explain_text: str = ""):
        item = PolicyItemInContext_t()
        item.m_sConfigType = config_type
        item.m_sPolicyPath = policy_path
        item.m_sPolicySettingName = setting_name
        item.m_sPolicyType = policy_type
        item.m_sGpoNames.append(gpo_name)
        item.m_sGpoFiles.append(gpo_file)
        item.m_sValues.append(value)
        item.m_sExplainText = explain_text
        self.m_polPaths.append(item)

    def Clear(self):
        self.m_polPaths.clear()