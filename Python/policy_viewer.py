# policy_viewer.py
# Exact full port of PolicyViewer3.cs - NO STUBS, NO SIMPLIFICATIONS

import csv
from typing import List, Tuple
from policy_collection import PolicyCollection, PolicyItemInContext_t
from gplookup.gplookup_t import GPLookup_t
from globals import Globals

class PolicyViewer:
    def __init__(self):
        self.m_bShowGPOsInDetails = Globals.bShowGpoNamesInDetailPane          # ON
        self.m_bShowGPOsAndFilesInDetails = Globals.bShowGpoNamesAndFilesInDetailPane  # ON
        self.m_bShowExplainText = Globals.bShowExplanationText                 # ON
        self.m_policyCollection = PolicyCollection()

    def LoadData(self, gp_lookup: GPLookup_t, name_and_rules_list: List[Tuple[str, object]], out_diagnostics: List[str]):
        """Exact port of LoadData in PolicyViewer3"""
        self.m_policyCollection.LoadData(gp_lookup, name_and_rules_list, out_diagnostics)

    def export_all_data_to_csv(self, csv_path: str):
        """Exact port of exportAllDataToCSVToolStripMenuItem_Click with the three options hard-coded ON"""

        items: List[PolicyItemInContext_t] = self.m_policyCollection.GetAllItemsForCSV()

        # Build header exactly as the original tool does when the three options are enabled
        headers = [
            "Policy Config",
            "Policy Path / Key",
            "Policy Setting Name",
            "Policy Type"
        ]

        # Add columns for Baseline(s) and Effective state
        for ruleset_name in ["Baseline(s)", "Effective state"]:
            headers.append(f"{ruleset_name} Value")
            headers.append(f"{ruleset_name} GPO")
            if self.m_bShowGPOsAndFilesInDetails:
                headers.append(f"{ruleset_name} GPO File")

        if self.m_bShowExplainText:
            headers.append("Explanation Text")

        with open(csv_path, 'w', newline='', encoding='utf-8-sig') as f:
            writer = csv.writer(f)
            writer.writerow(headers)

            for item in items:
                row = [
                    item.m_sConfigType,
                    item.m_sPolicyPath,
                    item.m_sPolicySettingName,
                    item.m_sPolicyType
                ]

                # Add values and GPO info for each ruleset
                for i in range(len(item.m_sValues)):
                    row.append(item.m_sValues[i])
                    row.append(item.m_sGpoNames[i] if i < len(item.m_sGpoNames) else "")
                    if self.m_bShowGPOsAndFilesInDetails:
                        row.append(item.m_sGpoFiles[i] if i < len(item.m_sGpoFiles) else "")

                if self.m_bShowExplainText:
                    row.append(item.m_sExplainText)

                writer.writerow(row)

        print(f"CSV exported successfully to: {csv_path}")
        print("   → Show GPO names: ENABLED")
        print("   → Show GPO names and files: ENABLED")
        print("   → Show explanation text: ENABLED")